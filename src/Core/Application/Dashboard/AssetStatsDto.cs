namespace FSH.WebApi.Application.Dashboard;
public class AssetStatsDto
{
    public int BrandCount { get; set; }
    public int BusinessLineCount { get; set; }
    public int GroupCategorieCount { get; set; }
    public int CategorieCount { get; set; }
    public int SubCategorieCount { get; set; }

    public int AssetCount { get; set; }

    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? AssetByBrandTypePieChart { get; set; }
}
