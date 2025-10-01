using System;
using System.Threading;
using SmartOffice.Commands;
using SmartOffice.Infrastructure;

namespace SmartOffice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.Initialize(); // init logs
            Logger.Info("SmartOffice starting...");

            // Graceful shutdown token
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Logger.Info("Ctrl+C pressed. Exiting...");
                e.Cancel = true;
                cts.Cancel();
            };

            // Command processor
            var processor = new CommandProcessor();

            Console.WriteLine("SmartOffice Console (type 'help')");
            while (!cts.IsCancellationRequested)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if (line == null) break;

                try
                {
                    var exit = processor.Process(line.Trim());
                    if (exit) break;
                }
                catch (Exception ex)
                {
                    Logger.Error($"Unhandled: {ex.Message}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Logger.Info("SmartOffice stopped.");
        }
    }
}
