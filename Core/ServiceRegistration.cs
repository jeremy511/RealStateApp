using RealState.Core.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.Services;
using System.Reflection;


namespace RealState.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IAgentService, AgentService>();
            service.AddTransient<IAdsService, AdsService>();
            service.AddTransient<IAdsTypeService, AdsTypeService>();
            service.AddTransient<IAdsUpgrateService, AdsUpgrateService>();
            service.AddTransient<ISaleService, SaleService>();
            service.AddTransient<IFavouritePropertyService, FavouritePropertyService>();
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));




        }
    }
}
