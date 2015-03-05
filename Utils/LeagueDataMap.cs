using CsvHelper.Configuration;
using Model;

namespace Utils
{
    public sealed class LeagueDataMap : CsvClassMap<LeagueData>
    {
        public LeagueDataMap()
        {
            Map(m => m.League).Name("Div");
            Map(m => m.DateTime).Name("Date");
            Map(m => m.HomeTeam).Name("HomeTeam");
            Map(m => m.AwayTeam).Name("AwayTeam");
            Map(m => m.FullTimeHomeGoals).Name("FTHG");
            Map(m => m.FullTimeAwayGoals).Name("FTAG");
            Map(m => m.FullTimeResult).Name("FTR");
            Map(m => m.HalfTimeHomeGoals).Name("HTHG");
            Map(m => m.HalfTimeAwayGoals).Name("HTAG");
            Map(m => m.HalfTimeResult).Name("HTR");
            Map(m => m.Referee).Name("Referee");
            Map(m => m.HomeTeamShots).Name("HS");
            Map(m => m.AwayTeamShots).Name("AS");
            Map(m => m.HomeTeamShotsOnTarget).Name("HST");
            Map(m => m.AwayTeamShotsOnTarget).Name("AST");
            Map(m => m.HomeTeamFoulsCommited).Name("HF");
            Map(m => m.AwayTeamFoulsCommited).Name("AF");
            Map(m => m.HomeTeamCorners).Name("HC");
            Map(m => m.AwayTeamCorners).Name("AC");
            Map(m => m.HomeTeamYellowCards).Name("HY");
            Map(m => m.AwayTeamYellowCards).Name("AY");
            Map(m => m.HomeTeamRedCards).Name("HR");
            Map(m => m.AwayTeamRedCards).Name("AR");
        }
    }
}
