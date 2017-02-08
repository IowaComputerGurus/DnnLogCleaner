using System.Linq;
using IowaComputerGurus.Utility.DnnLogCleaner.Services;
using log4net;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Jobs
{
    public class LocalFileSystemLogCleanupJob : ILogCleanupJob
    {
        private readonly ILocalFileService _localFileService;
        private readonly ILog _log;

        public LocalFileSystemLogCleanupJob(ILog log, ILocalFileService localFileService)
        {
            _log = log;
            _localFileService = localFileService;
        }

        public void CleanupLogFiles(SiteCleanupConfiguration siteInfo)
        {
            _log.Debug($"Starting clean of {siteInfo.SiteName}");
            if (siteInfo.LogHistoryDaysToKeep < 2)
            {
                _log.Error("Unable to process, must keep at least 2 days of logs");
                return;
            }

            var directoryExists = _localFileService.DirectoryExists(siteInfo.DnnRootDirectoryPath);
            if (!directoryExists)
            {
                _log.Error("Provided directory not fount, no further processing for this site");
                return;
            }

            //Verify that it is a DNN install
            var dnnLogPath = _localFileService.BuildDnnLogFolderPath(siteInfo.DnnRootDirectoryPath);
            var dnnLogExists = _localFileService.DirectoryExists(dnnLogPath);
            if (!dnnLogExists)
            {
                _log.Error($"Unable to find DNN log folder.  Looked in '{dnnLogPath}'");
                return;
            }

            //Get the list of log files
            _log.Debug($"Searching for files in {dnnLogPath}");
            var logFiles = _localFileService.FindFiles(dnnLogPath, "*.log.resources");
            if (logFiles.Count > siteInfo.LogHistoryDaysToKeep)
            {
                //Get the to delete files
                var toDelete = logFiles.OrderByDescending(lf => lf.CreationTimeUtc).Skip(siteInfo.LogHistoryDaysToKeep);
                foreach (var fileToDelete in toDelete)
                {
                    _log.Debug($"Deleting {fileToDelete.FullName}");
                    _localFileService.DeleteFile(fileToDelete.FullName);
                }
            }
            else
                _log.Debug(
                    $"Found {logFiles.Count} files and log rules allow for up to {siteInfo.LogHistoryDaysToKeep} no action taken");
        }
    }
}