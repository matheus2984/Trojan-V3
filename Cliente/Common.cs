using System;
using System.Diagnostics;

namespace Cliente
{
    public static class Common
    {
        public const int MINIMUM_THREAD_SLEEP_MS = 1;
        private static readonly Stopwatch _clock;
        public const int MS_PER_SECOND = 1000;
        public const int MS_PER_MINUTE = 60000;
        public const int MS_PER_HOUR = 3600000;

        public static Random Random;
        static Common()
        {
            _clock = Stopwatch.StartNew();
        }

        public static long Clock
        {
            get
            {
                lock (_clock)
                    return _clock.ElapsedMilliseconds;
            }
        }

        public static uint SecondsTicks
        {
            get { return (uint)(_clock.ElapsedMilliseconds / MS_PER_SECOND); }
        }

        public static uint MinutesTicks
        {
            get { return (uint)(_clock.ElapsedMilliseconds / MS_PER_MINUTE); }
        }

        public static uint HoursTicks
        {
            get { return (uint)(_clock.ElapsedMilliseconds / MS_PER_HOUR); }
        }
    }
}
