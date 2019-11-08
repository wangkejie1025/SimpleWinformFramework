using System;
using System.Collections.Generic;

namespace SmartFrameWork.Project
{
    public class ProjectNature
    {
        Type type;
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        public ProjectNature(Type type)
        {
            this.type = type;
        }
    }

    public class ProjectNaturePlugin
    {
        List<ProjectNature> natures = new List<ProjectNature>();
        public List<ProjectNature> Natures
        {
            get { return natures; }
            set { natures = value; }
        }
    }

    public class ProjectNaturePluginManager
    {
        static List<ProjectNaturePlugin> plugins = new List<ProjectNaturePlugin>();
        public static List<ProjectNaturePlugin> Plugins
        {
            get { return plugins; }
            set { plugins = value; }
        }
    }

    public class BasicProjectNaturePlugin : ProjectNaturePlugin
    {
        public BasicProjectNaturePlugin()
        {
            this.Natures.Add(new ProjectNature(typeof(ProjectInfo)));
            this.Natures.Add(new ProjectNature(typeof(FileInfo)));
            this.Natures.Add(new ProjectNature(typeof(FolderInfo)));
        }
    }
}
