using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;



namespace PhotoSorter
{
    public class CollectionsStatistics
    {
        public double basePhotosCompleteSize { get; }
        public int collectionsCount { get; }
        public double allCollectionsSize { get; }
        public int photosInCollectionCount { get; }
        public int collectionsWithFolderPresent { get; }
        public double savedSpaceOnDisk { get; }

        CollectionsListCreator collectionsList = new(); //nazwa do zmiany

        public CollectionsStatistics() { }
        public CollectionsStatistics(string collectionLibraryPath)
        {
            collectionsList.updateAllCollectionsFromFile();

            basePhotosCompleteSize = GetAllCollecitonsSize();
            collectionsCount = GetCollectionsCount();
            allCollectionsSize = CountSizeOfAllCollections();
            photosInCollectionCount = CountPhotosInAllCollections();
            collectionsWithFolderPresent = CountCollectionsWithFolders();
            savedSpaceOnDisk = CountSavedSpaceOnDisk();
                        
        }

        /// <summary>
        /// Counts photos quantity in collection. collectionFileCompletePath is the complete path to .txt file.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        /// <returns></returns>
        public static int CountPhotosInCollection(string collectionFileCompletePath)
        {
            if (collectionFileCompletePath == null) return 0;

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollectionFile(collectionFileCompletePath);
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

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollectionFile(collectionFileCompletePath);
            string photosPath = System.IO.Directory.GetParent(collectionFileCompletePath).ToString();
            double photosCompleteSize = 0.00D;

            foreach (var photo in selectedFilesList)
            {
                FileInfo fileInfo = new(photosPath + "\\" + photo);
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

        /// <summary>
        /// Gets quantity of all saved collections.
        /// </summary>
        /// <returns></returns>
        private int GetCollectionsCount()
        {
            int collectionCount = 0;
            foreach (var item in collectionsList.collectionsList)
            {
                collectionCount++;
            }
            return collectionCount;
        }

        /// <summary>
        /// Gets size of all saved collections.
        /// </summary>
        /// <returns></returns>
        private double GetAllCollecitonsSize()
        {
            double basePhotosCompleteSize = 0.00D;
            foreach (var item in collectionsList.collectionsList)
            {
                basePhotosCompleteSize += CountSizeOfBaseFolder(item.baseCollectionPath);
            }

            return basePhotosCompleteSize;
        }

        /// <summary>
        /// Counts size of photos in specified collections.
        /// </summary>
        /// <param name="baseCollectionPath">Path to base collection folder.</param>
        /// <returns></returns>
        private double CountSizeOfBaseFolder(string baseCollectionPath)
        {
            if (baseCollectionPath == null) return 0;

            string[] selectedFilesList = System.IO.Directory.GetFiles(baseCollectionPath, "*" + "jpg");
            double photosCompleteSize = 0.00D;

            foreach (var photo in selectedFilesList)
            {
                FileInfo fileInfo = new(photo);
                photosCompleteSize += (double)fileInfo.Length / 1048576;
            }
            return Math.Round(photosCompleteSize, 2);
        }

        /// <summary>
        /// Counts size of photos in all collections.
        /// </summary>
        /// <returns></returns>
        private double CountSizeOfAllCollections()
        {
            double photosCompleteSize = 0.00D;
            foreach (var collection in collectionsList.collectionsList)
            {
                photosCompleteSize += CountSizeOfPhotosInCollection(collection.completeTextFilePath);
            }
            return Math.Round(photosCompleteSize, 2);
        }

        /// <summary>
        /// Counts quantity of photos in all collections.
        /// </summary>
        /// <returns></returns>
        private int CountPhotosInAllCollections()
        {
            int photosCount = 0;
            foreach (var collection in collectionsList.collectionsList)
            {
                photosCount += CountPhotosInCollection(collection.completeTextFilePath);
            }
            return photosCount;
        }

        /// <summary>
        /// Returns quantity of collections with selected photos folder present.
        /// </summary>
        /// <returns></returns>
        private int CountCollectionsWithFolders()
        {
            int collectionsWithFolders = 0;
            foreach (var collection in collectionsList.collectionsList)
            {
                if (CheckIfPhotosFolderExists(collection.completeTextFilePath)) collectionsWithFolders++;
            }
            return collectionsWithFolders;
        }

        /// <summary>
        /// Counts saved memory space on disc due to collection mirrors.
        /// </summary>
        /// <returns></returns>
        private double CountSavedSpaceOnDisk()
        {
            double savedSpace = 0.00D;
            foreach (var collection in collectionsList.collectionsList)
            {
                if (!CheckIfPhotosFolderExists(collection.completeTextFilePath))
                {
                    savedSpace += CountSizeOfPhotosInCollection(collection.completeTextFilePath);
                }
            }
            return Math.Round(savedSpace, 2);
        }

    }

}
