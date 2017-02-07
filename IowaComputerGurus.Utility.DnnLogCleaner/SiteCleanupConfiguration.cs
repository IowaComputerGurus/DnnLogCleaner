namespace IowaComputerGurus.Utility.DnnLogCleaner
{
    /// <summary>
    ///     Configuration information for a single site to be cleaned up
    /// </summary>
    public class SiteCleanupConfiguration
    {
        /// <summary>
        ///     Gets or sets the type of the cleanup.
        /// </summary>
        /// <value>The type of the cleanup.</value>
        public CleanupType CleanupType { get; set; }

        /// <summary>
        ///     Site name used for logging/identification purposes
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }

        /// <summary>
        ///     Gets or sets the DNN root directory path.
        /// </summary>
        /// <value>The DNN root directory path.</value>
        public string DnnRootDirectoryPath { get; set; }

        /// <summary>
        ///     Gets or sets the log history days to keep.
        /// </summary>
        /// <value>The log history days to keep.</value>
        public int LogHistoryDaysToKeep { get; set; }

        /// <summary>
        ///     Gets or sets the FTP server.
        /// </summary>
        /// <value>The FTP server.</value>
        public string FtpServer { get; set; }

        /// <summary>
        ///     Gets or sets the FTP user.
        /// </summary>
        /// <value>The FTP user.</value>
        public string FtpUser { get; set; }

        /// <summary>
        ///     Gets or sets the FTP password.
        /// </summary>
        /// <value>The FTP password.</value>
        public string FtpPassword { get; set; }
    }
}