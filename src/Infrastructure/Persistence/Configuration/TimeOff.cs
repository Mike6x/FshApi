using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Leave;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
public class LeaveAllocationConfig : IEntityTypeConfiguration<LeaveAllocation>
{
    public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
    {
        builder
            .ToTable("LeaveAllocations", SchemaNames.TimeOff)
            .IsMultiTenant();
    }
}

public class LeaveApplicationConfig : IEntityTypeConfiguration<LeaveApplication>
{
    public void Configure(EntityTypeBuilder<LeaveApplication> builder)
    {
        builder
            .ToTable("LeaveApplications", SchemaNames.TimeOff)
            .IsMultiTenant();
        builder
            .Property(b => b.RequestRemarks)
                .HasMaxLength(256);
        builder
            .Property(b => b.ApproverRemarks)
                .HasMaxLength(256);
    }
}