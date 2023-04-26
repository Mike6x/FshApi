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