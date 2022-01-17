using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSorter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSorter.Tests
{
    [TestClass()]
    public class SelectedPhotosFolderTests
    {
        [TestMethod()]
        public void CreateSelectedPhotosFolderTest_CreateIndicatedFolder_CorrectFolderData()
        {
            // arrange
            List<string> selectedFilesList = new List<string>();
            string directoryFolder;
            string newDirectoryFolder;
            string fileSourceName;
            string fileDestinationName;
            var createSelectedPhotosFolder = CreateSelectedPhotosFolderTest(@"","Folder testowy");
          
            StreamReader streamReader = new StreamReader();

            // act

            createSelectedPhotosFolder();

            // assert

            Assert.Equals(streamReader.Read());
           
        }
    }
}