namespace FSH.WebApi.Application.Catalog.Brands;

public class DeleteRandomBrands : IRequest
{
}

public class CleanRandomBrandsHandler(
    ILogger<CleanRandomBrandsHandler> logger,
    ISender mediator,
    IReadRepository<Brand> repository) : IRequestHandler<DeleteRandomBrands>
{
    private readonly ILogger<CleanRandomBrandsHandler> _logger = logger;
    private readonly ISender _mediator = mediator;
    private readonly IReadRepository<Brand> _repository = repository;

    public async Task Handle(DeleteRandomBrands request, CancellationToken cancellationToken)
    {

        var items = await _repository.ListAsync(new RandomBrandsSpec(), cancellationToken);

        _logger.LogInformation("Brands Random: {brandsCount} ", items.Count.ToString());

        foreach (var item in items)
        {
            await _mediator.Send(new DeleteBrandRequest(item.Id), cancellationToken);
        }

        _logger.LogInformation("All random brands deleted.");
    }
}

public class RandomBrandsSpec : Specification<Brand>
{
    public RandomBrandsSpec() =>
        Query.Where(b => !string.IsNullOrEmpty(b.Name) && b.Name.Contains("Brand Random"));
}