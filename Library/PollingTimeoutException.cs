namespace PollingLibrary
{
    using System;

    /// <summary>
    /// Exception thrown when a polling times out.
    /// </summary>
    public class PollingTimeoutException : Exception
    {
        internal PollingTimeoutException() : base("The polling operation has timed out.") { }
    }
}
