using System;
using System.Collections.Generic;
using System.IO;


namespace PhotoSorter
{
    public class CollectionsStatistics
    {
        //Zwrócenie tablicy wszystkich kolekcji
        

        public int quantityOfPhotosInColleciton;
        public float sizeOfCollection;
        public bool isFolderPresent;

        public CollectionsStatistics() { }
        public CollectionsStatistics(string collectionLibraryPath)
        {

            //dane stałe
            //funkcja update/create dane
            //zwróć tablicę kolekcji
            //zlicz kolekcje
            //zlicz rozmiar każdej i sumuj
            //zlicz sztuki i zsumuj
            //wywołaj funkcję sprawdznia czy istnieje folder + zlicz oszczędność 
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

        /// <summary>
        /// Check collection if folder was created and is present od disk.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        /// <returns></returns>
        public static bool CheckIfPhotosFolderExists(string collectionFileCompletePath)
        {
            if (collectionFileCompletePath == null) return false;
            string folderPath = System.IO.Directory.GetParent(collectionFileCompletePath).ToString() + "\\" + System.IO.Path.GetFileNameWithoutExtension(collectionFileCompletePath);
            return System.IO.Directory.Exists(folderPath);
        }

        /// <summary>
        /// Change bool value of collection photos folder presence to particular string.
        /// </summary>
        /// <param name="photosFolderExists"></param>
        /// <returns></returns>
        public static string StringFromPhotosFolderExists(string collectionFileCompletePath)
        {
            if (CheckIfPhotosFolderExists(collectionFileCompletePath)) return "Istnieje";
            else return "Nie istnieje.";
        }

    }
}
