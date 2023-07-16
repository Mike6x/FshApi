using FSH.WebApi.Domain.Game;

namespace FSH.WebApi.Application.Game.VnPowers;
public class LotteryHelper
{
    public static VnPowerForcast PowerForcast(List<VnPowerResult> list)
    {
        var vnPowerForcast = new VnPowerForcast();
        int n = list.Count + 2;
        for (int i = 0; i < list.Count; i++)
        {
            // k = i == n - 2 ? 1 : (n + 1) / n;
            // k = i == n - 3 ? 1 : ((2 * n) + 1) / (2 * n);
            // decimal k = 1 + (1.0M / (n * (2 ^ (list.Count - i))));

            double v = n * Math.Pow(2, list.Count - i);

            decimal k = (decimal)(1 + (1 / v));

            vnPowerForcast.Number1 += k * list[i].Number1;
            vnPowerForcast.Number2 += k * list[i].Number2;
            vnPowerForcast.Number3 += k * list[i].Number3;
            vnPowerForcast.Number4 += k * list[i].Number4;
            vnPowerForcast.Number5 += k * list[i].Number5;
            vnPowerForcast.Number6 += k * list[i].Number6;
            vnPowerForcast.Number7 += k * list[i].Number7;
            vnPowerForcast.Number8 += k * list[i].Number8;
            vnPowerForcast.Number9 += k * list[i].Number9;
            vnPowerForcast.Number10 += k * list[i].Number10;
            vnPowerForcast.Number11 += k * list[i].Number11;
            vnPowerForcast.Number12 += k * list[i].Number12;
            vnPowerForcast.Number13 += k * list[i].Number13;
            vnPowerForcast.Number14 += k * list[i].Number14;
            vnPowerForcast.Number15 += k * list[i].Number15;
            vnPowerForcast.Number16 += k * list[i].Number16;
            vnPowerForcast.Number17 += k * list[i].Number17;
            vnPowerForcast.Number18 += k * list[i].Number18;
            vnPowerForcast.Number19 += k * list[i].Number19;
            vnPowerForcast.Number20 += k * list[i].Number20;
            vnPowerForcast.Number21 += k * list[i].Number21;
            vnPowerForcast.Number22 += k * list[i].Number22;
            vnPowerForcast.Number23 += k * list[i].Number23;
            vnPowerForcast.Number24 += k * list[i].Number24;
            vnPowerForcast.Number25 += k * list[i].Number25;
            vnPowerForcast.Number26 += k * list[i].Number26;
            vnPowerForcast.Number27 += k * list[i].Number27;
            vnPowerForcast.Number28 += k * list[i].Number28;
            vnPowerForcast.Number29 += k * list[i].Number29;
            vnPowerForcast.Number30 += k * list[i].Number30;
            vnPowerForcast.Number31 += k * list[i].Number31;
            vnPowerForcast.Number32 += k * list[i].Number32;
            vnPowerForcast.Number33 += k * list[i].Number33;
            vnPowerForcast.Number34 += k * list[i].Number34;
            vnPowerForcast.Number35 += k * list[i].Number35;
            vnPowerForcast.Number36 += k * list[i].Number36;
            vnPowerForcast.Number37 += k * list[i].Number37;
            vnPowerForcast.Number38 += k * list[i].Number38;
            vnPowerForcast.Number39 += k * list[i].Number39;
            vnPowerForcast.Number40 += k * list[i].Number40;
            vnPowerForcast.Number41 += k * list[i].Number41;
            vnPowerForcast.Number42 += k * list[i].Number42;
            vnPowerForcast.Number43 += k * list[i].Number43;
            vnPowerForcast.Number44 += k * list[i].Number44;
            vnPowerForcast.Number45 += k * list[i].Number45;
            vnPowerForcast.Number46 += k * list[i].Number46;
            vnPowerForcast.Number47 += k * list[i].Number47;
            vnPowerForcast.Number48 += k * list[i].Number48;
            vnPowerForcast.Number49 += k * list[i].Number49;
            vnPowerForcast.Number50 += k * list[i].Number50;
            vnPowerForcast.Number51 += k * list[i].Number51;
            vnPowerForcast.Number52 += k * list[i].Number52;
            vnPowerForcast.Number53 += k * list[i].Number53;
            vnPowerForcast.Number54 += k * list[i].Number54;
            vnPowerForcast.Number55 += k * list[i].Number55;
        }

        vnPowerForcast.Number1 = 100.0M * (1 + list.Count - vnPowerForcast.Number1) / n;
        vnPowerForcast.Number2 = 100.0M * (1 + list.Count - vnPowerForcast.Number2) / n;
        vnPowerForcast.Number3 = 100.0M * (1 + list.Count - vnPowerForcast.Number3) / n;
        vnPowerForcast.Number4 = 100.0M * (1 + list.Count - vnPowerForcast.Number4) / n;
        vnPowerForcast.Number5 = 100.0M * (1 + list.Count - vnPowerForcast.Number5) / n;
        vnPowerForcast.Number6 = 100.0M * (1 + list.Count - vnPowerForcast.Number6) / n;
        vnPowerForcast.Number7 = 100.0M * (1 + list.Count - vnPowerForcast.Number7) / n;
        vnPowerForcast.Number8 = 100.0M * (1 + list.Count - vnPowerForcast.Number8) / n;
        vnPowerForcast.Number9 = 100.0M * (1 + list.Count - vnPowerForcast.Number9) / n;
        vnPowerForcast.Number10 = 100.0M * (1 + list.Count - vnPowerForcast.Number10) / n;
        vnPowerForcast.Number11 = 100.0M * (1 + list.Count - vnPowerForcast.Number11) / n;
        vnPowerForcast.Number12 = 100.0M * (1 + list.Count - vnPowerForcast.Number12) / n;
        vnPowerForcast.Number13 = 100.0M * (1 + list.Count - vnPowerForcast.Number13) / n;
        vnPowerForcast.Number14 = 100.0M * (1 + list.Count - vnPowerForcast.Number14) / n;
        vnPowerForcast.Number15 = 100.0M * (1 + list.Count - vnPowerForcast.Number15) / n;
        vnPowerForcast.Number16 = 100.0M * (1 + list.Count - vnPowerForcast.Number16) / n;
        vnPowerForcast.Number17 = 100.0M * (1 + list.Count - vnPowerForcast.Number17) / n;
        vnPowerForcast.Number18 = 100.0M * (1 + list.Count - vnPowerForcast.Number18) / n;
        vnPowerForcast.Number19 = 100.0M * (1 + list.Count - vnPowerForcast.Number19) / n;
        vnPowerForcast.Number20 = 100.0M * (1 + list.Count - vnPowerForcast.Number20) / n;
        vnPowerForcast.Number21 = 100.0M * (1 + list.Count - vnPowerForcast.Number21) / n;
        vnPowerForcast.Number22 = 100.0M * (1 + list.Count - vnPowerForcast.Number22) / n;
        vnPowerForcast.Number23 = 100.0M * (1 + list.Count - vnPowerForcast.Number23) / n;
        vnPowerForcast.Number24 = 100.0M * (1 + list.Count - vnPowerForcast.Number24) / n;
        vnPowerForcast.Number25 = 100.0M * (1 + list.Count - vnPowerForcast.Number25) / n;
        vnPowerForcast.Number26 = 100.0M * (1 + list.Count - vnPowerForcast.Number26) / n;
        vnPowerForcast.Number27 = 100.0M * (1 + list.Count - vnPowerForcast.Number27) / n;
        vnPowerForcast.Number28 = 100.0M * (1 + list.Count - vnPowerForcast.Number28) / n;
        vnPowerForcast.Number29 = 100.0M * (1 + list.Count - vnPowerForcast.Number29) / n;
        vnPowerForcast.Number30 = 100.0M * (1 + list.Count - vnPowerForcast.Number30) / n;
        vnPowerForcast.Number31 = 100.0M * (1 + list.Count - vnPowerForcast.Number31) / n;
        vnPowerForcast.Number32 = 100.0M * (1 + list.Count - vnPowerForcast.Number32) / n;
        vnPowerForcast.Number33 = 100.0M * (1 + list.Count - vnPowerForcast.Number33) / n;
        vnPowerForcast.Number34 = 100.0M * (1 + list.Count - vnPowerForcast.Number34) / n;
        vnPowerForcast.Number35 = 100.0M * (1 + list.Count - vnPowerForcast.Number35) / n;
        vnPowerForcast.Number36 = 100.0M * (1 + list.Count - vnPowerForcast.Number36) / n;
        vnPowerForcast.Number37 = 100.0M * (1 + list.Count - vnPowerForcast.Number37) / n;
        vnPowerForcast.Number38 = 100.0M * (1 + list.Count - vnPowerForcast.Number38) / n;
        vnPowerForcast.Number39 = 100.0M * (1 + list.Count - vnPowerForcast.Number39) / n;
        vnPowerForcast.Number40 = 100.0M * (1 + list.Count - vnPowerForcast.Number40) / n;
        vnPowerForcast.Number41 = 100.0M * (1 + list.Count - vnPowerForcast.Number41) / n;
        vnPowerForcast.Number42 = 100.0M * (1 + list.Count - vnPowerForcast.Number42) / n;
        vnPowerForcast.Number43 = 100.0M * (1 + list.Count - vnPowerForcast.Number43) / n;
        vnPowerForcast.Number44 = 100.0M * (1 + list.Count - vnPowerForcast.Number44) / n;
        vnPowerForcast.Number45 = 100.0M * (1 + list.Count - vnPowerForcast.Number45) / n;
        vnPowerForcast.Number46 = 100.0M * (1 + list.Count - vnPowerForcast.Number46) / n;
        vnPowerForcast.Number47 = 100.0M * (1 + list.Count - vnPowerForcast.Number47) / n;
        vnPowerForcast.Number48 = 100.0M * (1 + list.Count - vnPowerForcast.Number48) / n;
        vnPowerForcast.Number49 = 100.0M * (1 + list.Count - vnPowerForcast.Number49) / n;
        vnPowerForcast.Number50 = 100.0M * (1 + list.Count - vnPowerForcast.Number50) / n;
        vnPowerForcast.Number51 = 100.0M * (1 + list.Count - vnPowerForcast.Number51) / n;
        vnPowerForcast.Number52 = 100.0M * (1 + list.Count - vnPowerForcast.Number52) / n;
        vnPowerForcast.Number53 = 100.0M * (1 + list.Count - vnPowerForcast.Number53) / n;
        vnPowerForcast.Number54 = 100.0M * (1 + list.Count - vnPowerForcast.Number54) / n;
        vnPowerForcast.Number55 = 100.0M * (1 + list.Count - vnPowerForcast.Number55) / n;

        return vnPowerForcast;
    }
}
