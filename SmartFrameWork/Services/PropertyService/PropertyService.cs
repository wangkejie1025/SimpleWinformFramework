using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using SmartFrameWork.Utils;

namespace SmartFrameWork.Services
{
    public static class PropertyService
    {
        static string propertyFileName;
        static string propertyXmlRootNodeName;

        static string configDirectory;
        static string dataDirectory;

        static Properties properties;

        public static void InitializeService(string configDirectory, string dataDirectory, string propertyName)
        {
            if (properties != null)
                throw new InvalidOperationException("Service is already initialized.");
            if (configDirectory == null || dataDirectory == null || propertyName == null)
                throw new ArgumentNullException();
            properties = new Properties();
            //根目录\config\
            PropertyService.configDirectory = configDirectory;
            //根目录\data\
            PropertyService.dataDirectory = dataDirectory;
            //根节点名称
            propertyXmlRootNodeName = propertyName;
            //配置文件名称
            propertyFileName = propertyName + ".xml";
            //属性的变化通知PropertyService，PropertyService再通知其他订阅者，更新属性值
            properties.PropertyChanged += new PropertyChangedEventHandler(PropertiesPropertyChanged);
        }
        /// <summary>
        /// the directory save config file
        /// </summary>
        public static string ConfigDirectory
        {
            get
            {
                return configDirectory;
            }
        }

        public static string DataDirectory
        {
            get
            {
                return dataDirectory;
            }
        }

        public static string Get(string property)
        {
            return properties[property];
        }

        public static T Get<T>(string property, T defaultValue)
        {
            return properties.Get(property, defaultValue);
        }

        public static void Set<T>(string property, T value)
        {
            properties.Set(property, value);
        }
        public static void Load()
        {
            if (properties == null)
                throw new InvalidOperationException("Service is not initialized.");
            if (string.IsNullOrEmpty(configDirectory) || string.IsNullOrEmpty(propertyXmlRootNodeName))
                throw new InvalidOperationException("No file name was specified on service creation");
            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            if (!LoadPropertiesFromStream(Path.Combine(configDirectory, propertyFileName)))
            {
                LoadPropertiesFromStream(Path.Combine(DataDirectory, "options", propertyFileName));
            }
        }
        public static bool LoadPropertiesFromStream(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            try
            {
                //加锁同步
                using (LockPropertyFile())
                {
                    using (XmlTextReader reader = new XmlTextReader(fileName))
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                if (reader.LocalName == propertyXmlRootNodeName)
                                {
                                    properties.ReadProperties(reader, propertyXmlRootNodeName);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
                MessageService.ShowError("Error loading properties: " + ex.Message + "\nSettings have been restored to default values.");
            }
            return false;
        }
        /// <summary>
        /// Acquires an exclusive lock on the properties file so that it can be opened safely.
        /// </summary>
        public static IDisposable LockPropertyFile()
        {
            Mutex mutex = new Mutex(false, "PropertyServiceSave-30F32619-F92D-4BC0-BF49-AA18BF4AC313");
            mutex.WaitOne();
            //在系统Dispose的时候释放锁
            return new CallbackOnDispose(
                delegate
                {
                    mutex.ReleaseMutex();
                    mutex.Close();
                });
        }
        public static void Save()
        {
            if (string.IsNullOrEmpty(configDirectory) || string.IsNullOrEmpty(propertyXmlRootNodeName))
                throw new InvalidOperationException("No file name was specified on service creation");
            //AppProperties.xml
            //.\\bin\\Debug\\config
            string fileName = Path.Combine(configDirectory, propertyFileName);
            //will create a new file
            using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement(propertyXmlRootNodeName);
                properties.WriteProperties(writer);
                writer.WriteEndElement();
            }
        }
        //订阅了PropertyChanged事件，从Property类触发
        static void PropertiesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, e);
            }
        }
        //通知绑定事件的处理函数
        public static event PropertyChangedEventHandler PropertyChanged;
    }
}
