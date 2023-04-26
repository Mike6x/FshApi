using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class ChannelByIdSpec : Specification<Channel, ChannelDetailsDto>, ISingleResultSpecification<Channel>
{
    public ChannelByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

public class ChannelByCodeSpec : Specification<Channel>, ISingleResultSpecification<Channel>
{
    public ChannelByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class ChannelByNameSpec : Specification<Channel>, ISingleResultSpecification<Channel>
{
    public ChannelByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}