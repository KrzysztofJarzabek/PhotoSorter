using System;
using System.Collections.Generic;
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

namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy LibraryManagement.xaml
    /// </summary>
    public partial class LibraryManagement : Page
    {
        List<string> collectionsList = new List<string>();

        public LibraryManagement()
        {
            InitializeComponent();
            collectionsList = CollectionsLibraryFile.GetCollectionsList();
            foreach (var item in collectionsList)
            {
                collectionsLibraryListBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(item));
            }

        }

        /// <summary>
        /// Gets path to collection .txt file based on selected collection name.
        /// </summary>
        /// <returns></returns>
        private string getPathFromCollectionsNameSelected()
        {
            foreach (var item in collectionsList)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(item) == collectionsLibraryListBox.SelectedItem.ToString()) return item;
            }
            return null;
        }


        /// <summary>
        /// Triggers selected photos files to display in collectionPhotoListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void collectionsLibraryListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            collectionPhotoListBox.ItemsSource = CollectionFile.GetColectionPhotosNamesList(getPathFromCollectionsNameSelected());
        }
    }
}
