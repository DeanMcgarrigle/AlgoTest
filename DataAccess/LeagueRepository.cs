using System;
using System.Collections.Generic;
using System.Linq;
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

        public int GetHomeTeamScore(string team, DateTime startDate)
        {
            var league = _context.LeagueData.Where(y => y.DateTime >= startDate)
                .FirstOrDefault(x => x.HomeTeam == team)
                .League;
            var homeTeamGoals =
                _context.LeagueData.Where(x => x.DateTime >= startDate)
                    .Where(
                        x =>
                            x.League == league
                           )
                    .GroupBy(x => x.HomeTeam)
                    .Select(x => new
                    {
                        Team = x.Key,
                        Goals = x.Sum(y => y.FullTimeHomeGoals)
                    })
                    .OrderByDescending(x => x.Goals)
                    .ToList();

            var mostGoals = homeTeamGoals.Max(y => y.Goals);
            var leastGoals = homeTeamGoals.Min(y => y.Goals);
            var difference = (mostGoals - leastGoals) / 9;
            var currentGoals = mostGoals;

            var homeGoalsAgg = Enumerable.Range(1, 10)
                .Select((x, i) => i == 0 ? currentGoals : currentGoals -= difference)
                .OrderBy(x => x)
                .ToList();

            var closest = homeGoalsAgg
                .Select(n => new { n, Distance = Math.Abs(n - homeTeamGoals.FirstOrDefault(x => x.Team == team).Goals) })
                .OrderBy(x => x.Distance)
                .First()
                .n;

            return homeGoalsAgg.IndexOf(closest) + 1;
        }

        public int GetAwayTeamScore(string team, DateTime startDate)
        {
            var league = _context.LeagueData.Where(y => y.DateTime >= startDate)
                            .FirstOrDefault(x => x.AwayTeam == team)
                            .League;
            var awayTeamGoals =
                _context.LeagueData.Where(x => x.DateTime >= startDate)
                    .Where(x => x.League == league)
                    .GroupBy(x => x.AwayTeam)
                    .Select(x => new
                    {
                        Team = x.Key,
                        Goals = x.Sum(y => y.FullTimeAwayGoals)
                    })
                    .OrderByDescending(x => x.Goals)
                    .ToList();

            var mostGoals = awayTeamGoals.Max(y => y.Goals);
            var leastGoals = awayTeamGoals.Min(y => y.Goals);
            var difference = (mostGoals - leastGoals) / 9;
            var currentGoals = mostGoals;

            var awayGoalsAgg = Enumerable.Range(1, 10)
                .Select((x, i) => i == 0 ? currentGoals : currentGoals -= difference)
                .OrderBy(x => x)
                .ToList();

            var closest = awayGoalsAgg
                .Select(n => new { n, Distance = Math.Abs(n - awayTeamGoals.FirstOrDefault(x => x.Team == team).Goals) })
                .OrderBy(x => x.Distance)
                .First()
                .n;

            return awayGoalsAgg.IndexOf(closest) + 1;
        }

        public Result GetLastSixHomeResults(string team)
        {
            return _context.LeagueData.OrderByDescending(x => x.DateTime)
                .Where(x => x.HomeTeam == team)
                .Take(6)
                .GroupBy(x => x.HomeTeam)
                .Select(x => new Result
                {
                    Wins = x.Count(y => y.FullTimeResult == "H"),
                    Draws = x.Count(y => y.FullTimeResult == "D"),
                    Losses = x.Count(y => y.FullTimeResult == "A"),
                    Points = x.Sum(y => y.FullTimeResult == "H" ? 3 : y.FullTimeResult == "D" ? 1 : 0),
                    AvgGoalsScoredPerGame = x.Average(y => y.FullTimeHomeGoals),
                    AvgGoalsConcededPerGame = x.Average(y => y.FullTimeAwayGoals)
                })
                .FirstOrDefault();
        }
    }
}
