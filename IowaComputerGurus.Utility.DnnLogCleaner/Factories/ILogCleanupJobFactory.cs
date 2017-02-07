using IowaComputerGurus.Utility.DnnLogCleaner.Jobs;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Factories
{
    /// <summary>
    ///     Ninject magical factory
    /// </summary>
    public interface ILogCleanupJobFactory
    {
        LocalFileSystemLogCleanupJob CreateLocalFileSystemCleanupJob();
    }
}