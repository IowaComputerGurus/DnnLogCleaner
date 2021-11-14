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
        private Mock<IConfigurationProvider> _configurationProviderMock;
        private ICleanupSitesJob _job;
        private Mock<ILogCleanupJobFactory> _logCleanupJobFactoryMock;
        private Mock<ILog> _loggerMock;

        [TestInitialize]
        public void Setup()
        {
            _logCleanupJobFactoryMock = new Mock<ILogCleanupJobFactory>();
            _configurationProviderMock = new Mock<IConfigurationProvider>();
            _loggerMock = new Mock<ILog>();
            _job = new CleanupSitesJob(_logCleanupJobFactoryMock.Object, _configurationProviderMock.Object,
                _loggerMock.Object);
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

        //TODO: Figure out how to test past the Ninject Factory stuff
    }
}