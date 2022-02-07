using System.Collections.Generic;


namespace PhotoSorter
{
    class CollectionsListCreator
    {
        public readonly List<CollectionObject> collectionsList = new List<CollectionObject>();

        public void UpdateAllCollectionsFromFile()
        {
            List<string> collectionTemporaryList = CollectionsLibraryFile.GetCollectionsList();

            foreach (var collection in collectionTemporaryList)
            {
                collectionsList.Add(new CollectionObject(collection) { });
            }
        }
    }
}
