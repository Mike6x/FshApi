using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerForcasts;

public class CreateVnPowerForcastRequest : IRequest<DefaultIdType>
{
    // public DefaultIdType Id { get; set; }
    public decimal Number1 { get; set; }
    public decimal Number2 { get; set; }
    public decimal Number3 { get; set; }
    public decimal Number4 { get; set; }
    public decimal Number5 { get; set; }
    public decimal Number6 { get; set; }
    public decimal Number7 { get; set; }
    public decimal Number8 { get; set; }
    public decimal Number9 { get; set; }

    public decimal Number10 { get; set; }
    public decimal Number11 { get; set; }
    public decimal Number12 { get; set; }
    public decimal Number13 { get; set; }
    public decimal Number14 { get; set; }
    public decimal Number15 { get; set; }
    public decimal Number16 { get; set; }
    public decimal Number17 { get; set; }
    public decimal Number18 { get; set; }
    public decimal Number19 { get; set; }

    public decimal Number20 { get; set; }
    public decimal Number21 { get; set; }
    public decimal Number22 { get; set; }
    public decimal Number23 { get; set; }
    public decimal Number24 { get; set; }
    public decimal Number25 { get; set; }
    public decimal Number26 { get; set; }
    public decimal Number27 { get; set; }
    public decimal Number28 { get; set; }
    public decimal Number29 { get; set; }

    public decimal Number30 { get; set; }
    public decimal Number31 { get; set; }
    public decimal Number32 { get; set; }
    public decimal Number33 { get; set; }
    public decimal Number34 { get; set; }
    public decimal Number35 { get; set; }
    public decimal Number36 { get; set; }
    public decimal Number37 { get; set; }
    public decimal Number38 { get; set; }
    public decimal Number39 { get; set; }

    public decimal Number40 { get; set; }
    public decimal Number41 { get; set; }
    public decimal Number42 { get; set; }
    public decimal Number43 { get; set; }
    public decimal Number44 { get; set; }
    public decimal Number45 { get; set; }
    public decimal Number46 { get; set; }
    public decimal Number47 { get; set; }
    public decimal Number48 { get; set; }
    public decimal Number49 { get; set; }

    public decimal Number50 { get; set; }
    public decimal Number51 { get; set; }
    public decimal Number52 { get; set; }
    public decimal Number53 { get; set; }
    public decimal Number54 { get; set; }
    public decimal Number55 { get; set; }
}

public class CreateVnPowerForcastRequestHandler : IRequestHandler<CreateVnPowerForcastRequest, DefaultIdType>
{
    private readonly IRepository<VnPowerForcast> _repository;

    public CreateVnPowerForcastRequestHandler(IRepository<VnPowerForcast> repository) =>
        _repository = repository;

    public async Task<DefaultIdType> Handle(CreateVnPowerForcastRequest request, CancellationToken cancellationToken)
    {
        var entity = new VnPowerForcast(
            request.Number1,
            request.Number2,
            request.Number3,
            request.Number4,
            request.Number5,
            request.Number6,
            request.Number7,
            request.Number8,
            request.Number9,
            request.Number10,
            request.Number11,
            request.Number12,
            request.Number13,
            request.Number14,
            request.Number15,
            request.Number16,
            request.Number17,
            request.Number18,
            request.Number19,
            request.Number20,
            request.Number21,
            request.Number22,
            request.Number23,
            request.Number24,
            request.Number25,
            request.Number26,
            request.Number27,
            request.Number28,
            request.Number29,
            request.Number30,
            request.Number31,
            request.Number32,
            request.Number33,
            request.Number34,
            request.Number35,
            request.Number36,
            request.Number37,
            request.Number38,
            request.Number39,
            request.Number40,
            request.Number41,
            request.Number42,
            request.Number43,
            request.Number44,
            request.Number45,
            request.Number46,
            request.Number47,
            request.Number48,
            request.Number49,
            request.Number50,
            request.Number51,
            request.Number52,
            request.Number53,
            request.Number54,
            request.Number55);

        // Add Domain Events to be raised after the commit
        entity.DomainEvents.Add(EntityCreatedEvent.WithEntity(entity));

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}