using E_Commerce.Domian.Entites.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Data.EntityConfigurations;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(p => p.Price)
            .HasColumnType("decimal(8,2)");

        builder.OwnsOne(p => p.Product);

        


    }
}
