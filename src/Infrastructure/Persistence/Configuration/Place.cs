using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Place;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class ChannelConfig : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder
            .ToTable("Channels", SchemaNames.Place)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class RetailerConfig : IEntityTypeConfiguration<Retailer>
{
    public void Configure(EntityTypeBuilder<Retailer> builder)
    {
        builder
            .ToTable("Retailers", SchemaNames.Place)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class StoreConfig : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder
            .ToTable("Stores", SchemaNames.Place)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}