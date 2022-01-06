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
using System.Windows.Shapes;

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
            if (createNewFolderStatus) SelectedPhotosFolder.CreateSelectedPhotosFolder(textFilePath, newFolderName);
            Close();
        }

        private void imageAddButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddFileToSelectedFilesList();
            CollectionFile.WriteSelectedFilesListToFile(textFilePath, selectedFilesList);
        }

        private void removeImageButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RemoveFileFromSelectedFilesList();
            CollectionFile.WriteSelectedFilesListToFile(textFilePath, selectedFilesList);
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
            statusTextBlock.Text = string.Empty;
            statusTextBlock.Text = messageText;
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

        private void DisplayPhoto(int numberOfFileInFolder)
        {
            BitmapImage currentImage = new BitmapImage();
            currentImage.BeginInit();
            currentImage.UriSource = new Uri(filesList.GetValueOrDefault(numberOfFileInFolder));
            currentImage.EndInit();
            pictureBox.Source = currentImage;

            Title = System.IO.Path.GetFileName(filesList.GetValueOrDefault(numberOfFileInFolder)).ToString();
        }


    }
}
