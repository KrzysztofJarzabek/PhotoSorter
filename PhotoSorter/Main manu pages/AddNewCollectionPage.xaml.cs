using Microsoft.Win32;
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

namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy AddNewCollection.xaml
    /// </summary>
    public partial class AddNewCollection : Page
    {
        public static bool createNewFolderChecked = false;

        public AddNewCollection()
        {
            InitializeComponent();
        }

        private void createFolderButton_Checked(object sender, RoutedEventArgs e)
        {
            createNewFolderChecked = true;
        }

        private void startSessionButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PhotoViewer photoViewerWindow = new PhotoViewer(mainFolderLocalizationTextBox.Text, collectionNameTextBox.Text);
            CollectionsLibraryFile.AddCollectionToLibraryFile(mainFolderLocalizationTextBox.Text + "\\" + collectionNameTextBox.Text + ".txt");

            photoViewerWindow.Show();
        }

        private void collectionNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            collectionNameTextBox.Clear();
        }

        private void mainFolderLocalizationTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            mainFolderLocalizationTextBox.Clear();
            mainFolderLocalizationTextBox.Text = OpenFolderDialog.ChooseFileDirectory();
        }
    }
}
