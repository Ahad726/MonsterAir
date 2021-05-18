using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonsterAir.Core.Contexts;
using MonsterAir.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAir.DataSeed
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly AirContext airContext;

        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext, AirContext airContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
            this.airContext = airContext;
        }
        public async Task InitializeAsync()
        {
            // Add pending migrations if exists
            if (applicationDbContext.Database.GetPendingMigrations().Count() > 0)
            {
                applicationDbContext.Database.Migrate();
            }

            if (airContext.Database.GetAppliedMigrations().Count() > 0)
            {
                airContext.Database.Migrate();
            }

            // Return if Admin role exists
            if (applicationDbContext.Roles.Any(x => x.Name == "Admin"))
            {
                return;
            }

            // Create Admin and manager Role
            roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

            //Create user 
            userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            }, "Abc@123").GetAwaiter().GetResult();

            // Assign role to admin user
            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync("admin@gmail.com"), "Admin");
        }


    }
}
