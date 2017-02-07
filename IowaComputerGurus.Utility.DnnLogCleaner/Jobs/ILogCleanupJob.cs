using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace IowaComputerGurus.Utility.DnnLogCleaner
{
    public interface ILogCleanupJob
    {
        void CleanupLogFiles(SiteCleanupConfiguration siteToClean);
    }
}
