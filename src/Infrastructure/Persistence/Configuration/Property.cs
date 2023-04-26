using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Property;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class AssetConfig : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder
            .ToTable("Assets", SchemaNames.Property)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class AssetStatusConfig : IEntityTypeConfiguration<AssetStatus>
{
    public void Configure(EntityTypeBuilder<AssetStatus> builder)
    {
        builder
            .ToTable("AssetStatuses", SchemaNames.Property)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class AssetHistoryConfig : IEntityTypeConfiguration<AssetHistory>
{
    public void Configure(EntityTypeBuilder<AssetHistory> builder)
    {
        builder
            .ToTable("AssetHistorys", SchemaNames.Property)
            .IsMultiTenant();
    }
}