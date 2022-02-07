using Microsoft.Win32;
using System;
using System.Windows;

namespace PhotoSorter
{
    public static class OpenFolderDialog
    {
        
        /// <summary>
        /// Opens a file dialog and returns folder of indicated file.
        /// </summary>
        /// <returns></returns>
        public static string ChooseFileDirectory() //utworzyć wspólną metodę/klasę
        {
            object fileName;
            OpenFileDialog mainDirectory = new OpenFileDialog();
            mainDirectory.ShowDialog();
            try
            {
                fileName = mainDirectory.FileName;
                return System.IO.Path.GetDirectoryName(fileName.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można otworzyć przeglądarki plików!", "UWAGA!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
