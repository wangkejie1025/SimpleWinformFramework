using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainApp.View
{
    public partial class ProjectView :SmartFrameWork.Views.ProjectView
    {
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public ProjectView()
        {
            InitializeComponent();
        }
    }
}
