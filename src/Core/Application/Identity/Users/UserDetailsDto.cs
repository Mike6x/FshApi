namespace FSH.WebApi.Application.Identity.Users;

public class UserDetailsDto
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    public bool EmailConfirmed { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    // Summary:
    //     Gets or sets the date and time, in UTC, when any user lockout ends.
    //
    // Remarks:
    //     A value in the past means the user is not locked out.
    public virtual DateTimeOffset? LockoutEnd { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }
}