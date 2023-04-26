using FSH.WebApi.Domain.Geo;

namespace FSH.WebApi.Application.Geo.States;

public class StateByIdSpec : Specification<State, StateDetailsDto>, ISingleResultSpecification<State>
{
    public StateByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class StateByCodeSpec : Specification<State>, ISingleResultSpecification<State>
{
    public StateByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class StateByNameSpec : Specification<State>, ISingleResultSpecification<State>
{
    public StateByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}