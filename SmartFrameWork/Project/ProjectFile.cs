using System;
using System.Collections.Generic;

namespace SmartFrameWork.Project
{
    class ProjectFile
    {
        private static Dictionary<string, Type> types = new Dictionary<string, Type>();
        [System.Xml.Serialization.XmlIgnore]
        public static Dictionary<string, Type> Types
        {
            get
            {
                if (types.Count == 0)
                {
                    try
                    {
                        types.Add(typeof(ProjectFile).Name, typeof(ProjectFile));
                        foreach (ProjectNaturePlugin plugin in ProjectNaturePluginManager.Plugins)
                        {
                            //Plugins是插件的集合，每一个插件又包含具体的对象类型
                            //在CoreProjectNaturePlugin()定义了project子项的具体类型
                            foreach (ProjectNature nature in plugin.Natures)
                            {
                                types.Add(nature.Type.Name, nature.Type);
                            }
                        }
                    }
                    catch(Exception ex) { Services.LoggingService.Error(ex); }
                }
                return types;

            }
            set { types = value; }
        }

        private ProjectInfo project;
        public ProjectInfo Project
        {
            get { return project; }
            set { project = value; }
        }

        public static ProjectFile Open(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer();
            //在get函数里定义的获取Type的类型，Types的类型在main函数中加入到ProjectNaturePluginManager.Plugins
            serializer.Types = Types;
            object file = serializer.DeSerialize(fileName);
            return file as ProjectFile;
        }
        //保存project
        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer();
            serializer.Types = Types;
            serializer.Serialize(fileName, this);
        }
    }
}
