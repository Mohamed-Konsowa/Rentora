using Microsoft.Extensions.DependencyInjection;
using Rentora.Application.IServices;
using Rentora.Application.Services;
using Rentora.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.Dependancies
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IImageService, CloudinaryService>();
            return services;
        }
    }
}
