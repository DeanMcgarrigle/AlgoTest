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

            // FILE STUFF
            var csvHandler = new CsvHandler<LeagueData, LeagueDataMap>(directory);
            var fixtures = csvHandler.Parse();

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
