using System;
using System.Collections.Generic;
namespace SmartFrameWork.Project
{
    public class ProjectLoadAction:Action
    {       
        public ProjectLoadAction()
        {
            
        }
        public override void Perform(ActionContext context)
        {
            try
            {
                ProjectManager.RecentProjects = Services.PropertyService.Get(nameof(ProjectManager.RecentProjects), new List<string>());
                var OpenedProjects = Services.PropertyService.Get(nameof(ProjectManager.OpenedProjects), new List<string>());
                foreach(var path in OpenedProjects)
                {
                    OpenProjectAction openProjectAction = new OpenProjectAction();
                    openProjectAction.Open(context,path);
                }
                ProjectManager.OpenedProjects = OpenedProjects;
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error(ex);
            }
        }
    }
}
