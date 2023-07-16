using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using FSH.WebApi.Domain.Settings;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FSH.WebApi.Infrastructure.DataSeeder;

public class DimensionSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DimensionSeeder> _logger;

    public DimensionSeeder(ISerializerService serializerService, ILogger<DimensionSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Dimensions.Any())
        {
            _logger.LogInformation("Started to Seed Dimensions.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string jsonString = await File.ReadAllTextAsync(path + "/DataSeeder/Dimensions.json", cancellationToken);
            var entityList = _serializerService.Deserialize<List<Dimension>>(jsonString);

            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    await _db.Dimensions.AddAsync(entity, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Dimensions.");
        }
    }
}