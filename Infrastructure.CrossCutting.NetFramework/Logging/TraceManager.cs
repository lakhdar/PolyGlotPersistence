
namespace Infrastructure.CrossCutting.NetFramework.Logging
{
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.CrossCutting.NetFramework.Resources;
    using System;
    using System.Diagnostics;
    using System.Security;

    /// <summary>
    /// Trace helper for application's logging
    /// </summary>
    public sealed class TraceManager
        : ILogger
    {
        #region Members

        TraceSource source;

        #endregion

        #region  Constructor

        /// <summary>
        /// Create a new instance of this trace manager
        /// </summary>
        public TraceManager()
        {
            // Create default source
            source = new TraceSource("Polyglot");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Trace internal message in configured listeners
        /// </summary>
        /// <param name="eventType">Event type to trace</param>
        /// <param name="message">Message of event</param>
        void TraceInternal(TraceEventType eventType, string message)
        {
            if (source != null)
            {
                try
                {
                    source.TraceEvent(eventType, (int)eventType, message);
                }
                catch (SecurityException)
                {
                    //Cannot access to file listener or cannot have
                    //privileges to write in event log
                    //do not propagete this :-(
                }
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        /// <param name="operationName"><see cref="Infrastructure.CrossCutting.Core.ILogger"/></param>
        public void TraceStartLogicalOperation(string operationName)
        {
            if (String.IsNullOrEmpty(operationName))
                throw new ArgumentNullException("operationName", Messages.exception_InvalidTraceMessage);

            System.Diagnostics.Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            System.Diagnostics.Trace.CorrelationManager.StartLogicalOperation(operationName);
        }
        
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        public void TraceStopLogicalOperation()
        {
            try
            {
                System.Diagnostics.Trace.CorrelationManager.StopLogicalOperation();
            }
            catch (InvalidOperationException)
            {
                //stack empty
            }
        }
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        public void TraceStart()
        {
            TraceInternal(TraceEventType.Start, Messages.message_START);
        }
        /// <summary>
        ///<see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        public void TraceStop()
        {

            TraceInternal(TraceEventType.Start, Messages.message_STOP);

        }
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Core.ILogger"/></param>
        public void TraceInfo(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Information, message);

        }
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Core.ILogger"/></param>
        public void TraceWarning(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Warning, message);

        }
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Core.ILogger"/></param>
        public void TraceError(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Error, message);

        }
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Core.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Core.ILogger"/></param>
        public void TraceCritical(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message", Messages.exception_InvalidTraceMessage);

            TraceInternal(TraceEventType.Critical, message);
        }

        #endregion

    }
}

