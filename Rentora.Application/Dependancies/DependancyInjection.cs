using Microsoft.Extensions.DependencyInjection;
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
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly));
            //services.AddAutoMapper(typeof(DependancyInjection).Assembly);
            //services.AddValidatorsFromAssembly(typeof(DependancyInjection).Assembly);
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BeheviorValidation<,>));
            //services.AddTransient<CustomMiddleware>();
            return services;
        }
    }
}
