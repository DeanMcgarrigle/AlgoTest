﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Xml.Schema;
using DataAccess;
using Model;

namespace AlgoTest
{
    public class PredictEngine
    {
        public static predictObject Predict(string home, string away)
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

            var league = overallHome[0].League;
            var shotStatsHome = Program.homeShots;
            var shotStatsAway = Program.awayShots;

            var homeShotStatsListO = shotStatsHome.OrderBy(x => x.OffensiveRatio).ThenBy(x => x.Shots).ToList();
            var homeShotStatsListD = shotStatsHome.OrderByDescending(x => x.DefensiveRatio).ToList();
            var awayShotStatsListO = shotStatsAway.OrderBy(x => x.OffensiveRatio).ToList();
            var awayShotStatsListD = shotStatsAway.OrderByDescending(x => x.DefensiveRatio).ThenBy(x => x.Shots).ToList();
            var homeGoalsScored = shotStatsHome.OrderByDescending(x => x.Goals).ThenByDescending(x => x.Shots).ToList();
            var homeGoalsConceded = shotStatsHome.OrderBy(x => x.GoalsConceded).ThenBy(x => x.ShotsFaced).ToList();
            var awayGoalsScored = shotStatsAway.OrderByDescending(x => x.Goals).ThenByDescending(x => x.Shots).ToList();
            var awayGoalsConceded = shotStatsAway.OrderBy(x => x.GoalsConceded).ThenBy(x => x.ShotsFaced).ToList();

            //var homeShotStats = homeShotStatsList.FirstOrDefault(x => x.Team.ToLower().Contains(home.ToLower()));
            //var awayShotStats = awayShotStatsList.FirstOrDefault(x => x.Team.ToLower().Contains(away.ToLower()));

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

            var homeFor = 0;
            var homeAgainst = 0;
            var normalisedHomeFor = 10;
            var normalisedHomeAgainst = 10;

            for (int i = 0; i < homeGoalsScored.Count(); i++)
            {
                if (homeGoalsScored[i].Team.ToLower().Contains(home.ToLower()))
                {
                    homeFor = normalisedHomeFor;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedHomeFor = normalisedHomeFor - 1;
                }
            }

            for (int i = 0; i < homeGoalsConceded.Count(); i++)
            {
                if (homeGoalsConceded[i].Team.ToLower().Contains(home.ToLower()))
                {
                    homeAgainst = normalisedHomeAgainst;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedHomeAgainst = normalisedHomeAgainst - 1;
                }
            }

            #endregion

            #region AwayTeam (Goals Scored/Conceded)

            var awayGoalsFor = overallAway.Sum(x => x.GoalsFor);
            var awayGoalsAgainst = overallAway.Sum(x => x.GoalsAgainst);


            var awayFor = 0;
            var awayAgainst = 0;
            var normalisedAwayFor = 10;
            var normalisedAwayAgainst = 10;

            for (int i = 0; i < awayGoalsScored.Count(); i++)
            {
                if (awayGoalsScored[i].Team.ToLower().Contains(home.ToLower()))
                {
                    awayFor = normalisedAwayFor;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedAwayFor = normalisedAwayFor - 1;
                }
            }

            for (int i = 0; i < awayGoalsConceded.Count(); i++)
            {
                if (awayGoalsConceded[i].Team.ToLower().Contains(home.ToLower()))
                {
                    awayAgainst = normalisedAwayAgainst;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedAwayAgainst = normalisedAwayAgainst - 1;
                }
            }

            #endregion

            #region Shot stats and conversion rates (Home)

            var offensiveRankH = 0;
            var defensiveRankH = 0;
            var normalisedOffensiveHome = 10;
            var normalisedDefensiveHome = 10;

            for (int i = 0; i < homeShotStatsListO.Count(); i++)
            {
                if (homeShotStatsListO[i].Team.ToLower().Contains(home.ToLower()))
                {
                    offensiveRankH = normalisedOffensiveHome;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedOffensiveHome = normalisedOffensiveHome - 1;
                }
            }

            for (int i = 0; i < homeShotStatsListD.Count(); i++)
            {
                if (homeShotStatsListD[i].Team.ToLower().Contains(home.ToLower()))
                {
                    defensiveRankH = normalisedDefensiveHome;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedDefensiveHome = normalisedDefensiveHome - 1;
                }
            }

            #endregion

            #region Shot stats and conversion rates (Away)

            var offensiveRankA = 0;
            var defensiveRankA = 0;
            var normalisedOffensiveAway = 10;
            var normalisedDefensiveAway = 10;

            for (int i = 0; i < awayShotStatsListO.Count(); i++)
            {
                if (awayShotStatsListO[i].Team.ToLower().Contains(away.ToLower()))
                {
                    offensiveRankA = normalisedOffensiveAway;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedOffensiveAway = normalisedOffensiveAway - 1;
                }
            }

            for (int i = 0; i < awayShotStatsListD.Count(); i++)
            {
                if (awayShotStatsListD[i].Team.ToLower().Contains(away.ToLower()))
                {
                    defensiveRankA = normalisedDefensiveAway;
                }
                if ((i + 1) % 2 == 0)
                {
                    normalisedDefensiveAway = normalisedDefensiveAway - 1;
                }
            }

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

            var h2hMax = (h2h.Count * 3);
            if (h2h.Count > 0)
            {
                homeh2hTotal = (homeh2hTotal / h2hMax) * 10;
                awayh2hTotal = (awayh2hTotal / h2hMax) * 10;
            }
            else
            {
                homeh2hTotal = 0;
                awayh2hTotal = 0;
            }

            var homeTeamStatScore = (Convert.ToDouble(offensiveRankH + defensiveRankH)) / 2;
            var awayTeamStatScore = (Convert.ToDouble(offensiveRankA + defensiveRankA)) / 2;

            var goalStatsHomeScore = (Convert.ToDouble(homeFor + homeAgainst))/2;
            var goalStatsAwayScore = (Convert.ToDouble(awayFor + awayAgainst))/2;


            // 71% //

            //homeTotal = (Convert.ToDouble(homeTeamFormValue + (homeTeamOverallFormValue)) / 3) + (homeGoalValue * 2) + (homeh2hTotal * 0.75) + (homeTeamStatScore * 0.20);
            //awayTotal = (Convert.ToDouble(awayTeamFormValue + (awayTeamOverallFormValue)) / 3) + (awayGoalValue * 2) + (awayh2hTotal * 0.75) + (awayTeamStatScore * 0.17);

            // 72%! // 

            //homeTotal = ((Convert.ToDouble(homeTeamFormValue + (homeTeamOverallFormValue)) / 3) * 1) + ((homeGoalValue * 2) * 0.1) + (goalStatsHomeScore * 0.51) + (homeh2hTotal * 0.9) + (homeTeamStatScore * 0.20);
            //awayTotal = ((Convert.ToDouble(awayTeamFormValue + (awayTeamOverallFormValue)) / 3) * 1) + ((awayGoalValue * 2) * 0.1) + (goalStatsAwayScore * 0.5) + (awayh2hTotal * 0.7535) + (awayTeamStatScore * 0.18);

            // Testing // 

            homeTotal = ((Convert.ToDouble(homeTeamFormValue + (homeTeamOverallFormValue)) / 3) * 1) + ((homeGoalValue * 2) * 0.1) + (goalStatsHomeScore * 0.51) + (homeh2hTotal * 0.9) + (homeTeamStatScore * 0.20);
            awayTotal = ((Convert.ToDouble(awayTeamFormValue + (awayTeamOverallFormValue)) / 3) * 1) + ((awayGoalValue * 2) * 0.1) + (goalStatsAwayScore * 0.5) + (awayh2hTotal * 0.7535) + (awayTeamStatScore * 0.19);

            homePct = (homeTotal / (homeTotal + awayTotal)) * 100;
            awayPct = (awayTotal / (homeTotal + awayTotal)) * 100;

            if (homePct >= 53.4)
            {
                resultString = "Home Win";
            }
            else if (awayPct >= 53.4)
            {
                resultString = "Away Win";
            }
            else
            {
                resultString = "Draw";
            }

            #endregion

            var result = string.Format("{0} ({1}%) vs {2} ({3}%): {4}", home, Math.Round(homePct, 2), away, Math.Round(awayPct, 2), resultString);

            var returnObject = new predictObject
            {
                predictedString = result,
                predictedValue = resultString[0].ToString()
            };

            return returnObject;
        }

        public class predictObject
        {
            public string predictedString { get; set; }
            public string predictedValue { get; set; }
        }
    }
}
