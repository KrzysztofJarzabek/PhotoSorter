using System.Collections.Generic;


namespace PhotoSorter
{
    class CollectionsListCreator
    {
        public readonly List<CollectionObject> collectionsList = new List<CollectionObject>();

        /// <summary>
        /// 
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
