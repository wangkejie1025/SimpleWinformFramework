using System;
using System.Drawing;
using System.Windows.Forms;
using SmartFrameWork.Project;

namespace SmartFrameWork.Views
{
    public partial class ProjectView : FrameWorkView
    {
        public static readonly string NAME = "Project Manager";

        public static readonly string ICON = "project.png";

        public override string Icon
        {
            get
            {
                return ICON;
            }
        }

        public override ISelectable SelectedItem
        {
            get
            {
                if (treeView.SelectedNode != null && treeView.SelectedNode.Tag is SmartFrameWork.ISelectable)
                {

                    return (ISelectable)treeView.SelectedNode.Tag;
                }
                return null;
            }
        }

        public override ActionGroup ContextAction
        {
            get
            {
                return base.ContextAction;
            }
            set
            {
                base.ContextAction = value;
                BuildMenu(this.treeView);
            }
        }

        public ProjectView()
        {
            InitializeComponent();
            this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.Click += new EventHandler(this.treeView_Click);
            this.treeView.DragEnter += new DragEventHandler(this.treeView_DragEnter);
            this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
            this.treeView.MouseClick += new MouseEventHandler(this.treeView_MouseClick);
        }

        public override FrameWorkWindow Window
        {
            set
            {
                base.Window = value;
                this.Window.Workspace.ProjectCreated += new Workspace.ProjectCreate(ProjectManager_ProjectCreated);
                this.Window.Workspace.ProjectClosed += new Workspace.ProjectClose(ProjectManager_ProjectClosed);
            }
        }

        void ProjectManager_ProjectClosed(IProject project)
        {
            //对已经打开的工程进行管理
            //treeView.Nodes.Clear();           
            if (project is Project.ProjectInfo)
            {
                Project.ProjectInfo prj = project as Project.ProjectInfo;
                if (ProjectManager.OpenedProjects.Contains(prj.ProjectFileName))
                    ProjectManager.OpenedProjects.Remove(prj.ProjectFileName);
            }
            treeView.Nodes.Remove(treeView.SelectedNode);
        }

        void ProjectManager_ProjectCreated(IProject project)
        {
            TreeNode node = buildTree(project as Element);
            treeView.Nodes.Add(node);
            treeView.ExpandAll();
            //对已经打开的工程进行管理
            if (project is Project.ProjectInfo)
            {
                Project.ProjectInfo prj = project as Project.ProjectInfo;
                ProjectManager.AddOpenedProjects(prj.ProjectFileName);
                ProjectManager.AddRecentProjects(prj.ProjectFileName);
                prj.ItemAdd += new ElementContainer.ItemEvent(prj_ItemAdd);
                prj.ItemRemoved += new ElementContainer.ItemEvent(prj_ItemRemoved);
            }
        }

        void prj_ItemRemoved(Element parent, Element item)
        {
            TreeNode node = parent.Tag as TreeNode;
            if (node != null && item.Tag is TreeNode)
            {
                node.Nodes.Remove(item.Tag as TreeNode);
            }
        }


        public int GetIconIndex(Element item)
        {
            //这都是对嵌入的资源进行操作 assembly={SmartCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(item.GetType());
            //extension=9 ManifestModule.Name="SmartCore.dll"
            int extension = assembly.ManifestModule.Name.LastIndexOf('.');
            //key="SmartCore.icons.project.png"
            string key = assembly.ManifestModule.Name.Substring(0, extension) + ".icons." + item.Image;
            if (!imageList.Images.ContainsKey(key))
            {
                //序列化读取嵌入的资源的对象流
                System.IO.Stream stream = assembly.GetManifestResourceStream(key);
                if (stream != null)
                {
                    Image icon = Image.FromStream(stream);
                    imageList.Images.Add(key, icon);
                }
            }
            return imageList.Images.IndexOfKey(key);
        }

        void prj_ItemAdd(Element parent, Element item)
        {
            TreeNode node = parent.Tag as TreeNode;
            if (node != null)
            {
                TreeNode subnode = new TreeNode();
                subnode.Text = item.Text;
                subnode.Tag = item;
                subnode.ImageIndex = subnode.SelectedImageIndex = GetIconIndex(item);
                item.Tag = subnode;
                node.Nodes.Add(subnode);
            }
        }

        void treeView_DoubleClick(object sender, EventArgs e)
        {
            TriggerSelectionAction();
        }

        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectionManager.Selection = SelectedItem;
            //Selection change应发property change事件
            //根据选中的对象显示右键菜单的可用性，菜单只需要创建一次，但是每次选择需要Validata
        }

        void treeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = this.treeView.GetNodeAt(e.X, e.Y);
                if (tn != null) this.treeView.SelectedNode = tn;
            }
        }

        public TreeNode buildTree(Element element)
        {
           TreeNode node = new TreeNode
            {
                Text = element.Text,
                Tag = element
            };
            node.ImageIndex = node.SelectedImageIndex = GetIconIndex(element);
            element.Tag = node;
            //构建proInfo对象时，由于proInfo是ElementContainer对象，所以继续遍历其内容
            if (element is ElementContainer)
            {
                ElementContainer container = element as ElementContainer;
                foreach (Element subnode in container.Items)
                {
                    TreeNode subtreenode = this.buildTree(subnode);
                    node.Nodes.Add(subtreenode);
                }
            }
            return node;
        }

        private void treeView_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void treeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            if (e.Item is TreeNode)
            {
                TreeNode node = e.Item as TreeNode;
                IDragable data = node.Tag as IDragable;
                if (data != null)
                {
                    DoDragDrop(e.Item, DragDropEffects.All);
                }
            }
        }

        private void treeView_Click(object sender, EventArgs e)
        {
            SelectionManager.Selection = SelectedItem;
        }
    }

    public class ProjectViewCreator : IViewCreator
    {
        public string Name
        {
            get { return ProjectView.NAME; }
        }

        public string Icon
        {
            get { return ProjectView.ICON; }
        }

        public FrameWorkView CreateView(FrameWorkWindow window)
        {
            ProjectView view = new ProjectView();
            return view;
        }
    }
}
