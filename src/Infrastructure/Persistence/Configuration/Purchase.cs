using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Purchase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class VendorConfig : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder
            .ToTable("Vendors", SchemaNames.Purchase)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}