using log4net.Appender;
using log4net.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Log4NetWrapperLite {
    /// <summary>
    ///  Represents the Appender responsible for writting to a textBox
    /// </summary>
    public class RichTextBoxLog4NetAppender : AppenderSkeleton {

        /// <summary>
        ///  Represents the current Logging Format strategy for the prefix and the message
        /// </summary>
        public ILogFormatter Formatter => _formatter;
        private ILogFormatter _formatter = new DefaultLogFormatter();

        /// <summary>
        ///  Represents the destiny richTextBox that is defined in the WPF User Control instance
        /// </summary>
        /// 
        /// <example>
        /// InitializeComponent();
        /// RichTextBoxLog4NetAppender appender = Logger.GetAppender&lt;RichTextBoxLog4NetAppender&gt;();
        /// if (appender != null)
        ///     appender.textBox = LogTextBox;
        /// </example>
        public RichTextBox RichTextBox {
            get { return _richTextBox; }
            set { _richTextBox = value; }
        }
        private RichTextBox _richTextBox;

        /// <summary>
        ///  Responsible for appending the loggingEvent
        /// </summary>
        protected override void Append(LoggingEvent loggingEvent) {
            if (_richTextBox != null) {
                Application.Current.Dispatcher.Invoke((Action)(() => Log(loggingEvent)));
            }
        }

        internal void Log(LoggingEvent loggingEvent) {
            Level level = loggingEvent.Level;

            //setting up prefix foreground color
            var levelToBrushPrefixColorMap = _formatter.LogLevelToPrefixForegroundBrush;
            Brush defaultPrefixForegroundColor = _formatter.DefaultPrefixForegroundColor;
            Brush prefixForegroundColor = levelToBrushPrefixColorMap.ContainsKey(level) ? levelToBrushPrefixColorMap[level] : defaultPrefixForegroundColor;

            //setting up prefix foreground color
            var levelToBrushTextColorMap = _formatter.LogLevelToTextForegroundBrush;
            Brush defaultTextForegroundColor = _formatter.DefaultTextForegroundColor;
            Brush textForegroundColor = levelToBrushTextColorMap.ContainsKey(level) ? levelToBrushTextColorMap[level] : defaultTextForegroundColor;

            string prefix = _formatter.FormatPrefix(loggingEvent);
            string msg = _formatter.FormatMessage(loggingEvent) + Environment.NewLine;

            AppendTextAux(prefixForegroundColor, _formatter.PrefixBackgroundColor, prefix);
            AppendTextAux(textForegroundColor, _formatter.TextBackgroundColor, msg);
        }

        private void AppendTextAux(Brush textColor, Brush backColor, string text) {
            TextRange tr = new TextRange(_richTextBox.Document.ContentEnd, _richTextBox.Document.ContentEnd);
            tr.Text = text;
            tr.ApplyPropertyValue(TextElement.BackgroundProperty, backColor);
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);            
        }
    }
}
