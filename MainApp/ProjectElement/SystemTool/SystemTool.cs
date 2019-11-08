using SmartFrameWork.Project;
using System;

namespace MainApp.ProjectElement
{
    [Serializable]
    public class SystemToolFolder : ElementContainer
    {
        public SystemToolFolder()
        {
            this.Text = "SystemTool";
            this.Image = "folder.png";
            //this.Name = "SystemTool";
        }
    }
}
