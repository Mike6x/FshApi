using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class CreatePricePlanRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public string? Color { get; set; }

    public string PackOfMea { get; set; } = default!; // Đơn vị tính - cái bộ, thùng
    public int PackQty { get; set; } // Quy cách thùng gồm mấy cái

    public decimal UnitPrice { get; set; }
    public decimal PriceVAT { get; set; }
    public decimal ListPrice { get; set; } // Recommended consumer price

    public int Priority { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    public DateTime? ListingDate { get; set; }
    public DateTime? NewDate { get; set; }
    public DateTime? ActiveDate { get; set; }
    public DateTime? EOLDate { get; set; }
    public DateTime? DisableDate { get; set; }

    public Guid ProductId { get; set; }
    public Guid PriceGroupId { get; set; }
}

public class CreatePricePlanRequestHandler : IRequestHandler<CreatePricePlanRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<PricePlan> _repository;

    public CreatePricePlanRequestHandler(IRepositoryWithEvents<PricePlan> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreatePricePlanRequest request, CancellationToken cancellationToken)
    {
        var entity = new PricePlan(
                request.Order,
                request.Code,
                request.Name,
                request.Description,
                request.IsActive ?? true,
                request.Color,
                request.PackOfMea,
                request.PackQty,
                request.UnitPrice,
                request.PriceVAT,
                request.ListPrice,
                request.Priority,
                request.EffectiveDate,
                request.ExpiredDate,
                request.ListingDate,
                request.NewDate,
                request.ActiveDate,
                request.EOLDate,
                request.DisableDate,
                request.ProductId,
                request.PriceGroupId);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
