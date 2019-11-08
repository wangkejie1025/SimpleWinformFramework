using SmartFrameWork.Project;
using System;

namespace MainApp.ProjectElement
{
    //没有加这个attribute反序列化的时候不会加入
    [Serializable]
    public class VirtualDeviceFolder : ElementContainer
    {
        public VirtualDeviceFolder()
        {
            this.Text = "VirtualDevice";
            this.Image = "folder.png";
            //this.Name = "VirtualDevice";
        }
    }
    public class SerialPort: ElementContainer
    {
        public SerialPort()
        {
            this.Text = "SerialPort";
            this.Image = "folder.png";
            //this.Name = "SerialPort";
        }
    }
    public class ModuBus_TCP : ElementContainer
    {
        public ModuBus_TCP()
        {
            this.Text = "ModuBus_TCP";
            this.Image = "folder.png";
            //this.Name = "ModuBus_TCP";
        }
    }
    public class Modubus_RTU : ElementContainer
    {
        public Modubus_RTU()
        {
            this.Text = "Modubus_RTU";
            this.Image = "folder.png";
            //this.Name = "Modubus_RTU";
        }
    }
    public class CANDriver : ElementContainer
    {
        public CANDriver()
        {
            this.Text = "CANDriver";
            this.Image = "folder.png";
            //this.Name = "CANDriver";
        }
    }
    public class Enternet : ElementContainer
    {
        public Enternet()
        {
            this.Text = "Enternet";
            this.Image = "folder.png";
            //this.Name = "Enternet";
        }
    }
}
