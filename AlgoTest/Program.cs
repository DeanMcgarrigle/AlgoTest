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
            const string directory = @"C:\Users\dean.mcgarrigle\Dropbox\public\FootballData";
            const string season = "1516";
            var leagues = new[] {"E0", "E1", "E2", "E3", "EC" };
            var fileUrl = string.Format("http://www.football-data.co.uk/mmz4281/{0}", season);
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);
            var csvHandler = new CsvHandler<LeagueData, LeagueDataMap>();

            Console.Write("What do you want to do?: ");

            string input = Console.ReadLine();

            if (input == "download")
            {
                foreach (var league in leagues)
                {
                    var results = csvHandler.DownloadFile(string.Format("{0}/{1}.csv", fileUrl, league)).Result;
                    foreach (var result in results)
                    {
                        leagueRepo.AddFixture(result);
                        leagueRepo.AddTeam(result);
                        Console.WriteLine("Adding Fixture: " + result.HomeTeam + " v " + result.AwayTeam);
                    }
                }

                context.SaveChanges();
                Console.WriteLine("Added fixtures");
                Console.ReadLine();
            }

            if (input == "predict")
            {
                Console.Write("Enter home team: ");
                string hometeam = Console.ReadLine();

                Console.Write("Enter away team: ");
                string awayteam = Console.ReadLine();

                Console.WriteLine("predicting...");
                
                var result = PredictEngine.Predict(hometeam, awayteam);
                Console.WriteLine(result);
                Console.ReadLine();
            }
        
            //DOWNLOAD STUFF
            
        }
    }
}
