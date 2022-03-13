using System.Windows.Controls;
using System.Windows.Input;


namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        CollectionsStatistics collectionsStatistics = new CollectionsStatistics(@"CollectionsLibrary.txt");
        MemorySaver memorySaver = new();
        public Statistics()
        {
            InitializeComponent();
            UpdateAllDataOnPage();
        }

        private void optimizeMemoryButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            memorySaver.OptimizeMemory();
            UpdateAllDataOnPage();
        }

        private void UpdateAllDataOnPage()
        {
            CollectionsStatistics collectionsStatistics = new CollectionsStatistics(@"CollectionsLibrary.txt");
            quantityOfCollectionsTextBox.Text = collectionsStatistics.collectionsCount.ToString();
            sizeOfBaseFilesTextBox.Text = collectionsStatistics.basePhotosCompleteSize.ToString() + " MB";
            sizeOfCollectionsTextBox.Text = collectionsStatistics.allCollectionsSize.ToString() + " MB";
            quantityOfPhotosInCollectionsTextBox.Text = collectionsStatistics.photosInCollectionCount.ToString();
            quantityOfCollectionsWithFoldersTextBox.Text = collectionsStatistics.collectionsWithFolderPresent.ToString();
            savedSpaceOnDiskTextBox.Text = collectionsStatistics.savedSpaceOnDisk.ToString() + " MB";

            if (collectionsStatistics.collectionsWithFolderPresent != 0)
            {
                optimizeMemoryTextBox.Text = "Usuń niepotrzebne foldery aby oszczędzić miejsce.";
                optimizeMemoryButton.Background = System.Windows.Media.Brushes.LightGreen;
            }
            else
            {
                optimizeMemoryTextBox.Text = "Wszystkie foldery zoptymalizowane.";
                optimizeMemoryButton.Background = System.Windows.Media.Brushes.White;
            }
        }
    }
}
