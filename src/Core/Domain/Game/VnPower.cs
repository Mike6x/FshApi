namespace FSH.WebApi.Domain.Game;
public class VnPower : AuditableEntity, IAggregateRoot
{
    public int DrawId { get; set; }
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
    public string WinStr { get; set; } = string.Empty;

    public virtual VnPowerResult VnPowerResult { get; set; } = default!;
    public virtual VnPowerForcast VnPowerForcast { get; set; } = default!;

    public VnPower(
        int drawId,
        DateTime drawDate,
        int winNumber1,
        int winNumber2,
        int winNumber3,
        int winNumber4,
        int winNumber5,
        int winNumber6,
        int bonusNumber,
        int jackpot1,
        int jackpot2,
        int firstPrize,
        int secondPrize,
        int thirdPrize,
        decimal jackpot1Value,
        decimal jackpot2Value)
    {
        DrawId = drawId;
        DrawDate = drawDate;
        WinNumber1 = winNumber1;
        WinNumber2 = winNumber2;
        WinNumber3 = winNumber3;
        WinNumber4 = winNumber4;
        WinNumber5 = winNumber5;
        WinNumber6 = winNumber6;
        BonusNumber = bonusNumber;
        Jackpot1Value = jackpot1Value;
        Jackpot2Value = jackpot2Value;
        Jackpot1 = jackpot1;
        Jackpot2 = jackpot2;
        FirstPrize = firstPrize;
        SecondPrize = secondPrize;
        ThirdPrize = thirdPrize;

        for (int i = 1; i < 66; i++)
        {
            bool value = i == winNumber1 || i == winNumber2 || i == winNumber3
                        || i == winNumber4 - 1 || i == winNumber5 || i == winNumber6 || i == bonusNumber;

            WinStr = WinStr + value + ',';
        }
    }

    public VnPower()
        : this(
              0, DateTime.Today, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
    {
    }

    public VnPower Update(
    int? drawId,
    DateTime? drawDate,
    int? winNumber1,
    int? winNumber2,
    int? winNumber3,
    int? winNumber4,
    int? winNumber5,
    int? winNumber6,
    int? bonusNumber,
    int? jackpot1,
    int? jackpot2,
    int? firstPrize,
    int? secondPrize,
    int? thirdPrize,
    decimal? jackpot1Value,
    decimal? jackpot2Value)
    {
        if (drawId.HasValue && DrawId != drawId) DrawId = drawId.Value;
        if (drawDate.HasValue && DrawDate != drawDate) DrawDate = drawDate.Value;

        if (winNumber1 > 0 && winNumber1 < 66 && WinNumber1 != winNumber1) WinNumber1 = winNumber1.Value;
        if (winNumber2 > 0 && winNumber2 < 66 && WinNumber2 != winNumber2) WinNumber2 = winNumber2.Value;
        if (winNumber3 > 0 && winNumber3 < 66 && WinNumber3 != winNumber3) WinNumber3 = winNumber3.Value;
        if (winNumber4 > 0 && winNumber4 < 66 && WinNumber4 != winNumber4) WinNumber4 = winNumber4.Value;
        if (winNumber5 > 0 && winNumber5 < 66 && WinNumber5 != winNumber5) WinNumber5 = winNumber5.Value;
        if (winNumber6 > 0 && winNumber6 < 66 && WinNumber6 != winNumber6) WinNumber6 = winNumber6.Value;
        if (bonusNumber > 0 && bonusNumber < 66 && BonusNumber != bonusNumber) BonusNumber = bonusNumber.Value;

        if (jackpot1.HasValue && Jackpot1 != jackpot1) Jackpot1 = jackpot1.Value;
        if (jackpot2.HasValue && Jackpot2 != jackpot2) Jackpot2 = jackpot2.Value;
        if (firstPrize.HasValue && FirstPrize != firstPrize) FirstPrize = firstPrize.Value;
        if (secondPrize.HasValue && SecondPrize != secondPrize) SecondPrize = secondPrize.Value;
        if (thirdPrize.HasValue && ThirdPrize != thirdPrize) ThirdPrize = thirdPrize.Value;

        if (jackpot1Value.HasValue && Jackpot1Value != jackpot1Value) Jackpot1Value = jackpot1Value.Value;

        if (jackpot2Value.HasValue && Jackpot2Value != jackpot2Value) Jackpot2Value = jackpot2Value.Value;

        for (int i = 1; i < 66; i++)
        {
            bool value = i == winNumber1 || i == winNumber2 || i == winNumber3
                        || i == winNumber4 - 1 || i == winNumber5 || i == winNumber6 || i == bonusNumber;

            WinStr = WinStr + value + ',';
        }

        // string[] boolsAsText = WinStr.Split(",");
        // bool[] boolResults = new bool[boolsAsText.Length];
        // for (int i = 0; i < boolsAsText.Length; i++)
        // {
        //    // Assuming '1' is true and '0' is false
        //    boolResults[i] = boolsAsText[i] == "1";
        // }

        return this;
    }
}