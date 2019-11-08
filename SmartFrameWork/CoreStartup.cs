using System;
using SmartFrameWork.Utils;
using System.IO;
using SmartFrameWork.Services;

namespace SmartFrameWork
{
    public class CoreStartup
    {
        //XML root element
        string propertiesName;
        string configDirectory;
        string dataDirectory;
        readonly string applicationName;

        /// <summary>
        /// Sets the name used for the properties (only name, without path or extension).
        /// Must be set before StartCoreServices() is called.XML file root element
        /// </summary>
        public string PropertiesName
        {
            get
            {
                return propertiesName;
            }
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentNullException("value");
                propertiesName = value;
            }
        }

        /// <summary>
        /// Sets the directory name used for the property service.
        /// Must be set before StartCoreServices() is called.
        /// Use null to use the default path "ApplicationData\ApplicationName".
        /// </summary>
        public string ConfigDirectory
        {
            get
            {
                return configDirectory;
            }
            set
            {
                configDirectory = value;
            }
        }
        /// <summary>
        /// Sets the data directory used to load resources.
        /// Must be set before StartCoreServices() is called.
        /// Use null to use the default path "ApplicationRootPath\data".
        /// </summary>
        public string DataDirectory
        {
            get
            {
                return dataDirectory;
            }
            set
            {
                dataDirectory = value;
            }
        }
        public CoreStartup(string applicationName)
        {
            this.applicationName = applicationName?? throw new ArgumentNullException("applicationName");
            //如果不设值propertiesName属性就会设置为applicationName + "Properties"，propertiesName是xml文件的根目录
            //propertiesName = applicationName + "Properties";
            MessageService.DefaultMessageBoxTitle = this.applicationName;
        }
        public void StartCoreServices()
        {
            if (configDirectory == null)
                configDirectory = Path.Combine(FileUtility.ApplicationRootPath,"config");
            PropertyService.InitializeService(configDirectory,
                                              dataDirectory ?? Path.Combine(FileUtility.ApplicationRootPath, "data"),
                                              propertiesName);
            PropertyService.Load();
        }
    }
}
