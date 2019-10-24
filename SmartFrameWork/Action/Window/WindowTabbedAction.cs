using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class  WindowTabbedAction : Action
    {
        public WindowTabbedAction()
        {
            this.Text = "Tabbed Style";
            this.Icon = "hwindow.png";
        }

        public WindowTabbedAction(string str)
        {
            this.Text = str;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.windowStyle = WindowSytle.Tabbed;
        }
    }
}
