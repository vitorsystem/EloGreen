using EloGreen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EloGreen.Infrastructure.Data.Mappings;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("TB_EG_SUPPLIER");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("TX_NAME");

        builder.Property(s => s.Document)
            .IsRequired()
            .HasMaxLength(14)
            .HasColumnName("NR_DOCUMENT");

        builder.Property(s => s.IsEsgCertified)
            .IsRequired()
            .HasColumnName("ST_ESG_CERTIFIED")
            .HasColumnType("NUMBER(1)")
            .HasConversion<int>();

        builder.Property(s => s.CreatedAt)
            .IsRequired()
            .HasColumnName("DT_CREATED_AT");

        builder.HasIndex(s => s.Name)
            .HasDatabaseName("IDX_SUP_NAME");
    }
}