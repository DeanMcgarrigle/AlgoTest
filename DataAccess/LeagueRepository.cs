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
