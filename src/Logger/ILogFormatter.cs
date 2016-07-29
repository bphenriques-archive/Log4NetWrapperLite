using log4net.Core;
using System.Windows.Media;

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

        /// <summary>
        ///  Defines the Text Background Color. Can be set to just the textbox background color
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Brush LevelToTextBackgroundBrush(Level level);

        /// <summary>
        ///  Defines the Text Foreground Color. Can be set to just the textbox background color
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Brush LevelToTextForegroundBrush(Level level);


        /// <summary>
        ///  Defines the Text Background Color for the prefix
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Brush LevelToPrefixBackgroundBrush(Level level);

        /// <summary>
        ///  Defines the Text Foreground Color for the prefix
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Brush LevelToPrefixForegroundBrush(Level level);
    }
}
