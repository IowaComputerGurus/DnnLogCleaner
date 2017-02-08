using IowaComputerGurus.Utility.DnnLogCleaner.Jobs;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Factories
{
    /// <summary>
    ///     Factory class for creation of concrete implementations.  
    /// </summary>
    /// <remarks>
    /// Concrete implementation is done automatically by the Ninject Factory Extensions
    /// </remarks>
    public interface ILogCleanupJobFactory
    {
        LocalFileSystemLogCleanupJob CreateLocalFileSystemCleanupJob();
    }
}