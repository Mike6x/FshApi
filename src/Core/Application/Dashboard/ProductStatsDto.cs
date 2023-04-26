namespace FSH.WebApi.Application.Dashboard;
public class ProductStatsDto
{
    public int BrandCount { get; set; }
    public int BusinessLineCount { get; set; }
    public int GroupCategorieCount { get; set; }
    public int CategorieCount { get; set; }
    public int SubCategorieCount { get; set; }

    public int ProductCount { get; set; }

    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }
}
