using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFrameWork.Project
{
    public class CloseProjectAction : Action
    {
        public CloseProjectAction()
        {
            this.Text = "Close Project";
            this.Icon = "closeproject.png";
        }

        public override bool IsEnable(ActionContext context)
        {
            return context.Selection is IProject;
        }

        public override void Perform(ActionContext context)
        {
            try
            {
                context.Project = context.Selection as IProject;
                var result = Services.MessageService.ShowQuestion("Do you want to close the selected project?");
                if(result==System.Windows.Forms.DialogResult.OK)
                context.Window.Workspace.CloseProject(context.Project);
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error(ex);
            }
        }
    }
}
