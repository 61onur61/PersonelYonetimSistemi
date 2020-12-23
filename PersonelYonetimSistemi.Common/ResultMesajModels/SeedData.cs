using Microsoft.AspNetCore.Identity;
using PersonelYonetimSistemi.Data.Models;

namespace PersonelYonetimSistemi.Common.ResultMesajModels
{
    public static class SeedData
    {
        public static void Seed(UserManager<Personels> userManager,RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<Personels> userManager)
        {
            if (userManager.FindByNameAsync(ResultMesajlari.ResultMesajlari.AdminEmail).Result == null)
            {
                var user = new Personels
                {
                    UserName = ResultMesajlari.ResultMesajlari.AdminEmail,
                    Email = ResultMesajlari.ResultMesajlari.AdminEmail
                };
                var result = userManager.CreateAsync(user, ResultMesajlari.ResultMesajlari.AdminPassword).Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ResultMesajlari.ResultMesajlari.AdminRole);
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(ResultMesajlari.ResultMesajlari.AdminRole).Result)
            {
                var role = new IdentityRole
                {
                    Name = ResultMesajlari.ResultMesajlari.AdminRole
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync(ResultMesajlari.ResultMesajlari.PersonelRole).Result)
            {
                var role = new IdentityRole
                {
                    Name = ResultMesajlari.ResultMesajlari.PersonelRole
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
