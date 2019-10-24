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

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.windowStyle = WindowSytle.MdiLayout;
        }
    }
}
