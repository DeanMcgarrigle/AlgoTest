using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace Utils
{
    public class CsvHandler<T, TMap> where TMap : CsvClassMap
    {
        public IEnumerable<T> Parse(string directory)
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

            return records.AsEnumerable();
        }

        public IEnumerable<T> Parse(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return GetRecords(sr);
            }
        }

        private static IEnumerable<T> GetRecords(TextReader sr)
        {
            var reader = new CsvReader(sr);
            reader.Configuration.RegisterClassMap<TMap>();
            return reader.GetRecords<T>();
        }
    }
}