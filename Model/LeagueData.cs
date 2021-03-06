﻿using System;

namespace Model
{
    public class LeagueData
    {
        public string League { get; set; }
        public DateTime DateTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int FullTimeHomeGoals { get; set; }
        public int FullTimeAwayGoals { get; set; }
        public string FullTimeResult { get; set; }
        public int HalfTimeHomeGoals { get; set; }
        public int HalfTimeAwayGoals { get; set; }
        public string HalfTimeResult { get; set; }
        public string Referee { get; set; }
        public int HomeTeamShots { get; set; }
        public int AwayTeamShots { get; set; }
        public int HomeTeamShotsOnTarget { get; set; }
        public int AwayTeamShotsOnTarget { get; set; }
        public int HomeTeamFoulsCommited { get; set; }
        public int AwayTeamFoulsCommited { get; set; }
        public int HomeTeamCorners { get; set; }
        public int AwayTeamCorners { get; set; }
        public int HomeTeamYellowCards { get; set; }
        public int AwayTeamYellowCards { get; set; }
        public int HomeTeamRedCards { get; set; }
        public int AwayTeamRedCards { get; set; }
    }
}
