using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static int CountPhotosInCollection(string textFilePath)
        {
            if (textFilePath == null) return 0;

            List<string> selectedFilesList = SelectedPhotosFolder.GetPhotosNamesFromCollection(textFilePath);
            return selectedFilesList.Count;
        }


    }
}
