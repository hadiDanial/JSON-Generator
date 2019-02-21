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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON (*.json;*.txt)|*.json;*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }
    
