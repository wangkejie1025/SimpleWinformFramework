using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SmartFrameWork.Utils;
using SmartFrameWork.Services;
using SmartFrameWork.Project;


namespace SmartFrameWork
{
    //父窗体，所有FrameWorkEditor，保存上下文信息
    public partial class FrameWorkWindow : DevExpress.XtraEditors.XtraForm
    {    
        private Workspace workspace = new Workspace();
        public Workspace Workspace
        {
            get { return workspace; }
            set { workspace = value; }
        }

        public WindowSytle WindowStyle
        {
            set
            {
                if (value == WindowSytle.MdiLayout)
                {
                    this.xtraTabbedMdiManager.MdiParent = null;
                }
                else
                {
                    this.xtraTabbedMdiManager.MdiParent = this;

                }
            }
        }

        private Dictionary<string, FrameWorkView> views = new Dictionary<string, FrameWorkView>();
        public Dictionary<string, FrameWorkView> Views
        {
            get { return views; }
            set { views = value; }
        }

        private Dictionary<string, IViewCreator> viewCreators = new Dictionary<string, IViewCreator>();
        public Dictionary<string, IViewCreator> ViewCreators
        {
            get { return viewCreators; }
            set { viewCreators = value; }
        }

        private List<FullScreenWindow> subWindows = new List<FullScreenWindow>();
        public List<FullScreenWindow> SubWindows
        {
            get { return subWindows; }
            set { subWindows = value; }
        }

        private Action loadAction;
        public Action LoadAction
        {
            get { return loadAction; }
            set { loadAction = value; }
        }
        private Action exitAction;
        public Action ExitAction
        {
            get { return exitAction; }
            set { exitAction = value; }
        }
        public FrameWorkWindow()
        {
            InitializeComponent();
            SelectionManager.SelectionChanged += SelectionManager_SelectionChanged;
            StatusManager.Status.StatusChange += Status_StatusChange;
        }

        void Status_StatusChange(int index, string text)
        {
            if (index >= 0 && index < Statusbar.ItemLinks.Count)
            {
                Statusbar.ItemLinks[index].Caption = text;
            }
        }

        void SelectionManager_SelectionChanged(object selection)
        {
            if (selection is ISelectable)
            {
                ValidateAction();
            }
        }

        public void ValidateAction(ActionContext context)
        {
            foreach (DevExpress.XtraBars.Bar bar in barManager.Bars)
            {
                foreach (DevExpress.XtraBars.BarItemLink item in bar.ItemLinks)
                {
                    if (item.Item.Tag != null && item.Item.Tag is Action)
                    {
                        Action action = ((Action)item.Item.Tag);
                        item.Item.Enabled = action.IsEnable(context);
                    }
                }
            }
        }

        public virtual ActionContext GetActionContext()
        {
            ActionContext context = new ActionContext
            {
                Window = this,
                //实现了ISelectable接口的对象说明是可以被选中的，获取当前被选中的对象，并且会触发selectChange事件
                //is检查一个对象是否兼容于指定的类型，并返回一个Boolean值
                //as会进行类型转换，如果转换失败则会返回null
                Selection = SelectionManager.Selection as ISelectable
            };
            //返回上限文信息
            return context;
        }

        public void ValidateAction()
        {
            ActionContext context = GetActionContext();
            ValidateAction(context);
        }

        //对于是view类型的
        void viewbarItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag is IViewCreator)
            {
                try
                {
                    IViewCreator creator = ((IViewCreator)e.Item.Tag);
                    FrameWorkView view = GetView(creator.Name);
                    if (view != null)
                    {
                        if (view.Panel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
                        {
                            DevExpress.XtraBars.BarButtonItem viewbarItem = (DevExpress.XtraBars.BarButtonItem)e.Item;
                            view.Panel.Show();
                        }
                        else
                        {
                            DevExpress.XtraBars.BarButtonItem viewbarItem = (DevExpress.XtraBars.BarButtonItem)e.Item;
                            view.Panel.Hide();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LoggingService.Error(ex);
                }
            }
        }
        //对于普通的按钮类型的
        void barItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DevExpress.XtraBars.BarItem barItem = e.Item;
                if (barItem.Tag != null && barItem.Tag is Action)
                {
                    Action action = (Action)barItem.Tag;
                    ActionContext context = GetActionContext();
                    action.Excuete(context);
                    this.ValidateAction();
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex);
            }
        }
        //对于实现了IViewCreator接口的，Name Icon和Create方法，实现IViewCreator接口中的create方法实例化具体的view对象
        public void AddViewCreator(IViewCreator creator)
        {
            //加入到字典中
            viewCreators.Add(creator.Name, creator);
        }

        public void AddMenuGroup(DevExpress.XtraBars.BarSubItem menu, ActionGroup group)
        {
            foreach (Action action in group.Items)
            {
                if (action is SmartFrameWork.ActionGroup)
                {
                    DevExpress.XtraBars.BarSubItem viewbarItem = new DevExpress.XtraBars.BarSubItem
                    {
                        Caption = StringUtils.GetString(action.GetType(), action.Text),
                        ImageIndex = GetIconIndex(action.GetType(), action.Icon),
                        Tag = action
                    };
                    menu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(viewbarItem,action.BeginGroup)});
                    this.barManager.Items.Add(viewbarItem);
                    viewbarItem.Popup += new EventHandler(Menu_Popup);

                    AddMenuGroup(viewbarItem, action as ActionGroup);
                }
                //列出所有MDI子窗体，在window菜单栏中加入的
                else if (action is SmartFrameWork.MdiListAction)
                {
                    //新建所有Mdi子窗体的baritem对象
                    DevExpress.XtraBars.BarMdiChildrenListItem baritem = new DevExpress.XtraBars.BarMdiChildrenListItem
                    {
                        //对于Action的text和image没有要求，获得的是子窗体的icon和text
                        Caption = StringUtils.GetString(action.GetType(), action.Text),
                        ImageIndex = GetIconIndex(action.GetType(), action.Icon),
                        Name = action.Text
                    };
                    //this properity is useless when the child has icon
                    //baritem.ShowChecks = true;
                    menu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(baritem,true)});
                    baritem.Tag = action;
                    this.barManager.Items.Add(baritem);
                }
                //如果是ViewListAction的类型，将所有的view对象加入到菜单的view中，控制view的显示和隐藏
                else if (action is SmartFrameWork.ViewListAction)
                {
                    bool begingroup = false;
                    foreach (IViewCreator creator in viewCreators.Values)
                    {
                        DevExpress.XtraBars.BarButtonItem viewbarItem = new DevExpress.XtraBars.BarButtonItem
                        {
                            Caption = creator.Name,
                            Name = creator.Name,
                            Enabled = true,
                            ImageIndex = GetIconIndex(creator.GetType(), creator.Icon)
                        };
                        menu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(viewbarItem,begingroup)});
                        viewbarItem.Tag = creator;
                        //绑定了显示隐藏事件
                        viewbarItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(viewbarItem_ItemClick);
                        this.barManager.Items.Add(viewbarItem);
                    }
                }
                else if (action is RecentProjectsAction)
                {
                    BarSubItem viewbarItem = new BarSubItem
                    {
                        Caption = StringUtils.GetString(action.GetType(), action.Text)
                    };
                    menu.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
                    new LinkPersistInfo(viewbarItem,action.BeginGroup)});
                    viewbarItem.Tag = action;
                    viewbarItem.Popup += new EventHandler(Menu_Popup);
                    this.barManager.Items.Add(viewbarItem);
                    RecentProjectsAction recentAction = action as RecentProjectsAction;
                    for (int i = 0; i < 10; i++)
                    {
                        BarButtonItem baritemRecentProject = new BarButtonItem
                        {
                            Caption = action.Text,
                            Tag = recentAction.OpenAction
                        };
                        viewbarItem.LinksPersistInfo.Add(new LinkPersistInfo(baritemRecentProject));
                        baritemRecentProject.ItemClick += new ItemClickEventHandler(baritemRecentProject_ItemClick);
                        this.barManager.Items.Add(baritemRecentProject);
                    }
                }
                else if (action is RecentFilesAction)
                {
                    BarSubItem viewbarItem = new BarSubItem
                    {
                        Caption = StringUtils.GetString(action.GetType(), action.Text)
                    };
                    menu.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
                    new LinkPersistInfo(viewbarItem,action.BeginGroup)});
                    viewbarItem.Tag = action;
                    viewbarItem.ItemClick += new ItemClickEventHandler(viewbarItem_ItemClick);
                    this.barManager.Items.Add(viewbarItem);
                }
                else
                {
                    BarButtonItem baritem = new BarButtonItem
                    {
                        Caption = StringUtils.GetString(action.GetType(), action.Text),
                        ImageIndex = GetIconIndex(action.GetType(), action.Icon),
                        Name = action.Text,
                        Enabled = true,
                        ItemShortcut = new BarShortcut(action.ShortCut)
                    };
                    menu.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
                    new LinkPersistInfo(baritem,action.BeginGroup)});
                    baritem.Tag = action;
                    baritem.ItemClick += new ItemClickEventHandler(barItem_ItemClick);
                    this.barManager.Items.Add(baritem);
                }
            }
        }
        //显示具体的菜单栏时的事件
        void Menu_Popup(object sender, EventArgs e)
        {
            ActionContext context = GetActionContext();
            BarSubItem menu = sender as BarSubItem;
            if (menu.Tag is RecentProjectsAction)
            {
                int i = 0;
                for (i = 0; i < ProjectManager.RecentProjects.Count && i < menu.ItemLinks.Count; i++)
                {
                    menu.ItemLinks[i].Visible = true;
                    menu.ItemLinks[i].Caption = ProjectManager.RecentProjects[i];
                }
                for (; i < menu.ItemLinks.Count; i++)
                {
                    menu.ItemLinks[i].Visible = false;
                }
            }
            else
            {
                foreach (BarItemLink link in menu.ItemLinks)
                {
                   BarItem item = link.Item;
                    if (item.Tag != null && item.Tag is Action)
                    {
                        Action action = item.Tag as Action;

                        item.Caption = StringUtils.GetString(action.GetType(), action.Text);
                        item.Enabled = action.IsEnable(context);
                        item.ImageIndex = GetIconIndex(action.GetType(), action.Icon);
                    }
                }
            }
        }

        void baritemRecentProject_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DevExpress.XtraBars.BarItem barItem = e.Item;
                if (barItem.Tag != null && barItem.Tag is Action)
                {
                    Action action = (Action)barItem.Tag;
                    ActionContext context = GetActionContext();
                    context.Source = barItem;
                    action.Excuete(context);
                    this.ValidateAction();
                }
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex);
            }
        }

        public void AddMenuGroup(ActionGroup group)
        {
            try
            {
                DevExpress.XtraBars.BarSubItem menu = new DevExpress.XtraBars.BarSubItem();
                this.MainMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(menu)});

                menu.Caption = group.Text;
                //menu.Id = 5;
                menu.Name = group.Text;

                menu.Popup += new EventHandler(Menu_Popup);
                this.barManager.Items.Add(menu);

                AddMenuGroup(menu, group);
            }
            catch (Exception ex)
            {
                SmartFrameWork.Services.LoggingService.Error(ex);
            }
        }

        public int GetIconIndex(Type type, string icon)
        {
            //图标的获得跟程序集相关，在程序集各自的icon文件夹中，比如UDSBootloader.ICONS.文件
            //每一个项目都有程序集信息
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(type);
            int extension = assembly.ManifestModule.Name.LastIndexOf('.');
            string key = String.Format("{0}.icons.{1}", assembly.ManifestModule.Name.Substring(0, extension), icon);
            System.IO.Stream stream = assembly.GetManifestResourceStream(key);
            if (stream != null)
            {
                Image iconImage = Image.FromStream(stream);
                imageList.Images.Add(key, iconImage);
                return imageList.Images.IndexOfKey(key);
            }
            return -1;
        }
        //工具栏
        public void AddToolBarGroup(ActionGroup actionGroup)
        {
            DevExpress.XtraBars.Bar bar = new DevExpress.XtraBars.Bar
            {
                BarName = actionGroup.Text,
                DockCol = actionGroup.Index,
                DockRow = 1,
                DockStyle = DevExpress.XtraBars.BarDockStyle.Top
            };

            //bar.Offset = 66;
            bar.OptionsBar.AllowRename = true;
            bar.Text = actionGroup.Text;

            foreach (Action action in actionGroup.Items)
            {
                if (action is SmartFrameWork.ActionGroup)
                {
                    DevExpress.XtraBars.BarSubItem viewbarItem = new DevExpress.XtraBars.BarSubItem
                    {
                        ImageIndex = GetIconIndex(action.GetType(), action.Icon)
                    };

                    AddMenuGroup(viewbarItem, action as ActionGroup);

                    viewbarItem.Popup += new EventHandler(Menu_Popup);

                    bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, viewbarItem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
                    this.barManager.Items.Add(viewbarItem);


                }
                else
                {
                    DevExpress.XtraBars.BarButtonItem barItem = new DevExpress.XtraBars.BarButtonItem
                    {
                        ImageIndex = GetIconIndex(action.GetType(), action.Icon),
                        Caption = action.Text,
                        Id = 1
                    };
                    if (action.Hint != null)
                    {
                        barItem.Hint = action.Hint;
                    }
                    else
                    {
                        barItem.Hint = action.Text;
                    }
                    barItem.Name = action.Text;
                    bar.LinksPersistInfo.Add(
                        new DevExpress.XtraBars.LinkPersistInfo(barItem));
                    barItem.Tag = action;
                    barItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barItem_ItemClick);
                }
            }
            barManager.Bars.Add(bar);
        }

        public void AddView(FrameWorkView view)
        {
            AddView(view, DevExpress.XtraBars.Docking.DockingStyle.Left);
        }

        public void AddView(FrameWorkView view, DevExpress.XtraBars.Docking.DockingStyle dockStyle)
        {
            //view对象要加入到devDockPanel容器中才能实现正常的功能
            DevExpress.XtraBars.Docking.DockPanel lastPanel = null;
            //遍历所有的view对象
            foreach (DevExpress.XtraBars.Docking.DockPanel panel in dockManager.RootPanels)
            {
                //当前的dock模式下是否已经存在panel
                if (panel.Dock == dockStyle)
                {
                    lastPanel = panel;
                    break;
                }
            }
            if (lastPanel == null) // 该位置没有View
            {
                DevExpress.XtraBars.Docking.DockPanel dockPanel = CreatePanel(view);

                dockPanel.Dock = dockStyle;
                views.Add(view.Name, view);
                dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] { dockPanel });
            }
            else// 当前的DockStyle已经存在View
            {
                DevExpress.XtraBars.Docking.DockPanel containerPanel;

                if (lastPanel.Tabbed)  // 该位置已经有2个或以上的View
                {
                    //当前位置的panel对象
                    containerPanel = lastPanel;
                }
                else//如果当前位置只有一个panel对象，要为panel设置tabbe的属性，这样后续可以直接加入
                {
                    containerPanel = new DevExpress.XtraBars.Docking.DockPanel();
                    containerPanel.Dock = dockStyle;
                    containerPanel.Tabbed = true;
                    containerPanel.ID = System.Guid.NewGuid();
                    containerPanel.FloatVertical = true;
                    this.Controls.Add(containerPanel);
                    containerPanel.Text = "Left";
                    dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] { containerPanel });
                    //获取上一个panel对象的view对象，因为view对象是包含在panel的control的ControlContainer中的
                    FrameWorkView lastview = lastPanel.Controls[0].Controls[0] as FrameWorkView;
                    //创建一个包含上一个view
                    DevExpress.XtraBars.Docking.DockPanel newlast = CreatePanel(lastview);
                    lastview.ParentPanel = containerPanel;

                    dockManager.RootPanels.RemoveAt(dockManager.RootPanels.IndexOf(lastPanel));

                    containerPanel.Controls.Add(newlast);

                }
                //如果包含view的panel已经是tabbed，说明已经存在多个view
                containerPanel.SuspendLayout();

                DevExpress.XtraBars.Docking.DockPanel dockPanel = CreatePanel(view);

                view.ParentPanel = containerPanel;
                //直接把当前view的panel加入到containerPanel就可以了
                containerPanel.Controls.Add(dockPanel);

                containerPanel.ActiveChild = dockPanel;

                views.Add(view.Name, view);

                containerPanel.ResumeLayout(false);
            }
            view.Window = this;
        }
        //view要加入到ControlContainer中，ControlContainer在加入到DockPanel的controls集合中
        protected DevExpress.XtraBars.Docking.DockPanel CreatePanel(FrameWorkView view)
        {
            try
            {
                DevExpress.XtraBars.Docking.DockPanel dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
                dockPanel.SuspendLayout();

                dockPanel.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(dockPanel_ClosingPanel);

                DevExpress.XtraBars.Docking.ControlContainer dockPanel_Container = new DevExpress.XtraBars.Docking.ControlContainer();

                dockPanel.Controls.Add(dockPanel_Container);

                dockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;

                dockPanel.ID = System.Guid.NewGuid();
                dockPanel.Name = view.Name;
                dockPanel.Text = view.Name;


                dockPanel_Container.Name = "dockPanel1_Container";
                dockPanel_Container.Dock = DockStyle.Fill;
                dockPanel_Container.TabIndex = 0;

                view.Dock = DockStyle.Fill;
                view.Panel = dockPanel;

                dockPanel_Container.Controls.Add(view);

                dockPanel.ImageIndex = GetIconIndex(view.GetType(), view.Icon);

                dockPanel.ResumeLayout(false);

                return dockPanel;
            }
            catch (Exception ex)
            {
                LoggingService.Error(ex);
            }
            return null;
        }

        void dockPanel_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            e.Cancel = true;
            e.Panel.Hide();
        }

        public FrameWorkEditor IsOpened(string name)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is FrameWorkEditor)
                {
                    if ((form as FrameWorkEditor).FileName == name)
                    {
                        return (form as FrameWorkEditor);
                    }
                }
            }
            return null;
        }

        public FrameWorkView GetView(String name)
        {
            if (views.ContainsKey(name))
            {
                return views[name];
            }
            return null;
        }

        public FrameWorkEditor GetActiveEditor()
        {
            if (this.ActiveMdiChild is FrameWorkEditor editor && !editor.IsClosed)
            {
                return editor;
            }
            return null;
        }

        public List<FrameWorkEditor> GetEditors()
        {
            List<FrameWorkEditor> editorCollection = new List<FrameWorkEditor>();
            foreach (Form form in this.MdiChildren)
            {
                if (form is FrameWorkEditor)
                {
                    editorCollection.Add(form as FrameWorkEditor);
                }
            }
            return editorCollection;
        }

        public List<FrameWorkEditor> GetEditors(Type type)
        {
            List<FrameWorkEditor> editorCollection = new List<FrameWorkEditor>();
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == type && form is FrameWorkEditor)
                {
                    editorCollection.Add(form as FrameWorkEditor);
                }
            }
            return editorCollection;
        }

        public FrameWorkEditor GetEditor(Type type)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == type && form is FrameWorkEditor)
                {
                    return (FrameWorkEditor)form;
                }
            }
            return null;
        }

        public FrameWorkEditor GetEditor(string name)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType().Name == name && form is FrameWorkEditor)
                {
                    return (FrameWorkEditor)form;
                }
            }
            return null;
        }

        public FrameWorkEditor FindEditorByTag(object tag)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is FrameWorkEditor && form.Tag == tag)
                {
                    return form as FrameWorkEditor;
                }
            }
            return null;
        }

        public void CloseAll()
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is FrameWorkEditor)
                {
                    if (((FrameWorkEditor)form).SaveSupport)
                    {
                        ((FrameWorkEditor)form).Save();
                        ((FrameWorkEditor)form).Dirty = false;
                    }
                }
                form.Close();
            }
            foreach (FrameWorkView view in views.Values)
            {
                view.Close();
            }
        }
        private void FrameWorkWindow_Load(object sender, EventArgs e)
        {
            this.ValidateAction();
            if (LoadAction != null)
            {
                ActionContext context = GetActionContext();
                LoadAction.Excuete(context);
            }
        }
        private void FrameWorkWindow_Shown(object sender, EventArgs e)
        {
            this.ValidateAction();
        }
        private void FrameWorkWindow_Activated(object sender, EventArgs e)
        {
            this.ValidateAction();
        }

        private void FrameWorkWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageService.ShowQuestion("Do you want to exit?");
            if (result == DialogResult.Cancel)
            { 
                e.Cancel = true;
                return;
            }
            Services.PropertyService.Set(nameof(ProjectManager.OpenedProjects), ProjectManager.OpenedProjects);
            Services.PropertyService.Set(nameof(ProjectManager.RecentProjects), ProjectManager.RecentProjects);
        }
        private void FrameWorkWindow_MdiChildActivate(object sender, EventArgs e)
        {
            ValidateAction();
            foreach (FrameWorkView view in Views.Values)
            {
                //目前是空函数
                view.ActiveEditorChanged();
            }
        }

        private void FrameWorkWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.ValidateAction();
            //if (ExitAction != null)
            //{
            //    ActionContext context = GetActionContext();
            //    ExitAction.Excuete(context);
            //}
        }
    }

    public enum WindowSytle
    {
        Tabbed, MdiLayout
    }
}