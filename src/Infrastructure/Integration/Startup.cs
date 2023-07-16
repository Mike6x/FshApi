using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.WebApi.Infrastructure.Integration;
internal static class Startup
{
    internal static IServiceCollection AddNetsuiteApi(this IServiceCollection services, IConfiguration config) =>
        services.Configure<NetsuiteSettings>(config.GetSection(nameof(NetsuiteSettings)));
}
