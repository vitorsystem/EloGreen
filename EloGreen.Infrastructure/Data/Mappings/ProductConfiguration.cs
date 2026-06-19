using EloGreen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloGreen.Infrastructure.Data.Mappings;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("TB_EG_PRODUCT");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("TX_NAME");

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .HasColumnName("TX_DESCRIPTION");

        builder.Property(p => p.BaseCarbonFootprint)
            .IsRequired()
            .HasColumnType("NUMBER(10,2)")
            .HasColumnName("NR_CARBON_FOOTPRINT");

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("DT_CREATED_AT");

        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_PRODUCT_SUPPLIER");
    }
}