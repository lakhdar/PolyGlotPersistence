namespace Infrastructure.CrossCutting.Core
{
    /// <summary>
    /// Trace manager contract for trace instrumentation
    /// </summary>
	public interface ILogger
	{
        /// <summary>
        /// Start logical operation in trace repository
        /// </summary>
        /// <param name="operationName"></param>
        void TraceStartLogicalOperation(string operationName);

        /// <summary>
        /// Stop actual logical operation in trace repository
        /// </summary>
        void TraceStopLogicalOperation();

        /// <summary>
        /// Send "start" flag to trace repository
        /// </summary>
        void TraceStart();

        /// <summary>
        /// Send "stop" flag to trace repository
        /// </summary>
        void TraceStop();

        /// <summary>
        /// Trace information message to trace repository
        /// <param name="message">Information message to trace</param>
        /// </summary>
        void TraceInfo(string message);

        /// <summary>
        /// Trace warning message to trace repository
        /// </summary>
        /// <param name="message">Warning message to trace</param>
        void TraceWarning(string message);

        /// <summary>
        /// Trace error message to trace repository
        /// </summary>
        /// <param name="message">Error message to trace</param>
        void TraceError(string message);

        /// <summary>
        /// Trace critical message to trace repository
        /// </summary>
        /// <param name="message">Critical message to trace</param>
        void TraceCritical(string message);
	}
}

