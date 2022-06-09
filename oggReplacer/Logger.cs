using System;
using CM3D2.Toolkit.Guest4168Branch.Logging;

namespace ArcScrapper
{
    internal class Logger : ILogger
    {
        void ILogger.Debug(string message, params object[] args)
        {
            if (Program.debug)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(message, args);
                Console.ResetColor();
            }
        }
        void ILogger.Error(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
        void ILogger.Info(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
        void ILogger.Warn(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
        void ILogger.Trace(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
        void ILogger.Fatal(string message, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }

        string ILogger.Name { get; }
    }
}
