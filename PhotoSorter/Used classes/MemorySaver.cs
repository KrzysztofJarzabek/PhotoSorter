namespace PhotoSorter
{
    public class MemorySaver
    {
        CollectionsListCreator collectionsObjectsList = new();
        public MemorySaver()
        {
            collectionsObjectsList.UpdateAllCollectionsFromFile();
        }

        /// <summary>
        /// Removes collection folder if exists.
        /// </summary>
        public void OptimizeMemory()
        {
            foreach (var collection in collectionsObjectsList.collectionsList)
            {
                if (CollectionsStatistics.CheckIfPhotosFolderExists(collection.collectionFileCompletePath))
                    SelectedPhotosFolder.DeletePhotosCollectionFolder(collection.collectionFileCompletePath);
            }
        }
    }
}
