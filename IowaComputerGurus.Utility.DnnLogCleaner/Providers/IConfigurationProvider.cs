using System.Collections.Generic;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Providers
{
    public interface IConfigurationProvider
    {
        List<SiteCleanupConfiguration> SitesToClean { get; }
    }
}