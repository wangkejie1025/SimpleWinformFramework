using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using myFormsApplication;
using SmartFrameWork.Utils;
using DevExpress.XtraGrid.Views.Base;

namespace myFormsApplication
{
    public partial class configForm : SmartFrameWork.FrameWorkEditor
    {
        private listconfigFormFile file;
        public listconfigFormFile File
        {
            get { return file; }
            set { file = value; bindingSource.DataSource = file.ConfigFile; }
        }
        string fileName = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "uds.config");
        public configForm()
        {
            InitializeComponent();
            //需要将gridControl的DataSource与bindingSource绑定,这样embedded navigater才会起作用
            this.gridControl.DataSource = bindingSource;          
        }
        public configForm(string formName)
        {
            //为什么Size不起作用呢，因为先调用父类的构造函数后设置了Szie属性，但是之后又调用了
            //InitializeComponent重新设置了Size属性，所以不起作用
            InitializeComponent();
            this.gridView1.CellValueChanged+=new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
            FormLocationHelper.Apply(this, formName, true);
            //需要将gridControl的DataSource与bindingSource绑定,这样embedded navigater才会起作用
            this.gridControl.DataSource = bindingSource;
        }
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //this.Dirty = true;
        }
        //屏蔽父类的事件，不然事件是链式传递的
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
           // base.OnFormClosing(e);
        }
        private void configForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //可以使用父类的FrameWorkEdit中的SaveONExit和Dirty标识符判断是否要保存
            Save();
        }

        private void configForm_Load(object sender, EventArgs e)
        {
            Open();
            foreach (string portName in SmartFrameWork.Utils.ControlSetting.GetCOMPortName())
            {
                cmbBindPort.Items.Add(portName);
            }
        }
        private void Open()
        {
            if (System.IO.File.Exists(fileName))
            {
                SmartFrameWork.XMLParser<listconfigFormFile> parser = new SmartFrameWork.XMLParser<listconfigFormFile>();
                listconfigFormFile file = parser.Open(fileName);
                if(file==null)
                    New();
                else
                File = file;
            }
            else
            {
                New();
            }
        }
        //父类中已经定义了Save(),如果父类中使用了virtual或者abstract，则子类可以override，否则使用new关键字显示的隐藏父类的函数
        public override void  Save()
        {
            //反序列化
            SmartFrameWork.XMLParser<listconfigFormFile> parser = new SmartFrameWork.XMLParser<listconfigFormFile>();
            listconfigFormFile file = File;
            parser.Save(fileName, file);
            this.Dirty = false;
        }
        private void New()
        {
            //File 是list对象
            File = new listconfigFormFile();
        }
    }
}
