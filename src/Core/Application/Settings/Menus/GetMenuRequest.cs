using FSH.WebApi.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Settings.Menus;

public class GetMenuRequest : IRequest<MenuDto>
{
    public Guid Id { get; set; }

    public GetMenuRequest(Guid id) => Id = id;
}

public class MenuByIdSpec : Specification<Menu, MenuDto>, ISingleResultSpecification<Menu>
{
    public MenuByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetMenuRequestHandler : IRequestHandler<GetMenuRequest, MenuDto>
{
    private readonly IRepository<Menu> _repository;
    private readonly IStringLocalizer _t;

    public GetMenuRequestHandler(IRepository<Menu> repository, IStringLocalizer<GetMenuRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<MenuDto> Handle(GetMenuRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Menu, MenuDto>)new MenuByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);
}

