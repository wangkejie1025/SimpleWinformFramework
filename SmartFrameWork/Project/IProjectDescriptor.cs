using System.Collections.Generic;

namespace SmartFrameWork.Project
{
    public interface IProjectDescriptor
    {
        string Name { get; }

        string Extension { get; }

        string Icon { get; }

        string Description { get; }

        IProject Create(string name, string path);
    }

    public class ProjectDescriptorManager
    {
        List<IProjectDescriptor> projectDescriptors = new List<IProjectDescriptor>();
        public List<IProjectDescriptor> ProjectDescriptors
        {
            get { return projectDescriptors; }
            set { projectDescriptors = value; }
        }
    }
}
