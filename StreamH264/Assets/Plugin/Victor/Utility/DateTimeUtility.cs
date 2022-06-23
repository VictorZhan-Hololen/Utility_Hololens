using System;

namespace Victor.Utility
{
    public class DateTimeUtility
    {
        public static long NowToSec
        {
            get => DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
        }
        public static long NowToMS
        {
            get => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
