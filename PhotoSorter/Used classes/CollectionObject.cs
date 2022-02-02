using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorter
{
    class CollectionObject
    {
        public string completeTextFilePath { get; }
        public string baseCollectionPath { get; }
        public string collectionName { get; }
        public string photosFolderPath { get; }

        public CollectionObject(string _completeTextFilePath)
        {
            completeTextFilePath = _completeTextFilePath;

            baseCollectionPath = setBaseCollectionPath(completeTextFilePath);
            collectionName = setCollectionName(completeTextFilePath);
            photosFolderPath = setPhotosFolderPath(completeTextFilePath);
        }

        /// <summary>
        /// Returns as string the base collection path.
        /// </summary>
        /// <param name="completeTextFilePath"></param>
        /// <returns></returns>
        private string setBaseCollectionPath(string completeTextFilePath)
        {
            return System.IO.Directory.GetParent(completeTextFilePath).ToString();
        }

        /// <summary>
        /// Returns as string collection name.
        /// </summary>
        /// <param name="completeTextFilePath"></param>
        /// <returns></returns>
        private string setCollectionName(string completeTextFilePath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(completeTextFilePath);
        }

        /// <summary>
        /// Returns as string selected photos folder path.
        /// </summary>
        /// <param name="completeTextFilePath"></param>
        /// <returns></returns>
        private string setPhotosFolderPath(string completeTextFilePath)
        {
            return System.IO.Path.GetDirectoryName(completeTextFilePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(completeTextFilePath);
        }
    }
}
