using log4net.Core;
using System.Collections.Generic;
using System.Text;
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
        ///  Represents the foreground color of the prefixes given the logging level
        /// </summary>
        /// <returns>
        /// Returns a map from a logging level to a brush
        /// </returns>
        /// <example> return e.RenderedMessage; </example>
        Dictionary<Level, Brush> LogLevelToPrefixForegroundBrush { get; }

        /// <summary>
        ///  Represents the foreground color of the text given the logging level
        /// </summary>
        /// <returns>
        /// Returns a map from a logging level to a brush
        /// </returns>
        /// <example> return e.RenderedMessage; </example>
        Dictionary<Level, Brush> LogLevelToTextForegroundBrush { get; }

        /// <summary>
        ///  Represents the default color for the prefix foreground color
        /// </summary>
        Brush DefaultPrefixForegroundColor { get; }

        /// <summary>
        ///  Defines the Prefix Background Color. Can be set to just the textbox background color
        /// </summary>
        Brush PrefixBackgroundColor { get; }

        /// <summary>
        ///  Defines the Text Background Color. Can be set to just the textbox background color
        /// </summary>
        Brush TextBackgroundColor { get; }

        /// <summary>
        ///  Defines the Text Foreground Color. Can be set to just the textbox background color
        /// </summary>
        Brush DefaultTextForegroundColor { get; }
    }
}
