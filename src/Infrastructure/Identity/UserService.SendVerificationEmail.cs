using FSH.WebApi.Application.Common.Exceptions;
using FSH.WebApi.Application.Common.Mailing;
using FSH.WebApi.Domain.Identity;
using FSH.WebApi.Shared.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FSH.WebApi.Infrastructure.Identity;

internal partial class UserService
{
    public async Task SendVerificationEmailAsync(string userId, string origin, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_t["User Not Found."]);

        bool isAdmin = await _userManager.IsInRoleAsync(user, FSHRoles.Admin);
        if (isAdmin)
        {
            throw new ConflictException(_t["Administrators do not have been verified"]);
        }

        user.IsActive = true;
        user.EmailConfirmed = false;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            var messages = new List<string> { string.Format(_t["User {0} : "], user.UserName) };
            await GenerateVerificationEmail(user, messages, origin);
        }

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
    }

    // Generate verification email
    private async Task GenerateVerificationEmail(ApplicationUser user, List<string> messages, string origin)
    {
        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
        RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
        {
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            Url = emailVerificationUri
        };
        var mailRequest = new MailRequest(
            new List<string> { user.Email ?? string.Empty },
            _t["Confirm Registration"],
            _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
        _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));
        messages.Add(_t[$"Please check {user.Email} to verify your account!"]);
    }
}