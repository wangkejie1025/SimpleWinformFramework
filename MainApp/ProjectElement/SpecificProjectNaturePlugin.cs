using SmartFrameWork.Project;
namespace MainApp.ProjectElement
{
    public class SpecificProjectNaturePlugin: ProjectNaturePlugin
    {
        public SpecificProjectNaturePlugin()
        {
            this.Natures.Add(new ProjectNature(typeof(TestCaseFolder)));
            this.Natures.Add(new ProjectNature(typeof(DeviceVariablesFolder)));
            this.Natures.Add(new ProjectNature(typeof(DatabaseFolder)));
            this.Natures.Add(new ProjectNature(typeof(SystemToolFolder)));
            //没有加入到类管理不会导入到Project中
            this.Natures.Add(new ProjectNature(typeof(VirtualDeviceFolder)));

            this.Natures.Add(new ProjectNature(typeof(SerialPort)));
            this.Natures.Add(new ProjectNature(typeof(ModuBus_TCP)));
            this.Natures.Add(new ProjectNature(typeof(Modubus_RTU)));
            this.Natures.Add(new ProjectNature(typeof(CANDriver)));
            this.Natures.Add(new ProjectNature(typeof(Enternet)));
        }
    }
}
