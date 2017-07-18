namespace PollingLibrary
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// Polls whatever you needed.
    /// </summary>
    public static partial class Polling
    {
        static Polling()
        {
            DefaultPollingTimeout = 10000;
            DefaultPollingInterval = 50;
        }

        /// <summary>
        /// Default polling timeout.
        /// </summary>
        public static int DefaultPollingTimeout { get; set; }

        /// <summary>
        /// Default interval to wait between polls.
        /// </summary>
        public static int DefaultPollingInterval { get; set; }

        private static Func<T, bool> DefaultCheck<T>()
        {
            var defaultValue = default(T);

            if (ReferenceEquals(defaultValue, null))
                return o => !ReferenceEquals(o, null);
            else
                return o => !defaultValue.Equals(o);
        }

        private static Tuple<T, bool> _Poll<T>(Func<T> toPoll, Func<T, bool> toCheck, int timeout, int interval)
        {
            var watch = new Stopwatch();
            watch.Start();

            for (;;)
            {
                var returned = toPoll();

                if (toCheck(returned))
                    return new Tuple<T, bool>(returned, true);

                if (watch.ElapsedMilliseconds > timeout)
                    return new Tuple<T, bool>(returned, false);

                Thread.Sleep(interval);
            }
        }
    }
}
