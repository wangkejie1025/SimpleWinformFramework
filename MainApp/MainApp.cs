using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using SmartFrameWork.Services;
using SmartFrameWork;
using SmartFrameWork.Views;
using SmartFrameWork.Project;
using MainApp.ProjectElement;
using SmartFrameWork.Utils;

namespace MainApp
{
    static class MainApp
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoggingService.Info("Application start");

            SetSkin("DevExpress Style");

            // paths relative to the application root:
            // "data/resources" for language resources, "data/options" for default options
            FileUtility.ApplicationRootPath = Application.StartupPath;

            // CoreStartup is a helper class making starting the Core easier.
            //if coreStartup.PropertiesName not set,PropertiesName is mainApp app+xml
            CoreStartup coreStartup = new CoreStartup("mainApp")
            {

                // Specify the name of the application settings file (.xml is automatically appended)
                //xml rootElement Name
                PropertiesName = "AppProperties"
            };

            // Initializes the Core services (ResourceService, PropertyService, etc.)
            //two file flod data and config in Application.StartupPath，winform location will save in config direction
            coreStartup.StartCoreServices();

            //注册project对象相关的类型
            ProjectNaturePluginManager.Plugins.Add(new BasicProjectNaturePlugin());
            ProjectNaturePluginManager.Plugins.Add(new CoreProjectNaturePlugin());
            ProjectNaturePluginManager.Plugins.Add(new SpecificProjectNaturePlugin());
            //可以使用resx文件实现多语言,如果要添加prjectView，需使用WorkspaceWindow类
            using (FrameWorkWindow mainWin = new WorkspaceWindow())
            {
                mainWin.SuspendLayout();
                mainWin.Text = "myCompany FCT";
                mainWin.Icon = Properties.Resources.App;
               
                CreateView(mainWin);
           
                CreateMenu(mainWin);
 
                CreateToolBar(mainWin);

                //mainWin.LoadAction = new myFormsApplication.Action.LoadAction();
                mainWin.LoadAction =new  ProjectLoadAction();
                mainWin.ResumeLayout();
                try
                {
                    LoggingService.Info("Running application...");
                    Application.Run(mainWin);
                }
                catch (Exception ex)
                {
                    MessageService.ShowException(ex, ex.Message);
                }
                finally
                {
                    PropertyService.Save();
                    LoggingService.Info("Application shutdown");
                }
            }
        }
        private static void SetSkin(string skinName)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }
        private static void CreateView(FrameWorkWindow mainWin)
        {
            ProjectView projectView = new ProjectView
            {
                Name = ProjectView.NAME
            };
            mainWin.AddView(projectView, DevExpress.XtraBars.Docking.DockingStyle.Left);
            mainWin.AddViewCreator(new ProjectViewCreator());
            ConsoleView consoleView = new ConsoleView
            {
                Name = ConsoleView.NAME
            };
            mainWin.AddView(consoleView, DevExpress.XtraBars.Docking.DockingStyle.Bottom);
            mainWin.AddViewCreator(new ConsoleViewCreator());

            PropertyView propertyView = new PropertyView()
            {
                Name = PropertyView.NAME
            };
            mainWin.AddView(propertyView, DevExpress.XtraBars.Docking.DockingStyle.Right);
            mainWin.AddViewCreator(new PropertyViewCreator());
        }
        private static void CreateMenu(FrameWorkWindow mainWin)
        {
            //创建File菜单栏
            CreateFileMenuItem(mainWin);
            //创建View菜单栏
            CreateViewMenuItem(mainWin);
            //创建Window菜单
            CreateWindowMenuItem(mainWin);
            //创建Help菜单
            CreateHelpMenuItem(mainWin);
        }
        private static void CreateFileMenuItem(FrameWorkWindow mainWin)
        {
            //xtraTabbedMdiManager Controller属性如果设置为FrameWorkWindow，则为tab形式
            //mainWin中的barmanager需要绑定imaglist
            ActionGroup fileMenu = new ActionGroup
            {
                Text = "&File",
                //index属性暂时还未起作用
                Index = 1
            };
            ProjectDescriptorManager manager = new ProjectDescriptorManager();
            IProjectDescriptor descriptor = new ProjectDescriptor();
            manager.ProjectDescriptors.Add(descriptor);
            fileMenu.Add(new NewProjectAction(manager)).BeginGroup = true;
            fileMenu.Add(new OpenProjectAction(descriptor.Name, descriptor.Extension));
            fileMenu.Add(new SaveProjectAction(descriptor.Extension));
            fileMenu.Add(new CloseProjectAction());
            mainWin.AddToolBarGroup(fileMenu);

            RecentProjectsAction recentProject = new RecentProjectsAction();
            recentProject.OpenAction = new OpenRecentProjectAction();
            fileMenu.Add(recentProject).BeginGroup = true;
            fileMenu.Add(new ExitAction());
            //窗体的关闭事件
            mainWin.ExitAction = new ExitAction();
            mainWin.AddMenuGroup(fileMenu);

        }
        private static void CreateViewMenuItem(FrameWorkWindow mainWin)
        {
            //需要把各种view加入到view视图中，意外关闭后重新打开
            ActionGroup viewMenu = new ActionGroup
            {
                Text = "&View"
            };
            viewMenu.Add(new ViewListAction());
            mainWin.AddMenuGroup(viewMenu);
        }
        private static void CreateWindowMenuItem(FrameWorkWindow mainWin)
        {
            ActionGroup windowMenu = new ActionGroup
            {
                Text = "&Window"
            };
            windowMenu.Add(new WindowMDIAction());
            windowMenu.Add(new WindowTabbedAction());
            windowMenu.Add(new ArrangeIconAction()).BeginGroup = true;
            windowMenu.Add(new CascadeWindowAction());
            windowMenu.Add(new TileHWindowAction());
            windowMenu.Add(new TileVWindowAction());
            windowMenu.Add(new MdiListAction()).BeginGroup = true;
            windowMenu.Add(new CloseAllAction());
            //windowMenu.Add(new myFormsApplication.Action.NewCofigFormAction());
            
            mainWin.AddMenuGroup(windowMenu);
        }
        private static void CreateHelpMenuItem(FrameWorkWindow mainWin)
        {
            ActionGroup helpMenu = new ActionGroup
            {
                Text = "&Help"
            };
            helpMenu.Add(new HelpAction());
            helpMenu.Add(new HelpSearchAction());
            helpMenu.Add(new TipofDayAction());
            helpMenu.Add(new AboutAction());
            mainWin.AddMenuGroup(helpMenu);
        }
        private static void CreateToolBar(FrameWorkWindow mainWin)
        {

        }
        //菜单栏和工具栏共享的Action
        private static void CreateToolBarItemFromMenuItem(FrameWorkWindow mainWin, ActionGroup menu)
        {

        }
    }
}
