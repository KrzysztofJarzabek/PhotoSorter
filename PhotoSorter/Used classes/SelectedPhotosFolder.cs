using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoSorter
{
    public static class SelectedPhotosFolder
    {

        internal static string directoryFolder;
        internal static string newDirectoryFolder;
        internal static string fileSourceName;
        internal static string fileDestinationName;

        /// <summary>
        /// Creates folder with copy of collection photos. TextFilePath is the complete path to collection .txt file.
        /// </summary>
        /// <param name="textFilePath"></param>
        /// <param name="newFolderName"></param>
        public static void CreateSelectedPhotosFolder(string textFilePath, string newFolderName)
        {
            CreateNewFolder(textFilePath, newFolderName);
            CopyPhotosToNewFolder(textFilePath);

            //zabezpieczenie jak wcześniejszy etap nie wyjdzie?
        }

        /// <summary>
        /// Removes folder with photos (if exists) from indicated collection.
        /// </summary>
        /// <param name="collectionFilePath"></param>
        public static void DeletePhotosCollectionFolder(string collectionFilePath)
        {
            string photosFolderDirectory = System.IO.Directory.GetParent(collectionFilePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(collectionFilePath);
            if (System.IO.Directory.Exists(photosFolderDirectory)) System.IO.Directory.Delete(photosFolderDirectory, true);
        }

        /// <summary>
        /// Gets photos names from collection .txt file from path txtFilePath.
        /// </summary>
        /// <param name="textFilePath"></param>
        /// <returns></returns>
        public static List<string> GetPhotosNamesFromCollection(string textFilePath)
        {
            StreamReader namesReader = new(textFilePath);
            List<string> selectedFilesList = new();

            do
            {
                string temporaryString = namesReader.ReadLine();

                if (temporaryString == null) break;

                selectedFilesList.Add(temporaryString);
            } while (true);
            namesReader.Close();

            return selectedFilesList;
        }

        private static void CreateNewFolder(string textFilePath, string newFolderName)
        {
            directoryFolder = System.IO.Directory.GetParent(textFilePath).ToString();
            newDirectoryFolder = directoryFolder + "\\" + newFolderName;
            try
            {
                System.IO.Directory.CreateDirectory(newDirectoryFolder);
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można utworzyć folderu z wybranymi zdjęciami", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void CopyPhotosToNewFolder(string textFilePath)
        {
            List<string> selectedFilesList = new();
            selectedFilesList = GetPhotosNamesFromCollection(textFilePath);

            foreach (var item in selectedFilesList)
            {
                fileSourceName = directoryFolder + "\\" + item.ToString();
                fileDestinationName = newDirectoryFolder + "\\" + item.ToString();
                try
                {
                    System.IO.File.Copy(fileSourceName, fileDestinationName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie można skopiować plików!", "Uwaga!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
