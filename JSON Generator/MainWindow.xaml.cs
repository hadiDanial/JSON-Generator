using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JSON_Generator
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        string folderPath;
        List<Post> posts;

        const string titleTag = "*.title";
        const string descriptionTag = "*.desc";
        const string tagTag = "*.tags";
        const string dateTag = "*.date";
        const string version = "v.1.2";

        public MainWindow()
            {
            InitializeComponent();
            }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
            {
            AddEntry addWin = new AddEntry();
            addWin.Show();
            addWin.Closed += AddWin_Closed;
            this.IsEnabled = false;
            }

        private void AddWin_Closed(object sender, EventArgs e)
            {
            this.IsEnabled = true;
            }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
            {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON (*.json;*.txt)|*.json;*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
            {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.Filter = "JSON (*.json)|*.json|Text (*.txt)|*.txt|All files (*.*)|*.*";
            save.Title = "Save File";
            //save.ShowDialog();
            if (save.ShowDialog() == true)
                {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                writer.WriteLine(textBox.Text);
                writer.Dispose();
                writer.Close();
                }
            }

        private void openFolderBtn_Click(object sender, RoutedEventArgs e)
            {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.ShowNewFolderButton = false;
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                folderPath = diag.SelectedPath;
                //textBox.Text = "//Folder: " + folderPath + " contains:\n";
                }
            }

        private void generateBtn_Click(object sender, RoutedEventArgs e)
            {
            if (folderPath != null && folderPath != "")
                {
                posts = new List<Post>();
                ProcessDirectory(folderPath, folderPath, true);
                GenerateJSON();
                }
            else
                {
                MessageBoxResult result = System.Windows.MessageBox.Show("No folder selected.", "Error");
                }
            }

        private void GenerateJSON(bool clear = false)
            {
            if (clear) textBox.Text = "";
            textBox.Text += "[\n";
            for (int i = 0; i < posts.Count; i++)
                {
                if (i == posts.Count - 1)
                    {
                    textBox.Text += JsonConvert.SerializeObject(posts[i], Formatting.Indented);
                    }
                else
                    {
                    textBox.Text += JsonConvert.SerializeObject(posts[i], Formatting.Indented) + ",\n";
                    }
                }

            textBox.Text += "\n]";
            }

        /// <summary>
        /// This function is used to iterate through the main folder and
        /// (if applicable) the subfolders to look for *.html files to add to the JSON.
        /// </summary>
        /// <param name="sourceDirectory">Directory to iterate through.</param>
        /// <param name="processSubFolders">True if recursive teration thru subfolders is required.</param>
        public void ProcessDirectory(string sourceDirectory, string sourcePath, bool processSubFolders)
            {
            // Retrieve the names of the files found in the given folder.
            string[] fileEntries = Directory.GetFiles(sourceDirectory);
            foreach (string fileName in fileEntries)
                {
                if (fileName.ToLower().EndsWith(".html"))
                    {
                    //list.Add(fileName.Replace(sourcePath, ""));
                    Post p;
                    string title;
                    string path = fileName.Replace(sourcePath, "");
                    string descFile = fileName.Replace(".html", ".txt");
                    string desc = "";
                    string tags = "";
                    string date = "";
                    if (File.Exists(descFile))
                        {
                        string text = File.ReadAllText(descFile);
                        title = GetTaggedText(text, titleTag);
                        desc = GetTaggedText(text, descriptionTag);
                        tags = GetTaggedText(text, tagTag);
                        date = GetTaggedText(text, dateTag);
                        date.Replace(" ", "");
                        }
                    else
                        {
                        title = fileName.Replace(sourceDirectory + "\\", "").Replace("_", " ").Replace(".html", "");
                        }
                    string[] splitPath = path.Split('\\');
                    string cleanPath = "";
                    for (int i = 0; i < splitPath.Length - 1; i++)
                        {
                        cleanPath += splitPath[i] + "\\";
                        }
                    string coverImage = cleanPath + "coverImg.png";
                    p = new Post(title, desc, path, coverImage, tags, date);
                    posts.Add(p);
                    }
                }

            // Self-recursion to loop through the folder structure until
            // the folder depth has reached the recursion level value.
            if (processSubFolders)
                {
                string[] subdirEntries = Directory.GetDirectories(sourceDirectory);
                foreach (string subdir in subdirEntries)
                    {
                    if ((File.GetAttributes(subdir) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                        {
                        this.ProcessDirectory(subdir, sourcePath, processSubFolders);
                        }
                    }
                }
            }

        private static string GetTaggedText(string text, string tag)
            {
            int pFrom = text.IndexOf(tag) + tag.Length;
            int pTo = text.LastIndexOf(tag);
            string taggedText;
            if (pTo != -1)
                {
                taggedText = text.Substring(pFrom, pTo - pFrom);
                taggedText.Replace("  ", " ");
                if (taggedText[0] == ' ') taggedText.Remove(0, 1);
                if (taggedText[taggedText.Length - 1] == ' ') taggedText.Remove(taggedText.Length - 1);
                }
            else
                {
                taggedText = "";
                }

            return taggedText;
            }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
            {
            MessageBoxResult result = System.Windows.MessageBox.Show("JSON Generator loops through a directory and creates a JSON file based on the criteria defined for my personal website. \nMade by Hadi Danial, " + version, "About");
            }

        private void sortBtn_Click(object sender, RoutedEventArgs e)
            {
            if (folderPath != null && folderPath != "")
                {
                if (posts == null || posts.Count == 0)
                    {
                    MessageBoxResult result = System.Windows.MessageBox.Show("JSON is empty. Please generate.", "Error");
                    }
                else
                    {
                    List<Post> SortedList = posts.OrderByDescending(o => o.date).ToList();
                    posts = SortedList;
                    GenerateJSON(true);
                    }
                }
            else
                {
                MessageBoxResult result = System.Windows.MessageBox.Show("No folder selected.", "Error");
                }
            }
        }



    }




