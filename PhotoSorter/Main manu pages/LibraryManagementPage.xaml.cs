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
            //collectionsList = CollectionsLibraryFile.GetCollectionsList();
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
            collectionsList = CollectionsLibraryFile.GetCollectionsList();
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
            collectionPhotoListBox.ItemsSource = CollectionsFile.GetCollectionPhotosNamesList(GetPathFromSelectedCollection());
        }

        /// <summary>
        /// Gets path to collection .txt file based on selected collection name.
        /// </summary>
        /// <returns></returns>
        private string GetPathFromSelectedCollection()
        {
            if (collectionsLibraryListBox.SelectedItem == null || collectionsLibraryListBox.SelectedItem.ToString() == "") return null;
            foreach (var item in collectionsList)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(item) == collectionsLibraryListBox.SelectedItem.ToString()) return item;
            }
            return null;
        }

        private void AddExistingCollection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string folderToAdd = OpenFolderDialog.ChooseFileDirectory();
            if (CollectionsLibraryFile.CheckIfCollectionAlreadySaved(folderToAdd + ".txt")) return;

            string textFilePath = System.IO.Directory.GetParent(folderToAdd).ToString() + "\\" + System.IO.Path.GetFileName(folderToAdd) + ".txt";
            CollectionsLibraryFile.AddCollectionToLibraryFile(textFilePath);
            CollectionsFile.WriteSelectedFilesListToFile(textFilePath, GetPhotosList(folderToAdd));
            DisplayCollectionsList();
            UpdatePhotosListBox();
        }

        private List<string> GetPhotosList(string photosDirectoryPath)
        {
            List<string> photosList = new List<string>();
            try
            {
                //można dodać opcje wyboru typu rozszerzenia
                int i = 1;
                List<string> temporaryList = Directory.GetFiles(photosDirectoryPath.ToString(), "*" + ".jpg").ToList();

                foreach (var item in temporaryList)
                {
                    photosList.Add(item);
                    i++;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Wrong directory!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return photosList;
        }

        //test

    }

}
