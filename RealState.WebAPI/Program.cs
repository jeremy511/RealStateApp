using Microsoft.AspNetCore.Identity;
using RealState.Core.Application;
using RealState.Infrastructure.Identity;
using RealState.Infrastructure.Identity.Entities;
using RealState.Infrastructure.Identity.Seeds;
using RealState.Infrastructure.Persistence;
using RealState.Infrastructure.Shared;
using RealState.WebAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfrastucture(builder.Configuration);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdmin.SeedAsync(userManager, roleManager);
        await DefaultClient.SeedAsync(userManager, roleManager);
        await DefaultAgent.SeedAsync(userManager, roleManager);

    }
    catch (Exception ex)
    {

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseSwaggerExtension();
app.UseSession();
app.UseHealthChecks("/health");



app.MapControllers();


app.Run();
