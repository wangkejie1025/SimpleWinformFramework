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
            CoreStartup coreStartup = new CoreStartup("mainApp");

            // Specify the name of the application settings file (.xml is automatically appended)
            //xml rootElement Name
            coreStartup.PropertiesName = "AppProperties";

            // Initializes the Core services (ResourceService, PropertyService, etc.)
            //two file flod data and config in Application.StartupPath，winform location will save in config direction
            coreStartup.StartCoreServices();

            ////可以使用resx文件实现多语言
            SmartFrameWork.FrameWorkWindow mainWin = new SmartFrameWork.FrameWorkWindow();
            mainWin.Text = "mainApp";      
            mainWin.Icon = Properties.Resources.App;
            //CreateView
            CreateView(mainWin);
            //Create menu
            CreateMenuItem(mainWin);
            //Create toolbar 
            CreateToolBarItem(mainWin);
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
                MessageService.ShowException(ex, "Error storing properties");
            }
            finally
            {
                // Save changed properties
                PropertyService.Save();
                LoggingService.Info("Application shutdown");
            }
        }
        private static void SetSkin(string skinName)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            //"DevExpress Style" "Office 2010 Blue"
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }
        private static void CreateView(SmartFrameWork.FrameWorkWindow mainWin)
        {
            SmartFrameWork.Views.ConsoleView consoleView = new SmartFrameWork.Views.ConsoleView();
            consoleView.Name = SmartFrameWork.Views.ConsoleView.NAME;         
            mainWin.AddView(consoleView, DevExpress.XtraBars.Docking.DockingStyle.Bottom);
            mainWin.AddViewCreator(new SmartFrameWork.Views.ConsoleViewCreator());
        }
        private static void CreateMenuItem(SmartFrameWork.FrameWorkWindow mainWin)
        {
            //xtraTabbedMdiManager Controller属性如果设置为FrameWorkWindow，则为tab形式
            //mainWin中的barmanager需要绑定imaglist
            SmartFrameWork.ActionGroup fileMenu = new SmartFrameWork.ActionGroup();
            fileMenu.Text = "&File";
            //index属性暂时还未起作用
            fileMenu.Index = 1;
            SmartFrameWork.ActionGroup open = new SmartFrameWork.ActionGroup();
            open.Text = "&Open";
            fileMenu.Add(open);
            fileMenu.Add(new SmartFrameWork.SaveAction());
            fileMenu.Add(new SmartFrameWork.CloseAction());
            fileMenu.Add(new SmartFrameWork.CloseAllAction());
            mainWin.AddMenuGroup(fileMenu);

            //需要把各种view加入到view视图中，意外关闭后重新打开
            SmartFrameWork.ActionGroup viewMenu = new SmartFrameWork.ActionGroup();
            viewMenu.Text = "&View";
            viewMenu.Add(new SmartFrameWork.ViewListAction());
            mainWin.AddMenuGroup(viewMenu);

            SmartFrameWork.ActionGroup windowMenu = new SmartFrameWork.ActionGroup();
            windowMenu.Text = "&Window";
            windowMenu.Add(new SmartFrameWork.ArrangeIconAction());
            windowMenu.Add(new SmartFrameWork.CascadeWindowAction());
            windowMenu.Add(new SmartFrameWork.TileHWindowAction());
            windowMenu.Add(new SmartFrameWork.TileVWindowAction());
            windowMenu.Add(new SmartFrameWork.CloseAllAction());
            windowMenu.Add(new myFormsApplication.Action.NewCofigFormAction());
            windowMenu.Add(new SmartFrameWork.MdiListAction());
            mainWin.AddMenuGroup(windowMenu);

            SmartFrameWork.ActionGroup helpMenu = new SmartFrameWork.ActionGroup();
            helpMenu.Text = "&Help";
            helpMenu.Add(new SmartFrameWork.HelpAction());
            helpMenu.Add(new SmartFrameWork.HelpSearchAction());
            helpMenu.Add(new SmartFrameWork.TipofDayAction());
            helpMenu.Add(new SmartFrameWork.AboutAction());
            mainWin.AddMenuGroup(helpMenu);

        }
        private static void CreateToolBarItem(SmartFrameWork.FrameWorkWindow mainWin)
        {

        }
        private static void CreateToolBarItemFromMenuItem(SmartFrameWork.FrameWorkWindow mainWin, SmartFrameWork.ActionGroup menu)
        {

        }
    }
}
