using SmartFrameWork.Project;
using MainApp.ProjectElement;

namespace MainApp
{
    //工程类型和具体的项目相关的
    public class ProjectDescriptor:IProjectDescriptor
    {
        public string Name { get { return "project"; } }

        public string Extension { get { return "prj"; } }

        public string Icon { get { return "App.ico"; } }

        public string Description { get { return ""; } }
        public IProject Create(string name, string path)
        {
            //将对应的对象加入到ProjectInfo中
            ProjectInfo projectInfo = new ProjectInfo();
            projectInfo.Name = name;
            projectInfo.Path = path;
            projectInfo.Add(new TestCaseFolder());

            var deviceVariables = new DeviceVariablesFolder();
            projectInfo.Add(deviceVariables);

            var virtualDevice = new VirtualDeviceFolder();
            virtualDevice.Add(new SerialPort());
            virtualDevice.Add(new Modubus_RTU());
            virtualDevice.Add(new ModuBus_TCP());
            virtualDevice.Add(new CANDriver());
            virtualDevice.Add(new Enternet());
            projectInfo.Add(virtualDevice);

            projectInfo.Add(new DatabaseFolder());
            projectInfo.Add(new SystemToolFolder());
            return projectInfo;
        }
    }
}
