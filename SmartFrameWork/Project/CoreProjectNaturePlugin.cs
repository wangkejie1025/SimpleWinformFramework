namespace SmartFrameWork.Project
{
    public class CoreProjectNaturePlugin:ProjectNaturePlugin
    {
        public CoreProjectNaturePlugin()
        {
            //project根节点
            this.Natures.Add(new ProjectNature(typeof(ProjectRepository)));
        }
    }
}
