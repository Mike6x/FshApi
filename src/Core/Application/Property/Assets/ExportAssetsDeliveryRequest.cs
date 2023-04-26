using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Application.People.Employees;
using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Property;

namespace FSH.WebApi.Application.Property.Assets;

public class ExportAssetsDeliveryRequest : IRequest<Stream>
{
    public DefaultIdType EmployeeId { get; set; }
}

public class ExportAssetsDeliveryRequestHandler : IRequestHandler<ExportAssetsDeliveryRequest, Stream>
{
    private readonly IReadRepository<Asset> _assetRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;
    private readonly IReadRepository<Employee> _employeeRepository;
    private readonly IExcelWriter _excelWriter;
    private readonly IStringLocalizer _t;

    public ExportAssetsDeliveryRequestHandler(
        IReadRepository<Asset> assetRepository,
        ICurrentUser currentUser,
        IUserService userService,
        IReadRepository<Employee> employeeRepository,
        IStringLocalizer<ExportAssetsDeliveryRequestHandler> localizer,
        IExcelWriter excelWriter)
            => (_assetRepository, _currentUser, _userService, _employeeRepository, _t, _excelWriter) = (assetRepository, currentUser, userService, employeeRepository, localizer, excelWriter);

    public async Task<Stream> Handle(ExportAssetsDeliveryRequest request, CancellationToken cancellationToken)
    {
        var assetDeliveryDto = new AssetDeliveryDto();
        var currentUser = await _userService.GetAsync(_currentUser.GetUserId().ToString(), cancellationToken);

        assetDeliveryDto.DeliveryMan = currentUser;

        var receiverMan = await _employeeRepository.FirstOrDefaultAsync(
            (ISpecification<Employee, EmployeeDto>)new EmployeeDtoByIdSpec(request.EmployeeId), cancellationToken)
        ?? throw new NotFoundException(_t["Employee {0} Not Found.", request.EmployeeId]);
        assetDeliveryDto.ReceiverMan = receiverMan;

        var spec = new AssetsByEmployeeIdSpec(request);
        assetDeliveryDto.Assets = await _assetRepository.ListAsync(spec, cancellationToken);
        return _excelWriter.WriteToTemplate(assetDeliveryDto, @".\Templates\AssetsDeliveryTemplate.xlsx");
    }
}

public class AssetsByEmployeeIdSpec : Specification<Asset>
{
    public AssetsByEmployeeIdSpec(ExportAssetsDeliveryRequest request)
        => Query
            .Where(e => e.EmployeeId.Equals(request.EmployeeId));
}