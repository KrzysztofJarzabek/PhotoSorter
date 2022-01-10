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
    /// Logika interakcji dla klasy LibraryManagement.xaml
    /// </summary>
    public partial class LibraryManagement : Page
    {
        List<string> collectionsList = new List<string>();

        public LibraryManagement()
        {
            InitializeComponent();
            collectionsList = CollectionsLibraryFile.GetCollectionsList();
            DisplayCollectionsList();

        }

        private void collectionsLibraryListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdatePhotosListBox();
        }

        private void removeCollectionButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć kolekcję?", "Uwaga!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string temporaryPathString = GetPathFromSelectedCollection();
                if (temporaryPathString != null)
                {
                    SelectedPhotosFolder.DeletePhotosCollectionFolder(temporaryPathString);
                    CollectionsFile.DeleteCollectionFile(temporaryPathString);

                    collectionsList.Remove(temporaryPathString);
                    CollectionsLibraryFile.WriteCollectionListToLibraryFile(collectionsList);

                    DisplayCollectionsList();
                    UpdatePhotosListBox();
                }
            }
        }

        /// <summary>
        /// Cleares library list and displays current collections list.
        /// </summary>
        private void DisplayCollectionsList()
        {
            
            collectionsLibraryListBox.Items.Clear();
            foreach (var item in collectionsList)
            {
                collectionsLibraryListBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(item));
               
            }
        }

        /// <summary>
        /// Cleares and updates collectionPhotoListBox.
        /// </summary>
        private void UpdatePhotosListBox()
        {
            collectionPhotoListBox.ItemsSource = null;
            string temporaryPathString = GetPathFromSelectedCollection();
            if (temporaryPathString != null)
                collectionPhotoListBox.ItemsSource = CollectionsFile.GetCollectionPhotosNamesList(temporaryPathString);
        }

        /// <summary>
        /// Gets path to collection .txt file based on selected collection name.
        /// </summary>
        /// <returns></returns>
        private string GetPathFromSelectedCollection()
        {
            if (collectionsLibraryListBox.SelectedItem != null)
            {
                foreach (var item in collectionsList)
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(item) == collectionsLibraryListBox.SelectedItem.ToString()) return item;
                }
            }
            return null;
        }

    }

}
