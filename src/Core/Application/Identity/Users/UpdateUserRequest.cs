namespace FSH.WebApi.Application.Identity.Users;

public class UpdateUserRequest
{
    public string Id { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public FileUploadRequest? Image { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;

    #region My Customize
    public string? UserName { get; set; }
    public bool IsActive { get; set; }
    public bool EmailConfirmed { get; set; }

    public string? ImageUrl { get; set; }

    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }

    // public virtual DateTimeOffset LockoutEnd { get; set; } = default!;

    public DateTimeOffset? LockoutEnd { get; set; }

    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; } = default!;
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; } = default!;

    #endregion
}