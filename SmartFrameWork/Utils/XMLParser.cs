using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class XMLParser<T>
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Type[] types;

        public XMLParser(Type[] types)
        {
            this.types = types;
        }

        public XMLParser()
        {
        }

        public void Save(string fileName, T config)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = null;
            try
            {
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T), types);
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
                xmlSerializer.Serialize(fs, config);
                fs.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception:"+ex.ToString());
                logger.Error(ex);
            }
        }

        public T Open(string fileName)
        {

            System.Xml.Serialization.XmlSerializer xmlSerializer = null;
            try
            {
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T), types);
                xmlSerializer.UnknownNode += new System.Xml.Serialization.XmlNodeEventHandler(xmlSerializer_UnknownNode);
                xmlSerializer.UnknownElement += new System.Xml.Serialization.XmlElementEventHandler(xmlSerializer_UnknownElement);
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                T fiuconfigs = (T)xmlSerializer.Deserialize(fs);
                fs.Close();
                return fiuconfigs;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return default(T);
        }

        void xmlSerializer_UnknownElement(object sender, System.Xml.Serialization.XmlElementEventArgs e)
        {
            
        }

        void xmlSerializer_UnknownNode(object sender, System.Xml.Serialization.XmlNodeEventArgs e)
        {
           
        }
    }
}
