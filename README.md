# DnnLogCleaner
A common oversight of DNN/Evoq site administrators is the cleanup, and management, of log files.  This utility was created with those people in mind.  Rather than implement inside of DNN in a manner that will add to resources, this standalone utility can manage logs of multiple DNN installations.

## Supported Environments

Currently this application supports execution while the application is local to the filesystem of the host computer. 

## Cleaned Files

The current version of this utility focuses on the management of the Log4Net files stored in the /Portals/_default/logs folder.  This includes "InstallLog" and generic daily log files. 

## Configuration

To aid in configuration a JSON formatted collection of "Sites to Clean" is used as the input for the application.  This file needs to be stored, with the name "sites.json" within the working directory of the application.  THe below sample shows the configuration of two local installations for cleaning.

```json
[  
   {  
      "CleanupType":"LocalFileSystem",
      "SiteName":"Test Site 1",
      "DnnRootDirectoryPath":"C:\\inetpub\\vhosts\\testsite.com\wwwroot",
      "LogHistoryDaysToKeep":5
   },
   {  
      "CleanupType":"LocalFileSystem",
      "SiteName":"Second Site",
      "DnnRootDirectoryPath":"C:\\inetpub\\vhosts\\testsite2.com\wwwroot",
      "LogHistoryDaysToKeep":5
   }
]
```

### Configuration Field Details

Field | Description
CleanupType | _LocalFileSystem_ is the only support tyle at this time
SiteName | Friendly name for the site used for reporting processes
DnnRootDirectoryPath | This is the full directory path to the site.  Should be to the root DNN folder only, nothing else, and any \ characters must be escaped
LogHistoryDaysToKeep | How many days of history should be retained

## Future Plans

Future releases of this application will support cleanup routines of remote installations using FTP.   Other features will be considered upon request, please don't hesitate to ask.
