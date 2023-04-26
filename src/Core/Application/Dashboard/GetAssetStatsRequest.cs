using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Dashboard;

public class GetAssetStatsRequest : IRequest<AssetStatsDto>
{
}

public class GetAssetStatsRequestHandler : IRequestHandler<GetAssetStatsRequest, AssetStatsDto>
{
    private readonly IReadRepository<Brand> _brandRepo;
    private readonly IReadRepository<BusinessLine> _businessLineRepo;
    private readonly IReadRepository<GroupCategorie> _groupCategorieRepo;
    private readonly IReadRepository<Categorie> _categorieRepo;
    private readonly IReadRepository<SubCategorie> _subCategorieRepo;

    private readonly IReadRepository<Asset> _assetRepo;

    private readonly IStringLocalizer _t;

    public GetAssetStatsRequestHandler(
        IReadRepository<Brand> brandRepo,
        IReadRepository<BusinessLine> businessLineRepo,
        IReadRepository<GroupCategorie> groupCategorieRepo,
        IReadRepository<Categorie> categorieRepo,
        IReadRepository<SubCategorie> subCategorieRepo,
        IReadRepository<Asset> assetRepo,
        IStringLocalizer<GetAssetStatsRequestHandler> localizer)
    {
        _brandRepo = brandRepo;
        _businessLineRepo = businessLineRepo;
        _groupCategorieRepo = groupCategorieRepo;
        _categorieRepo = categorieRepo;
        _subCategorieRepo = subCategorieRepo;
        _assetRepo = assetRepo;
        _t = localizer;
    }

    public async Task<AssetStatsDto> Handle(GetAssetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new AssetStatsDto
        {
            BrandCount = await _brandRepo.CountAsync(cancellationToken),
            BusinessLineCount = await _businessLineRepo.CountAsync(cancellationToken),
            GroupCategorieCount = await _groupCategorieRepo.CountAsync(cancellationToken),
            CategorieCount = await _categorieRepo.CountAsync(cancellationToken),
            SubCategorieCount = await _subCategorieRepo.CountAsync(cancellationToken),

            AssetCount = await _assetRepo.CountAsync(cancellationToken),
        };

        int selectedYear = DateTime.UtcNow.Year;
        double[] brandsFigure = new double[13];
        double[] businessLinesFigure = new double[13];
        double[] groupCategoriesFigure = new double[13];
        double[] categoriesFigure = new double[13];
        double[] subCategoriesFigure = new double[13];
        double[] assetsFigure = new double[13];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var businessLineSpec = new AuditableEntitiesByCreatedOnBetweenSpec<BusinessLine>(filterStartDate, filterEndDate);
            var groupCategorieSpec = new AuditableEntitiesByCreatedOnBetweenSpec<GroupCategorie>(filterStartDate, filterEndDate);
            var categorieSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Categorie>(filterStartDate, filterEndDate);
            var subCategorieSpec = new AuditableEntitiesByCreatedOnBetweenSpec<SubCategorie>(filterStartDate, filterEndDate);

            var assetSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Asset>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _brandRepo.CountAsync(brandSpec, cancellationToken);
            businessLinesFigure[i - 1] = await _businessLineRepo.CountAsync(businessLineSpec, cancellationToken);
            groupCategoriesFigure[i - 1] = await _groupCategorieRepo.CountAsync(groupCategorieSpec, cancellationToken);
            categoriesFigure[i - 1] = await _categorieRepo.CountAsync(categorieSpec, cancellationToken);
            subCategoriesFigure[i - 1] = await _subCategorieRepo.CountAsync(subCategorieSpec, cancellationToken);

            assetsFigure[i - 1] = await _assetRepo.CountAsync(assetSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Brands"], Data = brandsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["BusinessLines"], Data = businessLinesFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["GroupCategories"], Data = groupCategoriesFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Categories"], Data = categoriesFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["SubCategories"], Data = subCategoriesFigure });

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Assets"], Data = assetsFigure });

        return stats;
    }
}
