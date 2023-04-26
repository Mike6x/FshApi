namespace FSH.WebApi.Application.Dashboard;
public class EmployeeStatsDto
{
    public int BusinessUnitCount { get; set; }
    public int DepartmentCount { get; set; }
    public int SubDepartmentCount { get; set; }
    public int TeamCount { get; set; }

    public int TitleCount { get; set; }
    public int EmployeeCount { get; set; }

    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? OrganizationByBusinessUnitTypePieChart { get; set; }
}
