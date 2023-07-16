namespace FSH.WebApi.Application.Game.VnPowers;

public class VnPowerDto : IDto
{
    public DefaultIdType Id { get; set; }

    public int DrawId { get; set; } = default!;
    public DateTime DrawDate { get; set; }

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

    public decimal Jackpot1Value { get; set; }
    public decimal Jackpot2Value { get; set; }

    public string WinStr { get; set; } = default!;

    public DefaultIdType VnPowerResultId { get; set; }
    public int VnPowerResultRoundId { get; set; } = default!;
    public int VnPowerResultSubRoundId { get; set; } = default!;
}

public class VnPowerDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public int DrawId { get; set; } = default!;
    public DateTime DrawDate { get; set; }

    public int WinNumber1 { get; set; }
    public int WinNumber2 { get; set; }
    public int WinNumber3 { get; set; }
    public int WinNumber4 { get; set; }
    public int WinNumber5 { get; set; }
    public int WinNumber6 { get; set; }
    public int BonusNumber { get; set; }
    public decimal Jackpot1Value { get; set; }
    public decimal Jackpot2Value { get; set; }
    public DefaultIdType VnPowerResultId { get; set; }
    public int VnPowerResultRoundId { get; set; } = default!;
    public int VnPowerResultSubRoundId { get; set; } = default!;
}

public class VnPowerExportDto : IDto
{
    public DefaultIdType Id { get; set; }

    public int DrawId { get; set; } = default!;
    public DateTime DrawDate { get; set; }

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

    public decimal Jackpot1Value { get; set; }
    public decimal Jackpot2Value { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}
