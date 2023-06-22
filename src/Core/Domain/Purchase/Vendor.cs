﻿namespace FSH.WebApi.Domain.Purchase;

public class Vendor : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; private set; }

    public string TaxCode { get; set; }

    // public virtual ICollection<Product>? Products { get; set; }

    public Vendor(string code, string name, string? description, bool isActive, string? phone, string? email, string? address, string? contactPerson, string taxCode)
    {
        Code = code;
        Name = name;
        Description = description;
        IsActive = isActive;

        Phone = phone;
        Email = email;
        Address = address;
        ContactPerson = contactPerson;

        TaxCode = taxCode;
    }

    public Vendor()
    : this(string.Empty, string.Empty, string.Empty, true, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public Vendor Update(string? code, string? name, string? description, bool? isActive, string? phone, string? email, string? address, string? contactPerson, string? taxCode)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (phone is not null && Phone?.Equals(phone) is not true) Phone = phone;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (address is not null && Address?.Equals(address) is not true) Address = address;

        if (contactPerson is not null && ContactPerson?.Equals(contactPerson) is not true) ContactPerson = contactPerson;
        if (taxCode is not null && TaxCode?.Equals(taxCode) is not true) TaxCode = taxCode;

        return this;
    }
}
