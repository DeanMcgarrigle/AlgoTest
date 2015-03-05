using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Utils
{
    public class Downloader<T, TMap> where TMap : CsvClassMap
    {
        private readonly CsvHandler<T, TMap> _csvHandler;

        public Downloader(CsvHandler<T, TMap> csvHandler)
        {
            _csvHandler = csvHandler;
        }

        public async Task<IEnumerable<T>> DownloadFile(string urlFilePath)
        {
            try
            {
                bool fileDownloaded = false;
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(urlFilePath);
                    response.EnsureSuccessStatusCode();
                    var httpStream = await response.Content.ReadAsStreamAsync();

                    //// write file
                    //using (var fileStream = new FileStream(fileOutputPath, FileMode.Create, FileAccess.Write))
                    //{
                    //    using (var writer = new StreamReader(httpStream))
                    //    {
                    //        httpStream.CopyTo(fileStream);
                    //        fileStream.Flush();
                    //    }
                    //}

                    return _csvHandler.Parse(httpStream);
                }
            }
            catch (Exception ex)
            {
                return new List<T>().AsEnumerable();
            }
        }
    }

    //public interface IDownloader
    //{
    //    void DownloadFile(string urlFilePath, string fileOutputPath);
    //}
}
