using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vetsus.Application.Interfaces;

namespace Vetsus.Application.Configuration
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddHttpContextAccessor();
			services.AddScoped<IUserService, UserService>();
		}
    }

}
