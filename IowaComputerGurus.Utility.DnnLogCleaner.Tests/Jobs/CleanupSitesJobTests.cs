using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IowaComputerGurus.Utility.DnnLogCleaner.Factories;
using IowaComputerGurus.Utility.DnnLogCleaner.Jobs;
using IowaComputerGurus.Utility.DnnLogCleaner.Providers;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Tests.Jobs
{
    [TestClass]
    public class CleanupSitesJobTests
    {
        private Mock<ILogCleanupJobFactory> _logCleanupJobFactoryMock;
        private Mock<IConfigurationProvider> _configurationProviderMock;
        private Mock<ILog> _loggerMock;
        private ICleanupSitesJob _job;

        [TestInitialize]
        public void Setup()
        {
            _logCleanupJobFactoryMock = new Mock<ILogCleanupJobFactory>();
            _configurationProviderMock = new Mock<IConfigurationProvider>();
            _loggerMock = new Mock<ILog>();
            _job = new CleanupSitesJob(_logCleanupJobFactoryMock.Object, _configurationProviderMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void CleanSitesShouldCallConfigurationProviderSitesToCleanGetOnce()
        {
            //Arrange

            //Act
            _job.CleanSites();

            //Assert
            _configurationProviderMock.Verify(cp => cp.SitesToClean, Times.Once);
        }

        [TestMethod]
        public void CleanSitesShouldLogErrorWhenNoSitesReadyForCleaningAndExit()
        {
            //Arrange

            //Act
            _job.CleanSites();

            //Assert
            _loggerMock.Verify(l => l.Error("No sites found, nothing to process"), Times.Once);
        }

        [TestMethod]
        public void CleanSitesShouldCallFactoryMethodToCreateLocalFileCleanupJobWhenConfigured()
        {
            //TODO: See how to better test this
            //Arrange
            var jobs = new List<SiteCleanupConfiguration> {new SiteCleanupConfiguration {CleanupType = CleanupType.LocalFileSystem, SiteName = "Test"}};
            _configurationProviderMock.Setup(c => c.SitesToClean).Returns(jobs);
            _logCleanupJobFactoryMock.Setup(lf => lf.CreateLocalFileSystemCleanupJob()).Returns(new LocalFileSystemLogCleanupJob(_loggerMock.Object));

            //Act
            _job.CleanSites();

            //Assert
            _logCleanupJobFactoryMock.Verify(f => f.CreateLocalFileSystemCleanupJob(), Times.Once);
        }
        
    }
}
