using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class SaveAsAction : Action
    {
        public SaveAsAction()
            : this("Save As ...")
        {
        }

        public SaveAsAction(string text)
        {
            this.Text = text;
            this.Icon = "saveas.png";
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

        public override void Perform(SmartFrameWork.ActionContext context)
        {
            try
            {
                if (context.Window.GetActiveEditor() != null)
                {
                    context.Window.GetActiveEditor().SaveAs();
                }
            }
            catch (Exception ex)
            {
                SmartFrameWork.Services.MessageService.ShowException(ex,"Fail to save file");
            }
        }
    }
}
