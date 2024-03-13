using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    public static class Downloader
    {
        public static async Task<string> DownloadAndSaveImageAsync(string imageUrl, string fileName, string folderName)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] imageData = await webClient.DownloadDataTaskAsync(new Uri(imageUrl));

                    // Get Image
                    string imageExtension = Path.GetExtension(imageUrl);

                    // Make path of file
                    string dataFolderPath = "Data";
                    string imageFolderPath = Path.Combine(dataFolderPath, folderName);
                    string imagePath = Path.Combine(imageFolderPath, $"{fileName}{imageExtension}");

                    // save local
                    File.WriteAllBytes(imagePath, imageData);

                    return imagePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al descargar y guardar la imagen: {ex.Message}");
                return null;
            }
        }
    }
}
