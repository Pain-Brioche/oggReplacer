using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArcScrapper
{
    internal class Tools
    {
        internal static string GetGamePath()
        {
            Console.Write("Enter COM3D2.exe or CM3D2.exe path: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string path = Console.ReadLine();
            Console.ResetColor();
            string exePathCOM = Path.Combine(path, "COM3D2.exe");
            string exePathCM = Path.Combine(path, "CM3D2.exe");

            if (!File.Exists(exePathCOM) && !File.Exists(exePathCM))
            {
                Tools.WriteLine($"Can't find COM3D2.exe or CM3D2.exe, please check", ConsoleColor.Red);
                path = GetGamePath();
            }

            return path;
        }

        internal static List<string> ListArc(string path)
        {
            List<string> arcList = Directory.GetFiles(path, "*.arc", SearchOption.AllDirectories).ToList();
            return arcList;
        }

        internal static List<string> DeleteUnused(List<string> arcs)
        {
            List<string> toDelete = new List<string>();
            string originalArc;

            foreach (string arc in arcs)
            {
                if (!arc.EndsWith("_2.arc")) { continue; }

                originalArc = arc.Replace("_2", "");

                if (!File.Exists(originalArc))
                {
                    Console.WriteLine($"{Path.GetFileName(arc)} has no {Path.GetFileName(originalArc)} corresponding, it will be deleted.");
                    toDelete.Add(arc);
                }
            }

            foreach (string arc in toDelete)
            {
                File.Delete(arc);
                arcs.Remove(arc);
            }

            return arcs;
        }

        internal static void WriteLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        internal static void Write(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ResetColor();
        }
    }
}
