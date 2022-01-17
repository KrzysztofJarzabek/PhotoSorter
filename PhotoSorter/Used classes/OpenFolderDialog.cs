using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                OpenFileDialog mainDirectory = new OpenFileDialog();
                mainDirectory.ShowDialog();

                return System.IO.Path.GetDirectoryName(mainDirectory.FileName).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Nie można otworzyć przeglądarki plików!", "UWAGA!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
