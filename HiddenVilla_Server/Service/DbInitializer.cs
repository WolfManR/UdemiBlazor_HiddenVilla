using Common;

using DataAccess.Data;

using HiddenVilla_Server.Service.IService;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace HiddenVilla_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }
            }
            catch (System.Exception)
            {

            }

            if (context.Roles.Any(x => x.Name == SD.Role_Admin)) return;

            roleManager.CreateAsync(new(SD.Role_Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new(SD.Role_Customer)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new(SD.Role_Employee)).GetAwaiter().GetResult();

            userManager.CreateAsync(new()
            {
                UserName = "admin@gmail.com",
                Email ="admin@gmail.com",
                EmailConfirmed = true
            }, "Admin123*").GetAwaiter().GetResult();

            var user = context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
