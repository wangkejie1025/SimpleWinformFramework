using System;

namespace SmartFrameWork.Project
{
    [Serializable]
    public class ProjectRepository: ProjectInfo
    {
        public ProjectRepository()
        {
            this.Image = "folder_close.png";
            this.SelectionImage = "folder_open.png";
        }
        private Model.Repository repository = new Model.Repository();
        [System.Xml.Serialization.XmlIgnore]
        [System.ComponentModel.Browsable(false)]
        public Model.Repository Repository
        {
            get { return repository; }
            set { repository = value; }
        }
    }
}
