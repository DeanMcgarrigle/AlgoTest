using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using DataAccess;
using Model;
using Utils;

namespace AlgoTest
{
    class Program
    {
        private static int total = 0;
        private static int correct = 0;
        private static double accuracy = 0.0;
        public static List<LeagueShotStats> awayShots, homeShots; 
        static void Main(string[] args)
        {
            const string directory = @"C:\Users\dean.mcgarrigle\Dropbox\public\FootballData";
            const string fixtures = @"C:\Users\dean.mcgarrigle\Dropbox\public\FootballData\fixturelist.csv";

            Console.WriteLine("Which league do you want to simulate?");
            var division = Console.ReadLine();
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);
            var csvHandler = new CsvHandler<LeagueData, LeagueDataMap>();

            homeShots = leagueRepo.LeagueShotStatsHome().Where(x => x.League == division.ToUpper()).ToList();
            awayShots = leagueRepo.LeagueShotStatsAway().Where(x => x.League == division.ToUpper()).ToList();

            var input = Options();
            if (input == "download")
            {
                Console.WriteLine("Please select a season to download (1516 etc): ");
                var season = Console.ReadLine();
                var leagues = new[] { "E0", "E1", "E2", "E3", "EC" };
                var fileUrl = string.Format("http://www.football-data.co.uk/mmz4281/{0}", season);
                foreach (var league in leagues)
                {
                    var results = csvHandler.DownloadFile(string.Format("{0}/{1}.csv", fileUrl, league)).Result;
                    foreach (var result in results)
                    {
                        leagueRepo.AddFixture(result);
                        Console.WriteLine("Adding Fixture: " + result.HomeTeam + " v " + result.AwayTeam);
                    }
                }

                context.SaveChanges();
                Console.WriteLine("Added fixtures");
                Options();

            }

            if (input == "2")
            {
                string[] teams = File.ReadAllLines(fixtures);
                var query = from line in teams
                            let data = line.Split(',')
                            select new HeadToHead()
                            {
                                Home = data[0],
                                Away = data[1],
                            };
                foreach (var a in query.Take(130).Reverse().Take(100).Reverse())
                {
                    if (!RunSimulation(a.Home, a.Away)) break;
                }
                accuracy = Convert.ToDouble(correct)/Convert.ToDouble(total)*100;
                Console.WriteLine("Simulation was "+ accuracy + "% accurate");
                Options();

            }

            if (input == "1")
            {
                Run();
            }

            if (input == "4")
            {
                RunSimulation("Watford", "Man United");
                Options();
            }

        }

        public static void Run()
        {
            while (true)
            {
                Console.Write("Enter home team: ");
                string hometeam = Console.ReadLine();
                Console.Write("Enter away team: ");
                string awayteam = Console.ReadLine();
                Console.WriteLine("predicting...");
                var result = PredictEngine.Predict(hometeam, awayteam);
                Console.WriteLine(result.predictedString);
                Console.WriteLine("");
                Console.WriteLine("Press enter to simulate again");
                Console.ReadLine();
            }

        }

        public static bool RunSimulation(string home, string away)
        {
            var context = new AlgoTestContext();
            var leagueRepo = new LeagueRepository(context);

            var result = PredictEngine.Predict(home, away);
            var FTResult = leagueRepo.GetFTResult(home, away);
            if (FTResult != null)
            {
                total = total + 1;
                if (FTResult == result.predictedValue)
                {
                    correct = correct + 1;
                    Console.WriteLine(result.predictedString + " - correct");
                }

                else
                {
                    Console.WriteLine(result.predictedString + " - wrong");
                }
                return true;
            }
            return false;
        }

        public static string Options()
        {
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("1: Simulate Match");
            Console.WriteLine("2: Simulate Season");
            Console.WriteLine("3: Simulate Next Week");
            Console.WriteLine("4: Simulate Test Game");
            Console.WriteLine("5: Download Season Data");
            return Console.ReadLine();
        }

        public class HeadToHead
        {
            public string Home { get; set; }
            public string Away { get; set; }
        }

    }
}
