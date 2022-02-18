using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy LibraryManagement.xaml
    /// </summary>
    public partial class LibraryManagement : Page
    {
        List<string> collectionsList = new List<string>();

        CollectionsListCreator collectionsObjectsList = new();

        public LibraryManagement()
        {
            InitializeComponent();
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
                string collectionFileCompletePath = GetPathFromSelectedCollection();
                collectionsList = CollectionsLibraryFile.GetCollectionsList();
                if (collectionFileCompletePath != null)
                {
                    SelectedPhotosFolder.DeletePhotosCollectionFolder(collectionFileCompletePath);
                    CollectionTextFile.DeleteCollectionFile(collectionFileCompletePath);

                    collectionsList.Remove(collectionFileCompletePath);
                    CollectionsLibraryFile.WriteCollectionListToLibraryFile(collectionsList);

                    DisplayCollectionsList();
                    UpdatePhotosListBox();

                    DisplayStatusInfo("Pomyślnie usunięto kolekcję.");
                }
            }
        }

        /// <summary>
        /// Cleares library list and displays current collections list.
        /// </summary>
        private void DisplayCollectionsList()
        {
            collectionsLibraryListBox.Items.Clear();

            collectionsObjectsList.UpdateAllCollectionsFromFile();
            foreach (var collection in collectionsObjectsList.collectionsList)
            {
                collectionsLibraryListBox.Items.Add(collection.collectionName);
            }
        }

        /// <summary>
        /// Cleares and updates collectionPhotoListBox.
        /// </summary>
        private void UpdatePhotosListBox()
        {
            collectionPhotoListBox.ItemsSource = null;
            collectionPhotoListBox.ItemsSource = CollectionTextFile.GetCollectionPhotosNamesList(GetPathFromSelectedCollection());

            numberOfPhotosInCollection.Text = CollectionsStatistics.CountPhotosInCollection(GetPathFromSelectedCollection()).ToString();
            sizeOfPhotosCollection.Text = CollectionsStatistics.CountSizeOfPhotosInCollection(GetPathFromSelectedCollection()).ToString() + " [MB]";
            isFolderPresent.Text = CollectionsStatistics.StringFromPhotosFolderExists(GetPathFromSelectedCollection());
        }

        /// <summary>
        /// Gets path to collection .txt file based on selected collection name.
        /// </summary>
        /// <returns></returns>
        private string GetPathFromSelectedCollection()
        {
            if (collectionsLibraryListBox.SelectedItem == null || collectionsLibraryListBox.SelectedItem.ToString() == "") return null;
            collectionsObjectsList.UpdateAllCollectionsFromFile();
            foreach (var collection in collectionsObjectsList.collectionsList)
            {
                if (collection.collectionName == collectionsLibraryListBox.SelectedItem.ToString()) return collection.collectionFileCompletePath;
            }
            return null;
        }

        private void AddExistingCollection_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string folderToAdd = OpenFolderDialog.ChooseFileDirectory();

            if (folderToAdd == null) return;
            else if (CollectionsLibraryFile.CheckIfCollectionAlreadySaved(folderToAdd + ".txt")) return;

            string collectionFileCompletePath = System.IO.Directory.GetParent(folderToAdd).ToString() + "\\" + System.IO.Path.GetFileName(folderToAdd) + ".txt";

            CollectionsLibraryFile.AddCollectionToLibraryFile(collectionFileCompletePath);
            CollectionTextFile.WriteSelectedFilesListToFile(collectionFileCompletePath, GetPhotosList(folderToAdd));
            DisplayCollectionsList();
            UpdatePhotosListBox();

            DisplayStatusInfo("Pomyślnie dodano kolekcję.");
        }

        private void DisplayStatusInfo(string message)
        {
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Interval = TimeSpan.FromSeconds(2);
            informationTextBlock.Text = message;

            messageTimer.Tick += (s, en) =>
            {
                informationTextBlock.Text = "";
                messageTimer.Stop();
            };
            messageTimer.Start();
        }

        private List<string> GetPhotosList(string photosDirectoryPath)
        {
            List<string> photosList = new List<string>();
            try
            {
                List<string> temporaryList = Directory.GetFiles(photosDirectoryPath.ToString(), "*" + ".jpg").ToList();
                foreach (var item in temporaryList)
                {
                    photosList.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong directory!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return photosList;
        }

        private void createCollectionFolderBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string temporaryCollectionFileCompletePath = GetPathFromSelectedCollection();

            if (!CollectionsStatistics.CheckIfPhotosFolderExists(temporaryCollectionFileCompletePath))
            {
                SelectedPhotosFolder.CreateSelectedPhotosFolder(temporaryCollectionFileCompletePath, System.IO.Path.GetFileNameWithoutExtension(temporaryCollectionFileCompletePath));
                DisplayStatusInfo("Utworzono folder ze zdjęciami.");
                UpdatePhotosListBox();
            }
            else DisplayStatusInfo("Folder ze zdjęciami kolekcji już istnieje.");
        }

        private void deleteCollectionFolderBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string temporaryCollectionFileCompletePath = GetPathFromSelectedCollection();

            if (CollectionsStatistics.CheckIfPhotosFolderExists(temporaryCollectionFileCompletePath) && temporaryCollectionFileCompletePath != null)
            {
                SelectedPhotosFolder.DeletePhotosCollectionFolder(temporaryCollectionFileCompletePath);
                DisplayStatusInfo("Usunięto folder ze zdjęciami.");
                UpdatePhotosListBox();
            }
            else DisplayStatusInfo("Folder ze zdjęciami nie istnieje.");
        }

    }

}
