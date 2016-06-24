using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using System;
using System.IO;

namespace Log4NetWrapperLite {
    /// <summary>
    ///  Wraps methods concerning Log4Net
    /// </summary>
    public static class Logger {       
        /// <summary>
        ///  The current LoggingLevel defined in the log4net LogManager Repository
        /// </summary>
        public static Level CurrentLogLevel {
            get {
                return _logger.Logger.Repository.Threshold;
            }
            set {
                _logger.Logger.Repository.Threshold = value;
                ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            }
        }

        private static readonly ILog _logger = LogManager.GetLogger(typeof(Logger));

        static Logger() {
            XmlConfigurator.Configure();
        }

        /// <summary>
        ///  Returns the instance of the registred appender
        /// </summary>
        /// <typeparam name="T">
        /// Generic T represents the name of the class that implements IAppender
        /// <seealso cref="IAppender"/>
        /// </typeparam>
        /// <returns>
        /// Returns the IAppender registred
        /// </returns>
        /// <example>
        /// RichTextBoxLog4NetAppender appender = Logger.GetAppender&lt;RichTextBoxLog4NetAppender&gt;();
        /// </example>
        public static T GetAppender<T>() where T : IAppender {
            foreach (ILog log in LogManager.GetCurrentLoggers()) {
                foreach (IAppender appender in log.Logger.Repository.GetAppenders()) {
                    if (appender is T) {
                        return (T)appender;
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        ///  Logs Info Message
        /// </summary>
        /// <param name="message">
        /// The message to append
        /// <seealso cref="string" />
        /// </param>
        public static void Info(string message) {
            _logger.Info(message);
        }

        /// <summary>
        ///  Logs Debug Message
        /// </summary>
        public static void Debug(string message) {
            _logger.Debug(message);
        }

        /// <summary>
        ///  Logs Error Message
        /// </summary>
        public static void Error(string message) {
            _logger.Error(message);
        }

        /// <summary>
        ///  Logs Fatal Message
        /// </summary>
        public static void Fatal(string message) {
            _logger.Fatal(message);
        }

        /// <summary>
        ///  Logs Warning Message
        /// </summary>
        public static void Warn(string message) {
            _logger.Warn(message);
        }
    }
}
