using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMediaLocator
{
    class SaveHandler
    {
        public string path;
        private StringBuilder csv;

        public SaveHandler()
        {
            path = null;
            csv = new StringBuilder();
        }

        public Boolean SavePrep()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".csv",
                Filter = "Comma Separated Values (.csv)|*.csv",
                FileName = path
            };

            if (path != null)
            {
                saveFileDialog.FileName = Path.GetFileName(path);
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(path);
            }

            else
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog.FileName;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean SaveDocument(List<FileInfo> list)
        {
            if (!File.Exists(path))
            { 
                var line = "Filename,Size on disk,Date last modified,File path";
                csv.AppendLine(line);
            }

            foreach (FileInfo file in list)
            {
                string name = file.Name;
                string size = file.Length.ToString();
                string date = file.LastWriteTime.ToShortDateString();
                string fp = file.FullName;

                var line = $"{name},{size},{date},{fp}";
                csv.AppendLine(line);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(csv.ToString());
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
