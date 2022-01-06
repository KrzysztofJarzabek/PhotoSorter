using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoSorter
{
    public static class CollectionFile
    {

        /// <summary>
        /// Writes list of selected photos to indicated .txt file localization.
        /// </summary>
        /// <param name="textFilePath"></param>
        /// <param name="selectedFilesList"></param>
        public static void WriteSelectedFilesListToFile(string textFilePath, List<string> selectedFilesList)
        {
            StreamWriter fileWriter = new StreamWriter(textFilePath, false);
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
                MessageBox.Show("Cannot write photos list to collection file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Gets list of photos names in .txt collection file from indicated full path.
        /// </summary>
        /// <param name="collectionFilePath"></param>
        /// <returns></returns>
        public static List<string> GetColectionPhotosNamesList(string collectionFilePath)
        {
            List<string> CollectionFilesNames = new List<string>();
            StreamReader streamReader = new StreamReader(collectionFilePath, true);

            try
            {
                while (true)
                {
                    string temporaryReadString = streamReader.ReadLine();
                    if (temporaryReadString != null) CollectionFilesNames.Add(System.IO.Path.GetFileNameWithoutExtension(temporaryReadString));
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
    }
}
