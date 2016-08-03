using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap.Utility
{
    public class DailyLogger
    {
        public DailyLogger(string filePath)
        {
            directory = Path.GetDirectoryName(filePath);
            if (directory.Length <= 0) throw new ArgumentException();

            fileName = Path.GetFileName(filePath);
            if (fileName.Length <= 0) throw new ArgumentException();
        }
        private readonly string directory;
        private readonly string fileName;

        public void Log(string message)
        {
            Log(new[] { message });
        }
        public void Log(string[] messages)
        {
            if (directory.Length > 0)
            {
                Directory.CreateDirectory(directory);
            }

            var currTime = DateTime.Now;

            var todayFileName = currTime.ToString("yyMMdd") + "-" + fileName;
            var filePath = Path.Combine(directory, todayFileName);

            var timeTag = currTime.ToString("[HH:mm:ss] ");
            var lines = messages.Take(1).Select(t => timeTag + t).Concat(messages.Skip(1)).Select(t => t ?? string.Empty);

            File.AppendAllLines(filePath, lines);
        }

        public void Log(Exception exception)
        {
            Log(new[] { exception.ToString() });
        }
        public void Log(string message, Exception exception)
        {
            Log(new[] { message, exception.ToString() });
        }
    }
}
