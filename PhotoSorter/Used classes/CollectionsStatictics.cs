using System;
using System.Collections.Generic;
using System.IO;


namespace PhotoSorter
{
    public class CollectionsStatictics
    {
        //Zwrócenie tablicy wszystkich kolekcji
        //liczba zdjęć w kolekcji
        //rozmiar danej kolekcji, 
        //czy folder istnieje czy nie

        public int quantityOfPhotosInColleciton;
        public float sizeOfCollection;
        public bool isFolderPresent;

        public CollectionsStatictics() { }
        public CollectionsStatictics(string collectionLibraryPath)
        {

        }

        /// <summary>
        /// Counts photos quantity in collection. textFilePath is the complete path to .txt file.
        /// </summary>
        /// <param name="textFilePath"></param>
        /// <returns></returns>
        public static int CountPhotosInCollection(string textFilePath)
        {
            if (textFilePath == null) return 0;

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollection(textFilePath);
            return selectedFilesList.Count;
        }

        /// <summary>
        /// Counts the complete size [MB] of photos in collection. textFilePath is the complete path to .txt file.
        /// </summary>
        /// <param name="textFilePath"></param>
        /// <returns></returns>
        public static double CountSizeOfPhotosInCollection(string textFilePath)
        {
            if (textFilePath == null) return 0;

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollection(textFilePath);
            string picturesPath = System.IO.Directory.GetParent(textFilePath).ToString();
            double pictureCompleteSize = 0.00D;

            foreach (var picture in selectedFilesList)
            {
                FileInfo fileInfo = new(picturesPath + "\\" + picture);
                pictureCompleteSize += (double)fileInfo.Length / 1048576;
            }
            return Math.Round(pictureCompleteSize, 2);
        }

        public static bool CheckIfPhotosFolderExists(string textFilePath)
        {
            if (textFilePath == null) return false;
            string folderPath = System.IO.Directory.GetParent(textFilePath).ToString() + "\\" + System.IO.Path.GetFileNameWithoutExtension(textFilePath);
            return System.IO.Directory.Exists(folderPath);
        }

        public static string StringFromPhotosFolderExists(bool photosFolderExists)
        {
            if (photosFolderExists) return "Istnieje";
            else return "Nie istnieje.";
        }


    }
}
