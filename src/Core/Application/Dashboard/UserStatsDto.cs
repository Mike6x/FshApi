namespace FSH.WebApi.Application.Dashboard;

public class UserStatsDto
{
    public int ProductCount { get; set; }
    public int BrandCount { get; set; }
    public int UserCount { get; set; }
    public int RoleCount { get; set; }
    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }
}