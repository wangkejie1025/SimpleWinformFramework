namespace SmartFrameWork
{
    public class  WindowTabbedAction : Action
    {
        public WindowTabbedAction()
        {
            this.Text = "Tabbed Style";
            this.Icon = "tabbedwindow.png";
        }

        public WindowTabbedAction(string str)
        {
            this.Text = str;
        }

        public override void Perform(ActionContext context)
        {
            context.Window.WindowStyle = WindowSytle.Tabbed;
        }
    }
}
