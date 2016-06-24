using log4net.Core;
using System.Text;

namespace Log4NetWrapperLite {
    /// <summary>
    ///  Defines the strategy to format the prefix and the message of a loggingEvent
    /// </summary>
    public interface ILogFormatter {
        /// <summary>
        ///  Returns the loggingEvent prefix representation
        /// </summary>
        /// <param name="e">
        /// Parameter e represents a LoggingEvent
        /// <seealso cref="LoggingEvent" />
        /// </param>
        /// <returns>
        /// Returns the string representation of the prefix
        /// </returns>
        /// <example> return new StringBuilder(e.Level.ToString()).Append(" - ").ToString(); </example>
        string FormatPrefix(LoggingEvent e);

        /// <summary>
        ///  Returns the loggingEvent message representation
        /// </summary>
        /// <param name="e">
        /// Parameter e represents a LoggingEvent
        /// <seealso cref="LoggingEvent" />
        /// </param>
        /// <returns>
        /// Returns the string representation of the message
        /// </returns>
        /// <example> return e.RenderedMessage; </example>
        string FormatMessage(LoggingEvent e);
    }

    internal class DefaultLogFormatter : ILogFormatter {
        public string FormatPrefix(LoggingEvent e) {
            return new StringBuilder(e.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff"))
                   .Append(" ")
                   .Append(e.Level)
                   .Append(" ")
                   .Append(e.LoggerName)
                   .Append(" - ")
                   .ToString();
        }

        public string FormatMessage(LoggingEvent e) {
            return e.RenderedMessage;
        }
    }
}
