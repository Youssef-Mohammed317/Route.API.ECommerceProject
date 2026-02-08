using E_Commerce.Domian.Entites.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.EntityConfigurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.SubTotal)
            .HasColumnType("decimal(8,2)");

        builder.HasMany(p => p.Items)
            .WithOne();

        builder.HasOne(p => p.DeliveryMethod)
            .WithMany()
            .HasForeignKey(p => p.DeliveryMethodId);

        builder.OwnsOne(p => p.Address);



    }
}
