namespace FSH.WebApi.Domain.Sales;

public class Customer : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Address { get; private set; }

    public virtual ICollection<Order>? Orders { get; private set; }

    public Customer(string code, string firstName, string? lastName, string phone, string? email, string? address)
    {
        Code = code;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public Customer()
        : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
        Orders = new HashSet<Order>();
    }

    public Customer Update(string? code, string? firstName, string? lastName, string? phone, string? email, string? address)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (firstName is not null && FirstName?.Equals(firstName) is not true) FirstName = firstName;
        if (lastName is not null && LastName?.Equals(lastName) is not true) LastName = lastName;
        if (phone is not null && Phone?.Equals(phone) is not true) Phone = phone;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (address is not null && Address?.Equals(address) is not true) Address = address;

        return this;
    }
}
