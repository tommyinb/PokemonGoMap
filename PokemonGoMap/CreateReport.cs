using Newtonsoft.Json;
using PokemonGoMap.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokemonGoMap
{
    public class CreateReport
    {
        public const string Folder = @"..\Result";

        public static void Run()
        {
            Form1.Logger.Log("Start Report");

            var monstersPoints = LoadMonsters()
                .ToLookup(t => new { t.Id, t.Latitude, t.Longitude, Time = t.Time.ToString("yyyyMMddHHmm") })
                .Select(t => t.First())
                .ToLookup(t => t.Id);

            Directory.CreateDirectory(Folder);

            foreach (var monsterPoints in monstersPoints)
            {
                WriteMonster(monsterPoints);
            }

            WriteResult(monstersPoints);

            Form1.Logger.Log("End Report");
        }

        private static IEnumerable<Monster> LoadMonsters()
        {
            return Directory.EnumerateFiles(DownloadData.Folder, "*.json").Where(t =>
            {
                var name = Path.GetFileNameWithoutExtension(t);

                DateTime time;
                if (DateTime.TryParseExact(name, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out time) == false) return false;

                return time <= DateTime.Now
                    && time > DateTime.Now - TimeSpan.FromHours(24);
            }).SelectMany(t =>
            {
                var text = File.ReadAllText(t);
                return JsonConvert.DeserializeObject<Monster[]>(text);
            });
        }

        private static void WriteMonster(IGrouping<int, Monster> points)
        {
            var file = Path.Combine(Folder, points.Key + ".json");

            var data = points.Select(t => new
            {
                a = t.Latitude,
                o = t.Longitude,
                t = JavascriptUtil.GetTimeValue(t.Time) / 1000
            }).ToArray();

            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(file, json);
        }

        private static void WriteResult(ILookup<int, Monster> points)
        {
            var file = Path.Combine(Folder, "result.json");
            var data = new
            {
                t = JavascriptUtil.GetTimeValue(DateTime.Now),
                m = points.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Count())
            };

            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(file, json);
        }
    }
}
