using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Domain.People;

public class Employee : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string? Address { get; set; }
    public bool IsActive { get; private set; }

    public Gender Gender { get; private set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime JoiningDate { get; set; }
    public DateTime? LeavingDate { get; set; }
    public string? Description { get; private set; }

    public DefaultIdType TitleId { get; private set; }
    public virtual Dimension Title { get; private set; } = default!;

    public DefaultIdType? SuperiorId { get; private set; }
    public virtual Employee? Superior { get; private set; }
    public virtual ICollection<Employee> InverseSuperior { get; private set; } = default!;

    public DefaultIdType BusinessUnitId { get; private set; }
    public virtual BusinessUnit BusinessUnit { get; private set; } = default!;
    public DefaultIdType? DepartmentId { get; private set; }
    public virtual Department? Department { get; private set; }
    public DefaultIdType? SubDepartmentId { get; private set; }
    public virtual SubDepartment? SubDepartment { get; private set; }
    public DefaultIdType? TeamId { get; private set; }
    public virtual Team? Team { get; private set; }

    public string? UserId { get; private set; }

    public Employee(
        string code,
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string? address,
        bool isActive,
        Gender gender,
        DateTime dateOfBirth,
        DateTime joiningDate,
        DateTime? leavingDate,
        DefaultIdType titleId,
        DefaultIdType? superiorId,
        DefaultIdType businessUnitId,
        DefaultIdType? departmentId,
        DefaultIdType? subDepartmentteamId,
        DefaultIdType? teamId,
        string? description,
        string? userId)
    {
        Code = code;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address ?? string.Empty;
        Gender = gender;
        Description = description ?? string.Empty;

        DateOfBirth = dateOfBirth == default ? DateTime.UtcNow.AddYears(-18) : dateOfBirth;
        JoiningDate = joiningDate == default ? DateTime.UtcNow : joiningDate;
        LeavingDate = leavingDate;

        TitleId = titleId;
        SuperiorId = superiorId == DefaultIdType.Empty ? null : superiorId;

        BusinessUnitId = businessUnitId;
        DepartmentId = departmentId == DefaultIdType.Empty ? null : departmentId;
        SubDepartmentId = (departmentId == DefaultIdType.Empty || subDepartmentteamId == DefaultIdType.Empty) ? null : subDepartmentteamId;
        TeamId = (departmentId == DefaultIdType.Empty || subDepartmentteamId == DefaultIdType.Empty || teamId == DefaultIdType.Empty) ? null : teamId;

        IsActive = LeavingDate >= DateTime.Today && isActive;
        UserId = userId ?? string.Empty;
    }

    public Employee()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              true,
              Gender.Male,
              DateTime.UtcNow.AddYears(-18),
              DateTime.UtcNow,
              null,
              DefaultIdType.Empty,
              null,
              DefaultIdType.Empty,
              null,
              null,
              null,
              string.Empty,
              null)
    {
       InverseSuperior = new HashSet<Employee>();
    }

    public Employee Update(
        string? code,
        string? firstName,
        string? lastName,
        string? phoneNumber,
        string? email,
        string? address,
        bool? isActive,
        Gender? gender,
        DateTime? dateOfBirth,
        DateTime? joiningDate,
        DateTime? leavingDate,
        DefaultIdType? titleId,
        DefaultIdType? superiorId,
        DefaultIdType? businessUnitId,
        DefaultIdType? departmentId,
        DefaultIdType? subDepartmentId,
        DefaultIdType? teamId,
        string? description,
        string? userId)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (firstName is not null && FirstName?.Equals(firstName) is not true) FirstName = firstName;
        if (lastName is not null && LastName?.Equals(lastName) is not true) LastName = lastName;
        if (phoneNumber is not null && PhoneNumber?.Equals(phoneNumber) is not true) PhoneNumber = phoneNumber;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (address is not null && Address?.Equals(address) is not true) Address = address;

        if (isActive is not null && !IsActive.Equals(isActive)) IsActive = (bool)isActive;

        if (gender is not null && !Gender.Equals(gender)) Gender = (Gender)gender;

        if (dateOfBirth is not null && !DateOfBirth.Equals(dateOfBirth)) DateOfBirth = (DateTime)dateOfBirth;
        if (joiningDate is not null && !JoiningDate.Equals(joiningDate)) JoiningDate = (DateTime)joiningDate;
        if (leavingDate is not null && !LeavingDate.Equals(leavingDate)) LeavingDate = (DateTime)leavingDate;

        if (description is not null && Description?.Equals(description) is not true) Description = description;

        if (titleId.HasValue && titleId.Value != DefaultIdType.Empty && !TitleId.Equals(titleId.Value)) TitleId = titleId.Value;

        if (superiorId == DefaultIdType.Empty)
        {
            SuperiorId = null;
        }
        else if (superiorId.HasValue
                && superiorId.Value != DefaultIdType.Empty
                && !SuperiorId.Equals(superiorId.Value))
        {
            SuperiorId = superiorId.Value;
        }

        if (businessUnitId.HasValue
            && businessUnitId.Value != DefaultIdType.Empty
            && !BusinessUnitId.Equals(businessUnitId.Value))
        {
            BusinessUnitId = businessUnitId.Value;
        }

        if (departmentId.HasValue && departmentId == DefaultIdType.Empty) DepartmentId = null;
        else if (departmentId.HasValue && !DepartmentId.Equals(departmentId.Value)) DepartmentId = departmentId.Value;

        if ((departmentId.HasValue && departmentId == DefaultIdType.Empty) ||
            (subDepartmentId.HasValue && subDepartmentId == DefaultIdType.Empty))
        {
            SubDepartmentId = null;
        }
        else if (subDepartmentId.HasValue && !SubDepartmentId.Equals(subDepartmentId.Value))
        {
            SubDepartmentId = subDepartmentId.Value;
        }

        if ((departmentId.HasValue && departmentId == DefaultIdType.Empty) ||
            (subDepartmentId.HasValue && subDepartmentId == DefaultIdType.Empty) ||
            (teamId.HasValue && teamId == DefaultIdType.Empty))
        {
            TeamId = null;
        }
        else if (teamId.HasValue && !TeamId.Equals(teamId.Value))
        {
            TeamId = teamId.Value;
        }

        UserId = userId;

        return this;
    }
}