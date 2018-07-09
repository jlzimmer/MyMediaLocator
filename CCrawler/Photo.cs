using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace MyMediaLocator
{
    class Photo : Filetype
    {
        public Photo(string master) : base (master)
        {
            this.master = master;
            exts = new string[] { ".gif", ".jpeg", ".jpg", ".png" };
            paths = DoParse(exts);
        }

        public static string FetchData(string path)
        {
            string info;

            try
            {
                FileInfo fileinfo = new FileInfo(path);
                TagLib.File tagfile = TagLib.File.Create(path);
                var image = (TagLib.Image.File)tagfile;

                info = "\nTitle: " + fileinfo.Name + "\nDimensions: " + image.Properties.PhotoWidth + "x" + image.Properties.PhotoHeight + "\n\nSize on disk (in bytes): " + fileinfo.Length + "\nLast modified: " + fileinfo.LastWriteTime.ToShortDateString();
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
