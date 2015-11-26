using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlServer.Server;
using Model;

namespace DataAccess
{
    public class LeagueRepository
    {
        private readonly AlgoTestContext _context;
       
        public LeagueRepository(AlgoTestContext context)
        {
            _context = context;
        }

        public void AddFixture(LeagueData fixture)
        {
            if (
                !_context.LeagueData.Any(x =>
                        x.DateTime == fixture.DateTime
                        && x.HomeTeam == fixture.HomeTeam
                        && x.AwayTeam == fixture.AwayTeam))
            {
                _context.LeagueData.Add(fixture);
            }
        }

        public List<TeamStats> LoadTeam(string team)
        {
            var data = _context.LeagueData.Where(y => y.HomeTeam.Contains(team) || y.AwayTeam.Contains(team)).Select(x => new TeamStats
            {
                Team = team,
                Location = x.HomeTeam.Contains(team) ? "H" : "A",
                Date = x.DateTime,
                GoalsFor = x.HomeTeam.Contains(team) ? x.FullTimeHomeGoals : x.FullTimeAwayGoals,
                GoalsAgainst = x.HomeTeam.Contains(team) ? x.FullTimeAwayGoals : x.FullTimeHomeGoals,
                FTResult = x.FullTimeResult
            }).OrderByDescending(x => x.Date).Take(10);

            return data.ToList();
        }

        public List<TeamStats> LoadHomeTeam(string team)
        {
            var data = _context.LeagueData.Where(y => y.HomeTeam.Contains(team)).Select(x => new TeamStats
            {
                Team = team,
                Location = "H",
                Date = x.DateTime,
                GoalsFor = x.FullTimeHomeGoals,
                GoalsAgainst = x.FullTimeAwayGoals,
                FTResult = x.FullTimeResult
            }).OrderByDescending(x => x.Date).Take(10);

            return data.ToList();
        }

        public List<TeamStats> LoadAwayTeam(string team)
        {
            var data = _context.LeagueData.Where(y =>  y.AwayTeam.Contains(team)).Select(x => new TeamStats
            {
                Team = team,
                Location = "A",
                Date = x.DateTime,
                GoalsFor =  x.FullTimeAwayGoals,
                GoalsAgainst = x.FullTimeHomeGoals,
                FTResult = x.FullTimeResult
            }).OrderByDescending(x => x.Date).Take(10);

            return data.ToList();
        }

        public List<TeamStats> H2H(string home, string away)
        {
            var data =
                _context.LeagueData.Where(x => x.HomeTeam.Contains(home) && x.AwayTeam.Contains(away)).Select(y => new TeamStats
                    {
                        Team = home+ " v "+ away,
                        Location = "n/a",
                        Date = y.DateTime,
                        GoalsFor =  y.FullTimeHomeGoals,
                        GoalsAgainst =  y.FullTimeAwayGoals,
                        FTResult = y.FullTimeResult
                    });

            return data.ToList();
        }

        public List<LeagueShotStats> LeagueShotStatsHome()
        {
            var data =
                _context.LeagueData.Where(x => x.DateTime > new DateTime(2015, 07, 30))
                    .GroupBy(x => x.HomeTeam)
                    .Select(x => new LeagueShotStats
                    {
                        Team = x.Key,
                        Shots = x.Sum(y => y.HomeTeamShots),
                        Goals = x.Sum(y => y.FullTimeHomeGoals),
                        OnTarget = x.Sum(y => y.HomeTeamShotsOnTarget)
                    }).ToList();

            foreach (var a in data)
            {
                a.Ratio = Convert.ToDouble(a.Shots)/Convert.ToDouble(a.Goals);
            }

            return data;
        }

        public List<LeagueShotStats> LeagueShotStatsAway()
        {
            var data =
                _context.LeagueData.Where(x => x.DateTime > new DateTime(2015, 07, 30))
                    .GroupBy(x => x.AwayTeam)
                    .Select(x => new LeagueShotStats
                    {
                        Team = x.Key,
                        Shots = x.Sum(y => y.AwayTeamShots),
                        Goals = x.Sum(y => y.FullTimeAwayGoals),
                        OnTarget = x.Sum(y => y.AwayTeamShotsOnTarget)
                       
                    }).ToList();
            foreach (var a in data)
            {
                a.Ratio = Convert.ToDouble(a.Shots) / Convert.ToDouble(a.Goals);
            }

            return data;
        }

    }

    public class TeamStats
    {
        public string Team { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public string FTResult { get; set; }
    }

    public class LeagueShotStats
    {
        public string Team { get; set; }
        public int Shots { get; set; }
        public int Goals { get; set; }
        public int OnTarget { get; set; }
        public double Ratio { get; set; }
    }
}
