﻿using DataAccess;
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

            // FILE STUFF
            var fixtures = csvHandler.Parse(directory);
            foreach (var fixture in fixtures)
            {
                leagueRepo.AddFixture(fixture);
            }

            //DOWNLOAD STUFF
            var downloader = new Downloader<LeagueData, LeagueDataMap>(csvHandler);
            var results = downloader.DownloadFile(fileUrl).Result;
            foreach (var result in results)
            {
                leagueRepo.AddFixture(result);
            }

            context.SaveChanges();

        }
    }
}
