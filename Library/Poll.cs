namespace PollingLibrary
{
    using System;

    /// <summary>
    /// Polls whatever you needed.
    /// </summary>
    public static partial class Polling
    {
        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll)
        {
            return Poll(toPoll, DefaultPollingTimeout, DefaultPollingInterval);
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, int timeout)
        {
            return Poll(toPoll, DefaultCheck<T>(), timeout, DefaultPollingInterval);
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <param name="interval">Interval to wait between polls in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, int timeout, int interval)
        {
            return Poll(toPoll, DefaultCheck<T>(), timeout, interval);
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, Func<T, bool> toCheck)
        {
            return Poll(toPoll, toCheck, DefaultPollingTimeout, DefaultPollingInterval);
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, Func<T, bool> toCheck, int timeout)
        {
            var poll = _Poll(toPoll, toCheck, timeout, DefaultPollingInterval);

            if (poll.Item2)
                return poll.Item1;
            else
                throw new PollingTimeoutException();
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens, an exception is thrown.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <param name="interval">Interval to wait between polls in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        /// <exception cref="PollingTimeoutException">If the polling operation timed out</exception>
        public static T Poll<T>(Func<T> toPoll, Func<T, bool> toCheck, int timeout, int interval)
        {
            var poll = _Poll(toPoll, toCheck, timeout, interval);

            if (poll.Item2)
                return poll.Item1;
            else
                throw new PollingTimeoutException();
        }
    }
}
