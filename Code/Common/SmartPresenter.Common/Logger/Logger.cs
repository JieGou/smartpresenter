using log4net;
using System;
using System.Diagnostics;

namespace SmartPresenter.Common.Logger
{
    /// <summary>
    /// Class to manage logging throughout the application.
    /// </summary>
    [Serializable]
    public class Logger
    {
        #region PUBLIC STATIC MEMBERS

        /// <summary>
        /// Stores the log4net object using GetLogger method.
        /// </summary>
        private static ILog logMessage = LogManager.GetLogger(
                        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the log log Message.
        /// </summary>
        /// <value>The log Message.</value>
        public static ILog LogMsg
        {
            get
            {
                return logMessage;
            }
        }

        /// <summary>
        /// Logs entry of current method.
        /// </summary>
        public static void LogEntry()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            string methodName = stackFrame.GetMethod().Name;
            Logger.LogMsg.Debug(methodName + " Started.");
        }

        /// <summary>
        /// Logs exit of current method.
        /// </summary>
        public static void LogExit()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            string methodName = stackFrame.GetMethod().Name;
            Logger.LogMsg.Debug(methodName + " Completed.");
        }

        #endregion

        #region PUBLIC STATIC CONSTRUCTOR

        /// <summary>
        /// Initializes the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the instance of the class that raised the log event.
        /// </summary>
        /// <value>The instance.</value>
        public Object Instance { get; set; }

        /// <summary>
        /// Gets or sets the name of the handler.
        /// </summary>
        /// <value>The name of the raising handler.</value>
        public String HandlerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public String ModuleName { get; set; }
        /// <summary>
        /// Gets or sets the SDCEventArgs member.
        /// </summary>
        /// <value>The SDC event args.</value>
        public Object EventArgs { get; set; }

        #endregion
    }
}
