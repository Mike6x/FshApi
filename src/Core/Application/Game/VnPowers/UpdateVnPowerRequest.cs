using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;

public class UpdateVnPowerRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public int DrawId { get; set; }
    public DateTime? DrawDate { get; set; }

    public int? WinNumber1 { get; set; }
    public int? WinNumber2 { get; set; }
    public int? WinNumber3 { get; set; }
    public int? WinNumber4 { get; set; }
    public int? WinNumber5 { get; set; }
    public int? WinNumber6 { get; set; }
    public int? BonusNumber { get; set; }

    public int? Jackpot1 { get; set; }
    public int? Jackpot2 { get; set; }
    public int? FirstPrize { get; set; }
    public int? SecondPrize { get; set; }
    public int? ThirdPrize { get; set; }
    public int? RoundId { get; set; }
    public int? SubRoundId { get; set; }
    public decimal? Jackpot1Value { get; set; }
    public decimal? Jackpot2Value { get; set; }
}

public class UpdateVnPowerRequestHandler : IRequestHandler<UpdateVnPowerRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<VnPower> _repository;
    private readonly IStringLocalizer _t;
    private readonly IReadRepository<VnPowerResult> _vnPowerResultRepository;
    private readonly IReadRepository<VnPowerForcast> _vnPowerForcastRepository;
    public UpdateVnPowerRequestHandler(
        IRepositoryWithEvents<VnPower> repository,
        IStringLocalizer<UpdateVnPowerRequestHandler> localizer,
        IReadRepository<VnPowerResult> vnPowerResultRepository,
        IReadRepository<VnPowerForcast> vnPowerForcastRepository)
    {
        (_repository, _t, _vnPowerResultRepository, _vnPowerForcastRepository)
            = (repository, localizer, vnPowerResultRepository, vnPowerForcastRepository);
    }

    public async Task<DefaultIdType> Handle(UpdateVnPowerRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        entity.VnPowerForcast = await _vnPowerForcastRepository.GetByIdAsync(request.Id, cancellationToken) ?? new VnPowerForcast();
        entity.VnPowerResult = await _vnPowerResultRepository.GetByIdAsync(request.Id, cancellationToken) ?? new VnPowerResult();

        entity.Update(
            request.DrawId,
            request.DrawDate,
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
            request.Jackpot2Value);

        entity.VnPowerResult.ConvertFromWinNumber(
           entity.WinNumber1,
           entity.WinNumber2,
           entity.WinNumber3,
           entity.WinNumber4,
           entity.WinNumber5,
           entity.WinNumber6,
           entity.BonusNumber);

        if (request.RoundId.HasValue && entity.VnPowerResult.RoundId != request.RoundId)
            entity.VnPowerResult.RoundId = request.RoundId.Value;

        if (request.SubRoundId.HasValue && entity.VnPowerResult.SubRoundId != request.SubRoundId)
            entity.VnPowerResult.SubRoundId = request.SubRoundId.Value;

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}