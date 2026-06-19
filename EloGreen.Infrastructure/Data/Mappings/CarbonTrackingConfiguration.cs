using EloGreen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EloGreen.Infrastructure.Data.Mappings
{
    public class CarbonTrackingConfiguration : IEntityTypeConfiguration<CarbonTracking>
    {
        public void Configure(EntityTypeBuilder<CarbonTracking> builder)
        {
            builder.ToTable("TB_EG_CARBON_TRACKING");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.ActivityDescription)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("TX_ACTIVITY_DESC");

            builder.Property(ct => ct.CarbonEmitted)
                .IsRequired()
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("NR_CARBON_EMITTED");

            builder.Property(ct => ct.TrackingDate)
                .IsRequired()
                .HasColumnName("DT_TRACKING");

            builder.HasOne(ct => ct.Product)
                .WithMany(p => p.CarbonTrackings)
                .HasForeignKey(ct => ct.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TRACKING_PRODUCT");
        }
    }
}