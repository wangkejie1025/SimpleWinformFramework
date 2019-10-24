using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myFormsApplication
{
    //root
    public class listconfigFormFile
    {
        //对象的集合构成整个xml文件
        //ConfigFile节点
        private List<configFormFile> configFile = new List<configFormFile>();
        public List<configFormFile> ConfigFile
        {
            get { return configFile; }
            set { configFile = value; }
        }

    }
    //每一行都是一个configFormFile对象
    //ConfigFile节点中包含多个configFormFile对象
    public class configFormFile : SmartFrameWork.ISelectable
    {
        private string name;
        [System.Xml.Serialization.XmlAttribute]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string meaning;
        [System.Xml.Serialization.XmlAttribute]
        public string Meaning
        {
            get { return meaning; }
            set { meaning = value; }
        }
        private string bindPort;
        [System.Xml.Serialization.XmlAttribute]
        public string BindPort
        {
            get { return bindPort; }
            set { bindPort = value; }
        }
        private string zeroPoint;
        [System.Xml.Serialization.XmlAttribute]
        public string ZeroPoint
        {
            get { return zeroPoint; }
            set { zeroPoint = value; }
        }
    }
}
