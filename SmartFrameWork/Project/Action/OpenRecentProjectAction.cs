using System;
using SmartFrameWork.Project;

namespace SmartFrameWork
{
    public class OpenRecentProjectAction : OpenProjectAction
    {
        public OpenRecentProjectAction()
        {
            this.Text = "&Open Project";
            this.Icon = "open.png";
        }
        public OpenRecentProjectAction(string type, string extension) : base(type, extension) { }
        public override void Perform(ActionContext context)
        {
            try
            {
                //获取触发源
                DevExpress.XtraBars.BarItem barItem = context.Source as DevExpress.XtraBars.BarItem;
                if (barItem != null)
                {
                    string path = barItem.Links[0].Caption;
                    Open(context, path);
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
