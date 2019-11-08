using System.Windows.Forms;

namespace SmartFrameWork.Project
{
    public class OpenProjectAction:Action
    {
        string projectType;
        public string ProjectType
        {
            get { return projectType; }
            set { projectType = value; }
        }

        string extension;
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        public OpenProjectAction()
        {
            this.Text = "&Open Project";
            this.Icon = "openproject.png";
        }
        public OpenProjectAction(string type, string extension)
        {
            this.Text = "&Open Project";
            this.Icon = "openproject.png";
            this.ProjectType = type;
            this.Extension = extension;
        }
        public void Open(ActionContext context,string projectPath)
        {
            ProjectFile file = ProjectFile.Open(projectPath);
            if (file != null)
            {
                ProjectInfo project = file.Project;
                System.IO.FileInfo info = new System.IO.FileInfo(projectPath);
                project.Path = info.DirectoryName;
                if (ProjectManager.OpenedProjects.Contains(project.ProjectFileName))
                {
                    Services.MessageService.ShowMessage("The same project is already open！");
                    return;
                }
                //project.Validate();
                context.Window.Workspace.AddProject(project);
            }
            else
            {
                Services.MessageService.ShowError("Failed to open project!");
            }
        }
        public override bool IsEnable(ActionContext context)
        {
            return true;
        }
        public override void Perform(ActionContext context)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = ProjectType;
            dlg.DefaultExt = Extension;
            dlg.Filter = string.Format("{0}|*.{1}", ProjectType, Extension);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Open(context,dlg.FileName);              
            }
        }
    }
}
