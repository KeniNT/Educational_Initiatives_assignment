using System;
using System.IO;

namespace SmartOffice.Infrastructure
{
    public static class Logger
    {
        private static readonly object _lock = new();
        private static string _logFile = "smartoffice.log";

        public static void Initialize(string logFile = "smartoffice.log")
        {
            _logFile = logFile;
            try
            {
                File.AppendAllText(_logFile, $"---- Log started {DateTime.UtcNow:O} ----{Environment.NewLine}");
            }
            catch
            {
                // ignore
            }
        }

        public static void Info(string msg) => Write("INFO", msg);
        public static void Warn(string msg) => Write("WARN", msg);
        public static void Error(string msg) => Write("ERROR", msg);

        private static void Write(string level, string msg)
        {
            var line = $"{DateTime.UtcNow:O} [{level}] {msg}";
            lock (_lock)
            {
                try
                {
                    Console.WriteLine(line);
                    File.AppendAllText(_logFile, line + Environment.NewLine);
                }
                catch
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
