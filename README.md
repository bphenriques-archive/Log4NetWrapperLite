# Log4NetWrapperLite

Wraps common operations regarding [Log4Net](https://logging.apache.org/log4net/). More useful it provides a mechanism to present the logs on a richtextbox with custom colors.

The provided RichTextBox appender is also compatible with [Log4Net.Async](https://github.com/cjbhaines/Log4Net.Async)! 

## Screenshot
![alt tag](https://github.com/bphenriques/Log4NetWrapperLite/blob/master/img/Screenshot.png)

## Examples

### Logging events
```cs
using Log4NetWrapperLite;

public class Program {
  static void main(string[] args) {
    Logger.Info("My info Message");
    Logger.Error("My error Message");
    Logger.Warn("My warn Message");
    Logger.Fatal("My fatal Message");
    Logger.Debug("My debug Message");
  }
}
```

### Getting custom implementation of IAppender and using it (the below example retrives the log folder from the FileAppender)
```cs
var logFolder = Path.GetDirectoryName(Logger.GetAppender<FileAppender>()?.File ?? string.Empty);
```

### Setting up RichTextbox appender

App.config
```xml
<appender name="RichTextBoxLog4Net" type="Log4NetWrapperLite.RichTextBoxLog4NetAppender">
  <layout type="log4net.Layout.PatternLayout">
    <param name="ConversionPattern" value="%date{HH:mm:ss,fff} [%thread] %level - %message%newline" />
  </layout>
</appender>

<root>
  <level value="DEBUG" />
  <appender-ref ref="RichTextBoxLog4Net" />
</root>
```

In the User Control:
```cs
public ConsoleTextBoxView() {
  InitializeComponent();
  RichTextBoxLog4NetAppender appender = Logger.GetAppender<RichTextBoxLog4NetAppender>();
  if (appender != null)
      appender.RichTextBox = LogTextBox;

  Singleton<ConsoleViewManager>.Instance.textBox = LogTextBox;
}

//Quality Of Life extra
private void LogTextBox_TextChanged(object sender, TextChangedEventArgs e) {
  // set the current caret position to the end
  LogTextBox.ScrollToEnd();
}
```

### Customizing the RichTextBox style (this is the default implementation for a RichTextBox with black background :) ) 
```cs
using log4net.Core;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MyNamespace {
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
```
