using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class UpdateMenuRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public string Href { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public int Parent { get; set; }
}

public class UpdateMenuRequestHandler : IRequestHandler<UpdateMenuRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Menu> _repository;
    private readonly IStringLocalizer _t;

    public UpdateMenuRequestHandler(IRepositoryWithEvents<Menu> repository, IStringLocalizer<UpdateMenuRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateMenuRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive ?? true,
            request.Href,
            request.Icon,
            request.Parent);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}
