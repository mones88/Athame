using System;
using System.IO;
using AthameWPF.Utils;

namespace AthameWPF.Logging
{
    public class FileLogger : ILogger, IDisposable
    {
        private readonly string logDirectory;
        private string logFileName = String.Empty;
        private FileStream logFile;
        private StreamWriter writer;

        public FileLogger(string logDirectory)
        {
            this.logDirectory = logDirectory;
            FilenameFormat = "log{FileDate}.log";
            LineFormat = "[{Date}] [{Level}] {Tag}: {Message}";
        }

        public string FilenameFormat { get; set; }

        public string LineFormat { get; set; }

        private void EnsureLog()
        {
            var temp = new { FileDate = DateTime.Now.ToString("yyyyMMdd") };
            var formattedName = StringObjectFormatter.Format(FilenameFormat, temp);
            if (logFileName != formattedName)
            {
                logFile?.Dispose();
                writer?.Dispose();
                logFile = File.Open(Path.Combine(logDirectory, formattedName), FileMode.Append, FileAccess.Write, FileShare.Read);
                logFileName = formattedName;
                writer = new StreamWriter(logFile) {AutoFlush = true};
            }
        }

        public void Write(Level level, string moduleTag, string message)
        {
            EnsureLog();
            var vars = new
            {
                Date = DateTime.Now.ToString("O"),
                Level = level,
                Tag = moduleTag ?? "Global",
                Message = message
            };
            var formattedMessage = StringObjectFormatter.Format(LineFormat, vars);
            writer.WriteLine(formattedMessage);
            

        }

        public void Dispose()
        {
            logFile?.Dispose();
            writer?.Dispose();
        }
    }
}
