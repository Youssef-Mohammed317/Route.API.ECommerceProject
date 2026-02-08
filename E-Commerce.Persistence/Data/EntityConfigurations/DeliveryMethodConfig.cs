using E_Commerce.Domian.Entites.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Data.EntityConfigurations;

public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(p => p.Price)
            .HasColumnType("decimal(8,2)");
        builder.Property(p => p.ShortName)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            ;

        builder.Property(p => p.DeliveryTime)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
}
