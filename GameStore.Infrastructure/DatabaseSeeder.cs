using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Застосувати всі міграції до бази даних
            context.Database.Migrate();

            // Список ролей
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    IdentityRole role = new IdentityRole { Name = roleName };
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
            }

            // Створення адміністратора
            string adminEmail = "admin@example.com";
            string adminPassword = "AdminPassword(9";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Створення користувача
            string userEmail = "user@example.com";
            string userPassword = "UserPassword(9";
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                IdentityUser user = new IdentityUser { UserName = userEmail, Email = userEmail };
                IdentityResult result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
