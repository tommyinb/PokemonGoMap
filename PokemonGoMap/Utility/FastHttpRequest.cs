using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap.Utility
{
    public static class FastHttpRequest
    {
        private static void AdjustServicePoint()
        {
            if (ServicePointManager.DefaultConnectionLimit < 20)
            {
                ServicePointManager.DefaultConnectionLimit = 20;
            }

            if (ServicePointManager.Expect100Continue)
            {
                ServicePointManager.Expect100Continue = false;
            }
        }
        private static bool isServicePointAdjusted = false;

        public static HttpWebRequest Create(string requestUriString)
        {
            if (isServicePointAdjusted == false)
            {
                AdjustServicePoint();
                isServicePointAdjusted = true;
            }

            var request = WebRequest.CreateHttp(requestUriString);

            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";

            request.Proxy = null;

            return request;
        }
    }
}
