﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PhotoSorter
{
    public static class CollectionsLibraryFile
    {
        static readonly string path = @"CollectionsLibrary.txt";

        /// <summary>
        /// Adds created collection (.txt file) to library .txt file.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        public static void AddCollectionToLibraryFile(string collectionFileCompletePath)
        {
            //string completeFilePath = "@" + collectionName + ".txt";
            CreateFileIfNotPresent();

            StreamWriter fileConnection;
            try
            {
                fileConnection = new StreamWriter(path, true);
                fileConnection.WriteLine(collectionFileCompletePath);
                fileConnection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("UWAGA! Nie udało się zapisać pliku!");
                Console.WriteLine("Program zostanie zakończony bez zmian w pliku");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Gets List of lines from CollectionsLibrary .txt file.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCollectionsList()
        {
            CreateFileIfNotPresent();

            List<string> collectionsList = new List<string>();
            StreamReader streamReader = new StreamReader(path, true);
            while (true)
            {
                string temporaryReadString = streamReader.ReadLine();
                if (temporaryReadString != null) collectionsList.Add(temporaryReadString);
                else break;
            }
            streamReader.Close();

            return collectionsList;
        }

        /// <summary>
        /// Writes list of collections from arguments to library .txt file.
        /// </summary>
        /// <param name="CollectionsList"></param>
        public static void WriteCollectionListToLibraryFile(List<string> collectionsList)
        {
            StreamWriter fileWriter = new StreamWriter(path, false);
            try
            {
                foreach (var item in collectionsList)
                {
                    fileWriter.WriteLine(item);
                }
                fileWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można zapisać listy kolekcji w bibliotece!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Returns true if collection already exists in main collections ".txt" file.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static bool CheckIfCollectionAlreadySaved(string collectionName)
        {
            List<string> collectionsList = GetCollectionsList();
            foreach (var item in collectionsList)
            {
                if (item.ToString() == collectionName)
                {
                    MessageBox.Show("Kolekcja już istnieje w bazie!", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// If file is not present, function creates new library file in indicated path.
        /// </summary>
        /// <param name="path"></param>
        private static void CreateFileIfNotPresent()
        {
            StreamWriter fileConnection;
            if (!File.Exists(path))
            {
                fileConnection = File.CreateText(path);
                fileConnection.Close();
                try
                {
                    fileConnection = new StreamWriter(path, false);
                    fileConnection.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("UWAGA! Nie udało się utworzyć pliku!");
                    Console.WriteLine("Program zostanie zakończony bez zmian w pliku");
                    Console.ReadKey();
                }
            }
        }

    }
}
