using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Model;

namespace DataAccess
{
    public class LeagueRepository
    {
        private readonly AlgoTestContext _context;

         List<MetricData> list  = new List<MetricData>();

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
        
        public void AddTeam(LeagueData team)
        {
            _context.MetricData.AddOrUpdate(x => x.Team, new MetricData
            {
                Team = team.HomeTeam,
                League = team.League
            });
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
}
