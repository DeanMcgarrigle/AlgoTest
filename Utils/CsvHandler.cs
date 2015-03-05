using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using DataAccess;
using Model;

namespace Utils
{
    class CsvHandler
    {
        private static void Main(string[] args)
        {
            var context = new AlgoTestContext();
            var existingFixtures = context.LeagueData.ToList();
            const string directory = @"C:\Users\dean.mcgarrigle\Dropbox\Documents\FootballData";

            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            var index = 0;
            foreach (var file in files)
            {
                using (var sr = new StreamReader(file))
                {
                    var reader = new CsvReader(sr);
                    reader.Configuration.RegisterClassMap<ClassMap>();

                    //CSVReader will now read the whole file into an enumerable
                    IEnumerable<LeagueData> records = reader.GetRecords<LeagueData>();

                    //First 5 records in CSV file will be printed to the Output Window
                    foreach (var record in records)
                    {
                        var thisFixture =
                            existingFixtures.FirstOrDefault(
                                x =>
                                    x.DateTime == record.DateTime && x.HomeTeam == record.HomeTeam &&
                                    x.AwayTeam == record.AwayTeam);
                        if (thisFixture == null)
                        {
                            context.LeagueData.Add(record);
                            Console.WriteLine("Added: {0}", index++);
                        }

                        //Debug.Print("{0} {1} {2} {3}", record.League, record.HomeTeam, record.AwayTeam, record.DateTime );
                    }
                }
            }

            context.SaveChanges();

        }
    }
}
