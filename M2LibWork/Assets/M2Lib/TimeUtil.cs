using System;

namespace M2Lib
{
    public static class TimeUtil
    {
        private static DateTime EpochUTC { get; } = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long GetTimeStampUTC()
        {
            return (long)(DateTime.UtcNow - EpochUTC).TotalMilliseconds;
        }
    }
}