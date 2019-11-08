namespace SmartFrameWork.Views
{
    partial class ConsoleView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.barClear = new System.Windows.Forms.ToolStripMenuItem();
            this.barSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.ContextMenuStrip = this.contextMenuStrip;
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Location = new System.Drawing.Point(0, 20);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(150, 130);
            this.textBox.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barClear,
            this.barSaveAs});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(211, 80);
            this.contextMenuStrip.Text = "contextMenuStrip1";
            // 
            // barClear
            // 
            this.barClear.Name = "barClear";
            this.barClear.Size = new System.Drawing.Size(210, 24);
            this.barClear.Text = "&Clear";
            this.barClear.Click += new System.EventHandler(this.barClear_Click);
            // 
            // barSaveAs
            // 
            this.barSaveAs.Name = "barSaveAs";
            this.barSaveAs.Size = new System.Drawing.Size(210, 24);
            this.barSaveAs.Text = "&Save As";
            this.barSaveAs.Click += new System.EventHandler(this.barSaveAs_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ConsoleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ConsoleView";
            this.Controls.SetChildIndex(this.textBox, 0);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        public System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem barClear;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem barSaveAs;
    }
}
