using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorter
{
    public static class CollectionsLibraryFile
    {
        static string path = @"CollectionsLibrary.txt";

        public static void AddCollectionToLibraryFile(string collectionName)
        {
            //string completeFilePath = "@" + collectionName + ".txt";
            CreateFileIfNotPresent();

            StreamWriter fileConnection;
            try
            {
                fileConnection = new StreamWriter(path, true);
                fileConnection.WriteLine(collectionName); //zmienić sposób zapisu aby można to było wyekstraktować później
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

        /// <summary>
        /// Gets List of lines from CollectionsLibrary .txt file.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCollectionsList()
        {
            List<string> CollectionsList = new List<string>();
            StreamReader streamReader = new StreamReader(path, true);

            while (true)
            {
                string temporaryReadString = streamReader.ReadLine();
                if (temporaryReadString != null) CollectionsList.Add(temporaryReadString);
                else break;
            }
            streamReader.Close();

            return CollectionsList;
        }

        //function: check if collection already exists
        //function: modify name/size
        //function: remove collection
    }
}
