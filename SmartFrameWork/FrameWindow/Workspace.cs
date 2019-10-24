using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class Workspace
    {
        public delegate void ProjectCreate(IProject project);
        public event ProjectCreate ProjectCreated;


        public delegate void ProjectClose(IProject project);
        public event ProjectClose ProjectClosed;

        private IProject active;
        public IProject Active
        {
            get { return active; }
            set { active = value; }
        }
        private List<IProject> projects = new List<IProject>();
        public List<IProject> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        public void AddProject(IProject project)
        {
            active = project;
            if (ProjectCreated != null)
            {
                if (!Projects.Contains(project))
                    Projects.Add(project);
                ProjectCreated(project);
            }
        }

        public void CloseProject(IProject project)
        {
            active = null;
            if (ProjectClosed != null)
            {
                if (Projects.Contains(project))
                    Projects.Remove(project);
                ProjectClosed(project);
            }
        }
    }
}
