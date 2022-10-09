using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PhotoSorter
{
    /// <summary>
    /// Logika interakcji dla klasy AddNewCollection.xaml
    /// </summary>
    public partial class AddNewCollection : Page
    {
        public static bool createNewFolderChecked = false;

        public AddNewCollection()
        {
            InitializeComponent();
        }

        private void createFolderButton_Checked(object sender, RoutedEventArgs e)
        {
            createNewFolderChecked = true;
        }

        private void startSessionButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PhotoViewer photoViewerWindow = new PhotoViewer(mainFolderLocalizationTextBox.Text, collectionNameTextBox.Text);
            CollectionsLibraryFile.AddCollectionToLibraryFile(mainFolderLocalizationTextBox.Text + "\\" + collectionNameTextBox.Text + ".txt");

            photoViewerWindow.Show();
            DisplayStatusInfo("Otwarto przeglądarkę plików.");
        }

        private void collectionNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            collectionNameTextBox.Clear();
        }

        private void mainFolderLocalizationTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            mainFolderLocalizationTextBox.Clear();

            string mainFolderLocalization = OpenFolderDialog.ChooseFileDirectory();
            if (mainFolderLocalization == null) { DisplayStatusInfo("Wskazano niepoprawny format pliku"); return; }
            mainFolderLocalizationTextBox.Text = mainFolderLocalization;
        }

        private void DisplayStatusInfo(string message)
        {
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Interval = TimeSpan.FromSeconds(2);
            informationTextBlock.Text = message;

            messageTimer.Tick += (s, en) =>
            {
                informationTextBlock.Text = "";
                messageTimer.Stop();
            };
            messageTimer.Start();
        }
    }
}
