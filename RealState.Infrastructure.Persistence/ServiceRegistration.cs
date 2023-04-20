
using RealState.Core.Application.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Infrastructure.Persistence.Repositories;
using RealState.Infrastructure.Persistence.Contexts;

namespace RealState.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            #region "Contexts"

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            #endregion

            #region "Services"

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAdsRepository, AdsRepository>();
            services.AddTransient<IFavouritePretsRepository, FavouritePretsRepository>();
            services.AddTransient<IAdsTypeRepository, AdsTypeRepository>();
            services.AddTransient<IAdsUpgrateRepository, AdsUpgrateRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();






            #endregion

        }
    }
}
