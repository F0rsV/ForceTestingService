using System.Threading.Tasks;
using ForceTestingService.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace ForceTestingService.WEB.Utils
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (await roleManager.FindByNameAsync("teacher") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("teacher"));
            }
            if (await roleManager.FindByNameAsync("student") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("student"));
            }
        }
    }
}