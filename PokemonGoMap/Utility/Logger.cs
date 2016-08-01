using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap.Utility
{
    public static class Logger
    {
        public static void LogMessage(string message)
        {
            var line = DateTime.Now.ToString("[HH:mm:ss] ") + message;
            File.AppendAllLines("program.log", new[] { line });
        }

        public static void LogError(string message)
        {
            var line = DateTime.Now.ToString("[HH:mm:ss] ") + message;
            File.AppendAllLines("program.err", new[] { line });
        }
    }
}
