using PokemonGoMap.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap
{
    class DownloadData
    {
        public const string Folder = @"..\Data";

        public static void Run()
        {
            var url = @"https://skiplagged.com/api/pokemon.php?bounds=22.18628,113.672943,22.623665,114.551849";
            var request = FastHttpRequest.Create(url);
            request.Host = "skiplagged.com";
            request.Referer = "https://skiplagged.com/pokemon/";

            var startTime = DateTime.Now;
            Logger.LogMessage("Start Read");

            using (var response = request.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
                var rawResult = reader.ReadToEnd();

                var endTime = DateTime.Now;
                Logger.LogMessage("End Read");

                Directory.CreateDirectory(Folder);
                var fileName = startTime.ToString("yyyyMMddHHmmss");

                var filePath = Path.Combine(Folder, fileName + ".txt");
                File.WriteAllText(filePath, rawResult);
                Logger.LogMessage("Export to " + filePath);
            }
        }
    }
}
