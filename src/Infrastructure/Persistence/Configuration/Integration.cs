using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Integration;
using FSH.WebApi.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class ApiSerialConfig : IEntityTypeConfiguration<ApiSerial>
{
    public void Configure(EntityTypeBuilder<ApiSerial> builder)
    {
        builder
            .ToTable("ApiSerials", SchemaNames.Integrations)
            .IsMultiTenant();

        builder
            .Property(b => b.ItemCode)
                .HasMaxLength(256);
        builder
            .Property(b => b.ItemName)
                .HasMaxLength(256);
        builder
        .Property(b => b.ItemSerial)
            .HasMaxLength(256);
    }
}
