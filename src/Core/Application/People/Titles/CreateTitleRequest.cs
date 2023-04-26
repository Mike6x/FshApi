using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class CreateTitleRequest : IRequest<DefaultIdType>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int Grade { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateTitleRequestHandler : IRequestHandler<CreateTitleRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Title> _repository;

    public CreateTitleRequestHandler(IRepositoryWithEvents<Title> repository) => _repository = repository;

    public async Task<DefaultIdType> Handle(CreateTitleRequest request, CancellationToken cancellationToken)
    {
        var entity = new Title(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.Grade,
            request.IsActive);

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
