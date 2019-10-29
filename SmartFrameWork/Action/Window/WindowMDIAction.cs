using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class  WindowMDIAction : Action
    {
        public WindowMDIAction()
        {
            this.Text = "MDI Style";
        }

        public WindowMDIAction(string str)
        {
            this.Text = str;
        }

        public override void Perform(SmartFrameWork.ActionContext context)
        {
            context.Window.WindowStyle = WindowSytle.MdiLayout;
        }
    }
}
