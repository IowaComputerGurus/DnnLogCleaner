using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Jobs
{
    public class LocalFileSystemLogCleanupJob : ILogCleanupJob
    {
        private readonly ILog _log;

        public LocalFileSystemLogCleanupJob(ILog log)
        {
            _log = log;
        }

        public void CleanupLogFiles(SiteCleanupConfiguration siteInfo)
        {
            _log.Debug("In Local Log");
        }
    }
}
