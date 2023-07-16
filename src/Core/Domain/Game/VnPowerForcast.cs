using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Game;
public class VnPowerForcast(
    decimal number1,
    decimal number2,
    decimal number3,
    decimal number4,
    decimal number5,
    decimal number6,
    decimal number7,
    decimal number8,
    decimal number9,
    decimal number10,
    decimal number11,
    decimal number12,
    decimal number13,
    decimal number14,
    decimal number15,
    decimal number16,
    decimal number17,
    decimal number18,
    decimal number19,
    decimal number20,
    decimal number21,
    decimal number22,
    decimal number23,
    decimal number24,
    decimal number25,
    decimal number26,
    decimal number27,
    decimal number28,
    decimal number29,
    decimal number30,
    decimal number31,
    decimal number32,
    decimal number33,
    decimal number34,
    decimal number35,
    decimal number36,
    decimal number37,
    decimal number38,
    decimal number39,
    decimal number40,
    decimal number41,
    decimal number42,
    decimal number43,
    decimal number44,
    decimal number45,
    decimal number46,
    decimal number47,
    decimal number48,
    decimal number49,
    decimal number50,
    decimal number51,
    decimal number52,
    decimal number53,
    decimal number54,
    decimal number55) : IAggregateRoot
{
    [ForeignKey("VnPower")]
    public Guid Id { get; set; }

    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = [];

    public decimal Number1 { get; set; } = number1;
    public decimal Number2 { get; set; } = number2;
    public decimal Number3 { get; set; } = number3;
    public decimal Number4 { get; set; } = number4;
    public decimal Number5 { get; set; } = number5;
    public decimal Number6 { get; set; } = number6;
    public decimal Number7 { get; set; } = number7;
    public decimal Number8 { get; set; } = number8;
    public decimal Number9 { get; set; } = number9;

    public decimal Number10 { get; set; } = number10;
    public decimal Number11 { get; set; } = number11;
    public decimal Number12 { get; set; } = number12;
    public decimal Number13 { get; set; } = number13;
    public decimal Number14 { get; set; } = number14;
    public decimal Number15 { get; set; } = number15;
    public decimal Number16 { get; set; } = number16;
    public decimal Number17 { get; set; } = number17;
    public decimal Number18 { get; set; } = number18;
    public decimal Number19 { get; set; } = number19;

    public decimal Number20 { get; set; } = number20;
    public decimal Number21 { get; set; } = number21;
    public decimal Number22 { get; set; } = number22;
    public decimal Number23 { get; set; } = number23;
    public decimal Number24 { get; set; } = number24;
    public decimal Number25 { get; set; } = number25;
    public decimal Number26 { get; set; } = number26;
    public decimal Number27 { get; set; } = number27;
    public decimal Number28 { get; set; } = number28;
    public decimal Number29 { get; set; } = number29;

    public decimal Number30 { get; set; } = number30;
    public decimal Number31 { get; set; } = number31;
    public decimal Number32 { get; set; } = number32;
    public decimal Number33 { get; set; } = number33;
    public decimal Number34 { get; set; } = number34;
    public decimal Number35 { get; set; } = number35;
    public decimal Number36 { get; set; } = number36;
    public decimal Number37 { get; set; } = number37;
    public decimal Number38 { get; set; } = number38;
    public decimal Number39 { get; set; } = number39;

    public decimal Number40 { get; set; } = number40;
    public decimal Number41 { get; set; } = number41;
    public decimal Number42 { get; set; } = number42;
    public decimal Number43 { get; set; } = number43;
    public decimal Number44 { get; set; } = number44;
    public decimal Number45 { get; set; } = number45;
    public decimal Number46 { get; set; } = number46;
    public decimal Number47 { get; set; } = number47;
    public decimal Number48 { get; set; } = number48;
    public decimal Number49 { get; set; } = number49;

    public decimal Number50 { get; set; } = number50;
    public decimal Number51 { get; set; } = number51;
    public decimal Number52 { get; set; } = number52;
    public decimal Number53 { get; set; } = number53;
    public decimal Number54 { get; set; } = number54;
    public decimal Number55 { get; set; } = number55;

    public virtual VnPower VnPower { get; set; } = default!;

    public VnPowerForcast()
        : this(
              0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
    {
    }

    public VnPowerForcast Update(
        decimal? number1,
        decimal? number2,
        decimal? number3,
        decimal? number4,
        decimal? number5,
        decimal? number6,
        decimal? number7,
        decimal? number8,
        decimal? number9,
        decimal? number10,
        decimal? number11,
        decimal? number12,
        decimal? number13,
        decimal? number14,
        decimal? number15,
        decimal? number16,
        decimal? number17,
        decimal? number18,
        decimal? number19,
        decimal? number20,
        decimal? number21,
        decimal? number22,
        decimal? number23,
        decimal? number24,
        decimal? number25,
        decimal? number26,
        decimal? number27,
        decimal? number28,
        decimal? number29,
        decimal? number30,
        decimal? number31,
        decimal? number32,
        decimal? number33,
        decimal? number34,
        decimal? number35,
        decimal? number36,
        decimal? number37,
        decimal? number38,
        decimal? number39,
        decimal? number40,
        decimal? number41,
        decimal? number42,
        decimal? number43,
        decimal? number44,
        decimal? number45,
        decimal? number46,
        decimal? number47,
        decimal? number48,
        decimal? number49,
        decimal? number50,
        decimal? number51,
        decimal? number52,
        decimal? number53,
        decimal? number54,
        decimal? number55)
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

        return this;
    }
}
