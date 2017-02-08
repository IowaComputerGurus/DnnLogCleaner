using IowaComputerGurus.Utility.DnnLogCleaner.Jobs;
using IowaComputerGurus.Utility.DnnLogCleaner.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Tests.Jobs
{
    [TestClass]
    public class LocalFileSystemLogCleanupJobTests
    {
        private ILogCleanupJob _job;
        private Mock<ILocalFileService> _localFileServiceMock;
        private Mock<ILog> _loggerMock;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILog>();
            _localFileServiceMock = new Mock<ILocalFileService>();
            _job = new LocalFileSystemLogCleanupJob(_loggerMock.Object, _localFileServiceMock.Object);
        }

        [TestMethod]
        public void CleanupLogFilesShouldLogSiteNameAsDebug()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration {SiteName = "Test Site"};

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert
            _loggerMock.Verify(l => l.Debug($"Starting clean of {jobConfig.SiteName}"), Times.Once);
        }

        [TestMethod]
        public void CleanupLogFilesShouldLogErrorAndReturnIfDaysToKeepIsLessThan2()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration { SiteName = "Test Site" };

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert
            _loggerMock.Verify(l => l.Error($"Unable to process, must keep at least 2 days of logs"), Times.Once);
            _localFileServiceMock.Verify(lfs => lfs.DirectoryExists(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void CleanupLogFilesShouldCallFileServiceToVerifyDirectoryExists()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration {SiteName = "Test Site", LogHistoryDaysToKeep = 2, DnnRootDirectoryPath = "/Test"};

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert
            _localFileServiceMock.Verify(lfs => lfs.DirectoryExists(jobConfig.DnnRootDirectoryPath));
        }

        [TestMethod]
        public void CleanupLogFilesShouldLogErorIfDirectoryDoesNotExist()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration {SiteName = "Test Site", LogHistoryDaysToKeep = 2};
            _localFileServiceMock.Setup(lfs => lfs.DirectoryExists(jobConfig.DnnRootDirectoryPath)).Returns(false);

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert
            _loggerMock.Verify(l => l.Error("Provided directory not fount, no further processing for this site"));
        }

        [TestMethod]
        public void CleanupLogFileShouldCallFileServiceToGetDnnLogDirectory()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration { SiteName = "Test Site", LogHistoryDaysToKeep = 2 };
            var fullPath = string.Empty;
            _localFileServiceMock.Setup(lfs => lfs.DirectoryExists(jobConfig.DnnRootDirectoryPath)).Returns(true);
            _localFileServiceMock.Setup(lfs => lfs.BuildDnnLogFolderPath(jobConfig.DnnRootDirectoryPath))
                .Returns(fullPath);

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert
            _localFileServiceMock.Verify(lfs => lfs.BuildDnnLogFolderPath(jobConfig.DnnRootDirectoryPath), Times.Once);
        }

        [TestMethod]
        public void CleanupLogFileShouldCallLocalFileServiceToVerifyDnnLogDirectoryExists()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration { SiteName = "Test Site", LogHistoryDaysToKeep = 2 };
            var fullPath = "";
            _localFileServiceMock.Setup(lfs => lfs.DirectoryExists(jobConfig.DnnRootDirectoryPath)).Returns(true);
            _localFileServiceMock.Setup(lfs => lfs.BuildDnnLogFolderPath(jobConfig.DnnRootDirectoryPath))
                .Returns(fullPath);

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert
            _localFileServiceMock.Verify(lfs => lfs.DirectoryExists(fullPath), Times.Once);
        }

        [TestMethod]
        public void CleanupLogFileShouldLogErrorIfDnnLogPathIsNotFound()
        {
            //Arrange
            var jobConfig = new SiteCleanupConfiguration { SiteName = "Test Site", LogHistoryDaysToKeep = 2 };
            var fullPath = "";
            _localFileServiceMock.Setup(lfs => lfs.DirectoryExists(jobConfig.DnnRootDirectoryPath)).Returns(true);
            _localFileServiceMock.Setup(lfs => lfs.BuildDnnLogFolderPath(jobConfig.DnnRootDirectoryPath))
                .Returns(fullPath);
            _localFileServiceMock.Setup(lfs => lfs.DirectoryExists(fullPath)).Returns(false);

            //Act
            _job.CleanupLogFiles(jobConfig);

            //Assert

            //Assert
            _loggerMock.Verify(lm => lm.Error($"Unable to find DNN log folder.  Looked in '{fullPath}'"), Times.Once);
        }
    }
}