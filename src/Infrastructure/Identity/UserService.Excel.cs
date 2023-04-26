using Ardalis.Specification.EntityFrameworkCore;
using FSH.WebApi.Application.Common.Specification;
using FSH.WebApi.Application.Identity.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FSH.WebApi.Infrastructure.Identity;

internal partial class UserService
{
    public async Task<Stream> ExportAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<ApplicationUser>(filter);

        var list = await _userManager.Users
            .WithSpecification(spec)
            .ProjectToType<UserDetailsDto>()
            .ToListAsync(cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}