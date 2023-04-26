using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namotion.Reflection;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class BusinessUnitConfig : IEntityTypeConfiguration<BusinessUnit>
{
    public void Configure(EntityTypeBuilder<BusinessUnit> builder)
    {
        builder
            .ToTable("BusinessUnits", SchemaNames.Organization)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .ToTable("Departments", SchemaNames.Organization)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class SubDepartmentConfig : IEntityTypeConfiguration<SubDepartment>
{
    public void Configure(EntityTypeBuilder<SubDepartment> builder)
    {
        builder
            .ToTable("SubDepartments", SchemaNames.Organization)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .ToTable("Teams", SchemaNames.Organization)
            .IsMultiTenant();

        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}
