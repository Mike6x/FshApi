using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Price.PricePlans;

public class UpdatePricePlanRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public string? Color { get; set; }

    public string PackOfMea { get; set; } = default!; // Đơn vị tính - cái bộ, thùng
    public int PackQty { get; set; } // Quy cách thùng gồm mấy cái

    public decimal UnitPrice { get; set; }
    public decimal PriceVAT { get; set; }
    public decimal ListPrice { get; set; } // Recommended consumer price

    public int Priority { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    public DateTime? ListingDate { get; set; }
    public DateTime? NewDate { get; set; }
    public DateTime? ActiveDate { get; set; }
    public DateTime? EOLDate { get; set; }
    public DateTime? DisableDate { get; set; }

    public Guid ProductId { get; set; }
    public Guid PriceGroupId { get; set; }
}

public class UpdatePricePlanRequestHandler : IRequestHandler<UpdatePricePlanRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<PricePlan> _repository;
    private readonly IStringLocalizer _t;

    public UpdatePricePlanRequestHandler(IRepositoryWithEvents<PricePlan> repository, IStringLocalizer<UpdatePricePlanRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdatePricePlanRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
                request.Order,
                request.Code,
                request.Name,
                request.Description,
                request.IsActive,
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

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}