using SmartFrameWork.Project;
using System;

namespace MainApp.ProjectElement
{
    [Serializable]
    public class DeviceVariablesFolder : ElementContainer
    {
        public DeviceVariablesFolder()
        {
            this.Text = "DeviceVariables";
            this.Image = "folder.png";
            //this.Name = "DeviceVariables";
        }
    }
}
