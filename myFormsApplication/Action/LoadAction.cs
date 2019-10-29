using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartFrameWork.Services;

namespace myFormsApplication.Action
{
    public class LoadAction:SmartFrameWork.Action
    {
        public LoadAction()
        {
            this.Text = "LoadMainForm";
        }
        public override void Perform(SmartFrameWork.ActionContext context)
        {
            try
            {
                //如果设置了xtraTabbedMdiManager的control属性，则MDI形式是以tab的形式
                //复制的窗体事件需要重新绑定，否则不会执行
                //多个子窗体由dev框架BarMdiChildrenListItem自动管理
                configForm configForm = new configForm("configForm");
                configForm.Text = Properties.Resources.configFormText;
                configForm.Icon = Properties.Resources.configFormIcon;
                configForm.MdiParent = context.Window;
                //configForm.WindowState = FormWindowState.Maximized;
                configForm.Show();

                //configForm configForm1 = new configForm();
                //configForm1.Text = Properties.Resources.configFormText;
                //configForm1.Icon = Properties.Resources.configFormIcon;
                //configForm1.MdiParent = context.Window;
                ////configForm.WindowState = FormWindowState.Maximized;
                //configForm1.Show();
            }
            catch (Exception ex)
            {
                MessageService.ShowException(ex, "Load Error");
                LoggingService.Error("Load Error!"+ex.ToString());
            }

        }
    }
}
