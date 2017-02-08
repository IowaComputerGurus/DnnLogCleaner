using System;
using System.Collections.Generic;
using System.IO;
using IowaComputerGurus.Utility.DnnLogCleaner.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Tests.Providers
{
    [TestClass]
    public class JsonConfigurationProviderTests
    {
        private IConfigurationProvider _provider;

        [TestInitialize]
        public void Setup()
        {
            _provider = new JsonConfigurationProvider();

            //Cleanup old files
            if (File.Exists("sites.json"))
                File.Delete("sites.json");
        }

        [TestMethod]
        public void SitesToCleanShouldThrowErrorIfJsonFileDoesNotExist()
        {
            //Arrange
            var errorFound = false;

            //Act
            try
            {
                var sites = _provider.SitesToClean;
            }
            catch (ArgumentException ex) when (ex.Message == "Unable to find sites.json")
            {
                errorFound = true;
            }

            //Assert
            Assert.IsTrue(errorFound);
        }

        [TestMethod]
        public void SitesToCleanShouldReturnNullCollectionWithEmptyFile()
        {
            //Arrange
            File.WriteAllText("sites.json", "");

            //Act
            var sites = _provider.SitesToClean;

            //Assert
            Assert.IsNull(sites);
        }

        [TestMethod]
        public void SItesToCleanShouldReturnSingleItemWhenProperObjectExists()
        {
            //Arrange
            var toSave = new List<SiteCleanupConfiguration>
            {
                new SiteCleanupConfiguration {CleanupType = CleanupType.LocalFileSystem, DnnRootDirectoryPath = "", LogHistoryDaysToKeep = 5, SiteName = "Test Site"}
            };

            File.WriteAllText("sites.json", JsonConvert.SerializeObject(toSave));

            //Act
            var sites = _provider.SitesToClean;

            //Assert
            Assert.IsNotNull(sites);
            Assert.AreEqual(1, sites.Count);
        }

        [TestMethod]
        public void SitesToCleanShouldReturnMultipleItemsWhenProperObjectExists()
        {
            //Arrange
            var toSave = new List<SiteCleanupConfiguration>
            {
                new SiteCleanupConfiguration {CleanupType = CleanupType.LocalFileSystem, DnnRootDirectoryPath = "/", LogHistoryDaysToKeep = 5, SiteName = "Test Site"},
                new SiteCleanupConfiguration {CleanupType = CleanupType.LocalFileSystem, DnnRootDirectoryPath = "C:\\Users\\Mitch\\Desktop\\Test", LogHistoryDaysToKeep = 5, SiteName = "Second Site" }
            };

            File.WriteAllText("sites.json", JsonConvert.SerializeObject(toSave));

            //Act
            var sites = _provider.SitesToClean;

            //Assert
            Assert.IsNotNull(sites);
            Assert.AreEqual(2, sites.Count);
        }
    }
}