using CM3D2.Toolkit.Guest4168Branch.Logging;
using CM3D2.Toolkit.Guest4168Branch.Arc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArcScrapper
{
    internal class Program
    {
        internal static ILogger logger = new Logger();

        internal static bool debug = false;
        internal static bool logArcFileSystem = false;
        internal static bool scrapAudio = true;
        internal static bool scrapArc = true;
        internal static bool backupOldArc = true;
        internal static string gamePath;

        internal static readonly string blankOgg = AppContext.BaseDirectory + "\\Empty.ogg";

        static void Main(string[] args)
        {
            // user can drop .arc directly on the .exe
            List<string> arcs = args.ToList();

            if (arcs.Count == 0)
            {
                gamePath = Tools.GetGamePath();

                ConsoleKeyInfo key = new ConsoleKeyInfo();

                while (key.Key != ConsoleKey.Enter)
                {
                    if ((key.Key == ConsoleKey.D1) || (key.Key == ConsoleKey.NumPad1)) { scrapArc = !scrapArc; }
                    if ((key.Key == ConsoleKey.D2) || (key.Key == ConsoleKey.NumPad2)) { scrapAudio = !scrapAudio; }
                    if ((key.Key == ConsoleKey.D8) || (key.Key == ConsoleKey.NumPad8)) { backupOldArc = !backupOldArc; }
                    if ((key.Key == ConsoleKey.D9) || (key.Key == ConsoleKey.NumPad9)) { logArcFileSystem = !logArcFileSystem; }

                    Console.Clear();
                    Tools.WriteLine(gamePath, ConsoleColor.Green);
                    Console.ResetColor();
                    Console.WriteLine("Select options");
                    Console.Write($"1. Delete unused .arc: "); Tools.WriteLine(scrapArc.ToString(), ConsoleColor.Blue);
                    Console.Write($"2. Replace audio files: "); Tools.WriteLine(scrapAudio.ToString(), ConsoleColor.Blue);
                    Console.Write($"8. Backup old .arc: "); Tools.WriteLine(backupOldArc.ToString(), ConsoleColor.Blue);
                    Console.Write($"9. Display Arc log: "); Tools.WriteLine(logArcFileSystem.ToString(), ConsoleColor.Blue);
                    Console.Write("Press Enter to Start.");

                    key = Console.ReadKey();
                }

                string gameDataPath = Path.Combine(gamePath, "GameData");

                arcs = Tools.ListArc(gameDataPath);
            }

            // Delete unused .arc
            if (scrapArc) 
            { 
                arcs = Tools.DeleteUnused(arcs);
            }

            // Replace audio
            if (scrapAudio)
            {
                foreach (string arc in arcs)
                {
                    Console.WriteLine($"Processing {arc}");
                    ArcFile arcFile = new ArcFile(arc);

                    if (arcFile != null)
                    {
                        int i = arcFile.Process();
                        Console.WriteLine($"Number of replaced audio files: {i}");
                        bool hasChanged = i > 0;
                        if (hasChanged) { arcFile.Save(); }
                        arcFile.Clear(backupOldArc, hasChanged);
                    }
                }
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
