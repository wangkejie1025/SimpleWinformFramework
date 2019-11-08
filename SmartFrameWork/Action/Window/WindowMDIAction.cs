namespace SmartFrameWork
{
    public class  WindowMDIAction : Action
    {
        public WindowMDIAction()
        {
            this.Text = "MDI Style";
            this.Icon = "mdiwindow.png";
        }

        public WindowMDIAction(string str)
        {
            this.Text = str;
        }

        public override void Perform(ActionContext context)
        {
            context.Window.WindowStyle = WindowSytle.MdiLayout;
        }
    }
}
