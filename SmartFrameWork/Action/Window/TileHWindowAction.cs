using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class TileHWindowAction : Action
    {
        public TileHWindowAction()
        {
            this.Text = "Tile &Horizontal Window";
            this.Icon = "hwindow.png";
        }

        public TileHWindowAction(string str)
        {
            this.Text = str;
        }

        public override void perform(SmartFrameWork.ActionContext context)
        {
            context.Window.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }
    }
}
