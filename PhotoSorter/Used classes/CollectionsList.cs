using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorter
{
    class CollectionsListCreator
    {
        public readonly List<CollectionObject> collectionsList = new List<CollectionObject>();

        public void updateAllCollectionsFromFile()
        {
            List<string> collectionTemporaryList = CollectionsLibraryFile.GetCollectionsList();

            foreach (var collection in collectionTemporaryList)
            {
                collectionsList.Add(new CollectionObject(collection) { });
            }
        }
    }
}
