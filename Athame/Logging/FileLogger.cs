using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athame.Utils;

namespace Athame.Logging
{
    public class FileLogger : ILogger, IDisposable
    {
        private readonly string logDirectory;
        private string logFileName = String.Empty;
        private FileStream logFile;

        public FileLogger(string logDirectory)
        {
            this.logDirectory = logDirectory;
            FilenameFormat = "log{FileDate}.log";
            LineFormat = "[{Date}] [{Level}] {Tag}: {Message}";
        }

        public string FilenameFormat { get; set; }

        public string LineFormat { get; set; }

        private FileStream GetLogFile()
        {
            var temp = new { FileDate = DateTime.Now.ToString("yyyyMMdd") };
            var formattedName = StringObjectFormatter.Format(FilenameFormat, temp);
            if (logFileName != formattedName)
            {
                logFile?.Dispose();
                logFile = File.OpenWrite(Path.Combine(logDirectory, formattedName));
                logFileName = formattedName;
            }
            return logFile;
        }

        public void Write(Level level, string moduleTag, string message)
        {
            var vars = new
            {
                Date = DateTime.Now.ToString("O"),
                Level = level,
                Tag = moduleTag ?? "Global",
                Message = message
            };
            var formattedMessage = StringObjectFormatter.Format(LineFormat, vars);
            var writer = new StreamWriter(GetLogFile());
            writer.WriteLine(formattedMessage);
        }

        public void Dispose()
        {
            logFile?.Dispose();
        }
    }
}
