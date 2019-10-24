using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartFrameWork.Services
{
    //具体的IMessageService实现类
    public class MessageBoxMessageService : IMessageService
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        public void ShowException(Exception ex, string message)
        {
            message = message + "\r\n" + ex.ToString();
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public string OpenFileDialog(string fileName, string extension)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open " + fileName;
            dlg.DefaultExt = extension;
            dlg.Filter = string.Format("{0}|*.{1}", fileName, extension);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.FileName;
            }
            return null;
        }
        public string SaveFileDialog(string fileName, string extension)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.DefaultExt = extension;
            dlg.Filter = string.Format("{0}|*.{1}", fileName, extension);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dlg.FileName;
            }
            return null;
        }
        public bool AskQuestion(string question, string caption)
        {
            return false;
        }

        public int ShowCustomDialog(string caption, string dialogText, int acceptButtonIndex, int cancelButtonIndex, params string[] buttontexts)
        {
            return cancelButtonIndex;
        }

        public string ShowInputBox(string caption, string dialogText, string defaultValue)
        {
            return defaultValue;
        }

        public void ShowMessage(string message, string caption)
        {

        }

        public void InformSaveError(string fileName, string message, string dialogName, Exception exceptionGot)
        {

        }

        public ChooseSaveErrorResult ChooseSaveError(string fileName, string message, string dialogName, Exception exceptionGot, bool chooseLocationEnabled)
        {
            return ChooseSaveErrorResult.Ignore;
        }
    }
}
