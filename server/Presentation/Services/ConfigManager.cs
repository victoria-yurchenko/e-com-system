using Application.BackgroundServices;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Services;
using Infrastructure.DatabaseContext;
using Infrastructure.PaymentGateways;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Application.Configurations;
using Application.Factories;
using Application.Providers;
using Application.Providers.Messaging;
using Application.Interfaces.Authentication;
using Application.Services.Authentication;
using StackExchange.Redis;

namespace Presentation.Services
{
    public static class ConfigManager
    {
        public static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);
            ConfigureAuthentication(builder);
            ConfigureLogging(builder);

            return builder;
        }

        public static void ConfigureApplication(WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(app.Configuration["Swagger:Endpoint"] ?? string.Empty, app.Configuration["Swagger:Title"] ?? string.Empty);
                    options.RoutePrefix = app.Configuration["Swagger:RoutePrefix"] ?? string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseCors(app.Configuration["CORS:PolicyName"] ?? string.Empty);
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            RegisterServices(builder);
            RegisterBackgroundServices(builder);
            AddCors(builder);
            ConfigureDataProtection(builder);
            ConfigureDatabase(builder);
            ConfigureIdentity(builder);

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"] ?? string.Empty,
                    Version = builder.Configuration["Swagger:Version"] ?? string.Empty
                });
            });
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
                    };
                });
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            AddScopedServices(builder);
            AddAppConfigurationServices(builder);
            AddSingletoneServices(builder);
        }

        private static void AddAppConfigurationServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<ErrorMessagesConfig>(builder.Configuration.GetSection("ErrorMessages"));
            builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("Smtp"));
            builder.Services.Configure<CommonMessagesConfig>(builder.Configuration.GetSection("CommonMessages"));
        }

        private static void AddSingletoneServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ExceptionFactory>();

            builder.Services.AddSingleton<IMessageSender, EmailProvider>();
            builder.Services.AddSingleton<IFactory<IMessageSender>, MessageSenderFactory>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"] ?? string.Empty));
        }

        private static void AddScopedServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAdminSubscriptionRepository, AdminSubscriptionRepository>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
            builder.Services.AddScoped<IFactoryDependencyResolver, FactoryDependencyResolver>();
            builder.Services.AddScoped<IFactoryProvider, FactoryProvider>();
            builder.Services.AddScoped<IMessageSender, EmailMessageSenderService>();
            builder.Services.AddScoped(typeof(ResponseService<>));
        }

        private static void RegisterBackgroundServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<SubscriptionRenewalService>();
            builder.Services.Configure<HostOptions>(options =>
            {
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            });
        }

        private static void AddCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(builder.Configuration["CORS:PolicyName"] ?? string.Empty, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        private static void ConfigureDataProtection(WebApplicationBuilder builder)
        {
            builder.Services.AddDataProtection()
              .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["DataProtection:KeysPath"] ?? string.Empty))
              .SetApplicationName(builder.Configuration["App:Name"] ?? string.Empty);
        }

        private static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
        }

        private static void ConfigureIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<Role>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureLogging(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
        }
    }
}
