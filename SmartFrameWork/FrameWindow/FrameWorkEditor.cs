using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartFrameWork.Utils;

namespace SmartFrameWork
{
    public partial class FrameWorkEditor : DevExpress.XtraEditors.XtraForm
    {
        public FrameWorkEditor()
        {
            //默认构造函数里不能包含泛型的方法，这样窗体设计器无法使用
            InitializeComponent();
        }
        //所有可编辑的窗体的父类，参数名称用来设置保存的位置信息的属性,子类的构造函数会调用
        //父类的构造函数并把窗体名传递进来作为xml的属性，值为位置信息
        //在应用退出的时候会报错窗体的位置信息
        public FrameWorkEditor(string EditorName){
            //如果把这个函数放在默认的构造函数里，则子窗体会显示 SmartFrameWork.Services.PropertyService.Get[T](String property, T defaultValue) 
            //未将对象引用到对象的实例，问题出在this，该this指的是FrameWorkEditor的实例对象，所以设置的Bounds也是FrameWorkEditor
            //为什么父类只有location的参数起作用，Size不起作用
            //使用Winform的话是正常的，因为子窗体继承了父窗体的Loaction和Size属性,在子类的InitializeComponent被修改了
            //而且XtraForm使用的是ClientSize属性
            //FormLocationHelper.Apply(this, EditorName, true);
        }
        bool saveOnExit = true;
        public bool SaveOnExit
        {
            get { return saveOnExit; }
            set { saveOnExit = value; }
        }
        public virtual void Capture(string name)
        {

            Bitmap image = new Bitmap(this.Size.Width, this.Size.Height);
            this.DrawToBitmap(image, new Rectangle(0, 0, this.Size.Width, this.Size.Height));
            image.Save(name);
        }

        public virtual void Refresh()
        {

        }

        private bool isClosed;
        public virtual bool IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }

        private string extension = ".txt";
        public virtual string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        private string filter = "Text file|*.txt";
        public virtual string Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        string fileName;
        public virtual string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public virtual bool SaveSupport
        {
            get { return true; }
        }

        private bool dirty = false;
        public virtual bool Dirty
        {
            get { return dirty; }
            set { dirty = value; }
        }

        private bool autoSave = false;
        public bool AutoSave
        {
            get { return autoSave; }
            set
            {
                autoSave = value;
                timer.Enabled = value;
            }
        }
        //该窗体是否支持多个实例
        private bool isMultiInstances;
        public bool IsMultiInstances
        {
            get { return isMultiInstances; }
            set { isMultiInstances = value; }
        }
        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = Extension;
            dlg.Filter = Filter;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileName = dlg.FileName;
                Save(FileName);
            }
            Dirty = false;
        }

        public virtual void Save()
        {
            if (FileName != null)
            {
                Save(FileName);
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = Extension;
                dlg.Filter = Filter;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileName = dlg.FileName;
                    Save(FileName);
                }
            }
            Dirty = false;
        }

        public virtual void Save(string fileName) { }

        public virtual void Open(string fileName) { }

        public FrameWorkWindow Window
        {
            get
            {
                if (this.MdiParent is FrameWorkWindow)
                    return (FrameWorkWindow)this.MdiParent;
                else
                    return null;
            }
        }

        private void FrameWorkEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            isClosed = true;

            if (Window != null)
            {
                if (Window.MdiChildren.Length > 0)
                {
                    Window.MdiChildren[0].Activate();
                }
                Window.ValidateAction();
            }
        }

        private void FrameWorkEditor_Shown(object sender, EventArgs e)
        {
            if (Window != null)
            {
                Window.ValidateAction();
            }
        }

        private void FrameWorkEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Dirty && SaveOnExit)
            {
                if (MessageBox.Show("Do you want to save the file?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    Save();
                }
            }
            else
                if (MessageBox.Show("Do you want to eixt?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    //如果子类override的话，调用子类各自的save方法
                    Save();
                }
                else
                    e.Cancel = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.FileName != null)
            {
                Save(FileName);
            }
        }

        private void FrameWorkEditor_Activated(object sender, EventArgs e)
        {
        }

        private void FrameWorkEditor_MdiChildActivate(object sender, EventArgs e)
        {

        }
    }
}
