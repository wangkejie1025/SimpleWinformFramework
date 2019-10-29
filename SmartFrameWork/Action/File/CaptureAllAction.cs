using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    /// <summary>
    /// 获取所有MDI子窗体
    /// </summary>
    public class CaptureAllAction : Action
    {
        public CaptureAllAction()
            : this("&Capture All")
        {
        }

        public CaptureAllAction(string text)
        {
            this.Text = text;
            this.Icon = "savefile.png";
            this.ShortCut = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
        }
        public override void Perform(SmartFrameWork.ActionContext context)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (SmartFrameWork.FrameWorkEditor editor in context.Window.GetEditors())
                    {
                        editor.Capture(string.Format(@"{0}\{1}.png", dlg.SelectedPath, editor.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                SmartFrameWork.Services.MessageService.ShowException(ex,"Fail to save file");
            }
        }
    }
}
