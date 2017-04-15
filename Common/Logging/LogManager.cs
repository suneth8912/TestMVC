using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public static class LogManager
    {

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the error
        /// </summary>
        /// <param name="exception">The excpetion</param>
        /// <param name="errorMessage">Error Message</param>
        public static void LogError(Exception exception, string errorMessage = "")
        {
            logger.Error(exception, errorMessage);
        }


        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public static void LogError(string errorMessage)
        {
            logger.Error(errorMessage);
        }


        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public static void LogError(string errorMessage, string stackTrace)
        {
            logger.Error(errorMessage, stackTrace);
        }


        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="information">The information.</param>
        public static void LogInfo(string information)
        {
            logger.Info(information);
        }

    }
}
