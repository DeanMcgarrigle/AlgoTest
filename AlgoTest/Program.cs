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
            const string fileUrl = @"SomeFileURLHere";
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);
            var csvHandler = new CsvHandler<LeagueData, LeagueDataMap>();

            var lastSixHomeGamesForArsenal = leagueRepo.GetLastSixHomeResults("Arsenal");
            Console.WriteLine("Arsenal:");
            Console.WriteLine("Wins: {0}, Draws: {1}, Losses: {2}, Points: {3}, Average Goals Scored per Game: {4}, Average Goals Conceded per Game: {5}", 
                lastSixHomeGamesForArsenal.Wins, 
                lastSixHomeGamesForArsenal.Draws, 
                lastSixHomeGamesForArsenal.Losses, 
                lastSixHomeGamesForArsenal.Points,
                lastSixHomeGamesForArsenal.AvgGoalsScoredPerGame,
                lastSixHomeGamesForArsenal.AvgGoalsConcededPerGame);

            var seasonStart = new DateTime(2014, 08, 08);

            var fulhamHomeGoalsScore = leagueRepo.GetHomeTeamScore("Fulham", seasonStart);
            var bouremouthAwayGoalsScore = leagueRepo.GetAwayTeamScore("Bournemouth", seasonStart);
            Console.WriteLine();
            Console.WriteLine("Fulham: {0}, Bournemouth: {1}", fulhamHomeGoalsScore, bouremouthAwayGoalsScore);

            Console.ReadLine();

            //// FILE STUFF
            //var fixtures = csvHandler.ParseFile(directory);
            //foreach (var fixture in fixtures)
            //{
            //    leagueRepo.AddFixture(fixture);
            //}

            ////DOWNLOAD STUFF
            //var results = csvHandler.DownloadFile(fileUrl).Result;
            //foreach (var result in results)
            //{
            //    leagueRepo.AddFixture(result);
            //}

            //context.SaveChanges();

        }
    }
}
