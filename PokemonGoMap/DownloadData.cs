using Newtonsoft.Json;
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
            Form1.Logger.Log("Start Read");

            using (var response = request.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream());
                var text = reader.ReadToEnd();

                var endTime = DateTime.Now;
                Form1.Logger.Log("End Read");

                Directory.CreateDirectory(Folder);
                var fileName = startTime.ToString("yyyyMMddHHmmss");

                try
                {
                    dynamic result = JsonConvert.DeserializeObject(text);
                    if (result.pokemons == null)
                    {
                        var emptyPath = Path.Combine(Folder, fileName + ".empty.txt");
                        File.WriteAllText(emptyPath, text);
                        Form1.Logger.Log("Export to " + emptyPath);
                        return;
                    }
                }
                catch (Exception e)
                {
                    var badPath = Path.Combine(Folder, fileName + ".bad.txt");
                    File.WriteAllText(badPath, text);
                    Form1.Logger.Log(e);
                    Form1.Logger.Log("Export to " + badPath);
                    return;
                }

                var validPath = Path.Combine(Folder, fileName + ".txt");
                File.WriteAllText(validPath, text);
                Form1.Logger.Log("Export to " + validPath);
            }
        }
    }
}
