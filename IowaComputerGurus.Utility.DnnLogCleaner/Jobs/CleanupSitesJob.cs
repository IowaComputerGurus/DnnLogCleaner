using IowaComputerGurus.Utility.DnnLogCleaner.Factories;
using IowaComputerGurus.Utility.DnnLogCleaner.Providers;
using log4net;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Jobs
{
    public interface ICleanupSitesJob
    {
        void CleanSites();
    }

    public class CleanupSitesJob : ICleanupSitesJob
    {
        private readonly ILogCleanupJobFactory _logCleanupJobFactory;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly ILog _logger;

        public CleanupSitesJob(ILogCleanupJobFactory logCleanupJobFactory, IConfigurationProvider configurationProvider, ILog logger)
        {
            _logCleanupJobFactory = logCleanupJobFactory;
            _configurationProvider = configurationProvider;
            _logger = logger;
        }

        public void CleanSites()
        {
            //Get the sites to clean
            var sites = _configurationProvider.SitesToClean;
            if (sites == null || sites.Count == 0)
            {
                _logger.Error("No sites found, nothing to process");
                return;
            }

            //Loop through and run the jobs
            foreach (var site in sites)
            {
                switch (site.CleanupType)
                {
                    case CleanupType.LocalFileSystem:
                        var process = _logCleanupJobFactory.CreateLocalFileSystemCleanupJob();
                        process.CleanupLogFiles(site);
                        break;
                    default:
                        _logger.Error($"Unable to process '{site.SiteName}' as unable to find cleanup type {site.CleanupType}");
                        break;
                }
            }
        }
    }
}