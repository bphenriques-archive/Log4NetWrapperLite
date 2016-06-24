using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
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
        ///  Defines the Prefix Background Color, defaults to Brushes.Black
        /// </summary>
        public Brush PrefixBackgroundColor {
            get { return _prefixBackgroundColor; }
            set { _prefixBackgroundColor = value; }
        }
        private Brush _prefixBackgroundColor = Brushes.Black;

        /// <summary>
        ///  Defines the Prefix Foreground Color, defaults to Brushes.White
        /// </summary>
        public Brush PrefixForegroundColor {
            get { return _prefixForegroundColor; }
            set { _prefixForegroundColor = value; }
        }
        private Brush _prefixForegroundColor = Brushes.White;

        /// <summary>
        ///  Defines the Text Background Color, defaults to Brushes.Black
        /// </summary>
        public Brush TextBackgroundColor {
            get { return _textBackgroundColor; }
            set { _textBackgroundColor = value; }
        }
        private Brush _textBackgroundColor = Brushes.Black;

        private readonly Dictionary<string, Brush> logLevelToTextColor = new Dictionary<string, Brush>() {
            { "INFO", Brushes.Lime },
            { "DEBUG", Brushes.White },
            { "WARN", Brushes.Yellow },
            { "ERROR", Brushes.DarkOrange},
            { "FATAL", Brushes.Red }
        };

        /// <summary>
        ///  Responsible for appending the loggingEvent
        /// </summary>
        protected override void Append(LoggingEvent loggingEvent) {
            if (_richTextBox != null) {
                Application.Current.Dispatcher.Invoke((Action)(() => Log(loggingEvent)));
            }
        }

        internal void Log(LoggingEvent loggingEvent) {
            Brush prefixForegroundColor = logLevelToTextColor[loggingEvent.Level.ToString()];
            string prefix = _formatter.FormatPrefix(loggingEvent);
            string msg = _formatter.FormatMessage(loggingEvent) + Environment.NewLine;

            AppendTextAux(_prefixForegroundColor, _prefixBackgroundColor, prefix);
            AppendTextAux(prefixForegroundColor, _textBackgroundColor, msg);
        }

        private void AppendTextAux(Brush textColor, Brush backColor, string text) {
            TextRange tr = new TextRange(_richTextBox.Document.ContentEnd, _richTextBox.Document.ContentEnd);
            tr.Text = text;
            tr.ApplyPropertyValue(TextElement.BackgroundProperty, backColor);
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, textColor);            
        }
    }
}
