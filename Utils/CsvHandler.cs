using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Utils
{
    class CsvHandler
    {
        private static void Main(string[] args)
        {
            using (var sr = new StreamReader(@"C:\Users\dean.mcgarrigle\Downloads\E0.csv"))
            {
                var reader = new CsvReader(sr);
                reader.Configuration.RegisterClassMap<ClassMap>();
 
                //CSVReader will now read the whole file into an enumerable
                IEnumerable<DataModel> records = reader.GetRecords<DataModel>();
 
                //First 5 records in CSV file will be printed to the Output Window
                foreach (DataModel record in records.Take(5))
                {
                    Debug.Print("{0} {1} {2} {3}", record.League, record.HomeTeam, record.AwayTeam, record.DateTime );
                }
            }
        }
    }
}
