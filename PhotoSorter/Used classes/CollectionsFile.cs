using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PhotoSorter
{
    public static class CollectionsFile
    {

        /// <summary>
        /// Writes list of selected photos to indicated .txt file localization.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        /// <param name="selectedFilesList"></param>
        public static void WriteSelectedFilesListToFile(string collectionFileCompletePath, List<string> selectedFilesList)
        {
            StreamWriter fileWriter = new StreamWriter(collectionFileCompletePath, false);
            try
            {
                foreach (var item in selectedFilesList)
                {
                    fileWriter.WriteLine(System.IO.Path.GetFileName(item));
                }
                fileWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można zapisać zdjęć w pliku danej kolekcji.", "Bład!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets list of photos names in .txt collection file from indicated full path.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        /// <returns></returns>
        public static List<string> GetCollectionPhotosNamesList(string collectionFileCompletePath)
        {
            if (collectionFileCompletePath == null || collectionFileCompletePath == "") return null;

            List<string> CollectionFilesNames = new List<string>();
            StreamReader streamReader = new StreamReader(collectionFileCompletePath, true);
            try
            {
                while (true)
                {
                    string temporaryReadString = streamReader.ReadLine();
                    if (temporaryReadString != null) CollectionFilesNames.Add(System.IO.Path.GetFileName(temporaryReadString));
                    else break;
                }
                streamReader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Nie znaleziono kolekcji!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return CollectionFilesNames;
        }

        /// <summary>
        /// Delete collection .txt file.
        /// </summary>
        /// <param name="collectionFileCompletePath"></param>
        public static void DeleteCollectionFile(string collectionFileCompletePath)
        {
            System.IO.File.Delete(collectionFileCompletePath);
        }


        //dodać metodę pobierającą rozmiar plików i sumującą. Na podstawie GetColectionPhotosNamesLIst

        //if (temporaryReadString == null ) break;
        //          else if(!temporaryReadString.StartsWith("Size:")) CollectionFilesNames.Add(System.IO.Path.GetFileName(temporaryReadString));
        //          //pomysł, żeby dopisywać lini
    }
}
