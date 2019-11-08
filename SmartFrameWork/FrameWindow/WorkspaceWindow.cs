using System.Windows.Forms;

namespace SmartFrameWork
{
    /// <summary>
    /// use for project window
    /// </summary>
    public class WorkspaceWindow : FrameWorkWindow
    {
        //多了一个projet管理的workspace侧边栏
        private string projectExtension = "prj";
        public string ProjectExtension
        {
            get { return projectExtension; }
            set { projectExtension = value; }
        }

        public override ActionContext GetActionContext()
        {
            ActionContext context = base.GetActionContext();
            //context.Project = Workspace.Active;
            //context.Project =(Project.IProject) context.Selection;
            return context;
        }

        public WorkspaceWindow()
        {
            this.FormClosing += new FormClosingEventHandler(WorkspaceWindow_FormClosing);
        }

        void WorkspaceWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
