using System.Windows;
using System.Windows.Input;


namespace PhotoSorter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
          
        }

        private void addNewCollectionButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tabsDisplayFrame.Content = new AddNewCollection();
        }

        private void libraryButton_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            tabsDisplayFrame.Content = new LibraryManagement();
        }

        private void exitButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void statisticsButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tabsDisplayFrame.Content = new Statistics();
        }
    }

}
