using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace MyMediaLocator
{
    abstract class Filetype
    {
        protected string master;
        protected string[] exts;
        public List<FileInfo> paths;

        public Filetype(string master)
        {
            this.master = master;
        }

        protected List<FileInfo> DoParse(string[] exts)
        {
            List<FileInfo> list = new List<FileInfo>();
            foreach (string file in Directory.EnumerateFiles(master, "*.*", SearchOption.AllDirectories)) //.Where(file => exts.Contains(Path.GetExtension(file).ToLower())))
            {
                try
                {
                    if (exts.Contains(Path.GetExtension(file).ToLower()))
                    {
                        FileInfo info = new FileInfo(file);
                        list.Add(info);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("UNAUTHORIZED ACCESS:" + file);
                    Console.ReadKey();
                }
            }

            return list;
        }
    }
}
