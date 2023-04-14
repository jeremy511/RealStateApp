using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces.Services;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Contexts;
using RealState.Infrastructure.Identity.Services;


namespace RealState.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        //Extension Method - Decorator
            public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
            {
                #region Contexts
                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
                }
                else
                {
                    services.AddDbContext<IdentityContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                    });
                }
                #endregion

                #region Identity
                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();



                services.AddAuthentication();
                //services.ConfigureApplicationCookie(options => {

                //    options.LoginPath = "/User";
                //    options.AccessDeniedPath = "/User/AccessDenied";
                //    });

                #endregion

                #region Services

                services.AddTransient<IAccountService, AccountService>();

                #endregion


            }

        }
    }
