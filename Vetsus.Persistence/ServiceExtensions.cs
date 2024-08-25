using Microsoft.Extensions.DependencyInjection;
using Vetsus.Application.Interfaces.Persistence;
using Vetsus.Persistence.Contexts;
using Vetsus.Persistence.Repositories.uow;

namespace Vetsus.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<DapperDataContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
