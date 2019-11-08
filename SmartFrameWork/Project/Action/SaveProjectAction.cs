using System;

namespace SmartFrameWork.Project
{
    public class SaveProjectAction :Action
    {
        string extension = "prj";
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        public SaveProjectAction(string extension)
        {
            this.Text = "&Save Project";
            this.Icon = "savefile.png";
            this.Extension = extension;
        }

        public override bool IsEnable(ActionContext context)
        {
            return (context.Project != null);
        }

        public override void Perform(ActionContext context)
        {
            try
            {
                //通过上下文获取project对象
                IProject prj = context.Project;
                ProjectFile file = new ProjectFile();
                file.Project = prj as ProjectInfo;
                file.Project.Extension = Extension;
                file.Save(file.Project.ProjectFileName);
                //需要创建子文件夹
                if (prj is FolderInfo)
                {
                    (prj as FolderInfo).Save();
                }
            }
            catch (Exception ex)
            {
                Services.MessageService.ShowException(ex);
            }
        }
    }
}
