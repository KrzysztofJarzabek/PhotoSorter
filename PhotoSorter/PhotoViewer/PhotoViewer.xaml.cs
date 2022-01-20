using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class PhotoViewer : Window
    {
        public Dictionary<int, string> filesList = new Dictionary<int, string>();
        public List<string> selectedFilesList = new List<string>();
        internal string directoryPath;
        internal string textFilePath;
        internal string newFolderName;
        internal bool createNewFolderStatus;
        internal int currentOpenedFile;
        internal double collectionDataSize = 0.0F;

        public PhotoViewer(string photosPath, string collectionName)
        {
            InitializeComponent();

            directoryPath = photosPath;
            newFolderName = collectionName;
            textFilePath = photosPath + "\\" + collectionName + ".txt"; // do zmiany
            createNewFolderStatus = AddNewCollection.createNewFolderChecked;
            currentOpenedFile = 1;

            FindFilesInDirectory(ref filesList);
            DisplayPhoto(currentOpenedFile);
        }


        private void exitButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CollectionsFile.WriteSelectedFilesListToFile(textFilePath, selectedFilesList);
            Close();
            if (createNewFolderStatus) SelectedPhotosFolder.CreateSelectedPhotosFolder(textFilePath, newFolderName);
        }

        private void imageAddButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddFileToSelectedFilesList();
            CollectionsFile.WriteSelectedFilesListToFile(textFilePath, selectedFilesList);
        }

        private void removeImageButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RemoveFileFromSelectedFilesList();
            CollectionsFile.WriteSelectedFilesListToFile(textFilePath, selectedFilesList);
        }

        private void AddFileToSelectedFilesList()
        {
            if (!selectedFilesList.Contains(filesList.GetValueOrDefault(currentOpenedFile)))
            {
                selectedFilesList.Add(filesList.GetValueOrDefault(currentOpenedFile));
                selectedFilesList.Sort();

                DisplayStatus("Dodano: " + System.IO.Path.GetFileName(filesList.GetValueOrDefault(currentOpenedFile)));
            }
            else DisplayStatus("Zdjęcie już istnieje na liście!");
        }

        private void RemoveFileFromSelectedFilesList()
        {
            if (selectedFilesList.Contains(filesList.GetValueOrDefault(currentOpenedFile)))
            {
                selectedFilesList.Remove(filesList.GetValueOrDefault(currentOpenedFile));
                selectedFilesList.Sort();
                DisplayStatus("Usunięto: " + System.IO.Path.GetFileName(filesList.GetValueOrDefault(currentOpenedFile)));
            }
            else DisplayStatus("Zdjęcia nie ma na liście wybranych zdjęć!");
        }

        private void FindFilesInDirectory(ref Dictionary<int, string> filesList)
        {
            try
            {
                //można dodać opcje wyboru typu rozszerzenia
                int i = 1;
                List<string> temporaryList = Directory.GetFiles(directoryPath.ToString(), "*" + ".jpg").ToList();

                foreach (var item in temporaryList)
                {
                    filesList.Add(i, item);
                    i++;
                }
                DisplayStatus("Folder załadowano poprawnie!");
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong directory!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DisplayStatus(string messageText)
        {
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Interval = TimeSpan.FromSeconds(2);
            statusTextBlock.Text = messageText; ;

            messageTimer.Tick += (s, en) =>
            {
                statusTextBlock.Text = string.Empty;
                messageTimer.Stop();
            };
            messageTimer.Start();
        }

        private void nextImageButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (currentOpenedFile < filesList.Count) currentOpenedFile++;
            DisplayPhoto(currentOpenedFile);
        }

        private void previousImageButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (currentOpenedFile > 1) currentOpenedFile--;
            DisplayPhoto(currentOpenedFile);
        }

        private void DisplayPhoto(int currentOpenedFile)
        {
            BitmapImage currentImage = new BitmapImage();
            currentImage.BeginInit();
            currentImage.UriSource = new Uri(filesList.GetValueOrDefault(currentOpenedFile));
            currentImage.EndInit();
            pictureBox.Source = currentImage;

            Title = System.IO.Path.GetFileName(filesList.GetValueOrDefault(currentOpenedFile)).ToString();
        }


        //long length = new System.IO.FileInfo(filesList.GetValueOrDefault(currentOpenedFile)).Length;
        //collectionDataSize = length / 1048576;
        //        MessageBox.Show(collectionDataSize.ToString(), "Uwaga");
    }
}
