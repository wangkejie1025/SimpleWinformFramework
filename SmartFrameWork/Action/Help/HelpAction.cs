using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class HelpAction : Action
    {
        public HelpAction() : this("&Contents")
        {
        }

        public HelpAction(string text)
        {
            this.Text = text;
            this.ShortCut = System.Windows.Forms.Keys.F1;
            this.BeginGroup = true;
            //this.Icon = "help.png";
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
            //StartupEditor editor = new StartupEditor();
            //editor.MdiParent = context.Window;
            //editor.Show();
        }
    }

    public class HelpSearchAction : Action
    {
        public HelpSearchAction()
            : this("&Search ...")
        {
        }

        public HelpSearchAction(string text)
        {
            this.Text = text;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
        }
    }
  
    public class TipofDayAction : Action
    {
        public TipofDayAction()
            : this("&Tips of Day ...")
        {
        }

        public TipofDayAction(string text)
        {
            this.Text = text;
            this.BeginGroup = true;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
        }
    }
}
