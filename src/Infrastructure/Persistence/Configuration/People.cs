using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable("Employees", SchemaNames.People)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.FirstName)
                .HasMaxLength(256);
        builder
            .Property(b => b.LastName)
            .HasMaxLength(256);
    }
}

public class TitleConfig : IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder
            .ToTable("Titles", SchemaNames.People)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}