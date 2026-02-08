using E_Commerce.Domian.Entites.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.DbContexts
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>()
                .ToTable("Addresses");
            builder.Entity<ApplicationUser>()
                .ToTable("Users");
            builder.Entity<IdentityRole>()
                .ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Address)
                .WithOne(b => b.User)
                .HasForeignKey<Address>(b => b.UserId);
        }
        public DbSet<Address> Addresses { get; set; }

    }
}
