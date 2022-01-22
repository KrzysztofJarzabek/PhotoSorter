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
        CollectionsStatictics collectionsStatictics = new();

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

            numberOfPhotosInCollection.Text = CollectionsStatictics.CountPhotosInCollection(GetPathFromSelectedCollection()).ToString();
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

            if (folderToAdd == null) return;
            else if (CollectionsLibraryFile.CheckIfCollectionAlreadySaved(folderToAdd + ".txt")) return;

            string textFilePath = System.IO.Directory.GetParent(folderToAdd).ToString() + "\\" + System.IO.Path.GetFileName(folderToAdd) + ".txt";
            CollectionsLibraryFile.AddCollectionToLibraryFile(textFilePath);
            CollectionsFile.WriteSelectedFilesListToFile(textFilePath, GetPhotosList(folderToAdd));
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

        private void createCollectionPhotosFolder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string temporaryPathString = GetPathFromSelectedCollection();
            bool folderExists = !System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(temporaryPathString) + "\\" + System.IO.Path.GetFileNameWithoutExtension(temporaryPathString));
            if (folderExists)
            {
                SelectedPhotosFolder.CreateSelectedPhotosFolder(temporaryPathString, System.IO.Path.GetFileNameWithoutExtension(temporaryPathString));
                DisplayStatusInfo("Utworzono folder ze zdjęciami.");
            }
            else DisplayStatusInfo("Folder ze zdjęciami kolekcji już istnieje.");
        }

        private void deleteCollectionPhotosFolder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string temporaryPathString = GetPathFromSelectedCollection();
            bool folderExists = System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(temporaryPathString) + "\\" + System.IO.Path.GetFileNameWithoutExtension(temporaryPathString));
            if (folderExists && temporaryPathString != null)
            {
                SelectedPhotosFolder.DeletePhotosCollectionFolder(temporaryPathString);
                DisplayStatusInfo("Usunięto folder ze zdjęciami.");
            }
            else DisplayStatusInfo("Folder ze zdjęciami nie istnieje.");
        }

    }

}
