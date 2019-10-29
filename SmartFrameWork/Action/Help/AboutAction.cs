using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class AboutAction : Action
    {
        public AboutAction()
            : this("&About ...")
        {
            
        }

        public AboutAction(string text)
        {
            this.Icon = "help.png";
            this.Text = text;
            this.BeginGroup = true;
        }

        public override void Perform(SmartFrameWork.ActionContext context)
        {
            //AboutDialog dlg = new AboutDialog();
            //dlg.ShowDialog();
        }
    }
}
