using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class CloseAction : Action
    {
        public CloseAction()
        {
            this.Text = "&Close";
            this.ShortCut = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C;
            //this.BeginGroup = true;
        }

        public CloseAction(string text)
        {
            this.Text = text;
        }

        public override bool IsEnable(ActionContext context)
        {
            return context.Window.GetActiveEditor() != null;
        }

        public override void Perform(SmartFrameWork.ActionContext context)
        {
            if (context.Window.GetActiveEditor() != null)
            {
                context.Window.GetActiveEditor().Close();
            }
        }
    }
}
