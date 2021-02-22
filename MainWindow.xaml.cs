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

namespace MyPaint_V1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DirectoryInfo dInfo = new DirectoryInfo(@"C:\Users\enesi\Desktop");
            DirTreeView.Items.Add(verzeichnis(dInfo));
            

        }

        private TreeViewItem verzeichnis(DirectoryInfo directoryInfo)
        {
            TreeViewItem dI = new TreeViewItem();
            dI.Header = directoryInfo.Name;
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                dI.Items.Add(verzeichnis(directory));
            }

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".PNG" || file.Extension == ".JPG")
                {
                    TreeViewItem fi = new TreeViewItem();
                    fi.Header = file.Name;

                    fi.Selected += (sender, e) => SelectEventHandler(sender, e, file.FullName);

                    dI.Items.Add(fi);
                }
            }
            return dI;
        }
        private void SelectEventHandler(object sender, RoutedEventArgs e, string path)
        {
           //Damit sollte es funktioniern, aber es tut es nicht.
            
            BilderView.Source = new BitmapImage(new Uri(path, UriKind.Absolute));

        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                Uri fileUri = new Uri(openFileDialog.FileName);
                BilderView.Source = new BitmapImage(fileUri);
            }
        }
    }
}
