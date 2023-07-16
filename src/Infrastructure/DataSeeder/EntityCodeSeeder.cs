using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Settings;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FSH.WebApi.Infrastructure.DataSeeder;

public class EntityCodeSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<EntityCodeSeeder> _logger;

    public EntityCodeSeeder(ISerializerService serializerService, ILogger<EntityCodeSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.EntityCodes.Any())
        {
            _logger.LogInformation("Started to Seed .");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string jsonString = await File.ReadAllTextAsync(path + "/DataSeeder/entityCodes.json", cancellationToken);

            var entityList = _serializerService.Deserialize<List<EntityCode>>(jsonString);

            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    await _db.EntityCodes.AddAsync(entity, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded EntityCodes.");
        }
    }
}