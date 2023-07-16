using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class CreateVnPowerRequest : IRequest<DefaultIdType>
{
    public int DrawId { get; set; }
    public DateTime? DrawDate { get; set; }

    public int WinNumber1 { get; set; }
    public int WinNumber2 { get; set; }
    public int WinNumber3 { get; set; }
    public int WinNumber4 { get; set; }
    public int WinNumber5 { get; set; }
    public int WinNumber6 { get; set; }
    public int BonusNumber { get; set; }

    public int Jackpot1 { get; set; }
    public int Jackpot2 { get; set; }
    public int FirstPrize { get; set; }
    public int SecondPrize { get; set; }
    public int ThirdPrize { get; set; }
    public int RoundId { get; set; }
    public int SubRoundId { get; set; }
    public decimal Jackpot1Value { get; set; }
    public decimal Jackpot2Value { get; set; }
}

public class CreateVnPowerRequestHandler : IRequestHandler<CreateVnPowerRequest, DefaultIdType>
{
    private readonly IRepository<VnPower> _repository;

    public CreateVnPowerRequestHandler(IRepository<VnPower> repository) =>
        _repository = repository;

    public async Task<DefaultIdType> Handle(CreateVnPowerRequest request, CancellationToken cancellationToken)
    {
        var entity = new VnPower(
            request.DrawId,
            request.DrawDate ?? DateTime.Today,
            request.WinNumber1,
            request.WinNumber2,
            request.WinNumber3,
            request.WinNumber4,
            request.WinNumber5,
            request.WinNumber6,
            request.BonusNumber,
            request.Jackpot1,
            request.Jackpot2,
            request.FirstPrize,
            request.SecondPrize,
            request.ThirdPrize,
            request.Jackpot1Value,
            request.Jackpot1Value)
        {
            VnPowerResult = new VnPowerResult().ConvertFromWinNumber(
            request.WinNumber1,
            request.WinNumber2,
            request.WinNumber3,
            request.WinNumber4,
            request.WinNumber5,
            request.WinNumber6,
            request.BonusNumber),

            VnPowerForcast = new VnPowerForcast()
        };

        entity.VnPowerResult.RoundId = request.RoundId;
        entity.VnPowerResult.SubRoundId = request.SubRoundId;

        // Add Domain Events to be raised after the commit
        entity.DomainEvents.Add(EntityCreatedEvent.WithEntity(entity));

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}