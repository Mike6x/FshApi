using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Application.Identity.Users.Password;

namespace FSH.WebApi.Host.Controllers.Identity;

public class UsersController : VersionNeutralApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Users)]
    [OpenApiOperation("Get list of all users.", "")]
    public Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken)
    {
        return _userService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(FSHAction.View, FSHResource.Users)]
    [OpenApiOperation("Get a user's details.", "")]
    public Task<UserDetailsDto> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }

    [HttpGet("{id}/roles")]
    [MustHavePermission(FSHAction.View, FSHResource.UserRoles)]
    [OpenApiOperation("Get a user's roles.", "")]
    public Task<List<UserRoleDto>> GetRolesAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetRolesAsync(id, cancellationToken);
    }

    [HttpPost("{id}/roles")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    [MustHavePermission(FSHAction.Update, FSHResource.UserRoles)]
    [OpenApiOperation("Update a user's assigned roles.", "")]
    public Task<string> AssignRolesAsync(string id, UserRolesRequest request, CancellationToken cancellationToken)
    {
        return _userService.AssignRolesAsync(id, request, cancellationToken);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Users)]
    [OpenApiOperation("Creates a new user.", "")]
    public Task<string> CreateAsync(CreateUserRequest request)
    {
        // TODO: check if registering anonymous users is actually allowed (should probably be an appsetting)
        // and return UnAuthorized when it isn't
        // Also: add other protection to prevent automatic posting (captcha?)
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("self-register")]
    [TenantIdHeader]
    [AllowAnonymous]
    [OpenApiOperation("Anonymous user creates a user.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    public Task<string> SelfRegisterAsync(CreateUserRequest request)
    {
        // TODO: check if registering anonymous users is actually allowed (should probably be an appsetting)
        // and return UnAuthorized when it isn't
        // Also: add other protection to prevent automatic posting (captcha?)
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("{id}/toggle-status")]
    [MustHavePermission(FSHAction.Update, FSHResource.Users)]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    [OpenApiOperation("Toggle a user's active status.", "")]
    public async Task<ActionResult> ToggleStatusAsync(string id, ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        if (id != request.UserId)
        {
            return BadRequest();
        }

        await _userService.ToggleStatusAsync(request, cancellationToken);
        return Ok();
    }

    [HttpGet("confirm-email")]
    [AllowAnonymous]
    [OpenApiOperation("Confirm email address for a user.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> ConfirmEmailAsync([FromQuery] string tenant, [FromQuery] string userId, [FromQuery] string code, CancellationToken cancellationToken)
    {
        return _userService.ConfirmEmailAsync(userId, code, tenant, cancellationToken);
    }

    [HttpGet("confirm-phone-number")]
    [AllowAnonymous]
    [OpenApiOperation("Confirm phone number for a user.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> ConfirmPhoneNumberAsync([FromQuery] string userId, [FromQuery] string code)
    {
        return _userService.ConfirmPhoneNumberAsync(userId, code);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Request a password reset email for a user.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    public Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var origin = Request.Headers["origin"];
        return _userService.ForgotPasswordAsync(request, origin);
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Reset a user's password.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    public Task<string> ResetPasswordAsync(ResetPasswordRequest request)
    {
        return _userService.ResetPasswordAsync(request);
    }

    private string GetOriginFromRequest() => $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";

    #region My Customize

    [HttpGet("GetByUserName")]
    [MustHavePermission(FSHAction.View, FSHResource.Users)]
    [OpenApiOperation("Get a user's details by UserName.", "")]
    public Task<UserDetailsDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return _userService.GetByUserNameAsync(userName, cancellationToken);
    }

    [HttpPut("update")]
    [MustHavePermission(FSHAction.Update, FSHResource.Users)]
    [OpenApiOperation("Update profile details of user.", "")]
    public async Task<ActionResult> UpdateUserAsync(UpdateUserRequest request)
    {
        string? origin = GetOriginFromRequest();
        await _userService.UpdateAsync(request, origin);
        return Ok();
    }

    [HttpPost("{id}/verification-email")]
    [MustHavePermission(FSHAction.Update, FSHResource.Users)]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    [OpenApiOperation("Send a user's verification email.", "")]
    public async Task<ActionResult> SendVerificationEmailAsync(string userId, CancellationToken cancellationToken)
    {
        string? origin = GetOriginFromRequest();
        await _userService.SendVerificationEmailAsync(userId, origin, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Users)]

    [OpenApiOperation("Delete a User.", "")]
    public Task<string> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.DeleteAsync(id, cancellationToken);
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Users)]
    [OpenApiOperation("Export a users using available filters.", "")]
    public async Task<FileResult> ExportAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var result = await _userService.ExportAsync(filter, cancellationToken);

        return File(result, "application/octet-stream", "UserExports");
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Users)]
    [OpenApiOperation("Search user using available filters.", "")]
    public async Task<PaginationResponse<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        return await _userService.SearchAsync(filter, cancellationToken);
    }
    #endregion

    #region Chat Users
    [HttpGet("{contactId}/chatusers")]
    [MustHavePermission(FSHAction.View, FSHResource.ChatMessages)]
    [OpenApiOperation("Get list of all chat users.", "")]
    public async Task<List<UserDetailsDto>> GetChatUsersAsync(string contactId, CancellationToken cancellationToken)
    {
        var list = await _userService.GetListAsync(cancellationToken);

        return list.Where(user => user.Id.ToString() != contactId).ToList();
    }

    [HttpGet("{id}/chatuser")]
    [MustHavePermission(FSHAction.View, FSHResource.ChatMessages)]
    [OpenApiOperation("Get a chatuser's details.", "")]
    public Task<UserDetailsDto> GetChatUserAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }

    #endregion
}
