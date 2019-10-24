using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class CascadeWindowAction : Action
    {
        public CascadeWindowAction()
        {
            this.Text = "&Cascade Window";
            this.Icon = "cascade.png";
        }

        public CascadeWindowAction(string text)
        {
            this.Text = text;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }
    }
}
