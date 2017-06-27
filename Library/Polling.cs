namespace PollingLibrary
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// Polls whatever you needed.
    /// </summary>
    public static class Polling
    {
        private const int _timeout = 10000;

        private const int _interval = 50;

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="timeout">Timeout in milliseconds (defaults to 10s)</param>
        /// <param name="interval">Interval to wait between polls in milliseconds (defaults to 50ms)</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, int timeout = _timeout, int interval = _interval)
        {
            return Poll(toPoll, DefaultCheck<T>(), timeout, interval);
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function (which will be default(T)) is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="timeout">Timeout in milliseconds (defaults to 10s)</param>
        /// <param name="interval">Interval to wait between polls in milliseconds (defaults to 50ms)</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll, int timeout = _timeout, int interval = _interval)
        {
            return Poll(toPoll, DefaultCheck<T>(), _timeout, _interval);
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <param name="timeout">Timeout in milliseconds (defaults to 10s)</param>
        /// <param name="interval">Interval to wait between polls in milliseconds (defaults to 50ms)</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, Func<T, bool> toCheck, int timeout = _timeout, int interval = _interval)
        {
            var poll = _Poll(toPoll, toCheck, timeout, interval);

            if (poll.Item2)
                return poll.Item1;
            else
                throw new PollingTimeoutException();
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <param name="timeout">Timeout in milliseconds (defaults to 10s)</param>
        /// <param name="interval">Interval to wait between polls in milliseconds (defaults to 50ms)</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(
            Func<T> toPoll, Func<T, bool> toCheck, int timeout = _timeout, int interval = _interval)
        {
            return _Poll(toPoll, toCheck, timeout, interval).Item1;
        }

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
