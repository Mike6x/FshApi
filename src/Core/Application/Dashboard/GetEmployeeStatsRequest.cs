using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Dashboard;

public class GetEmployeeStatsRequest : IRequest<EmployeeStatsDto>
{
}

public class GetEmployeeStatsRequestHandler : IRequestHandler<GetEmployeeStatsRequest, EmployeeStatsDto>
{
    private readonly IReadRepository<BusinessUnit> _businessUnitRepo;
    private readonly IReadRepository<Department> _departmentRepo;
    private readonly IReadRepository<SubDepartment> _subDepartmentRepo;
    private readonly IReadRepository<Team> _teamRepo;

    private readonly IReadRepository<Employee> _employeeRepo;

    private readonly IStringLocalizer _t;

    public GetEmployeeStatsRequestHandler(
        IReadRepository<BusinessUnit> businessUnitRepo,
        IReadRepository<Department> departmentRepo,
        IReadRepository<SubDepartment> subDepartmentRepo,
        IReadRepository<Team> teamRepo,
        IReadRepository<Employee> employeeRepo,
        IStringLocalizer<GetEmployeeStatsRequestHandler> localizer)
    {
        _businessUnitRepo = businessUnitRepo;
        _departmentRepo = departmentRepo;
        _subDepartmentRepo = subDepartmentRepo;
        _teamRepo = teamRepo;
        _employeeRepo = employeeRepo;
        _t = localizer;
    }

    public async Task<EmployeeStatsDto> Handle(GetEmployeeStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new EmployeeStatsDto
        {
            BusinessUnitCount = await _businessUnitRepo.CountAsync(cancellationToken),
            DepartmentCount = await _departmentRepo.CountAsync(cancellationToken),
            SubDepartmentCount = await _subDepartmentRepo.CountAsync(cancellationToken),
            TeamCount = await _teamRepo.CountAsync(cancellationToken),

            EmployeeCount = await _employeeRepo.CountAsync(cancellationToken),
        };

        int selectedYear = DateTime.UtcNow.Year;

        double[] businessUnitsFigure = new double[13];
        double[] departmentsFigure = new double[13];
        double[] subDepartmentsFigure = new double[13];
        double[] teamsFigure = new double[13];

        // double[] titlesFigure = new double[13];

        double[] employeesFigure = new double[13];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var businessUnitSpec = new AuditableEntitiesByCreatedOnBetweenSpec<BusinessUnit>(filterStartDate, filterEndDate);
            var depatmentSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Department>(filterStartDate, filterEndDate);
            var subDepartmentSpec = new AuditableEntitiesByCreatedOnBetweenSpec<SubDepartment>(filterStartDate, filterEndDate);
            var teamSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Team>(filterStartDate, filterEndDate);

           // var titleSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Title>(filterStartDate, filterEndDate);

            var employeeSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Employee>(filterStartDate, filterEndDate);

            businessUnitsFigure[i - 1] = await _businessUnitRepo.CountAsync(businessUnitSpec, cancellationToken);
            departmentsFigure[i - 1] = await _departmentRepo.CountAsync(depatmentSpec, cancellationToken);
            subDepartmentsFigure[i - 1] = await _subDepartmentRepo.CountAsync(subDepartmentSpec, cancellationToken);
            teamsFigure[i - 1] = await _teamRepo.CountAsync(teamSpec, cancellationToken);

            // titlesFigure[i - 1] = await _titleRepo.CountAsync(titleSpec, cancellationToken);

            employeesFigure[i - 1] = await _employeeRepo.CountAsync(employeeSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["BusinessUnits"], Data = businessUnitsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Departments"], Data = departmentsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["SubDepartments"], Data = subDepartmentsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Teams"], Data = teamsFigure });

        // stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Titles"], Data = titlesFigure });

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Employees"], Data = employeesFigure });

        return stats;
    }
}
