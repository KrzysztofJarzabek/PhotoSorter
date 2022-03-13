using System.Collections.Generic;


namespace PhotoSorter
{
    class CollectionsListCreator
    {
        public readonly List<CollectionObject> collectionsList = new List<CollectionObject>();

        /// <summary>
        /// Creates or updates list of collection objects using CollectionsLibraryFile class.
        /// </summary>
        public void UpdateAllCollectionsFromFile()
        {
            List<string> collectionTemporaryList = CollectionsLibraryFile.GetCollectionsList();
            collectionsList.Clear();

            foreach (var collection in collectionTemporaryList)
            {
                collectionsList.Add(new CollectionObject(collection) { });
            }
        }
    }
}
