using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace MyMediaLocator
{
    class Audio : Filetype
    {
        public Audio(string master) : base (master)
        {
            this.master = master;
            exts = new string[] { ".aac", ".flac", ".mp3", ".wav" };
            paths = DoParse(exts);
        }

        public static string FetchData(string path)
        {
            string info;

            try
            {
                FileInfo fileinfo = new FileInfo(path);
                TagLib.File tagfile = TagLib.File.Create(path);

                info = "\nTitle: " + tagfile.Tag.Title + "\nArtist: " + tagfile.Tag.FirstAlbumArtist + "\nAlbum: " + tagfile.Tag.Album + "\nLength: " + tagfile.Properties.Duration + "\n\nSize on disk (in bytes): " + fileinfo.Length + "\nLast modified: " + fileinfo.LastWriteTime.ToShortDateString();
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