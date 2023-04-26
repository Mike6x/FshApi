using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class CreateMenuRequest : IRequest<Guid>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public string Href { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public int Parent { get; set; }
}

public class CreateMenuRequestHandler : IRequestHandler<CreateMenuRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Menu> _repository;

    public CreateMenuRequestHandler(IRepositoryWithEvents<Menu> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateMenuRequest request, CancellationToken cancellationToken)
    {
        var entity = new Menu(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive ?? true,
            request.Href,
            request.Icon,
            request.Parent);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
