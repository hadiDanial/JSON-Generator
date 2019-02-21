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
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace JSON_Generator
    {
    /// <summary>
    /// Interaction logic for AddEntry.xaml
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
            Blog blog = new Blog(blogTitle.Text, blogDesc.Text, coverImgName.Content.ToString());
            Preview.Text = blog.ToString();
            }
        }
    }
    
