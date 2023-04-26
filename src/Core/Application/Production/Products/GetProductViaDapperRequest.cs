using FSH.WebApi.Domain.Production;
using Mapster;

namespace FSH.WebApi.Application.Production.Products;

public class GetProductViaDapperRequest : IRequest<ProductDto>
{
    public DefaultIdType Id { get; set; }

    public GetProductViaDapperRequest(DefaultIdType id) => Id = id;
}

public class GetProductViaDapperRequestHandler : IRequestHandler<GetProductViaDapperRequest, ProductDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetProductViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetProductViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ProductDto> Handle(GetProductViaDapperRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.QueryFirstOrDefaultAsync<Product>(
            $"SELECT * FROM public.\"Products\" WHERE \"Id\"  = '{request.Id}' AND \"Tenant\" = '@tenant'", cancellationToken: cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Product {0} Not Found.", request.Id]);

        return product.Adapt<ProductDto>();
    }
}