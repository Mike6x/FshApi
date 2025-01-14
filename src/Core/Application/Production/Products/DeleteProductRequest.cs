﻿using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Production.Products;

public class DeleteProductRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public DeleteProductRequest(DefaultIdType id) => Id = id;
}

public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, DefaultIdType>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;

    public DeleteProductRequestHandler(IRepository<Product> repository, IStringLocalizer<DeleteProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Product {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityDeletedEvent.WithEntity(product));

        await _repository.DeleteAsync(product, cancellationToken);

        return request.Id;
    }
}