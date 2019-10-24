using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class CloseAllAction : Action
    {
        public CloseAllAction()
        {
            this.Text = "Close &All";
        }

        public CloseAllAction(string name)
        {
            this.Text = name;
        }
        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.CloseAll();
        }
    }
}
