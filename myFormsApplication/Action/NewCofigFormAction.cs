using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartFrameWork;

namespace myFormsApplication.Action
{
    public class NewCofigFormAction:SmartFrameWork.Action
    {
        public NewCofigFormAction()
        {
            this.Text = "New CofigForm";
        }
        public override bool IsEnable(SmartFrameWork.ActionContext context)
        {
            FrameWorkEditor editor = context.Window.GetEditor(typeof(configForm));
            return (editor==null);
        }
        public override void perform(SmartFrameWork.ActionContext context)
        {
            //如果设置了xtraTabbedMdiManager的control属性，则MDI形式是以tab的形式
            //复制的窗体事件需要重新绑定，否则不会执行,在resx文件中查看需要选择查看字符串还是查看图标
            configForm configForm = new configForm();
            configForm.Text = Properties.Resources.configFormText;
            configForm.Icon = Properties.Resources.configFormIcon;
            configForm.MdiParent = context.Window;
            //configForm.WindowState = FormWindowState.Maximized;
            configForm.Show();
        }
    }
}
