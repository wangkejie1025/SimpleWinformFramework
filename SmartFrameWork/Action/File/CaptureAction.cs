using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class CaptureAction : Action
    {
        public CaptureAction()
            : this("&Capture")
        {
        }

        public CaptureAction(string text)
        {
            this.Text = text;
            this.Icon = "savefile.png";
            this.ShortCut = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
        }

        public override bool IsEnable(ActionContext context)
        {
            FrameWorkEditor editor = context.Window.GetActiveEditor();
            if (editor != null)
            {
                return true;
            }
            return false;
        }

        public override void Perform(SmartFrameWork.ActionContext context)
        {
            try
            {
                //if (context.Window.GetActiveEditor() != null)
                //{
                //    string fileName =SmartFrameWork.Services.MessageService.SaveFileDialog("Image files", "png");
                //    if (fileName != null)
                //    {
                //        context.Window.GetActiveEditor().Capture(fileName);
                //    }
                //}
            }
            catch (Exception ex)
            {
                SmartFrameWork.Services.MessageService.ShowException(ex,"Fail to save file");
            }
        }
    }
}
