using System.IO;
using IowaComputerGurus.Utility.DnnLogCleaner.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Tests.Services
{
    [TestClass]
    public class LocalFileServiceTests
    {
        private Mock<ILog> _loggerMock;
        private ILocalFileService _fileService;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILog>();
            _fileService = new LocalFileService(_loggerMock.Object);
            if (Directory.Exists("Test"))
                Directory.Delete("Test");
        }

        [TestMethod]
        public void DirectoryExistsShouldReturnFalseIfDirectoryDoesNotExist()
        {
            //Arrange
            var directoryPath = "Test";

            //Act
            var result = _fileService.DirectoryExists(directoryPath);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DirectoryExistsShouldReturnTrueIfDirectoryExists()
        {
            //Arrange
            var directoryPath = "Test";
            Directory.CreateDirectory(directoryPath);

            //Act
            var result = _fileService.DirectoryExists(directoryPath);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BuildDnnDirectoryShouldCreateProperPathWhenSuppliedRelativePath()
        {
            //Arrange
            var startPath = "test";
            var fullStartPath = new DirectoryInfo(startPath).FullName;

            //Act
            var result = _fileService.BuildDnnLogFolderPath(startPath);

            //Assert
            Assert.AreEqual($"{fullStartPath}\\portals\\_default\\logs", result);
        }
    }
}