using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data.Entities;

namespace TaskTracker.Infrustructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount<=0)
            {

                await _roleManager.CreateAsync(new Role()
                {
                    Name="Admin",
                    TenantId=1

                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name="Maintainer",
                    TenantId = 1
                });
            }
        }

    }
}
