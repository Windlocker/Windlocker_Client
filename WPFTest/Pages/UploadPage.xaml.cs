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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest.Pages
{
    /// <summary>
    /// UploadPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UploadPage : Page
    {
        private static List<string> Files = new List<string>();
        public UploadPage()
        {
            InitializeComponent();
        }

        private void FileListBox_PreviewDragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if(e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop,true))
            {
                string[] filenames = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop, true);
                foreach(var filename in filenames)
                {
                    if (File.Exists(filename) == false)
                    {
                        break;
                    }
                    else
                    {
                        if (Files.Any(ev => ev.EndsWith(filename)) == true) { break; }
                        else
                        {
                            Files.Add(filename);
                            FileListBox.Items.Add(filename);
                        }
                        Console.WriteLine(filename);
                    }
                }
            }
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog()
            {
                Title = "Select a File"
            };
            DialogResult dr = o.ShowDialog();
            if(dr == DialogResult.OK)
            {
                foreach(var f in o.FileNames)
                {
                    if (Files.Any(ev => ev.EndsWith(f)) == true) { break; }
                    else
                    {
                        Files.Add(f);
                        FileListBox.Items.Add(f);
                    }
                    //중복체크도 필요
                }
            }
        }
    }
}
