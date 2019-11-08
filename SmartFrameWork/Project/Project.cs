using System;
using System.Collections.Generic;

namespace SmartFrameWork.Project
{
    //用来管理打开的工程和最近打开的工程
    public class ProjectManager
    {
        private static List<string> recentProjects = new List<string>();
        public static List<string> RecentProjects
        {
            get { return recentProjects; }
            set { recentProjects = value; }
        }
        private static List<string> openedProjects = new List<string>();
        public static List<string> OpenedProjects
        {
            get { return openedProjects; }
            set { openedProjects = value; }
        }
        //在ProjectManager_ProjectCreated事件中添加路径
        public static void AddOpenedProjects(string project)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(project);
            if (!OpenedProjects.Contains(file.FullName))
                OpenedProjects.Add(file.FullName);
            Services.PropertyService.Set(nameof(OpenedProjects), OpenedProjects);

        }
        public static void AddRecentProjects(string project)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(project);
            if (!ProjectManager.RecentProjects.Contains(file.FullName))
            {
                ProjectManager.RecentProjects.Insert(0, file.FullName);
            }
            if (ProjectManager.RecentProjects.Count > 10)
            {
                ProjectManager.RecentProjects.RemoveAt(ProjectManager.RecentProjects.Count - 1);
            }
            Services.PropertyService.Set(nameof(RecentProjects), RecentProjects);
        }
        //config保存的是list<string>的路径
        public static void Save(string fileName, List<string> config)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = null;
            try
            {
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
                xmlSerializer.Serialize(fs, config);
                fs.Close();
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error(ex);
            }
        }

        public static List<string> Parse(string fileName)
        {

            System.Xml.Serialization.XmlSerializer xmlSerializer = null;
            try
            {
                xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                List<string> fiuconfigs = (List<string>)xmlSerializer.Deserialize(fs);
                fs.Close();
                return fiuconfigs;
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error(ex);
            }
            return new List<string>();
        }
    }
    [Serializable]
    public class ProjectInfo : FolderInfo, IProject
    {
        public ProjectInfo()
        {
            this.Image = "folder_close.png";
            this.SelectionImage = "folder_open.png";
        }

        public override string Text
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        public override string Filter
        {
            get
            {
                return ".prj";
            }
            set
            {
                base.Filter = value;
            }
        }
        private string extension = "prj";
        [Group("Property")]
        [System.ComponentModel.ReadOnly(true)]
        [System.Xml.Serialization.XmlIgnore]
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        string description;
        [Group("Property")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [System.Xml.Serialization.XmlIgnore]
        [Group("Location")]
        [System.ComponentModel.ReadOnly(true)]
        public string ProjectFileName
        {
            get { return string.Format(@"{0}\{1}.{2}", this.Path, this.Name, extension); }
        }


        private FolderInfo GetFolder(FolderInfo folder, Type type)
        {
            foreach (Element element in folder.Items)
            {
                if (element.GetType() == type && element is FolderInfo)
                {
                    return element as FolderInfo;
                }
                else if (element is FolderInfo)
                {
                    FolderInfo subfolder = GetFolder(element as FolderInfo, type);
                    if (subfolder != null)
                    {
                        return subfolder;
                    }
                }
            }
            return null;
        }

        public FolderInfo GetFolder(Type type)
        {
            return GetFolder(this, type);
        }
    }

    [Serializable]
    public class FolderInfo : ElementContainer, IFolder //文件夹信息
    {
        public FolderInfo()
        {
            this.Image = "folder_close.png";
            this.SelectionImage = "folder_open.png";
        }

        private string name;
        [Group("Location")]
        [Text("Foler")]
        [System.ComponentModel.ReadOnly(true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string filter;
        [Group("Basic")]
        [System.ComponentModel.ReadOnly(true)]
        public virtual string Filter
        {
            get { return filter; }
            set { filter = value; }
        }
        private System.IO.DirectoryInfo dirInfo;
        private string path;
        [Group("Location")]
        [Text("Path")]
        [System.ComponentModel.ReadOnly(true)]
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.Browsable(false)]
        public string Path
        {
            get
            {
                if (dirInfo != null)
                {
                    return dirInfo.FullName;
                }
                else if (path != null)
                {
                    dirInfo = new System.IO.DirectoryInfo(path);
                    return dirInfo.FullName;
                }
                return null;
            }
            set
            {
                path = value;
                if (value != null)
                {
                    dirInfo = new System.IO.DirectoryInfo(value);
                }
            }
        }

        public virtual bool IsAllowSubFolder()
        {
            return false;
        }

        public override bool Add(Element ele)
        {
            foreach (Element element in Items)
            {
                if (ele.Text == element.Text)
                {
                    return false;
                }
            }

            if (ele is FileInfo)//若是文件类型
            {
                if (!AddFile(ele as FileInfo))
                {
                    return false;
                }
            }

            if (ele is FolderInfo)//若是文件夹类型
            {
                if (!AddFolder(ele as FolderInfo))
                {
                    return false;
                }
            }
            return base.Add(ele);
        }

        private bool AddFolder(FolderInfo folder)
        {
            folder.Path = this.Path + "\\" + folder.Name;
            try
            {
                System.IO.Directory.CreateDirectory(folder.Path);
                return true;
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error(ex.Message);
            }
            return false;
        }

        private bool AddFile(FileInfo file)
        {
            if (file.Path != null)
            {
                string src = string.Format(@"{0}\{1}", file.Path, file.Name);
                string dest = string.Format(@"{0}\{1}", this.Path, file.Name);
                System.IO.FileInfo srcFile = new System.IO.FileInfo(src);
                System.IO.FileInfo destFile = new System.IO.FileInfo(dest);
                if (srcFile.FullName != destFile.FullName)
                {
                    System.IO.File.Copy(srcFile.FullName, destFile.FullName, true);
                    return true;
                }
            }
            file.Path = this.Path;
            return true;
        }

        public override void Validate()
        {
            //根据Name创建文件夹
            if (Parent != null)
            {
                FolderInfo parentFolder = (Parent as FolderInfo);
                this.Path = (Parent as FolderInfo).Path + "\\" + this.Name;
            }
            if (!System.IO.Directory.Exists(this.Path))
            {
                System.IO.Directory.CreateDirectory(this.Path);
            }
            base.Validate();
        }

        public FileInfo GetFile(string name)
        {
            foreach (Element ele in this.Items)
            {
                FileInfo file = ele as FileInfo;
                if (file != null && file.Name == name)
                {
                    return file;
                }
            }
            return null;
        }

        public virtual void Save()
        {
            foreach (Element element in Items)
            {
                if (element is FileInfo)//若是文件类型
                {
                    (element as FileInfo).Save();
                }

                if (element is FolderInfo)//若是文件夹类型
                {
                    (element as FolderInfo).Save();
                }
            }
        }

        public override void Remove()
        {
            base.Remove();
            if (this.Parent != null)
                this.Parent.Remove(this);
        }
    }


    [Serializable]
    public class FileInfo : Element, IFile, IDragable
    {
        public FileInfo()
        {
            this.Image = "file.png";
        }

        private string name;
        [Group("Location")]
        [Text("File Name")]
        [System.ComponentModel.ReadOnly(true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private System.IO.DirectoryInfo dirInfo;
        private string path;
        [Group("Location")]
        [Text("Path")]
        [System.ComponentModel.ReadOnly(true)]
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.Browsable(false)]
        public string Path
        {
            get
            {
                if (dirInfo != null)
                    return dirInfo.FullName;
                else if (path != null)
                {
                    dirInfo = new System.IO.DirectoryInfo(path);
                    return dirInfo.FullName;
                }
                return null;
            }
            set
            {
                path = value;
                if (value != null)
                {
                    dirInfo = new System.IO.DirectoryInfo(value);
                }
            }
        }


        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.ReadOnly(true)]
        [Group("Location")]
        public string Extension
        {
            get
            {

                System.IO.FileInfo info = new System.IO.FileInfo(this.Path + "\\" + this.Name);
                return info.Extension;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.ReadOnly(true)]
        [Group("Location")]
        public string FullName
        {
            get
            {
                System.IO.FileInfo info = new System.IO.FileInfo(this.Path + "\\" + this.Name);
                return info.FullName;
            }
            set
            {
                System.IO.FileInfo info = new System.IO.FileInfo(value);
                this.Name = info.Name;
                this.Text = info.Name.Split('.')[0];
                this.Path = info.DirectoryName;
            }
        }

        public virtual void Save()
        { }

        public override void Validate()
        {
            if (Parent != null)
            {
                FolderInfo parentFolder = (Parent as FolderInfo);
                this.Path = (Parent as FolderInfo).Path;
            }
            base.Validate();
        }

        public override void Remove()
        {
            if (Parent != null)
            {
                Parent.Remove(this);
            }
        }
    }

    [Serializable]
    public class Element : Utils.Observable, ISelectable
    {
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public ElementContainer Root
        {
            get
            {
                ElementContainer p = parent;
                while (p.Parent != null)
                {
                    p = p.Parent;
                }
                return p;
            }
        }

        private ElementContainer parent;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public ElementContainer Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private string text;
        [Group("Basic")]
        [Text("Name")]
        [System.ComponentModel.ReadOnly(true)]
        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string image;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        private string selectionImage;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public string SelectionImage
        {
            get
            {
                if (selectionImage != null)
                    return selectionImage;
                else
                    return image;
            }
            set { selectionImage = value; }
        }


        public virtual void Validate()
        {

        }

        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.Browsable(false)]
        public virtual bool Removable
        {
            get { return true; }
        }
    }

    [Serializable]
    public class ElementContainer : Element
    {
        public delegate void ItemEvent(Element parent, Element item);
        public event ItemEvent ItemAdd;
        public new event ItemEvent ItemRemoved;

        protected void AddEvent(Element item, Element child)
        {
            if (ItemAdd != null)
            {
                ItemAdd(item, child);
            }
            else if (Parent != null)
            {
                Parent.AddEvent(item, child);
            }
        }

        protected void RemoveEvent(Element item, Element child)
        {
            if (ItemRemoved != null)
            {
                ItemRemoved(item, child);
            }
            else if (Parent != null)
            {
                Parent.RemoveEvent(item, child);
            }
        }

        private List<Element> items;
        [System.ComponentModel.Browsable(false)]
        public List<Element> Items
        {
            get
            {
                if (items == null)
                {
                    items = new List<Element>();
                }
                return items;
            }
        }


        public virtual bool Exist(Element ele)
        {
            foreach (Element element in Items)
            {
                if (ele.Text == element.Text)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Validate()
        {
            base.Validate();
            if (items != null)
            {
                foreach (Element ele in items)
                {
                    ele.Parent = this;
                    ele.Validate();
                }
            }
        }

        public virtual bool Add(Element ele)
        {
            Items.Add(ele);
            ele.Parent = this;
            AddEvent(this, ele);
            return true;
        }

        public virtual void Remove(Element ele)
        {
            Items.Remove(ele);
            RemoveEvent(this, ele);
        }

        public Element GetElement(Type type)
        {
            foreach (Element ele in this.Items)
            {
                if (ele.GetType() == type)
                {
                    return ele;
                }

                if (ele is ElementContainer)
                {
                    ElementContainer container = ele as ElementContainer;
                    Element subele = container.GetElement(type);
                    if (subele != null)
                    {
                        return subele;
                    }
                }
            }
            return null;
        }

        public Element GetElement(Type type, string name)
        {
            foreach (Element ele in this.Items)
            {
                if (ele.GetType() == type && ele.Text == name)
                {
                    return ele;
                }

                if (ele is ElementContainer)
                {
                    ElementContainer container = ele as ElementContainer;
                    Element subele = container.GetElement(type, name);
                    if (subele != null)
                    {
                        return subele;
                    }
                }
            }
            return null;
        }
    }

    public interface IModule : ISelectable
    {
    }

    public interface IModuleFolder : ISelectable
    {
    }

    public interface IFolder : ISelectable
    {
    }

    public interface IFile : ISelectable
    {
    }

    public interface IProject : ISelectable
    {
        string Description { get; set; }        
    }
}
