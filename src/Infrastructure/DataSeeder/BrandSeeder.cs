using System.Reflection;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Infrastructure.DataSeeder;

public class BrandSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<BrandSeeder> _logger;

    public BrandSeeder(ISerializerService serializerService, ILogger<BrandSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Brands.Any())
        {
            _logger.LogInformation("Started to Seed Brands.");

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            string jsonString = await File.ReadAllTextAsync(path + "/DataSeeder/brands.json", cancellationToken);
            var entityList = _serializerService.Deserialize<List<Brand>>(jsonString);

            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    await _db.Brands.AddAsync(entity, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Brands.");
        }
    }
}