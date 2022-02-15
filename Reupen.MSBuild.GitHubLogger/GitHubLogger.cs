using System;
using System.IO;

namespace Reupen.MSBuild.GitHubLogger
{
    using Microsoft.Build.Utilities;
    using Microsoft.Build.Framework;
    public class GitHubLogger : Logger
    {
        public override void Initialize(Microsoft.Build.Framework.IEventSource eventSource)
        {
            eventSource.WarningRaised += EventSource_WarningRaised;
            eventSource.ErrorRaised += EventSource_ErrorRaised;
        }

        private static string EscapeMessage(string str)
        {
            return str.Replace("%", "%25").Replace("\r\n", "\n").Replace("\n", "%0A");
        }
        private static string EscapeValue(string str)
        {
            return EscapeMessage(str).Replace(":", "%3A").Replace(",", "%2C");
        }

        public static string FormatEvent(string severity, string file, int lineNumber, int endLineNumber, int columnNumber, int endColumnNumber, string code, string message)
        {
            string firstMessageLine;

            using (var reader = new StringReader(message))
            {
                firstMessageLine = reader.ReadLine() ?? string.Empty;
            }

            var title = $"[{code}] {EscapeValue(firstMessageLine)}";
            var escapedMessage = EscapeMessage(message);
            var endLine = endLineNumber == 0 ? lineNumber : endLineNumber;
            var endColumn = endColumnNumber == 0 ? columnNumber : endColumnNumber;

            return
                $"::{severity} file={file},line={lineNumber},endLine={endLine},col={columnNumber},endColumn={endColumn},title={title}::{escapedMessage}";
        }
        private static void EventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            var line = FormatEvent("error", e.File, e.LineNumber, e.EndLineNumber, e.ColumnNumber, e.EndColumnNumber, e.Code, e.Message);
            Console.WriteLine(line, e);
        }

        private static void EventSource_WarningRaised(object sender, BuildWarningEventArgs e)
        {
            var line = FormatEvent("warning", e.File, e.LineNumber, e.EndLineNumber, e.ColumnNumber, e.EndColumnNumber, e.Code, e.Message);
            Console.WriteLine(line, e);
        }
    }
}