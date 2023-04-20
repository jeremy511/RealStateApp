using Microsoft.AspNetCore.Identity;
using RealState.Core.Application.Enums;
using RealState.Infrastructure.Identity.Entities;


namespace RealState.Infrastructure.Identity.Seeds
{
    public class DefaultDeveloper
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser users = new();

            users.FirstName = "Jhon";
            users.LastName = "Doe";
            users.UserName = "developeruser";
            users.Email = "developer@gmail.com";
            users.IdCard = "405-5667697-8";
            users.Photo = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            users.EmailConfirmed = true;
            users.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != users.Id))
            {
                await userManager.CreateAsync(users, "123Pa$$");
                await userManager.AddToRoleAsync(users, Roles.Developer.ToString());
             


            }
        }
    }
}
