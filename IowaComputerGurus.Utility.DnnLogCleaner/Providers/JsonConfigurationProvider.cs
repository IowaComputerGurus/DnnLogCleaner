using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Providers
{
    public class JsonConfigurationProvider : IConfigurationProvider
    {
        private List<SiteCleanupConfiguration> _sitesToClean;

        public List<SiteCleanupConfiguration> SitesToClean
        {
            get
            {
                //Return if we have it
                if (_sitesToClean != null)
                    return _sitesToClean;

                //Otherwise make sure we don't have a file
                if (!File.Exists("sites.json"))
                    throw new ArgumentException("Unable to find sites.json");

                //Deserialize and return
                _sitesToClean = JsonConvert.DeserializeObject<List<SiteCleanupConfiguration>>(File.ReadAllText("sites.json"));
                return _sitesToClean;
            }
        }
    }
}