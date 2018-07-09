using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace MyMediaLocator
{
    class Video : Filetype
    {
        public Video(string master) : base (master)
        {
            this.master = master;
            exts = new string[] { ".mov", ".mp4", ".m4v" };
            paths = DoParse(exts);
        }

        public static string FetchData(string path)
        {
            string info;

            try
            {
                FileInfo fileinfo = new FileInfo(path);
                TagLib.File tagfile = TagLib.File.Create(path);

                info = "\nTitle: " + fileinfo.Name + "\nCreator: " + tagfile.Tag.FirstAlbumArtist + "\n\nSize on disk (in bytes): " + fileinfo.Length + "\nLast modified: " + fileinfo.LastWriteTime.ToShortDateString();
            }
            catch (UnauthorizedAccessException)
            {
                return $"UNAUTHORIZED ACCESS: {path}";
            }
            catch (UnsupportedFormatException)
            {
                return $"UNSUPPORTED FILETYPE: {path}";
            }

            return info;
        }
    }
}
