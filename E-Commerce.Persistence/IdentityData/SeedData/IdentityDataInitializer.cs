using E_Commerce.Domian.Entites.IdentityModule;
using E_Commerce.Domian.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.SeedData
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }


        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user_1 = new ApplicationUser
                    {
                        DisplayName = "Mohamed Tarek",
                        UserName = "MohamedTarek",
                        Email = "mohamedtarek@gmail.com",
                        PhoneNumber = "01234567891"
                    };
                    var user_2 = new ApplicationUser
                    {
                        DisplayName = "Sara Mohamed",
                        UserName = "SaraMohamed",
                        Email = "saramohamed@gmail.com",
                        PhoneNumber = "01234567892"
                    };
                    await _userManager.CreateAsync(user_1, "mohamedtarek");
                    await _userManager.CreateAsync(user_2, "saramohamed");

                    await _userManager.AddToRoleAsync(user_1, "Admin");
                    await _userManager.AddToRoleAsync(user_2, "SuperAdmin");

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while seed identity database : {ex.Message}");
            }
        }
    }
}
