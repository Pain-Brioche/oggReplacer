using System;
using System.IO;
using System.Collections.Generic;
using CM3D2.Toolkit.Guest4168Branch.Arc;


namespace ArcScrapper
{
    internal class ArcFile
    {
        private ArcFileSystem arc = new ArcFileSystem();

        private string FilePath { get; set; }
        private string FileName { get; set; }
        private string TempPath { get; set; }


        internal ArcFile(string path)
        {
            if (Program.logArcFileSystem)
            {
                arc.Logger = Program.logger;
            }            

            FilePath = path;
            FileName = Path.GetFileName(path);
            TempPath = $"{FilePath}_temp";
            arc.LoadArc(path);
        }

        public int Process()
        {
            int replacedFiles = 0;
            Directory.CreateDirectory(FilePath + "_temp");

            foreach (KeyValuePair<string, CM3D2.Toolkit.Guest4168Branch.Arc.Entry.ArcFileEntry> kvp in this.arc.Files)
            {
                string name = kvp.Value.Name;
                if (Path.GetExtension(name) != ".ogg") { continue; }

                string tempFilePath = Path.Combine(TempPath, name);

                File.Copy(Program.blankOgg, tempFilePath, true);

                arc.LoadFile(tempFilePath, kvp.Value.Parent);

                replacedFiles++;                
            }

            return replacedFiles;
        }

        public void Save()
        {
            System.Threading.Thread.Sleep(1000);

            using (Stream stream = File.Open(FilePath + ".new", FileMode.CreateNew))
            {
                arc.Save(stream);
            }
        }

        public void Clear(bool isBackup, bool hasChanged)
        {
            try
            {
                if (Directory.Exists(TempPath))
                {
                    Directory.Delete(TempPath, true);
                }
                if (hasChanged)
                {
                    if (File.Exists(FilePath))
                    {
                        if (isBackup)
                        {
                            File.Move(FilePath, FilePath + ".bak");
                        }
                        else
                        {
                            File.Delete(FilePath);
                        }
                    }
                }

                if (File.Exists(FilePath + ".new"))
                {
                    File.Move(FilePath + ".new", FilePath);
                }

            }
            catch(Exception ex)
            {
                System.Threading.Thread.Sleep(500);
                Clear(isBackup, hasChanged);
            }

            arc.Clear();
        }
    }
}
