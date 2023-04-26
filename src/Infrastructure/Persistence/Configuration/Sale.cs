using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Place;
using FSH.WebApi.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class StockConfig : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder
            .ToTable("Stocks", SchemaNames.Sale)
            .IsMultiTenant();
    }
}

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ToTable("Orders", SchemaNames.Sale)
            .IsMultiTenant();
        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
    }
}

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .ToTable("OrderItems", SchemaNames.Sale)
            .IsMultiTenant();

    }
}

public class StaffConfig : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder
            .ToTable("Staffs", SchemaNames.Sale)
            .IsMultiTenant();

    }
}

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("Customers", SchemaNames.Sale)
            .IsMultiTenant();
        builder
            .Property(b => b.Code)
                .HasMaxLength(256);
    }
}