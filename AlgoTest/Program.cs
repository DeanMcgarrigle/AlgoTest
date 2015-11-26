﻿using System;
using System.IO;
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
            const string fixtures = @"C:\Users\dean.mcgarrigle\Dropbox\public\FootballData\fixturelist.csv";
            const string season = "1516";
            var leagues = new[] { "E0", "E1", "E2", "E3", "EC" };
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
                        Console.WriteLine("Adding Fixture: " + result.HomeTeam + " v " + result.AwayTeam);
                    }
                }

                context.SaveChanges();
                Console.WriteLine("Added fixtures");
                Console.ReadLine();
            
            }

            if (input == "simulate")
            {
                string[] teams = File.ReadAllLines(fixtures);


                var query = from line in teams
                            let data = line.Split(',')
                            select new HeadToHead()
                            {
                                Home = data[0],
                                Away = data[1],
                            };

                foreach (var a in query)
                {
                    RunSimulation(a.Home, a.Away);
                }
                Console.ReadLine();

            }

            if (input == "predict")
            {
             Run();
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
                Console.WriteLine(result);
                Console.WriteLine("");
                Console.WriteLine("Press enter to predict another game");
                Console.ReadLine();
            }
        }

        public static void RunSimulation(string home, string away)
        {
                var result = PredictEngine.Predict(home, away);
                Console.WriteLine(result);
        }

        public class HeadToHead
        {
            public string Home { get; set; }
            public string Away { get; set; }
        }

    }
}
