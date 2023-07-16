using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowerResults;

public class CreateVnPowerResultRequest : IRequest<DefaultIdType>
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public int Number3 { get; set; }
    public int Number4 { get; set; }
    public int Number5 { get; set; }
    public int Number6 { get; set; }
    public int Number7 { get; set; }
    public int Number8 { get; set; }
    public int Number9 { get; set; }

    public int Number10 { get; set; }
    public int Number11 { get; set; }
    public int Number12 { get; set; }
    public int Number13 { get; set; }
    public int Number14 { get; set; }
    public int Number15 { get; set; }
    public int Number16 { get; set; }
    public int Number17 { get; set; }
    public int Number18 { get; set; }
    public int Number19 { get; set; }

    public int Number20 { get; set; }
    public int Number21 { get; set; }
    public int Number22 { get; set; }
    public int Number23 { get; set; }
    public int Number24 { get; set; }
    public int Number25 { get; set; }
    public int Number26 { get; set; }
    public int Number27 { get; set; }
    public int Number28 { get; set; }
    public int Number29 { get; set; }

    public int Number30 { get; set; }
    public int Number31 { get; set; }
    public int Number32 { get; set; }
    public int Number33 { get; set; }
    public int Number34 { get; set; }
    public int Number35 { get; set; }
    public int Number36 { get; set; }
    public int Number37 { get; set; }
    public int Number38 { get; set; }
    public int Number39 { get; set; }

    public int Number40 { get; set; }
    public int Number41 { get; set; }
    public int Number42 { get; set; }
    public int Number43 { get; set; }
    public int Number44 { get; set; }
    public int Number45 { get; set; }
    public int Number46 { get; set; }
    public int Number47 { get; set; }
    public int Number48 { get; set; }
    public int Number49 { get; set; }

    public int Number50 { get; set; }
    public int Number51 { get; set; }
    public int Number52 { get; set; }
    public int Number53 { get; set; }
    public int Number54 { get; set; }
    public int Number55 { get; set; }

    public int RoundId { get; set; }
    public int SubRoundId { get; set; }
}

public class CreateVnPowerResultRequestHandler : IRequestHandler<CreateVnPowerResultRequest, DefaultIdType>
{
    private readonly IRepository<VnPowerResult> _repository;

    public CreateVnPowerResultRequestHandler(IRepository<VnPowerResult> repository) =>
        _repository = repository;

    public async Task<DefaultIdType> Handle(CreateVnPowerResultRequest request, CancellationToken cancellationToken)
    {
        var entity = new VnPowerResult(
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
            request.Number55,
            request.RoundId,
            request.SubRoundId);

        // Add Domain Events to be raised after the commit
        entity.DomainEvents.Add(EntityCreatedEvent.WithEntity(entity));

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}