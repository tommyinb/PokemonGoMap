using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap.Utility
{
    public static class StreamUtil
    {
        public static byte[] ReadAllBytes(this Stream stream)
        {
            var buffer = new byte[1024 * 1024];

            using (var memoryStream = new MemoryStream())
            {
                while (true)
                {
                    var read = stream.Read(buffer, 0, buffer.Length);

                    if (read <= 0) break;

                    memoryStream.Write(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }

        public static string ReadAllText(this Stream stream, Encoding encoding)
        {
            using (var reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
