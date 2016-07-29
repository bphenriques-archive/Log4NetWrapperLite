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
        public ILogFormatter Formatter { get; set; } = new DefaultLogFormatter();

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
        public RichTextBox RichTextBox { get; set; }

        /// <summary>
        ///  Responsible for appending the loggingEvent
        /// </summary>
        protected override void Append(LoggingEvent loggingEvent) {
            if (RichTextBox != null) {
                Application.Current.Dispatcher.Invoke((Action)(() => Log(loggingEvent)));
            }
        }

        internal void Log(LoggingEvent loggingEvent) {
            Level level = loggingEvent.Level;

            //setting up prefix foreground and background color
            Brush prefixForegroundColor = Formatter.LevelToPrefixForegroundBrush(level);
            Brush prefixBackgroundColor = Formatter.LevelToPrefixBackgroundBrush(level);

            Brush textForegroundColor = Formatter.LevelToTextForegroundBrush(level);
            Brush textBackgroundColor = Formatter.LevelToTextBackgroundBrush(level);

            string prefix = Formatter.FormatPrefix(loggingEvent);
            string msg = Formatter.FormatMessage(loggingEvent) + Environment.NewLine;

            RichTextBox.BeginChange();
            AppendTextAux(prefixForegroundColor, prefixBackgroundColor, prefix);
            AppendTextAux(textForegroundColor, textBackgroundColor, msg);
            RichTextBox.EndChange();
        }

        private void AppendTextAux(Brush foregroundColor, Brush backgroundColor, string text) {
            TextRange tr = new TextRange(RichTextBox.Document.ContentEnd, RichTextBox.Document.ContentEnd);
            tr.Text = text;
            tr.ApplyPropertyValue(TextElement.BackgroundProperty, backgroundColor);
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, foregroundColor);            
        }
    }
}
