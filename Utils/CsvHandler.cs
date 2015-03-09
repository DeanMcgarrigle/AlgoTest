using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Utils
{
    public class CsvHandler<T, TMap> where TMap : CsvClassMap
    {
        public IEnumerable<T> ParseFile(string directory)
        {
            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            var records = new List<T>();
            foreach (var file in files)
            {
                using (var sr = new StreamReader(file))
                {
                    records.AddRange(GetRecords(sr));
                }
            }

            return records;
        }

        public async Task<IEnumerable<T>> DownloadFile(string urlFilePath)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(urlFilePath);
                    response.EnsureSuccessStatusCode();
                    var httpStream = await response.Content.ReadAsStreamAsync();

                    using (var sr = new StreamReader(httpStream))
                    {
                        return GetRecords(sr);
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        private static IEnumerable<T> GetRecords(TextReader sr)
        {
            var reader = new CsvReader(sr);
            reader.Configuration.RegisterClassMap<TMap>();
            var records = reader.GetRecords<T>().ToList();
            return records;
        }
    }
}