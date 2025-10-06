using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data.Entities.Identity;

namespace SchoolProject.Infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    Fullname="TaskTrackerProject",
                    PhoneNumber="123456",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TenantId=1
                };
                await _userManager.CreateAsync(defaultuser, "M12345_m");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}
