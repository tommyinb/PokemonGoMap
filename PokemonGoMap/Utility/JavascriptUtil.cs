using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoMap.Utility
{
    public static class JavascriptUtil
    {
        public static DateTime GetDateTime(long value)
        {
            var baseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var utcTime = baseTime.AddMilliseconds(value);

            return utcTime.ToLocalTime();
        }
        public static long GetTimeValue(DateTime dateTime)
        {
            var baseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var timeDifference = dateTime.ToUniversalTime().Subtract(baseTime);

            return (long)timeDifference.TotalMilliseconds;
        }
    }
}
