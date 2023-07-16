using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Menus;
public class DeleteMenuRequest(Guid id) : IRequest<Guid>
{
    public Guid Id { get; set; } = id;
}

public class DeleteMenuRequestHandler : IRequestHandler<DeleteMenuRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Menu> _repository;
    private readonly IStringLocalizer _t;

    public DeleteMenuRequestHandler(IRepositoryWithEvents<Menu> repository, IStringLocalizer<DeleteMenuRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteMenuRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}