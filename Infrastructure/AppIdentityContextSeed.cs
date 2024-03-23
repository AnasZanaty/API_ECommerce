using Core.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if ( !userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Anas", 
                    Email ="Anas@gmail.com",
                    UserName="AnasBahgat",
                    Address= new Address
                    {
                        FirstName = "ANas",
                        LastName ="Zanaty",
                        street="2",
                        city="Shebin",
                        state ="Menoufia",
                        zipcode ="15013"
                    }
                    
                };
                await userManager.CreateAsync(user, "Password123!");
            }
        }

    }
}
