namespace FSH.WebApi.Application.Dashboard;
public class DistributionStatsDto
{
    public int ChannelCount { get; set; }
    public int RetailerCount { get; set; }
    public int StoreCount { get; set; }
    public int PriceGroupCount { get; set; }

    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? DistributionByRetailerTypePieChart { get; set; }
}
