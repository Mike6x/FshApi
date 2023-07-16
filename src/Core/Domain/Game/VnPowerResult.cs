using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Game;
public class VnPowerResult : IAggregateRoot
{
    [ForeignKey("VnPower")]
    public Guid Id { get; set; }

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = new();

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

    public virtual VnPower VnPower { get; set; } = default!;

    public VnPowerResult(
        int number1,
        int number2,
        int number3,
        int number4,
        int number5,
        int number6,
        int number7,
        int number8,
        int number9,
        int number10,
        int number11,
        int number12,
        int number13,
        int number14,
        int number15,
        int number16,
        int number17,
        int number18,
        int number19,
        int number20,
        int number21,
        int number22,
        int number23,
        int number24,
        int number25,
        int number26,
        int number27,
        int number28,
        int number29,
        int number30,
        int number31,
        int number32,
        int number33,
        int number34,
        int number35,
        int number36,
        int number37,
        int number38,
        int number39,
        int number40,
        int number41,
        int number42,
        int number43,
        int number44,
        int number45,
        int number46,
        int number47,
        int number48,
        int number49,
        int number50,
        int number51,
        int number52,
        int number53,
        int number54,
        int number55,
        int roundId,
        int subRoundId)
    {
        // Id = id;
        Number1 = number1;
        Number2 = number2;
        Number3 = number3;
        Number4 = number4;
        Number5 = number5;
        Number6 = number6;
        Number7 = number7;
        Number8 = number8;
        Number9 = number9;
        Number10 = number10;
        Number11 = number11;
        Number12 = number12;
        Number13 = number13;
        Number14 = number14;
        Number15 = number15;
        Number16 = number16;
        Number17 = number17;
        Number18 = number18;
        Number19 = number19;
        Number20 = number20;
        Number21 = number21;
        Number22 = number22;
        Number23 = number23;
        Number24 = number24;
        Number25 = number25;
        Number26 = number26;
        Number27 = number27;
        Number28 = number28;
        Number29 = number29;
        Number30 = number30;
        Number31 = number31;
        Number32 = number32;
        Number33 = number33;
        Number34 = number34;
        Number35 = number35;
        Number36 = number36;
        Number37 = number37;
        Number38 = number38;
        Number39 = number39;
        Number40 = number40;
        Number41 = number41;
        Number42 = number42;
        Number43 = number43;
        Number44 = number44;
        Number45 = number45;
        Number46 = number46;
        Number47 = number47;
        Number48 = number48;
        Number49 = number49;
        Number50 = number50;
        Number51 = number51;
        Number52 = number52;
        Number53 = number53;
        Number54 = number54;
        Number55 = number55;
        RoundId = roundId;
        SubRoundId = subRoundId;
    }

    public VnPowerResult()
        : this(
              0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
    {
    }

    public VnPowerResult Update(
        int? number1,
        int? number2,
        int? number3,
        int? number4,
        int? number5,
        int? number6,
        int? number7,
        int? number8,
        int? number9,
        int? number10,
        int? number11,
        int? number12,
        int? number13,
        int? number14,
        int? number15,
        int? number16,
        int? number17,
        int? number18,
        int? number19,
        int? number20,
        int? number21,
        int? number22,
        int? number23,
        int? number24,
        int? number25,
        int? number26,
        int? number27,
        int? number28,
        int? number29,
        int? number30,
        int? number31,
        int? number32,
        int? number33,
        int? number34,
        int? number35,
        int? number36,
        int? number37,
        int? number38,
        int? number39,
        int? number40,
        int? number41,
        int? number42,
        int? number43,
        int? number44,
        int? number45,
        int? number46,
        int? number47,
        int? number48,
        int? number49,
        int? number50,
        int? number51,
        int? number52,
        int? number53,
        int? number54,
        int? number55,
        int? roundId,
        int? subRoundId)
    {
        if (number1.HasValue && Number1 != number1) Number1 = number1.Value;
        if (number2.HasValue && Number2 != number2) Number2 = number2.Value;
        if (number3.HasValue && Number3 != number3) Number3 = number3.Value;
        if (number4.HasValue && Number4 != number4) Number4 = number4.Value;
        if (number5.HasValue && Number5 != number5) Number5 = number5.Value;
        if (number6.HasValue && Number6 != number6) Number6 = number6.Value;
        if (number7.HasValue && Number7 != number7) Number7 = number7.Value;
        if (number8.HasValue && Number8 != number8) Number8 = number8.Value;
        if (number9.HasValue && Number9 != number9) Number9 = number9.Value;
        if (number10.HasValue && Number10 != number10) Number10 = number10.Value;
        if (number11.HasValue && Number11 != number11) Number11 = number11.Value;
        if (number12.HasValue && Number12 != number12) Number12 = number12.Value;
        if (number13.HasValue && Number13 != number13) Number13 = number13.Value;
        if (number14.HasValue && Number14 != number14) Number14 = number14.Value;
        if (number15.HasValue && Number15 != number15) Number15 = number15.Value;
        if (number16.HasValue && Number16 != number16) Number16 = number16.Value;
        if (number17.HasValue && Number17 != number17) Number17 = number17.Value;
        if (number18.HasValue && Number18 != number18) Number18 = number18.Value;
        if (number19.HasValue && Number19 != number19) Number19 = number19.Value;
        if (number20.HasValue && Number20 != number20) Number20 = number20.Value;
        if (number21.HasValue && Number21 != number21) Number21 = number21.Value;
        if (number22.HasValue && Number22 != number22) Number22 = number22.Value;
        if (number23.HasValue && Number23 != number23) Number23 = number23.Value;
        if (number24.HasValue && Number24 != number24) Number24 = number24.Value;
        if (number25.HasValue && Number25 != number25) Number25 = number25.Value;
        if (number26.HasValue && Number26 != number26) Number26 = number26.Value;
        if (number27.HasValue && Number27 != number27) Number27 = number27.Value;
        if (number28.HasValue && Number28 != number28) Number28 = number28.Value;
        if (number29.HasValue && Number29 != number29) Number29 = number29.Value;
        if (number30.HasValue && Number30 != number30) Number30 = number30.Value;
        if (number31.HasValue && Number31 != number31) Number31 = number31.Value;
        if (number32.HasValue && Number32 != number32) Number32 = number32.Value;
        if (number33.HasValue && Number33 != number33) Number33 = number33.Value;
        if (number34.HasValue && Number34 != number34) Number34 = number34.Value;
        if (number35.HasValue && Number35 != number35) Number35 = number35.Value;
        if (number36.HasValue && Number36 != number36) Number36 = number36.Value;
        if (number37.HasValue && Number37 != number37) Number37 = number37.Value;
        if (number38.HasValue && Number38 != number38) Number38 = number38.Value;
        if (number39.HasValue && Number39 != number39) Number39 = number39.Value;
        if (number40.HasValue && Number40 != number40) Number40 = number40.Value;
        if (number41.HasValue && Number41 != number41) Number41 = number41.Value;
        if (number42.HasValue && Number42 != number42) Number42 = number42.Value;
        if (number43.HasValue && Number43 != number43) Number43 = number43.Value;
        if (number44.HasValue && Number44 != number44) Number44 = number44.Value;
        if (number45.HasValue && Number45 != number45) Number45 = number45.Value;
        if (number46.HasValue && Number46 != number46) Number46 = number46.Value;
        if (number47.HasValue && Number47 != number47) Number47 = number47.Value;
        if (number48.HasValue && Number48 != number48) Number48 = number48.Value;
        if (number49.HasValue && Number49 != number49) Number49 = number49.Value;
        if (number50.HasValue && Number50 != number50) Number50 = number50.Value;
        if (number51.HasValue && Number51 != number51) Number51 = number51.Value;
        if (number52.HasValue && Number52 != number52) Number52 = number52.Value;
        if (number53.HasValue && Number53 != number53) Number53 = number53.Value;
        if (number54.HasValue && Number54 != number54) Number54 = number54.Value;
        if (number55.HasValue && Number55 != number55) Number55 = number55.Value;

        if (roundId.HasValue && RoundId != roundId) RoundId = roundId.Value;
        if (subRoundId.HasValue && SubRoundId != subRoundId) SubRoundId = subRoundId.Value;
        return this;
    }

    public VnPowerResult ConvertFromWinNumber(
        int winNumber1,
        int winNumber2,
        int winNumber3,
        int winNumber4,
        int winNumber5,
        int winNumber6,
        int bonusNumber)
    {
        foreach (var prop in typeof(VnPowerResult).GetProperties())
        {
            if (!prop.Name.StartsWith("Number")) continue;
            try
            {
                int val = 0;
                var propertyType = prop.PropertyType;
                if (prop.Name[6..] == winNumber1.ToString() ||
                    prop.Name[6..] == winNumber2.ToString() ||
                    prop.Name[6..] == winNumber3.ToString() ||
                    prop.Name[6..] == winNumber4.ToString() ||
                    prop.Name[6..] == winNumber5.ToString() ||
                    prop.Name[6..] == winNumber6.ToString() ||
                    prop.Name[6..] == bonusNumber.ToString())
                {
                    val = 1;
                }

                object? obj = Convert.ChangeType(val, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                prop.SetValue(this, obj);
            }
            catch
            {
                // if any error
                // return await Task.FromResult(new List<T>());
            }
        }

        return this;
    }
}
