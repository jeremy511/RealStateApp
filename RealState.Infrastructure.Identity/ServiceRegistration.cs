using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealState.Core.Application.Interfaces.Services;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Contexts;
using RealState.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealState.Core.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using RealState.Core.Application.Dtos.Account;
using Microsoft.AspNetCore.Http;

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

                services.ConfigureApplicationCookie(options => {

                    options.LoginPath = "/User";
                    options.AccessDeniedPath = "/User/AccessDenied";
                    });

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized to access this resource" });
                        return c.Response.WriteAsync(result);
                    }
                };

            }); 

            #endregion

            #region Services

            services.AddTransient<IAccountService, AccountService>();

                #endregion


            }

        }
    }
