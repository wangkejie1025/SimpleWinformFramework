using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using MessageService = SmartFrameWork.Services;

namespace SmartFrameWork
{
    /// <summary>
    /// base view class
    /// </summary>
    public partial class FrameWorkView : UserControl
    {
        private DockPanel panel;
        public DockPanel Panel
        {
            get { return panel; }
            set { panel = value; }
        }

        private DockPanel parentPanel;
        public DockPanel ParentPanel
        {
            get { return parentPanel; }
            set { parentPanel = value; }
        }

        private bool isDefault = false;
        public bool IsDefault
        {
            get { return isDefault; }
            set
            {
                isDefault = value;
                if (isDefault && parentPanel != null)
                {
                    parentPanel.ActiveChild = panel;
                }
            }
        }
        private bool toolBarVisible;
        public bool ToolBarVisible
        {
            get { return toolBarVisible; }
            set { toolBarVisible = value; barManager.Bars["Main toolbar"].Visible = toolBarVisible; }
        }
        public int GetIconIndex(Type type, string icon)
        {
            //嵌入的资源最后的命名空间就是程序集的名称+文件夹名称+文件名
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(type);
            int extension = assembly.ManifestModule.Name.LastIndexOf('.');
            string key = assembly.ManifestModule.Name.Substring(0, extension) + ".icons." + icon;
            System.IO.Stream stream = assembly.GetManifestResourceStream(key);
            if (stream != null)
            {
                Image iconImage = Image.FromStream(stream);
                imageList.Images.Add(key, iconImage);
                return imageList.Images.IndexOfKey(key);
            }
            return -1;
        }

        private FrameWorkWindow window;
        public virtual FrameWorkWindow Window
        {
            get { return window; }
            set { window = value; }
        }

        public FrameWorkView()
        {
            InitializeComponent();
        }

        public virtual string Icon
        {
            get { return null; }
        }

        /// <summary>
        /// 执行Action前由框架调用
        /// </summary>
        public virtual ISelectable SelectedItem
        {
            get { return null; }
        }

        /// <summary>
        /// 项目关闭时由框架调用
        /// </summary>
        public virtual void Close()
        {

        }

        public virtual void ActiveEditorChanged()
        {

        }

        public virtual ActionContext GetActionContext(ActionContext context)
        {
            return context;
        }

        #region 选中事件

        private ActionGroup selectionAction;
        public virtual ActionGroup SelectionAction
        {
            get { return selectionAction; }
            set { selectionAction = value; }
        }

        public void TriggerSelectionAction()
        {
            TriggerAction(selectionAction);
        }

        private bool isTriggered = false;
        public bool IsTriggered
        {
            get { return isTriggered; }
            set { isTriggered = value; }
        }

        public void TriggerAction(ActionGroup actionGroup)
        {
            isTriggered = false;

            if (actionGroup == null)
            {
                return;
            }
            ActionContext context = GetActionContext(window.GetActionContext());
            context.Selection = SelectedItem;
            foreach (Action action in actionGroup.Items)
            {
                try
                {
                    if (action.IsEnable(context))
                    {
                        action.Excuete(context);
                        window.ValidateAction();
                        isTriggered = true;
                    }
                }
                catch (Exception e)
                {
                    SmartFrameWork.Services.MessageService.ShowException(e,"Fail to execute action [" + action.Text + "]!");
                }
            }
        }


        #endregion


        #region 右键菜单

        private ActionGroup contextAction;
        public virtual ActionGroup ContextAction
        {
            get { return contextAction; }
            set { contextAction = value; }
        }
        protected void BuildSubMenu(BarSubItem barSubItem, ActionGroup group)
        {
            foreach (Action action in group.Items)
            {
                BarButtonItem barItem = new BarButtonItem();
                barItem.Tag = action;
                barItem.Caption = action.Text;
                barItem.ImageIndex = GetIconIndex(action.GetType(), action.Icon);
                barItem.ItemShortcut = new BarShortcut(action.ShortCut);

                barItem.ItemClick += new ItemClickEventHandler(this.menu_ItemClick);

                barSubItem.LinksPersistInfo.Add(new LinkPersistInfo(barItem, action.BeginGroup));
                this.barManager.Items.Add(barItem);
            }
        }

        protected void BuildMenu(System.Windows.Forms.Control control)
        {
            this.barManager.SetPopupContextMenu(control, this.ctxMenu);

            foreach (Action action in contextAction.Items)
            {
                if (action is ActionGroup)
                {
                    BarSubItem subBarItem = new BarSubItem();
                    subBarItem.Caption = action.Text;
                    subBarItem.ImageIndex = GetIconIndex(action.GetType(), action.Icon);
                    this.ctxMenu.LinksPersistInfo.Add(new LinkPersistInfo(subBarItem, action.BeginGroup));
                    this.barManager.Items.Add(subBarItem);
                    BuildSubMenu(subBarItem, action as ActionGroup);
                }

                else
                {
                    BarButtonItem barItem = new BarButtonItem();
                    barItem.Caption = action.Text;
                    barItem.Tag = action;
                    barItem.ItemShortcut = new BarShortcut(action.ShortCut);
                    barItem.ImageIndex = GetIconIndex(action.GetType(), action.Icon);
                    barItem.ItemClick += new ItemClickEventHandler(this.menu_ItemClick);
                    this.ctxMenu.LinksPersistInfo.Add(new LinkPersistInfo(barItem, action.BeginGroup));
                    this.barManager.Items.Add(barItem);
                }
            }
        }

        private void menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevExpress.XtraBars.BarItem menuItem = e.Item;
            if (menuItem.Tag != null && menuItem.Tag is Action)
            {
                Action action = menuItem.Tag as Action;
                try
                {
                    ActionContext context = GetActionContext(window.GetActionContext());
                    context.Selection = SelectedItem;
                    action.Excuete(context);
                    window.ValidateAction();
                }
                catch (Exception ex)
                {
                    SmartFrameWork.Services.MessageService.ShowException(ex,"Fail to execute action [" + action.Text + "]!");
                }
            }
        }

        private void ctxMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            foreach (DevExpress.XtraBars.BarItem item in this.barManager.Items)
            {
                if (item.Tag != null && item.Tag is Action)
                {
                    Action action = item.Tag as Action;
                    ActionContext context = GetActionContext(window.GetActionContext());
                    context.Selection = SelectedItem;

                    item.Enabled = action.IsEnable(context);
                    item.Caption = action.Text;
                    item.ImageIndex = GetIconIndex(action.GetType(), action.Icon);
                }
            }
        }

        #endregion
    }
}
