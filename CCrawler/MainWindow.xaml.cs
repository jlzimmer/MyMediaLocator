using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MyMediaLocator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string dirPath = null;
        int selection = 0;
        List<FileInfo> data = new List<FileInfo>();
        SaveHandler csv = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dirPath = dialog.SelectedPath.ToString();
                Path.Text = dirPath;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Filenames.Items.Clear();
            data.Clear();

            if (dirPath == null)
            {
                System.Windows.Forms.MessageBox.Show("Please select a valid directory to crawl through for file data.", "Directory error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            selection = MediaType.SelectedIndex;
            switch (selection)
            {
                case 0:
                    System.Windows.Forms.MessageBox.Show("Please select a datatype from the drop down menu.", "Filetype error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                case 1:
                    Audio audio = new Audio(dirPath);
                    data.AddRange(audio.paths);
                    break;

                case 2:
                    Photo photo = new Photo(dirPath);
                    data.AddRange(photo.paths);
                    break;

                case 3:
                    Video video = new Video(dirPath);
                    data.AddRange(video.paths);
                    break;
            }

            foreach (FileInfo path in data)
            {
                Filenames.Items.Add(path.Name);
            }

            header.SelectedIndex = 1;
            Filenames.SelectedIndex = 0;
            dirPath = null;
        }

        private void Filenames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Filenames.SelectedIndex < 0)
            {
                return;
            }

            string path = data[Filenames.SelectedIndex].FullName;
            switch (selection)
            {
                case 0:
                    return;

                case 1:
                    Fileinfo.Text = Audio.FetchData(path);
                    break;

                case 2:
                    Fileinfo.Text = Photo.FetchData(path);
                    break;

                case 3:
                    Fileinfo.Text = Video.FetchData(path);
                    break;
            }
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            Filenames.Items.Clear();

            int order = SortOrder.SelectedIndex;

            switch (order)
            {
                case 0:
                    System.Windows.Forms.MessageBox.Show("Please select a sort order from the drop down menu.", "Sort error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                case 1:
                    data = data.OrderBy(x => x.Name).ToList();
                    break;

                case 2:
                    data = data.OrderBy(x => x.Length).ToList();
                    break;

                case 3:
                    data = data.OrderBy(x => x.LastWriteTime).ToList();
                    break;
            }

            foreach (FileInfo path in data)
            {
                Filenames.Items.Add(path.Name);
            }
        }

        private void SaveSelect_Click(object sender, RoutedEventArgs e)
        {
            if (csv == null)
            {
                csv = new SaveHandler();
            }

            if (!csv.SavePrep())
            {
                System.Windows.Forms.MessageBox.Show("There was an issue finding a suitable save location.", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SavePath.Text = csv.path;
            Save.IsEnabled = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(!csv.SaveDocument(data))
            {
                System.Windows.Forms.MessageBox.Show("There was an issue writing to the selected save location. \nPlease select a new save path.", "Save error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Save.IsEnabled = false;
                return;
            }

            System.Windows.Forms.MessageBox.Show($"The file was saved in CSV format at location \n{csv.path}", "Save successful!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            header.SelectedIndex = 0;
        }
    }
}