using Application.BackgroundServices;
using Application.Interfaces;
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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(); 

            RegisterServices(builder);
            RegisterBackgroundServices(builder);
            ConfigureDataProtection(builder);
            ConfigureDatabase(builder);
            ConfigureIdentity(builder);

            builder.Services.AddAuthorization();
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
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAdminSubscriptionRepository, AdminSubscriptionRepository>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
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

        private static void ConfigureDataProtection(WebApplicationBuilder builder)
        {
            builder.Services.AddDataProtection()
              .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration["DataProtection:KeysPath"]))
              .SetApplicationName(builder.Configuration["App:Name"]);
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
                // password setup i.e.:
                // options.Password.RequireDigit = true;
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
