using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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


namespace PhotoSorter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // PhotoViewer photoViewerWindow = new PhotoViewer();
            //    photoViewerWindow.Show();

        }


        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tabsDisplayFrame.Content = new AddNewCollection();
        }

        private void ListViewItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            tabsDisplayFrame.Content = new LibraryManagement();
        }

        private void exitButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }

}
