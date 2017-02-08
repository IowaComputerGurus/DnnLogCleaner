using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace IowaComputerGurus.Utility.DnnLogCleaner.Services
{
    public interface ILocalFileService
    {
        bool DirectoryExists(string directoryPath);
        string BuildDnnLogFolderPath(string directoryRoot);
        List<FileInfo> FindFiles(string directoryPath, string searchPattern);
        void DeleteFile(string filePath);
    }

    public class LocalFileService : ILocalFileService
    {
        private readonly ILog _logger;

        public LocalFileService(ILog logger)
        {
            _logger = logger;
        }

        public bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public string BuildDnnLogFolderPath(string directoryRoot)
        {
            var directoryInfo = new DirectoryInfo(directoryRoot);
            return Path.Combine(directoryInfo.FullName, "portals\\_default\\logs");
        }

        public List<FileInfo> FindFiles(string directoryPath, string searchPattern)
        {
            var directory = new DirectoryInfo(directoryPath);
            return directory.GetFiles(searchPattern).ToList();
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                _logger.Error("Unable to delete file", ex);
            }
        }
    }
}