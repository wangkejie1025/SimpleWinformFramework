using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SmartFrameWork
{
    public partial class FullScreenWindow : DevExpress.XtraEditors.XtraForm
    {
        private FrameWorkWindow parentWindow;
        public FrameWorkWindow ParentWindow
        {
            get { return parentWindow; }
            set
            {
                parentWindow = value;
                if (parentWindow != null)
                {
                    parentWindow.SubWindows.Add(this);
                }
            }
        }

        private Action loadAction;
        public Action LoadAction
        {
            get { return loadAction; }
            set { loadAction = value; }
        }

        public FullScreenWindow()
        {
            InitializeComponent();
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

        public FrameWorkEditor GetActiveEditor()
        {
            FrameWorkEditor editor = this.ActiveMdiChild as FrameWorkEditor;
            if (editor != null && !editor.IsClosed)
            {
                return editor;
            }
            return null;
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
            foreach (System.Windows.Forms.Form form in this.MdiChildren)
            {
                if (form is SmartFrameWork.FrameWorkEditor)
                {
                    ((SmartFrameWork.FrameWorkEditor)form).Save();
                }
                form.Close();
            }
        }

        private void FullScreenWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FullScreenWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentWindow.SubWindows.Remove(this);
            foreach (Form form in this.MdiChildren)
            {
                form.MdiParent = ParentWindow;
                form.Show();
            }
        }
    }
}