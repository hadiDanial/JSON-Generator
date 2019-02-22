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
        List<string> list;
        List<Blog> blogs;

        const string descriptionTag = "*.desc";

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
            save.ShowDialog();
            StreamWriter writer = new StreamWriter(save.OpenFile());
            writer.WriteLine(textBox.Text);
            writer.Dispose();
            writer.Close();
            }

        private void openFolderBtn_Click(object sender, RoutedEventArgs e)
            {
            FolderBrowserDialog diag = new FolderBrowserDialog();
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
                list = new List<string>();
                blogs = new List<Blog>();
                ProcessDirectory(folderPath, folderPath, true);
                textBox.Text = "[\n";
                for (int i = 0; i < blogs.Count; i++)
                    {
                    if(i == blogs.Count - 1)
                        {
                        textBox.Text += JsonConvert.SerializeObject(blogs[i]);
                        }
                    else
                        {
                    textBox.Text += JsonConvert.SerializeObject(blogs[i]) + ",\n";
                        }
                    }

                textBox.Text += "\n]";
                }
            else
                {
                MessageBoxResult result = System.Windows.MessageBox.Show("No folder selected.", "Error");
                }
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
                    list.Add(fileName.Replace(sourcePath, ""));
                    Blog b;
                    string title = fileName.Replace(sourceDirectory+"\\","").Replace("_"," ");
                    string path = fileName.Replace(sourcePath, "");
                    string descFile = fileName.Replace(".html", ".txt");
                    string desc = "";
                    if (File.Exists(descFile))
                        {
                        desc = File.ReadAllText(descFile);
                        int pFrom = desc.IndexOf(descriptionTag) + descriptionTag.Length;
                        int pTo = desc.LastIndexOf(descriptionTag);

                        desc = desc.Substring(pFrom, pTo - pFrom);
                        }
                    string[] p = path.Split('\\');
                    string cleanPath = "";
                    for (int i = 0; i < p.Length - 1; i++)
                        {
                        cleanPath += p[i] + "\\";
                        }
                    string coverImage = cleanPath + "coverImg.png";
                    b = new Blog(title, desc, path, coverImage);
                    blogs.Add(b);
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

        public void AddBlog(string newBlog)
            {
            textBox.Text.Remove(textBox.Text.Length - 1);
            textBox.Text += ", \n" + newBlog + "\n]";
            }

        }


    
    }

        
    
    
