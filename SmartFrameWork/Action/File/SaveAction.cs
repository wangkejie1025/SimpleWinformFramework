using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class SaveAction : Action
    {
        public SaveAction()
            : this("&Save")
        {
        }

        public SaveAction(string text)
        {
            this.Text = text;
            this.Icon = "savefile.png";
            this.ShortCut = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
        }

        public override bool IsEnable(ActionContext context)
        {
            FrameWorkEditor editor = context.Window.GetActiveEditor();
            if (editor != null && editor.SaveSupport)
            {
                return true;
            }
            return false;
        }

        public override void Perform(ActionContext context)
        {
            try
            {
                if (context.Window.GetActiveEditor() != null)
                {
                    context.Window.GetActiveEditor().Save();
                }
            }
            catch (Exception ex)
            {
                Services.MessageService.ShowException( ex,"Fail to save file");
            }
        }
    }
}
