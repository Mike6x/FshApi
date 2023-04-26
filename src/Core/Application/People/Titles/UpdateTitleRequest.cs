using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class UpdateTitleRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Grade { get; set; }

    public bool IsActive { get; set; }
}

public class UpdateTitleRequestHandler : IRequestHandler<UpdateTitleRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Title> _repository;
    private readonly IStringLocalizer _t;

    public UpdateTitleRequestHandler(IRepositoryWithEvents<Title> repository, IStringLocalizer<UpdateTitleRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateTitleRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.Update(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.Grade,
            request.IsActive);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}