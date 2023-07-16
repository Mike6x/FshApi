using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class VnPowerConfig : IEntityTypeConfiguration<VnPower>
{
    public void Configure(EntityTypeBuilder<VnPower> builder)
    {
        builder
            .ToTable("VnPowers", SchemaNames.Game)
            .IsMultiTenant();
    }
}

public class VnPowerResultConfig : IEntityTypeConfiguration<VnPowerResult>
{
    public void Configure(EntityTypeBuilder<VnPowerResult> builder)
    {
        builder
            .ToTable("VnPowerResults", SchemaNames.Game);
    }
}

public class VnPowerForcastConfig : IEntityTypeConfiguration<VnPowerForcast>
{
    public void Configure(EntityTypeBuilder<VnPowerForcast> builder)
    {
        builder
            .ToTable("VnPowerForcasts", SchemaNames.Game);
    }
}