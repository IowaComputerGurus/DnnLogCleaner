using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IowaComputerGurus.Utility.DnnLogCleaner;
using IowaComputerGurus.Utility.DnnLogCleaner.Factories;
using IowaComputerGurus.Utility.DnnLogCleaner.Jobs;
using log4net;
using log4net.Config;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;

namespace IowaComputerGurus.Utility.DnnLogCleanerRunner
{
    internal class Program
    {
        private static IKernel _kernel;

        static void Main(string[] args)
        {
            //Setup log4net
            XmlConfigurator.Configure();

            //Setup Ninject
            SetupNinJect();

            //Run the job
            var masterJob = _kernel.Get<ICleanupSitesJob>();
            masterJob.CleanSites();
        }

        private static void SetupNinJect()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<ICleanupSitesJob>().To<CleanupSitesJob>();
            _kernel.Bind<ILogCleanupJobFactory>().ToFactory();
            _kernel.Bind<ILog>().ToProvider<LogProvider>();
        }

        internal class LogProvider : Provider<ILog>
        {
            protected override ILog CreateInstance(IContext context)
            {
                var serviceName = context.Request.ParentRequest.Service;
                return LogManager.GetLogger(serviceName);
            }
        }
    }
}
