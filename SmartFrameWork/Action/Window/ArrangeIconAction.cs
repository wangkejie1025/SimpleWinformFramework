using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class ArrangeIconAction : Action
    {
        public ArrangeIconAction()
        {
            this.Text = "Arrange &Icon";
        }

        public ArrangeIconAction(string str)
        {
            this.Text = str;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
        }
    }
}
