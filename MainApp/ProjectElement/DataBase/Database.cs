using SmartFrameWork.Project;
using System;

namespace MainApp.ProjectElement
{
    [Serializable]
    public class DatabaseFolder: ElementContainer
    {
        public DatabaseFolder()
        {
            this.Text = "Database";
            this.Image = "folder.png";
            //this.Name = "Database";
        }
    }
}
