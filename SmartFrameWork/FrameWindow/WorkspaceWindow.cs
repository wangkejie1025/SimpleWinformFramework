using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartFrameWork
{
    /// <summary>
    /// use for project window
    /// </summary>
    public class WorkspaceWindow : SmartFrameWork.FrameWorkWindow
    {
        //多了一个projet管理的workspace侧边栏
        private string projectExtension = "prj";
        public string ProjectExtension
        {
            get { return projectExtension; }
            set { projectExtension = value; }
        }

        public override SmartFrameWork.ActionContext GetActionContext()
        {
            ActionContext context = base.GetActionContext();
            context.Project = Workspace.Active;
            return context;
        }

        public WorkspaceWindow()
        {
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(WorkspaceWindow_FormClosing);
        }

        void WorkspaceWindow_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //SmartFrameWork.Configuration.Save();
            //SmartFrameWork.IProject prj = Workspace.Active;

            //if (prj != null)
            //{
            //    DialogResult result = MessageBox.Show("Do you want to save the project before exit?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //    {
            //        SmartFrameWork.ProjectFile file = new SmartFrameWork.ProjectFile();
            //        file.Project = prj as SmartFrameWork.Project;
            //        file.Project.Extension = ProjectExtension;
            //        file.Save(file.Project.ProjectFileName);
            //    }
            //    if (result == DialogResult.Cancel)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
    }
}
