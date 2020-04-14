using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentManagement.DataLayer
{
    public class DataSeedUsers
    {
        public static async Task UserandRoleSeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach(var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
                // Create Admin User
                if (userManager.FindByEmailAsync("colburn@test.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "colburn@test.com",
                        Email = "colburn@test.com"
                    };
                    var result = await userManager.CreateAsync(user, "Password1");
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
                // Create Manager User
                if (userManager.FindByEmailAsync("manager@test.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "manager@test.com",
                        Email = "manager@test.com"
                    };
                    var result = await userManager.CreateAsync(user, "Password1");
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Manager").Wait();
                    }
                }
                // Create Staff User
                //if (userManager.FindByEmailAsync("jdoe@test.com").Result == null)
                //{
                //    IdentityUser user = new IdentityUser
                //    {
                //        UserName = "jdoe@test.com",
                //        Email = "jdoe@test.com"
                //    };
                //    var result = await userManager.CreateAsync(user, "Password1");
                //    if (result.Succeeded)
                //    {
                //        userManager.AddToRoleAsync(user, "Staff").Wait();
                //    }
                //}
                // Create 'No Role' User
                if (userManager.FindByEmailAsync("jane@test.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "jane@test.com",
                        Email = "jane@test.com"
                    };
                    var result = userManager.CreateAsync(user, "Password1").Result;
                    // No role assigned
                }
            }
        }
    }
}
