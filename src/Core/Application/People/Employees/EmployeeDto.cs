using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.People.Employees;

public class EmployeeDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }

    public Gender Gender { get; set; } = Gender.Male;

    public DateTime DateOfBirth { get; set; } = DateTime.Today;
    public DateTime JoiningDate { get; set; } = DateTime.Today;
    public DateTime? LeavingDate { get; set; }
    public string Description { get; set; } = string.Empty;

    public Guid TitleId { get; set; }
    public string TitleCode { get; set; } = default!;
    public string TitleName { get; set; } = default!;
    public int TitleGrade { get; set; } = default!;

    public Guid? SuperiorId { get; set; }
    public string? SuperiorFirstName { get; set; }
    public string? SuperiorLastName { get; set; }
    public Guid BusinessUnitId { get; set; }
    public string BusinessUnitName { get; set; } = default!;
    public Guid? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public Guid? SubDepartmentId { get; set; }
    public string? SubDepartmentName { get; set; }
    public Guid? TeamId { get; set; }
    public string? TeamName { get; set; }
    public string? UserId { get; set; }
}

public class EmployeeDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}

public class EmployeeExportDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string Code { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }

    public Gender Gender { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime? LeavingDate { get; set; }

    public string? Description { get; set; }

    public Guid TitleId { get; set; }
    public string TitleName { get; set; } = default!;
    public Guid? SuperiorId { get; set; }
    public string? SuperiorFirstName { get; set; }
    public string? SuperiorLastName { get; set; }
    public Guid BusinessUnitId { get; set; }
    public string BusinessUnitName { get; set; } = default!;
    public DefaultIdType? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public Guid? SubDepartmentId { get; set; }
    public string? SubDepartmentName { get; set; }
    public DefaultIdType? TeamId { get; set; }
    public string? TeamName { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? UserId { get; set; }
}