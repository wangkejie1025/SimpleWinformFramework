using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartFrameWork.Services;

namespace SmartFrameWork
{
    public class SaveAllAction : Action
    {
        public SaveAllAction()
            : this("Save &All")
        {
        }

        public SaveAllAction(string text)
        {
            this.Text = text;
            this.Icon = "saveall.png";
            this.ShortCut = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A;
        }

        public override bool IsEnable(ActionContext context)
        {
            return context.Window.GetActiveEditor() != null;
        }

        public override void Perform(SmartFrameWork.ActionContext context)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageService.ShowException( ex,"Fail to save file");
            }
        }
    }
}
