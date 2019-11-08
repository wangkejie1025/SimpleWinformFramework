using System.Windows.Forms;

namespace SmartFrameWork.Project
{
    public partial class NewProjectDialog : Form
    {
        private ProjectDescriptorManager manager;
        public ProjectDescriptorManager Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        private string projectPath;
        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }
        private string desription;
        public string Desription
        {
            get { return desription; }
            set { desription = value; }
        }
        private IProjectDescriptor descriptor;
        public IProjectDescriptor Descriptor
        {
            get { return descriptor; }
            set { descriptor = value; }
        }
        public NewProjectDialog()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = fdb.SelectedPath + @"\";
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (!this.txtPath.Text.Equals("") && !this.txtName.Text.Equals(""))
            {
                this.projectName = this.txtName.Text;
                this.projectPath = this.txtPath.Text;
                this.desription = this.txtComment.Text.Equals("") ? "default" : this.txtComment.Text;
                //在action的构造函数中传进manager
                this.descriptor = manager.ProjectDescriptors[0];
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            else Services.MessageService.ShowError("Invalid input, Please try again!");
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
