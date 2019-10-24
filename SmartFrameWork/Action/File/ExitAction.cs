using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.Close();
        }
    }
}
