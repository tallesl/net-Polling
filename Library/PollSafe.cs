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
        /// If a timeout happens the last value returned by the function (which will be default(T)) is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll)
        {
            return _Poll(toPoll, DefaultCheck<T>(), DefaultPollingTimeout, DefaultPollingInterval).Item1;
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function (which will be default(T)) is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll, int timeout)
        {
            return _Poll(toPoll, DefaultCheck<T>(), timeout, DefaultPollingInterval).Item1;
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function (which will be default(T)) is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <param name="interval">Interval to wait between polls in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll, int timeout, int interval)
        {
            return _Poll(toPoll, DefaultCheck<T>(), timeout, interval).Item1;
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll, Func<T, bool> toCheck)
        {
            return _Poll(toPoll, toCheck, DefaultPollingTimeout, DefaultPollingInterval).Item1;
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll, Func<T, bool> toCheck, int timeout)
        {
            return _Poll(toPoll, toCheck, timeout, DefaultPollingInterval).Item1;
        }

        /// <summary>
        /// Polls the given function until it returns a value other than default(T) or a timeout occur.
        /// If a timeout happens the last value returned by the function is returned.
        /// </summary>
        /// <typeparam name="T">Type to be returned</typeparam>
        /// <param name="toPoll">Function to be polled</param>
        /// <param name="toCheck">Function that decides if the polled value should be returned</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        /// <param name="interval">Interval to wait between polls in milliseconds</param>
        /// <returns>The type returned by the polled function</returns>
        public static T PollSafe<T>(Func<T> toPoll, Func<T, bool> toCheck, int timeout, int interval)
        {
            return _Poll(toPoll, toCheck, timeout, interval).Item1;
        }
    }
}
