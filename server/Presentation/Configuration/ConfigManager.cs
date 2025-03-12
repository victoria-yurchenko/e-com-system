using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Application.BackgroundServices;
using Application.Interfaces.Factories;
using Application.Services;
using Application.Configurations;
using Application.Factories;
using Application.Providers;
using Application.Providers.Messaging;
using Application.Interfaces.Authentication;
using Application.Services.Authentication;
using Application.Interfaces.Subscriptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Gateways;
using Application.Interfaces.Payment;
using Application.Services.Payment;
using Application.Interfaces.UserManagment;
using Application.Interfaces.Analytics;
using Application.Services.Analytics;
using Application.Providers.Factories;
using Application.Interfaces.Notifications;
using Application.Services.ServerResponces;
using Application.Enums;
using Application.Services.Notifications;
using Infrastructure.DatabaseContext;
using Infrastructure.PaymentGateways;
using Infrastructure.Repositories;
using Domain.Entities;
using System.Text;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Application.Dispatchers;
using Application.Interfaces.Strategies;
using Application.Strategies;
using Application.Interfaces.Utils;
using Application.Utils;
using Application.Interfaces.Cache;
using Application.Interfaces.Verification;
using Application.Services.Verification;
using Application.Cache;

namespace Presentation.Configuration
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
            RegisterApplicationDependencies(builder);
            RegisterApplicationConfigurationServices(builder);
            RegisterSingletoneServices(builder);
        }

        private static void RegisterApplicationConfigurationServices(WebApplicationBuilder builder)
        {
            builder.Services.Configure<ErrorMessagesConfig>(builder.Configuration.GetSection("ErrorMessages"));
            builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("Smtp"));
            builder.Services.Configure<CommonMessagesConfig>(builder.Configuration.GetSection("CommonMessages"));
            builder.Services.Configure<SmsConfig>(builder.Configuration.GetSection("SmsConfig"));
        }

        private static void RegisterSingletoneServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ExceptionFactory>();

            builder.Services.AddSingleton<IMessageProvider, EmailProvider>();
            builder.Services.AddSingleton<IFactory<IMessageProvider, DeliveryMethod>, MessagingProviderFactory>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"] ?? string.Empty));
        }

        private static void RegisterApplicationDependencies(WebApplicationBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterApplicationServices(builder);
            RegisterGateways(builder);
            RegisterResolvers(builder);
            RegisterProviders(builder);
            RegisterStrategies(builder);
            RegisterDispatchers(builder);
            RegisterFactories(builder);
            RegisterCaches(builder);
        }

        private static void RegisterCaches(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IVerificationCache, RedisVerificationCache>();
        }

        private static void RegisterFactories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IStrategiesFactory<,>), typeof(StrategiesFactory<,>));
        }

        private static void RegisterDispatchers(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<NotificationDispatcher>();
            builder.Services.AddScoped<BaseDispatcher<NotificationDispatcher>, NotificationDispatcher>();
        }

        private static void RegisterStrategies(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<VerificationStrategy>();
            builder.Services.AddScoped<INotificationStrategy, VerificationStrategy>();
        }

        private static void RegisterResolvers(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFactoryDependencyResolver, FactoryDependencyResolver>();
        }

        private static void RegisterGateways(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();
        }

        private static void RegisterApplicationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IParameterExtractorService, ParameterExtractorService>();
            builder.Services.AddScoped<IVerificationService, VerificationService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<ErrorMessagesConfig>();
            builder.Services.AddScoped(typeof(ResponseService<>));
        }

        private static void RegisterProviders(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFactoryProvider, FactoryProvider>();
            builder.Services.AddScoped<IMessageProvider, EmailProvider>();
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAdminSubscriptionRepository, AdminSubscriptionRepository>();
            builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
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
                options.AddPolicy(builder.Configuration["CORS:PolicyName"] ?? "Default", policy =>
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
                .AddRoles<Domain.Entities.Role>()
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
