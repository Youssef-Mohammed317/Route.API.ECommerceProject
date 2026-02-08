using E_Commerce.Domian.Entites.OrderModule;
using E_Commerce.Domian.Entites.ProductModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        #region Constructors
        public StoreDbContext()
        {
        }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        #endregion

        #region Config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
        #endregion

        #region Modules

        #region ProductModule
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods
        {
            get; set;
        }
        #endregion

        #endregion


    }
}
