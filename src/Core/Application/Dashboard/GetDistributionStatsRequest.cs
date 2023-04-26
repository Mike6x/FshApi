using FSH.WebApi.Domain.Place;
using FSH.WebApi.Domain.Price;

namespace FSH.WebApi.Application.Dashboard;

public class GetDistributionStatsRequest : IRequest<DistributionStatsDto>
{
}

public class GetDistributionStatsRequestHandler : IRequestHandler<GetDistributionStatsRequest, DistributionStatsDto>
{
    private readonly IReadRepository<Channel> _channelRepo;
    private readonly IReadRepository<Retailer> _retailerRepo;
    private readonly IReadRepository<Store> _storeRepo;
    private readonly IReadRepository<PriceGroup> _priceGroupRepo;

    private readonly IStringLocalizer _t;

    public GetDistributionStatsRequestHandler(
        IReadRepository<Channel> channelRepo,
        IReadRepository<Retailer> retailerRepo,
        IReadRepository<Store> storeRepo,
        IReadRepository<PriceGroup> priceGroupRepo,
        IStringLocalizer<GetDistributionStatsRequestHandler> localizer)
    {
        _channelRepo = channelRepo;
        _retailerRepo = retailerRepo;
        _storeRepo = storeRepo;
        _priceGroupRepo = priceGroupRepo;

        _t = localizer;
    }

    public async Task<DistributionStatsDto> Handle(GetDistributionStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new DistributionStatsDto
        {
            ChannelCount = await _channelRepo.CountAsync(cancellationToken),
            RetailerCount = await _retailerRepo.CountAsync(cancellationToken),
            StoreCount = await _storeRepo.CountAsync(cancellationToken),
            PriceGroupCount = await _priceGroupRepo.CountAsync(cancellationToken),
        };

        int selectedYear = DateTime.UtcNow.Year;

        double[] channelsFigure = new double[13];
        double[] retailersFigure = new double[13];
        double[] storesFigure = new double[13];
        double[] priceGroupFigure = new double[13];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var channelSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Channel>(filterStartDate, filterEndDate);
            var retailerSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Retailer>(filterStartDate, filterEndDate);
            var storeSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Store>(filterStartDate, filterEndDate);
            var priceGroupSpec = new AuditableEntitiesByCreatedOnBetweenSpec<PriceGroup>(filterStartDate, filterEndDate);

            channelsFigure[i - 1] = await _channelRepo.CountAsync(channelSpec, cancellationToken);
            retailersFigure[i - 1] = await _retailerRepo.CountAsync(retailerSpec, cancellationToken);
            storesFigure[i - 1] = await _storeRepo.CountAsync(storeSpec, cancellationToken);
            priceGroupFigure[i - 1] = await _priceGroupRepo.CountAsync(priceGroupSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Channels"], Data = channelsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Retailers"], Data = retailersFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Stores"], Data = storesFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["PriceGroups"], Data = priceGroupFigure });

        return stats;
    }
}
