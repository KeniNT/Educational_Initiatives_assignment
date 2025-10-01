using System;

namespace Creational
{
    public class Logger
    {
        private static Logger instance;
        private static readonly object lockObj = new();

        private Logger() { }

        public static Logger GetInstance()
        {
            lock (lockObj)
            {
                if (instance == null)
                    instance = new Logger();
            }
            return instance;
        }

        public void Log(string message) => Console.WriteLine($"[LOG]: {message}");
    }
}
