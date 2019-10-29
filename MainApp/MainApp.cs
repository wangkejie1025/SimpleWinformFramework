using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using SmartFrameWork.Services;
using SmartFrameWork;
using SmartFrameWork.Views;

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

            // Set the root path of our application. ICSharpCode.Core looks for some other
            // paths relative to the application root:
            // "data/resources" for language resources, "data/options" for default options
            FileUtility.ApplicationRootPath = Application.StartupPath;

            // CoreStartup is a helper class making starting the Core easier.
            // The parameter is used as the application name, e.g. for the default title of
            // MessageService.ShowMessage() calls.
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

            ////可以使用resx文件实现多语言
            using (FrameWorkWindow mainWin = new FrameWorkWindow())
            {
                mainWin.Text = "mainApp";
                mainWin.Icon = Properties.Resources.App;
                //CreateView
                CreateView(mainWin);
                //Create menu
                CreateMenu(mainWin);
                //Create toolbar 
                CreateToolBar(mainWin);
                //add loadAction
                mainWin.LoadAction = new myFormsApplication.Action.LoadAction();
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
                    //Save changed properties
                    PropertyService.Save();
                    LoggingService.Info("Application shutdown");
                }
            }
        }
        private static void SetSkin(string skinName)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            //"DevExpress Style" "Office 2010 Blue"
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }
        private static void CreateView(FrameWorkWindow mainWin)
        {
            ConsoleView consoleView = new ConsoleView
            {
                Name = ConsoleView.NAME
            };
            mainWin.AddView(consoleView, DevExpress.XtraBars.Docking.DockingStyle.Bottom);
            mainWin.AddViewCreator(new ConsoleViewCreator());
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
            ActionGroup open = new ActionGroup
            {
                Text = "&Open"
            };
            fileMenu.Add(open);
            fileMenu.Add(new SaveAction());
            fileMenu.Add(new SaveAllAction());
            fileMenu.Add(new CloseAction());
            fileMenu.Add(new CloseAllAction());
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
            windowMenu.Add(new ArrangeIconAction());
            windowMenu.Add(new CascadeWindowAction());
            windowMenu.Add(new TileHWindowAction());
            windowMenu.Add(new TileVWindowAction());
            windowMenu.Add(new CloseAllAction());
            windowMenu.Add(new myFormsApplication.Action.NewCofigFormAction());
            windowMenu.Add(new MdiListAction());
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
            ActionGroup toolBarFileGroup = new ActionGroup();
            toolBarFileGroup.Add(new OpenFileAction());
            toolBarFileGroup.Add(new SaveAllAction());
            mainWin.AddToolBarGroup(toolBarFileGroup);
        }
        //菜单栏和工具栏共享的Action
        private static void CreateToolBarItemFromMenuItem(FrameWorkWindow mainWin, ActionGroup menu)
        {

        }
    }
}
