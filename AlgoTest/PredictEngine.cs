using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using DataAccess;
using Model;

namespace AlgoTest
{
    public class PredictEngine
    {
        public static string Predict(string home, string away)
        {
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);

            var overallForm = "";
            var homeForm = "";
            var awayForm = "";

            var overallHome = leagueRepo.LoadTeam(home);
            var overallAway = leagueRepo.LoadTeam(away);
            var hometeam = leagueRepo.LoadHomeTeam(home);
            var awayteam = leagueRepo.LoadAwayTeam(away);
            var h2h = leagueRepo.H2H(home, away);
            var homeShotStatsList = leagueRepo.LeagueShotStatsHome();
            var awayShotStatsList = leagueRepo.LeagueShotStatsAway();

            var homeShotStats = homeShotStatsList.Where(x => x.Team.Contains(home));
            var awayShotStats = awayShotStatsList.Where(x => x.Team.Contains(away));

            #region Home Team (Home Form)

            foreach (var stats in hometeam)
            {
                var r = "";

                switch (stats.FTResult)
                {
                    case "H":
                        r = "W";
                        break;
                    case "A":
                        r = "L";
                        break;
                    default:
                        r = "D";
                        break;
                }

                homeForm = homeForm + r;
            }

            var homeValues = homeForm.ToCharArray();
            var homeTeamFormValue = 0;

            foreach (var val in homeValues)
            {
                if (val.ToString() == "W")
                {
                    homeTeamFormValue = homeTeamFormValue + 3;
                }
                if (val.ToString() == "D")
                {
                    homeTeamFormValue = homeTeamFormValue + 1;
                }
            }


            #endregion

            #region Away Team (Away Form)

            foreach (var stats in awayteam)
            {
                var r = "";
                switch (stats.FTResult)
                {
                    case "A":
                        r = "W";
                        break;
                    case "H":
                        r = "L";
                        break;
                    default:
                        r = "D";
                        break;
                }
                awayForm = awayForm + r;
            }

            var awayValues = awayForm.ToCharArray();
            var awayTeamFormValue = 0;

            foreach (var val in awayValues)
            {
                if (val.ToString() == "W")
                {
                    awayTeamFormValue = awayTeamFormValue + 3;
                }
                if (val.ToString() == "D")
                {
                    awayTeamFormValue = awayTeamFormValue + 1;
                }
            }

            #endregion

            #region Home Team (Overall Form)

            foreach (var stats in overallHome)
            {
                var r = "D";

                if (stats.Location == "H")
                {
                    if (stats.FTResult == "H")
                    {
                        r = "W";
                    }
                    if (stats.FTResult == "A")
                    {
                        r = "L";
                    }
                }
                if (stats.Location == "A")
                {
                    if (stats.FTResult == "H")
                    {
                        r = "L";
                    }
                    if (stats.FTResult == "A")
                    {
                        r = "W";
                    }
                }

                overallForm = overallForm + r;
            }

            var homeOverallValues = overallForm.ToCharArray();
            var homeTeamOverallFormValue = 0;

            foreach (var val in homeOverallValues)
            {
                if (val.ToString() == "W")
                {
                    homeTeamOverallFormValue = homeTeamOverallFormValue + 3;
                }
                if (val.ToString() == "D")
                {
                    homeTeamOverallFormValue = homeTeamOverallFormValue + 1;
                }
            }

            #endregion

            #region Away Team (Overall Form)

            overallForm = "";
            foreach (var stats in overallAway)
            {
                var r = "D";

                if (stats.Location == "H")
                {
                    if (stats.FTResult == "H")
                    {
                        r = "W";
                    }
                    if (stats.FTResult == "A")
                    {
                        r = "L";
                    }
                }
                if (stats.Location == "A")
                {
                    if (stats.FTResult == "H")
                    {
                        r = "L";
                    }
                    if (stats.FTResult == "A")
                    {
                        r = "W";
                    }
                }

               
                overallForm = overallForm + r;
            }

            var awayOverallValues = overallForm.ToCharArray();
            var awayTeamOverallFormValue = 0;

            foreach (var val in awayOverallValues)
            {
                if (val.ToString() == "W")
                {
                    awayTeamOverallFormValue = awayTeamOverallFormValue + 3;
                }
                if (val.ToString() == "D")
                {
                    awayTeamOverallFormValue = awayTeamOverallFormValue + 1;
                }
            }

            #endregion

            #region HeadToHead

            var homeh2hTotal = 0.0;
            var awayh2hTotal = 0.0;

            foreach (var val in h2h)
            {
                if (val.GoalsFor > val.GoalsAgainst)
                {
                    homeh2hTotal = homeh2hTotal + 3;
                }
                else if (val.GoalsFor == val.GoalsAgainst)
                {
                    homeh2hTotal = homeh2hTotal + 1;
                    awayh2hTotal = awayh2hTotal + 1;
                }
                else
                {
                    awayh2hTotal = awayh2hTotal + 3;
                }
            }

            #endregion

            #region HomeTeam (Goals Scored/Conceded)

            var homeGoalsFor = overallHome.Sum(x => x.GoalsFor);
            var homeGoalsAgainst = overallHome.Sum(x => x.GoalsAgainst);

            #endregion

            #region AwayTeam (Goals Scored/Conceded)

            var awayGoalsFor = overallAway.Sum(x => x.GoalsFor);
            var awayGoalsAgainst = overallAway.Sum(x => x.GoalsAgainst);

            #endregion

            #region Shot stats and conversion rates (Home)



            #endregion

            #region Shot stats and conversion rates (Home)

            #endregion

            #region Calculations

            var homeTotal = 0.0;
            var awayTotal = 0.0;
            var homePct = 0.0;
            var awayPct = 0.0;
            var homeGoalValue = 0.0;
            var awayGoalValue = 0.0;
            var resultString = "";

           if (homeGoalsFor > awayGoalsFor)
            {
                homeGoalValue = homeGoalValue + 2;
            }
            else if (homeGoalsFor == awayGoalsFor)
            {
                homeGoalValue = homeGoalValue + 1;
                awayGoalValue = awayGoalValue + 1;
            }
            else
            {
                awayGoalValue = awayGoalValue + 2;
            }

            if (homeGoalsFor > homeGoalsAgainst)
            {
                homeGoalValue = homeGoalValue + 1;
            }

            if (awayGoalsFor > awayGoalsAgainst)
            {
                awayGoalValue = awayGoalValue + 1;
            }

            var h2hMax = (h2h.Count*3);
            if (h2h.Count > 0)
            {
                homeh2hTotal = (homeh2hTotal/h2hMax)*10;
                awayh2hTotal = (awayh2hTotal/h2hMax)*10;
            }
            else
            {
                homeh2hTotal = 0;
                awayh2hTotal = 0;
            }
           

            homeTotal = (Convert.ToDouble(homeTeamFormValue + homeTeamOverallFormValue) / 3) + (homeGoalValue * 2) + (homeh2hTotal *0.75);
            awayTotal = (Convert.ToDouble(awayTeamFormValue + awayTeamOverallFormValue) / 3) + (awayGoalValue * 2) + (awayh2hTotal * 0.75);

            homePct = (homeTotal / (homeTotal + awayTotal)) * 100;
            awayPct = (awayTotal / (homeTotal + awayTotal)) * 100;

            if (homePct >= 54.0)
            {
                resultString = "Home Win";
            }
            else if (awayPct >= 54.0)
            {
                resultString = "Away Win";
            }
            else
            {
                resultString = "Draw";
            }

            #endregion

            var result = string.Format("{0} ({1}%) vs {2} ({3}%): {4}", home, Math.Round(homePct, 2), away, Math.Round(awayPct, 2), resultString);

            return result;
        }
    }
}
