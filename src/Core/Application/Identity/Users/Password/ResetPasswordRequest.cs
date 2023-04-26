namespace FSH.WebApi.Application.Identity.Users.Password;
public class ResetPasswordRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string ConfirmPassword { get; set; } = default!;

    public string Token { get; set; } = default!;
}

public class ResetPasswordRequestValidator : CustomValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator(IStringLocalizer<ResetPasswordRequestValidator> T)
    {
        RuleFor(r => r.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage(T["Invalid Email Address."]);

        RuleFor(r => r.Token)
            .NotEmpty()
            .WithMessage(T["Invalid Reset Tocken.."]);

        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(T["Password is required."]);

        RuleFor(r => r.ConfirmPassword)
            .Equal(r => r.Password)
            .WithMessage(T["Passwords do not match."]);
    }
}

// public class ResetPasswordRequest
// {
//    public string? Email { get; set; }

// public string? Password { get; set; }

// public string? Token { get; set; }
// }