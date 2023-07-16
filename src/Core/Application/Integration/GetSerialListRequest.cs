using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration;

public class GetSerialListRequest : BaseFilter, IRequest<List<ApiSerial>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class GetSerialListRequestHandler : IRequestHandler<GetSerialListRequest, List<ApiSerial>>
{
    private readonly IWarrantyApi _warrantyApi;
    private readonly IStringLocalizer _t;

    public GetSerialListRequestHandler(IWarrantyApi warrantyApi, IStringLocalizer<GetSerialListRequestHandler> localizer) =>
        (_warrantyApi, _t) = (warrantyApi, localizer);

    public async Task<List<ApiSerial>> Handle(GetSerialListRequest request, CancellationToken ct)
    {
        var modify_from = request.FromDate ?? DateTime.Today;
        var modify_to = request.ToDate ?? DateTime.Today.AddDays(1);
        if (modify_to <= modify_from) modify_to = modify_from.AddDays(1);

        var parameters = new Dictionary<string, string>
                        {
                            { "type", "serial" },
                            { "modify_to", modify_to.ToString("yyyy-MM-dd") },
                            { "modify_from", modify_from.ToString("yyyy-MM-dd") },
                            { "serial_num", string.Empty }
                        };

        var result = await _warrantyApi.GetSerialsAsync(parameters);
        return result ?? [];
    }
}

// var parameters1 = new Dictionary<string, string>
//                {
//                    { "type", "serial" },
//                    { "modify_to", "2023-03-01" },
//                    { "modify_from", "2023-03-01" },
//                    { "serial_num", "" }
//                };