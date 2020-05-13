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

    public partial class MainWindow : Window
    {
        int fileCount = 0;
        string[] fileArray = new string[10];
		bool checkCase = false;

        public MainWindow()
        {
            InitializeComponent();
        }

		char convertPatToUppercase(int index, string pat)
		{
			char result = pat[index];
			Char.ToUpper(result);
			return result;
		}

		internal void KMPSearchwithCase(string pat, string txt, int row)
		{
			int M = pat.Length;
			int N = txt.Length;

			int[] lps = new int[M];
			int j = 0;

			computeLPSArray(pat, M, lps);

			int i = 0;
			while (i < N)
			{
				if (pat[j] == txt[i] || pat[j] == Char.ToUpper(txt[i]) || pat[j] == Char.ToLower(txt[i]) || Char.ToLower(pat[j]) == txt[i])
				{
					j++;
					i++;
				}
				if (j == M)
				{
					txtResults.Text += "column: " + (i - j) + "\nrow: " + row + "\n";
					j = lps[j - 1];
				}

				else if (i < N && pat[j] != txt[i])
				{
					if (j != 0)
						j = lps[j - 1];
					else
						i = i + 1;
				}
			}
		}

		internal void KMPSearch(string pat, string txt, int row)
		{
			int M = pat.Length;
			int N = txt.Length;

			int[] lps = new int[M];
			int j = 0;

			computeLPSArray(pat, M, lps);

			int i = 0; 
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
 
				else if (i < N && pat[j] != txt[i])
				{
					if (j != 0)
						j = lps[j - 1];
					else
						i = i + 1;
				}
			}
		}

		void computeLPSArray(string pat, int M, int[] lps)
		{
			int len = 0;
			int i = 1;
			lps[0] = 0; 

			while (i < M)
			{
				if (pat[i] == pat[len])
				{
					len++;
					lps[i] = len;
					i++;
				}
				else
				{
					if (len != 0)
					{
						len = lps[len - 1];
					}
					else
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
				checkCase = (bool)matchCase.IsChecked;
				foreach (string line in lines)
				{
					j++;
					if (checkCase) { KMPSearch(pat, line, j); }
					else { KMPSearchwithCase(pat, line, j); }
				}
			}
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
        }

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			txtResults.Text = String.Empty;
		}
	}
}

