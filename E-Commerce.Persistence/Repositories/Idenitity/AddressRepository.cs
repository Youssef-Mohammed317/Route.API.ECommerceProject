using E_Commerce.Domian.Entites.BasketModule;
using E_Commerce.Domian.Entites.IdentityModule;
using E_Commerce.Domian.Interfaces.Identity;
using E_Commerce.Persistence.IdentityData.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories.Idenitity;

public class AddressRepository(StoreIdentityDbContext context) : IAddressRepository
{
    public async Task<Address?> GetAddressAsync(string userId)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);

        return address;

    }
    public async Task<Address?> CreateOrUpdateAddressAsync(string userId, Address newAddress)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);

        if (address == null)
        {

            address = new Address
            {
                UserId = userId,
                FirstName = newAddress.FirstName,
                LastName = newAddress.LastName,
                Street = newAddress.Street,
                City = newAddress.City,
                Country = newAddress.Country,
            };

            context.Addresses.Add(address);
        }
        else
        {


            address.UserId = userId;
            address.FirstName = newAddress.FirstName;
            address.LastName = newAddress.LastName;
            address.Street = newAddress.Street;
            address.City = newAddress.City;
            address.Country = newAddress.Country;

            context.Addresses.Update(address);
        }

        await context.SaveChangesAsync();

        return address;
    }
}
