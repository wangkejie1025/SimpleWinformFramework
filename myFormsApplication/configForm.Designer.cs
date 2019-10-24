namespace myFormsApplication
{
    partial class configForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMeaning = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmZeroPoint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmBindPort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBindPort = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBindPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.AllowDrop = true;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView1;
            this.gridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbBindPort});
            this.gridControl.Size = new System.Drawing.Size(678, 277);
            this.gridControl.TabIndex = 0;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmName,
            this.clmMeaning,
            this.clmZeroPoint,
            this.clmBindPort});
            this.gridView1.GridControl = this.gridControl;
            this.gridView1.Name = "gridView1";
            // 
            // clmName
            // 
            this.clmName.Caption = "Name";
            this.clmName.FieldName = "Name";
            this.clmName.Name = "clmName";
            this.clmName.Visible = true;
            this.clmName.VisibleIndex = 0;
            // 
            // clmMeaning
            // 
            this.clmMeaning.Caption = "Meaning";
            this.clmMeaning.FieldName = "Meaning";
            this.clmMeaning.Name = "clmMeaning";
            this.clmMeaning.Visible = true;
            this.clmMeaning.VisibleIndex = 1;
            // 
            // clmZeroPoint
            // 
            this.clmZeroPoint.Caption = "ZeroPoint";
            this.clmZeroPoint.FieldName = "ZeroPoint";
            this.clmZeroPoint.Name = "clmZeroPoint";
            this.clmZeroPoint.Visible = true;
            this.clmZeroPoint.VisibleIndex = 3;
            // 
            // clmBindPort
            // 
            this.clmBindPort.Caption = "BindPort";
            this.clmBindPort.ColumnEdit = this.cmbBindPort;
            this.clmBindPort.FieldName = "BindPort";
            this.clmBindPort.Name = "clmBindPort";
            this.clmBindPort.Visible = true;
            this.clmBindPort.VisibleIndex = 2;
            // 
            // cmbBindPort
            // 
            this.cmbBindPort.AutoHeight = false;
            this.cmbBindPort.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBindPort.Name = "cmbBindPort";
            // 
            // configForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 277);
            this.Controls.Add(this.gridControl);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "configForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.configForm_FormClosing);
            this.Load += new System.EventHandler(this.configForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBindPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn clmName;
        private DevExpress.XtraGrid.Columns.GridColumn clmMeaning;
        private DevExpress.XtraGrid.Columns.GridColumn clmZeroPoint;
        private DevExpress.XtraGrid.Columns.GridColumn clmBindPort;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbBindPort;
    }
}

