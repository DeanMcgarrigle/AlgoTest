using System;
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

            #endregion

            #region HomeTeam (Goals Scored)

            #endregion

            #region AwayTeam (Goals Scored)

            #endregion

            #region Stadium & Attendance

            #endregion

            #region Calculations

            var homeTotal = 0.0;
            var awayTotal = 0.0;
            var homePct = 0.0;
            var awayPct = 0.0;
            var resultString = "";

            homeTotal = Convert.ToDouble(homeTeamFormValue + homeTeamOverallFormValue);
            awayTotal = Convert.ToDouble(awayTeamFormValue + awayTeamOverallFormValue);

            homePct = (homeTotal / (homeTotal + awayTotal)) * 100;
            awayPct = (awayTotal / (homeTotal + awayTotal)) * 100;

            if (homePct >= 55.0)
            {
                resultString = "Home Win";
            }
            else if (awayPct >= 55.0)
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
