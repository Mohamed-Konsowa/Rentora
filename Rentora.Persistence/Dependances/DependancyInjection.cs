using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rentora.Application.IRepositories;
using Rentora.Persistence.Repositories;
using Rentora.Persistence.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Rentora.Domain.Models;
using Rentora.Persistence.Services;

namespace Rentora.Persistence.Dependances
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string strConnection)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(strConnection));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(url =>
            {
                var actionContext = url.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = url.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!);
            });
            //services.AddDataProtection();
            //services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IRentRepository, RentRepository>();

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
                //.AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            });
            //services.AddAuthentication();
            //services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            //services.AddSingleton(nameof(IEmailSender));

            services.AddHostedService<OtpCleanupService>();

            return services;
        }
    }
}