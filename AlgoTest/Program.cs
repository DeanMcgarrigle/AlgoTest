using System;
using System.Linq;
using DataAccess;
using Model;
using Utils;

namespace AlgoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string directory = @"C:\Users\dean.mcgarrigle\Dropbox\Documents\FootballData";
            const string season = "1415";
            var leagues = new[] {"E0", "E1", "E2", "EC" };
            var fileUrl = string.Format("http://www.football-data.co.uk/mmz4281/{0}", season);
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);
            var csvHandler = new CsvHandler<LeagueData, LeagueDataMap>();

            var lastSixHomeGamesForArsenal = leagueRepo.GetLastSixResults("Arsenal", LeagueRepository.FixtureType.Away);
            Console.WriteLine("Arsenal:");
            Console.WriteLine("Wins: {0}, Draws: {1}, Losses: {2}, Points: {3}, Average Goals Scored per Game: {4}, Average Goals Conceded per Game: {5}", 
                lastSixHomeGamesForArsenal.Wins, 
                lastSixHomeGamesForArsenal.Draws, 
                lastSixHomeGamesForArsenal.Losses, 
                lastSixHomeGamesForArsenal.Points,
                lastSixHomeGamesForArsenal.AvgGoalsScoredPerGame,
                lastSixHomeGamesForArsenal.AvgGoalsConcededPerGame);

            var seasonStart = new DateTime(2014, 08, 08);

            var fulhamHomeGoalsForScore = leagueRepo.GetGoalsForScore("Fulham", seasonStart, LeagueRepository.FixtureType.Home);
            var fulhamHomeGoalsAgainstScore = leagueRepo.GetGoalsAgainstScore("Fulham", seasonStart, LeagueRepository.FixtureType.Home);
            var bouremouthAwayGoalsForScore = leagueRepo.GetGoalsForScore("Bournemouth", seasonStart, LeagueRepository.FixtureType.Away);
            var bouremouthAwayGoalsAgainstScore = leagueRepo.GetGoalsAgainstScore("Bournemouth", seasonStart, LeagueRepository.FixtureType.Away);
            Console.WriteLine();
            Console.WriteLine("Fulham: {0} {2}, Bournemouth: {1} {3}", fulhamHomeGoalsForScore, bouremouthAwayGoalsForScore, fulhamHomeGoalsAgainstScore, bouremouthAwayGoalsAgainstScore);

            //// FILE STUFF
            //var fixtures = csvHandler.ParseFile(directory);
            //foreach (var fixture in fixtures)
            //{
            //    leagueRepo.AddFixture(fixture);
            //}

            //DOWNLOAD STUFF
            foreach (var league in leagues)
            {
                var results = csvHandler.DownloadFile(string.Format("{0}/{1}.csv", fileUrl, league)).Result;
                foreach (var result in results)
                {
                    leagueRepo.AddFixture(result);
                }
            }

            context.SaveChanges();
            Console.WriteLine("Added fixtures");
            Console.ReadLine();
        }
    }
}
