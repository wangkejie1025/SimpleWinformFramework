using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class TileVWindowAction : Action
    {
        public TileVWindowAction()
        {
            this.Text = "Tile &Vertical Window";
        }

        public TileVWindowAction(string str)
        {
            this.Text = str;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }
    }
}
