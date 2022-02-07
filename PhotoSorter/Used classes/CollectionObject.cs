using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorter
{
    class CollectionObject
    {
        public string collectionFileCompletePath { get; }
        public string baseCollectionPath { get; }
        public string collectionName { get; }
        public string photosFolderPath { get; }

        public CollectionObject(string collectionFileCompletePath)
        {
            this.collectionFileCompletePath = collectionFileCompletePath;

            baseCollectionPath = setBaseCollectionPath(this.collectionFileCompletePath);
            collectionName = setCollectionName(this.collectionFileCompletePath);
            photosFolderPath = setPhotosFolderPath(this.collectionFileCompletePath);
        }

        /// <summary>
        /// Returns as string the base collection path.
        /// </summary>
        /// <param name="completeTextFilePath"></param>
        /// <returns></returns>
        private string setBaseCollectionPath(string collectionFileCompletePath)
        {
            return System.IO.Directory.GetParent(collectionFileCompletePath).ToString();
        }

        /// <summary>
        /// Returns as string collection name.
        /// </summary>
        /// <param name="completeTextFilePath"></param>
        /// <returns></returns>
        private string setCollectionName(string collectionFileCompletePath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(collectionFileCompletePath);
        }

        /// <summary>
        /// Returns as string selected photos folder path.
        /// </summary>
        /// <param name="completeTextFilePath"></param>
        /// <returns></returns>
        private string setPhotosFolderPath(string collectionFileCompletePath)
        {
            return System.IO.Path.GetDirectoryName(collectionFileCompletePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(collectionFileCompletePath);
        }
    }
}
