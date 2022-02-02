using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;


namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {

        public Statistics()
        {
            InitializeComponent();
            CollectionsStatistics collectionsStatistics = new CollectionsStatistics(@"CollectionsLibrary.txt");

            quantityOfCollectionsTextBox.Text = collectionsStatistics.collectionsCount.ToString();
            sizeOfBaseFilesTextBox.Text = collectionsStatistics.basePhotosCompleteSize.ToString() + " MB";
            sizeOfCollectionsTextBox.Text = collectionsStatistics.allCollectionsSize.ToString() + " MB";
            quantityOfPhotosInCollectionsTextBox.Text = collectionsStatistics.photosInCollectionCount.ToString();
            quantityOfCollectionsWithFoldersTextBox.Text = collectionsStatistics.collectionsWithFolderPresent.ToString();
            savedSpaceOnDiskTextBox.Text = collectionsStatistics.savedSpaceOnDisk.ToString() + " MB";
        }

        private void optimizeMemoryButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
