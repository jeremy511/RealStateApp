using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Domain.Settings;
using RealState.Infrastructure.Shared.Services;


namespace RealState.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));         
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
