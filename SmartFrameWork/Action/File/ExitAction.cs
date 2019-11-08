using SmartFrameWork.Project;

namespace SmartFrameWork
{
    public class ExitAction : Action
    {
        public ExitAction()
        {
            this.Text = "&Exit";
            this.ShortCut = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
            this.BeginGroup = true;
        }

        public ExitAction(string text)
        {
            this.Text = text;
        }
        //在系统退出的时候进行保存
        public override void Perform(ActionContext context)
        {         
            context.Window.Close();
           // ProjectManager.Save();
        }
    }
}
