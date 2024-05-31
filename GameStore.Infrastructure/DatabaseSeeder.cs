using GameStore.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate();

            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    IdentityRole role = new IdentityRole { Name = roleName };
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
            }

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

            if (!context.Products.Any())
            {
                Categories shooter = new() { Name = "Shooter" };
                Categories adventure = new() { Name = "Adventure" };
                Categories rpg = new() { Name = "RPG" };
                Categories battleRoyale = new() { Name = "Battle Royale" };
                Categories strategy = new() { Name = "Strategy" };
                Categories survival = new() { Name = "Survival" };

                context.Categories.AddRange(shooter, adventure, rpg, battleRoyale, strategy, survival);
                await context.SaveChangesAsync();

                context.Products.AddRange(
                    new Products
                    {
                        Name = "Minecraft",
                        Description = "Survival game",
                        Price = 750,
                        Categories = survival,
                        Image = "minecraft.jpg"
                    },
                    new Products
                    {
                        Name = "PUBG",
                        Description = "",
                        Price = 599,
                        Categories = battleRoyale,
                        Image = "pubg.jpg"
                    },
                    new Products
                    {
                        Name = "Counter Strike 2",
                        Description = "",
                        Price = 550,
                        Categories = shooter,
                        Image = "cs.jpg"
                    },
                    new Products
                    {
                        Name = "Red Dead Redemption 2",
                        Description = "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.",
                        Price = 899,
                        Categories = adventure,
                        Image = "rdr2.jpg"
                    },
                    new Products
                    {
                        Name = "Manor Lords",
                        Description = "",
                        Price = 599,
                        Categories = strategy,
                        Image = "manorlords.jpg"
                    },
                    new Products
                    {
                        Name = "Elden Ring",
                        Description = "THE NEW FANTASY ACTION RPG. Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between.",
                        Price = 1799,
                        Categories = rpg,
                        Image = "eldenring.jpg"
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
