using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class MenuConfig : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder
            .ToTable("Menus", SchemaNames.Settings)
            .IsMultiTenant();
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
        builder
            .Property(b => b.Href)
                .HasMaxLength(256);
        builder
             .Property(b => b.Icon)
                 .HasMaxLength(256);
        builder
             .Property(b => b.Description)
                 .HasMaxLength(256);
    }
}

public class DimensionConfig : IEntityTypeConfiguration<Dimension>
{
    public void Configure(EntityTypeBuilder<Dimension> builder)
    {
        builder
            .ToTable("Dimensions", SchemaNames.Settings)
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

public class BackgroundJobConfig : IEntityTypeConfiguration<BackgroundJob>
{
    public void Configure(EntityTypeBuilder<BackgroundJob> builder)
    {
        builder
            .ToTable("BackgroundJobs", SchemaNames.Settings)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class CronJobConfig : IEntityTypeConfiguration<CronJob>
{
    public void Configure(EntityTypeBuilder<CronJob> builder)
    {
        builder
            .ToTable("CronJobs", SchemaNames.Settings)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class EntityCodeConfig : IEntityTypeConfiguration<EntityCode>
{
    public void Configure(EntityTypeBuilder<EntityCode> builder)
    {
        builder
            .ToTable("EntityCodes", SchemaNames.Settings)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}