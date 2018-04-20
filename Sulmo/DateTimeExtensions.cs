using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmo
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTIme(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }


}
