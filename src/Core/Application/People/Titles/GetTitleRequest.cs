using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Titles;

public class GetTitleRequest : IRequest<TitleDetailsDto>
{
    public DefaultIdType Id { get; set; }
    public GetTitleRequest(DefaultIdType id) => Id = id;
}

public class GetTitleRequestHandler : IRequestHandler<GetTitleRequest, TitleDetailsDto>
{
    private readonly IRepository<Title> _repository;
    private readonly IStringLocalizer _t;

    public GetTitleRequestHandler(IRepository<Title> repository, IStringLocalizer<GetTitleRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<TitleDetailsDto> Handle(GetTitleRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Title, TitleDetailsDto>)new TitleByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}