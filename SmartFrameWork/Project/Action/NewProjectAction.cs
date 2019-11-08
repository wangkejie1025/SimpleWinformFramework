using System;
using System.Windows.Forms;

namespace SmartFrameWork.Project
{
    public class NewProjectAction:Action
    {
        ProjectDescriptorManager manager;
        public ProjectDescriptorManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public NewProjectAction(ProjectDescriptorManager manager)
        {
            this.Text = "&New Project";
            this.Icon = "new.png";
            this.Manager = manager;
        }

        public override bool IsEnable(ActionContext context)
        {
            return true;
        }
        public override void Perform(ActionContext context)
        {
            try
            {
                NewProjectDialog dlg = new NewProjectDialog();
                dlg.Manager=this.manager;
                if (dlg.ShowDialog() == DialogResult.OK)
                {               
                    IProjectDescriptor projectDescripor = dlg.Descriptor;
                    //Create会触发Validate验证
                    IProject project = projectDescripor.Create(dlg.ProjectName, string.Format(@"{0}\{1}", dlg.ProjectPath, dlg.ProjectName));
                    project.Description = dlg.Desription;
                    ProjectInfo projectInfo = project as ProjectInfo;
                    ProjectFile file = new ProjectFile
                    {
                        Project = projectInfo
                    };
                    file.Project.Extension = projectDescripor.Extension;
                    //如果已经存在
                    if (System.IO.File.Exists(file.Project.ProjectFileName))
                    {
                        var dialogResult= Services.MessageService.ShowQuestion("The same project is already exist,press OK will delete the project on you disk.");
                        if (dialogResult == DialogResult.OK)
                        {
                            Utils.FileUtility.DelectDirectory(file.Project.Path);
                        }
                        else
                            return;
                    }
                    //file.Project.Validate();
                    file.Save(file.Project.ProjectFileName);
                    //显示到view上 会触发projectCreated事件
                    //关于最近打开的文件和已经打开的文件的路径都在工程创建的事件中执行
                    context.Window.Workspace.AddProject(project);
                }
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error(ex);
            }
        }
    }
}
