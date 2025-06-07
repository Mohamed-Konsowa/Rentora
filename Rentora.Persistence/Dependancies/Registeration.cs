using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rentora.Application.IRepositories;
using Rentora.Persistence.Repositories;
using Rentora.Persistence.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Rentora.Persistence.BackgroundServices;

namespace Rentora.Persistence.Dependancies
{
    public static class Registeration
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
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IRentRepository, RentRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            });

            
            services.AddHostedService<OtpCleanupService>();

            return services;
        }
    }
}