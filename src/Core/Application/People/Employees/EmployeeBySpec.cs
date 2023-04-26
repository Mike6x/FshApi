using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class EmployeeByIdSpec : Specification<Employee, EmployeeDetailsDto>, ISingleResultSpecification<Employee>
{
    public EmployeeByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class EmployeeDtoByIdSpec : Specification<Employee, EmployeeDto>, ISingleResultSpecification<Employee>
{
    public EmployeeDtoByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class EmployeeByIdWithTeamSpec : Specification<Employee, EmployeeDetailsDto>, ISingleResultSpecification<Employee>
{
    public EmployeeByIdWithTeamSpec(DefaultIdType id) =>
        Query
            .Include(e => e.Team)
            .Where(e => e.Id == id);
}

public class EmployeeByCodeSpec : Specification<Employee>, ISingleResultSpecification<Employee>
{
    public EmployeeByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class EmployeeByNameSpec : Specification<Employee>, ISingleResultSpecification<Employee>
{
    public EmployeeByNameSpec(string name) =>
        Query
            .Where(e => e.FirstName == name || e.LastName == name);
}

public class EmployeeByEmailSpec : Specification<Employee>, ISingleResultSpecification<Employee>
{
    public EmployeeByEmailSpec(string email) =>
        Query
            .Where(e => e.Email == email);
}

public class EmployeeByPhoneNumberSpec : Specification<Employee>, ISingleResultSpecification<Employee>
{
    public EmployeeByPhoneNumberSpec(string phone) =>
        Query
            .Where(e => e.PhoneNumber == phone);
}