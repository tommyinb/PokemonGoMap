using Newtonsoft.Json;
using PokemonGoMap.Utility;
using System;
using System.Collections;
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
                var rawJson = response.GetResponseStream().ReadAllText(Encoding.UTF8);
                
                var endTime = DateTime.Now;
                Form1.Logger.Log("End Read");

                Directory.CreateDirectory(Folder);
                var fileName = startTime.ToString("yyyyMMddHHmmss");

                var rawPath = Path.Combine(Folder, fileName + ".raw.txt");
                File.WriteAllText(rawPath, rawJson);
                Form1.Logger.Log("Export to " + rawPath);

                try
                {
                    dynamic result = JsonConvert.DeserializeObject(rawJson);
                    var pokemons = ((IEnumerable)result.pokemons).Cast<dynamic>();
                    var monsters = pokemons.Select(pokemon => new Monster
                    {
                        Id = pokemon.pokemon_id,
                        Name = pokemon.pokemon_name,
                        Latitude = pokemon.latitude,
                        Longitude = pokemon.longitude,
                        Time = JavascriptUtil.GetDateTime((long)pokemon.expires * 1000),
                        Source = "skiplagged"
                    }).ToArray();

                    var saveJson = JsonConvert.SerializeObject(monsters);
                    var validPath = Path.Combine(Folder, fileName + ".json");
                    File.WriteAllText(validPath, saveJson);
                    Form1.Logger.Log("Export to " + validPath);
                }
                catch (Exception e)
                {
                    Form1.Logger.Log(e);
                }
            }
        }
    }
}
