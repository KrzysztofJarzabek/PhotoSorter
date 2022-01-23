using System;
using System.Collections.Generic;
using System.IO;


namespace PhotoSorter
{
    public class CollectionsStatistics
    {
        //Zwrócenie tablicy wszystkich kolekcji
        //liczba zdjęć w kolekcji
        //rozmiar danej kolekcji, 
        //czy folder istnieje czy nie

        public int quantityOfPhotosInColleciton;
        public float sizeOfCollection;
        public bool isFolderPresent;

        public CollectionsStatistics() { }
        public CollectionsStatistics(string collectionLibraryPath)
        {

        }

        /// <summary>
        /// Counts photos quantity in collection. collectionFileCompletePath is the complete path to .txt file.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        /// <returns></returns>
        public static int CountPhotosInCollection(string collectionFileCompletePath)
        {
            if (collectionFileCompletePath == null) return 0;

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollection(collectionFileCompletePath);
            return selectedFilesList.Count;
        }

        /// <summary>
        /// Counts the complete size [MB] of photos in collection. collectionFileCompletePath is the complete path to .txt file.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        /// <returns></returns>
        public static double CountSizeOfPhotosInCollection(string collectionFileCompletePath)
        {
            if (collectionFileCompletePath == null) return 0;

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollection(collectionFileCompletePath);
            string photosPath = System.IO.Directory.GetParent(collectionFileCompletePath).ToString();
            double photosCompleteSize = 0.00D;

            foreach (var picture in selectedFilesList)
            {
                FileInfo fileInfo = new(photosPath + "\\" + picture);
                photosCompleteSize += (double)fileInfo.Length / 1048576;
            }
            return Math.Round(photosCompleteSize, 2);
        }

        public static bool CheckIfPhotosFolderExists(string collectionFileCompletePath)
        {
            if (collectionFileCompletePath == null) return false;
            string folderPath = System.IO.Directory.GetParent(collectionFileCompletePath).ToString() + "\\" + System.IO.Path.GetFileNameWithoutExtension(collectionFileCompletePath);
            return System.IO.Directory.Exists(folderPath);
        }

        public static string StringFromPhotosFolderExists(bool photosFolderExists)
        {
            if (photosFolderExists) return "Istnieje";
            else return "Nie istnieje.";
        }

    }
}
