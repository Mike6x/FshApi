using FSH.WebApi.Application.Common.Exceptions;
using FSH.WebApi.Application.Identity.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FSH.WebApi.Infrastructure.Identity;

internal partial class UserService
{
    #region My Customize

    public async Task<UserDetailsDto> GetByNameAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.UserName == userName)
                .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_t["User Not Found."]);

        return user.Adapt<UserDetailsDto>();
    }

    public async Task<UserDetailsDto> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Email == email)
                .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_t["User Not Found."]);

        return user.Adapt<UserDetailsDto>();
    }

    public async Task<UserDetailsDto> GetByPhoneAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_t["User Not Found."]);

        return user.Adapt<UserDetailsDto>();
    }

    #endregion
}