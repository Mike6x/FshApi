using FSH.WebApi.Application.Geo.Countries;
using FSH.WebApi.Application.Geo.Districts;
using FSH.WebApi.Application.Geo.Provinces;
using FSH.WebApi.Application.Geo.States;
using FSH.WebApi.Application.Geo.Wards;
using FSH.WebApi.Domain.Geo;
using FSH.WebApi.Domain.Settings;

namespace FSH.WebApi.Application.Settings.Dimensions;
public class DeleteDimensionRequest(DefaultIdType id) : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; } = id;
}

public class DeleteDimensionRequestHandler : IRequestHandler<DeleteDimensionRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Dimension> _repository;
    private readonly IReadRepository<Country> _countryRepo;
    private readonly IReadRepository<State> _stateRepo;
    private readonly IReadRepository<Province> _provinceRepo;
    private readonly IReadRepository<District> _districtRepo;
    private readonly IReadRepository<Ward> _wardRepo;

    private readonly IStringLocalizer _t;

    public DeleteDimensionRequestHandler(
        IRepositoryWithEvents<Dimension> repository,
        IReadRepository<Country> countryRepo,
        IReadRepository<State> stateRepo,
        IReadRepository<Province> provinceRepo,
        IReadRepository<District> districtRepo,
        IReadRepository<Ward> wardRepo,
        IStringLocalizer<DeleteDimensionRequestHandler> localizer)
        => (_repository, _countryRepo, _stateRepo, _provinceRepo, _districtRepo, _wardRepo, _t)
        = (repository, countryRepo, stateRepo, provinceRepo, districtRepo, wardRepo, localizer);

    public async Task<DefaultIdType> Handle(DeleteDimensionRequest request, CancellationToken cancellationToken)
    {
        if (await _countryRepo.AnyAsync(new CountriesByTypesSpec(request.Id), cancellationToken)
            || await _stateRepo.AnyAsync(new StatesByTypeSpec(request.Id), cancellationToken)
            || await _provinceRepo.AnyAsync(new ProvincesByTypeSpec(request.Id), cancellationToken)
            || await _districtRepo.AnyAsync(new DistrictsByTypeSpec(request.Id), cancellationToken)
            || await _wardRepo.AnyAsync(new WardsByTypeSpec(request.Id), cancellationToken)
            )
        {
            throw new ConflictException(_t["Entity cannot be deleted as it's being used."]);
        }

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity ?? throw new NotFoundException(_t["Entity {0} Not Found."]);

        await _repository.DeleteAsync(entity, cancellationToken);

        return request.Id;
    }
}
