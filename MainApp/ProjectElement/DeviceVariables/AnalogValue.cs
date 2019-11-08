using SmartFrameWork;
using SmartFrameWork.Project;
using System;

namespace MainApp.ProjectElement
{
    public class AnalogInputContainer : ElementContainer
    {
        public AnalogInputContainer()
        {
            this.Text = "AD";
            this.Image = "folder.png";
            //this.Name = "AD";
        }
    }
    public class AnalogInputItem : Element, IDragable
    {

    }
}
