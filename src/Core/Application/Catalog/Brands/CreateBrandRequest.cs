namespace FSH.WebApi.Application.Catalog.Brands;

public class CreateBrandRequest : IRequest<Guid>
{
    public int Order { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateBrandRequestValidator : CustomValidator<CreateBrandRequest>
{
    public CreateBrandRequestValidator(IReadRepository<Brand> repository, IStringLocalizer<CreateBrandRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BrandByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Brand {0} already Exists.", name]);
}

public class CreateBrandRequestHandler : IRequestHandler<CreateBrandRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Brand> _repository;

    public CreateBrandRequestHandler(IRepositoryWithEvents<Brand> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = new Brand(
            request.Order,
            request.Code,
            request.Name,
            request.Description,
            request.IsActive ?? true);

        await _repository.AddAsync(brand, cancellationToken);

        return brand.Id;
    }
}