using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFrameWork
{
    public class OpenFileAction:Action
    {
        public OpenFileAction()
        {
            this.Text = "&Open File";
            this.Icon = "openfile.png";
            this.ShortCut = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
        }
        public override bool IsEnable(ActionContext context)
        {
            return base.IsEnable(context);
        }
        public override void Perform(ActionContext context)
        {
            base.Perform(context);
        }
    }
}
