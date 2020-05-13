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
        int fileCount = 0;
        string[] fileArray = new string[10];

        public MainWindow()
        {
            InitializeComponent();
        }

		internal void KMPSearch(string pat, string txt, int row)
		{
			int M = pat.Length;
			int N = txt.Length;

			// create lps[] that will hold the longest 
			// prefix suffix values for pattern 
			int[] lps = new int[M];
			int j = 0; // index for pat[] 

			// Preprocess the pattern (calculate lps[] 
			// array) 
			computeLPSArray(pat, M, lps);

			int i = 0; // index for txt[] 
			while (i < N)
			{
				if (pat[j] == txt[i])
				{
					j++;
					i++;
				}
				if (j == M)
				{
					txtResults.Text += "column: " + (i - j) + "\nrow: " + row + "\n";
					j = lps[j - 1];
				}

				// mismatch after j matches 
				else if (i < N && pat[j] != txt[i])
				{
					// Do not match lps[0..lps[j-1]] characters, 
					// they will match anyway 
					if (j != 0)
						j = lps[j - 1];
					else
						i = i + 1;
				}
			}
		}

		void computeLPSArray(string pat, int M, int[] lps)
		{
			// length of the previous longest prefix suffix 
			int len = 0;
			int i = 1;
			lps[0] = 0; // lps[0] is always 0 

			// the loop calculates lps[i] for i = 1 to M-1 
			while (i < M)
			{
				if (pat[i] == pat[len])
				{
					len++;
					lps[i] = len;
					i++;
				}
				else // (pat[i] != pat[len]) 
				{
					// This is tricky. Consider the example. 
					// AAACAAAA and i = 7. The idea is similar 
					// to search step. 
					if (len != 0)
					{
						len = lps[len - 1];

						// Also, note that we do not increment 
						// i here 
					}
					else // if (len == 0) 
					{
						lps[i] = len;
						i++;
					}
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
        {
			for (int i = 0; i < fileCount; i++)
			{
				string[] lines = System.IO.File.ReadAllLines(@fileArray[fileCount]);
				string fileName = System.IO.Path.GetFileName(fileArray[fileCount]);
				string pat = txtToSearch.Text;
				int j = 0;
				txtResults.Text += "\n" + fileName + ": \n";
				foreach (string line in lines)
				{
					j++;
					KMPSearch(pat, line, j);
				}
			}

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
            //MessageBox.Show(System.IO.File.ReadAllText(fileArray[fileCount]));
        }
    }
}

