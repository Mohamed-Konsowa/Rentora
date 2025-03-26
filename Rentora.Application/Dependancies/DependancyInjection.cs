using Microsoft.Extensions.DependencyInjection;
using Rentora.Application.IServices;
using Rentora.Application.Services;
using Rentora.Presentation.Services;
using System.Reflection;

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

            //Configuration of mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configuration of automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
