using log4net.Core;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Log4NetWrapperLite {
    internal class DefaultLogFormatter : ILogFormatter {
        private Dictionary<Level, Brush> _logLevelToPrefixBrush = new Dictionary<Level, Brush>() {
            { Level.Info, Brushes.White },
            { Level.Debug, Brushes.White },
            { Level.Warn, Brushes.White },
            { Level.Error, Brushes.White},
            { Level.Fatal, Brushes.White }
        };
        public Dictionary<Level, Brush> LogLevelToPrefixForegroundBrush => _logLevelToPrefixBrush;

        private Dictionary<Level, Brush> _logLevelToTextBrush = new Dictionary<Level, Brush>() {
            { Level.Info, Brushes.Lime },
            { Level.Debug, Brushes.White },
            { Level.Warn, Brushes.Yellow },
            { Level.Error, Brushes.DarkOrange},
            { Level.Fatal, Brushes.Red }
        };
        public Dictionary<Level, Brush> LogLevelToTextForegroundBrush => _logLevelToTextBrush;


        public Brush DefaultPrefixForegroundColor => Brushes.White;
        public Brush PrefixBackgroundColor => Brushes.Black;
        public Brush TextBackgroundColor => Brushes.Black;
        public Brush DefaultTextForegroundColor => Brushes.White;

        public string FormatPrefix(LoggingEvent e) {
            return new StringBuilder(e.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss,fff"))
                   .Append(" |")
                   .Append(e.Level)
                   .Append("| - ")
                   .ToString();
        }

        public string FormatMessage(LoggingEvent e) {
            return e.RenderedMessage;
        }
    }
}
