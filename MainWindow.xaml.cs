using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Alg_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //variables
        public int fileCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //string path = @"C:\Users\Waqas\source\repos\Alg_03\.gitignore";
            //using (StreamReader sr = File.OpenText(path))
            //{
            //    string s;
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(s);
            //    }
            //    string text = System.IO.File.ReadAllText(path);
            //    MessageBox.Show(count.ToString());
            //}
        }

        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            fileCount++;
            string[] fileArray = new string[10];
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    lbFiles.Items.Add(System.IO.Path.GetFileName(filename));
                    fileArray[fileCount] = System.IO.Path.GetFullPath(filename);
                }
            }
            MessageBox.Show(System.IO.File.ReadAllText(fileArray[fileCount]));
        }
    }
}
