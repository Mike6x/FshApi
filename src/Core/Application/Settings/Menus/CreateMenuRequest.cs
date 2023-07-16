using FSH.WebApi.Application.Settings.EntityCodes;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;

public class CreateMenuRequest : IRequest<Guid>
{
    public int Order { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }

    public string Href { get; set; } = default!;
    public string? Icon { get; set; }
    public int Parent { get; set; }
}

public class CreateMenuRequestHandler : IRequestHandler<CreateMenuRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Menu> _repository;
    private readonly IReadRepository<EntityCode> _entityCodeRepository;
    public CreateMenuRequestHandler(IRepositoryWithEvents<Menu> repository, IReadRepository<EntityCode> entityCodeRepository)
        => (_repository, _entityCodeRepository) = (repository, entityCodeRepository);

    public async Task<Guid> Handle(CreateMenuRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Code))
        {
            var dedaultCode = await _entityCodeRepository.FirstOrDefaultAsync(new EntityCodeByCodeSpec(nameof(Menu)), cancellationToken);
            request.Code = dedaultCode == null ? Guid.NewGuid().ToString() : dedaultCode.AutoCode;
        }

        if (string.IsNullOrEmpty(request.Name))
        {
            request.Name = request.Code;
        }

        var entity = new Menu(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive,
            request.Href,
            request.Icon,
            request.Parent);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
