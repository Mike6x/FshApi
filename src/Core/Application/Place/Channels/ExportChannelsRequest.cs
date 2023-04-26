using FSH.WebApi.Application.Common.DataIO;
using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class ExportChannelsRequest : BaseFilter, IRequest<Stream>
{
}

public class ExportChannelsRequestHandler : IRequestHandler<ExportChannelsRequest, Stream>
{
    private readonly IReadRepository<Channel> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportChannelsRequestHandler(IReadRepository<Channel> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportChannelsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportChannelsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportChannelsSpecification : EntitiesByBaseFilterSpec<Channel, ChannelExportDto>
{
    public ExportChannelsSpecification(ExportChannelsRequest request)
        : base(request) =>
        Query
           .SearchBy(request);

}