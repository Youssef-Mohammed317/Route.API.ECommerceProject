using E_Commerce.Domian.Entites.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Interfaces.Identity;

public interface IAddressRepository
{
    Task<Address?> CreateOrUpdateAddressAsync(string userId, Address newAddress);
    Task<Address?> GetAddressAsync(string userId);
}
