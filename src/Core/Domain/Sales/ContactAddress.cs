using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Sales;
public class ContactAddress
{
    public string? Address { get; set; }

    public string? PostCode { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public DefaultIdType? CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;
}
