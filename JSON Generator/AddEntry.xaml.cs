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
using Newtonsoft.Json;

namespace JSON_Generator
    {
    /// <summary>
    /// Not used anymore, keeping it around in case I decide to add manual entry later. For now, it can't be accessed from the main window.
    /// </summary>
    public partial class AddEntry : Window
        {
        public AddEntry()
            {
            InitializeComponent();
            }

        private void coverImgBtn_Click(object sender, RoutedEventArgs e)
            {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Picture (*.png;*.jpg; *.svg)|*.png;*.jpg; *.svg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                coverImgName.Content = openFileDialog.FileName;
            }

        private void addBlogBtn_Click(object sender, RoutedEventArgs e)
            {
            //Post blog = new Post(blogTitle.Text, blogDesc.Text, coverImgName.Content.ToString());
            //Preview.Text = JsonConvert.SerializeObject(blog);
                //blog.ToString();
            }
        }
    }
    
