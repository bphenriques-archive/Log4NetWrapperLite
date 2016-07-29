using log4net.Core;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Log4NetWrapperLite {
    internal class DefaultLogFormatter : ILogFormatter {
        private StringBuilder stringBuilder = new StringBuilder();

        public Dictionary<Level, Brush> LogLevelToTextForegroundBrush { get; } = new Dictionary<Level, Brush>() {
            { Level.Info, Brushes.Lime },
            { Level.Debug, Brushes.White },
            { Level.Warn, Brushes.Yellow },
            { Level.Error, Brushes.DarkOrange},
            { Level.Fatal, Brushes.Red }
        };

        public string FormatPrefix(LoggingEvent e) {
            stringBuilder.Clear();
            return stringBuilder
                .Append(e.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff"))
                .Append(" |")
                .Append(e.Level)
                .Append("| - ")
                .ToString();
        }

        public string FormatMessage(LoggingEvent e) {
            return e.RenderedMessage;
        }

        public Brush LevelToPrefixBackgroundBrush(Level level) {
            return Brushes.Black;
        }

        public Brush LevelToPrefixForegroundBrush(Level level) {            
            return Brushes.White;
        }

        public Brush LevelToTextBackgroundBrush(Level level) {
            return Brushes.Black;
        }

        public Brush LevelToTextForegroundBrush(Level level) {
            if (LogLevelToTextForegroundBrush.ContainsKey(level)) {
                return LogLevelToTextForegroundBrush[level];
            }
            return Brushes.White;
        }
    }
}
