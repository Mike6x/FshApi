using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Price;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class PriceGroupConfig : IEntityTypeConfiguration<PriceGroup>
{
    public void Configure(EntityTypeBuilder<PriceGroup> builder)
    {
        builder
            .ToTable("PriceGroups", SchemaNames.Price)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);
        builder
             .Property(b => b.Description)
                 .HasMaxLength(2048);
    }
}

public class PricePlanConfig : IEntityTypeConfiguration<PricePlan>
{
    public void Configure(EntityTypeBuilder<PricePlan> builder)
    {
        builder
            .ToTable("PricePlans", SchemaNames.Price)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);
        builder
             .Property(b => b.Description)
                 .HasMaxLength(2048);
    }
}