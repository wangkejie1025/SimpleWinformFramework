namespace SmartFrameWork.Views
{
    partial class NaviBarView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelFilter = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelFilter);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 20);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(207, 42);
            this.panelControl1.TabIndex = 4;
            // 
            // navBarControl
            // 
            this.navBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl.Location = new System.Drawing.Point(0, 62);
            this.navBarControl.Name = "navBarControl";
            this.navBarControl.Size = new System.Drawing.Size(207, 378);
            this.navBarControl.TabIndex = 5;
            this.navBarControl.Text = "navBarControl";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(46, 13);
            this.textEdit1.MenuManager = this.barManager;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(156, 24);
            this.textEdit1.TabIndex = 0;
            // 
            // labelFilter
            // 
            this.labelFilter.Location = new System.Drawing.Point(5, 15);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(35, 18);
            this.labelFilter.TabIndex = 1;
            this.labelFilter.Text = "Filter:";
            // 
            // NaviBarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl);
            this.Controls.Add(this.panelControl1);
            this.Name = "NaviBarView";
            this.Size = new System.Drawing.Size(207, 440);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.navBarControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl;
        private DevExpress.XtraEditors.LabelControl labelFilter;
        private DevExpress.XtraEditors.TextEdit textEdit1;
    }
}
