// File: Data/Seed/DbSeeder.cs
using Microsoft.AspNetCore.Identity;
using NeoPay.Entities;

namespace NeoPay
{
    public static class Seeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // 1️⃣ Seed roles
            string[] roles = { "ADMIN", "USER" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2️⃣ Seed admin
            string adminEmail = "admin@neopay.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "NeoPay Admin",
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(newAdmin, adminPassword);
                if (createResult.Succeeded)
                    await userManager.AddToRoleAsync(newAdmin, "ADMIN");
            }
        }
    }
}

