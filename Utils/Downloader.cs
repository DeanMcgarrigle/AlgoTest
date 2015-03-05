using System;
using System.IO;
using System.Net.Http;

namespace Utils
{
    public class Downloader : IDownloader
    {
        public async void DownloadFile(string urlFilePath, string fileOutputPath)
        {
            try
            {
                bool fileDownloaded = false;
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(urlFilePath);
                    response.EnsureSuccessStatusCode();
                    var httpStream = await response.Content.ReadAsStreamAsync();

                    // write file
                    using (var fileStream = new FileStream(fileOutputPath, FileMode.Create, FileAccess.Write))
                    {
                        using (var writer = new StreamReader(httpStream))
                        {
                            httpStream.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    public interface IDownloader
    {
        void DownloadFile(string urlFilePath, string fileOutputPath);
    }
}
