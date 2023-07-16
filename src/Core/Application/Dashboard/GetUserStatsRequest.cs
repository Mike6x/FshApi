using FSH.WebApi.Application.Identity.Roles;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Dashboard;

public class GetUserStatsRequest : IRequest<UserStatsDto>
{
}

public class GetUserStatsRequestHandler : IRequestHandler<GetUserStatsRequest, UserStatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Brand> _brandRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public GetUserStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Brand> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<GetUserStatsRequestHandler> localizer)
    {
        _userService = userService;
        _roleService = roleService;
        _brandRepo = brandRepo;
        _productRepo = productRepo;
        _t = localizer;
    }

    public async Task<UserStatsDto> Handle(GetUserStatsRequest request, CancellationToken cancellationToken)
    {
        var users = await _userService.GetListAsync(cancellationToken);
        var onlineUsers = users.Where(x => x.IsLive).ToList();

        var stats = new UserStatsDto
        {
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            BrandCount = await _brandRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken),
            UserOnlineCount = onlineUsers.Count
        };

        int selectedYear = DateTime.UtcNow.Year - 1;
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];
        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _brandRepo.CountAsync(brandSpec, cancellationToken);
            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Products"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Brands"], Data = brandsFigure });

        return stats;
    }
}
