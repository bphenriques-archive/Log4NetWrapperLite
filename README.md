# Log4NetWrapperLite

Wraps common operations regarding [Log4Net](https://logging.apache.org/log4net/). More useful it provides a mechanism to present the logs on a richtextbox with custom colors.

The provided RichTextBox appender is also compatible with [Log4Net.Async](https://github.com/cjbhaines/Log4Net.Async)! 

## Screenshot
![alt tag](https://github.com/bphenriques/Log4NetWrapperLite/blob/master/img/Screenshot.png)

## Installation
- Add Log4NetWrapperLite nuget package in your nuget packages browser
-  Add RichTextBoxLog4Net appender in the App.config
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

- Set the richtext box in the user Control:
```cs
public ConsoleTextBoxView() {
  InitializeComponent();
  RichTextBoxLog4NetAppender appender = Logger.GetAppender<RichTextBoxLog4NetAppender>();
  if (appender != null)
      appender.RichTextBox = LogTextBox;
}

//Not needed. It's just a Quality Of Life feature
private void LogTextBox_TextChanged(object sender, TextChangedEventArgs e) {
  // set the current caret position to the end
  LogTextBox.ScrollToEnd();
}
``` 
- Log using:
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

#### Changing RichTextBox style (below is the default style) 
```cs
using log4net.Core;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MyNamespace {
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
```
