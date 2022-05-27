using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ZaloPay
{
    public class Util
    {
        public static long GetTimeStamp(DateTime date)
        {
            return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static long GetTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }
    }
}