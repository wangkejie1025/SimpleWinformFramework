using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartFrameWork.Services;

namespace SmartFrameWork.Views
{
    public  delegate void UpdateTextBoxDelegate(string str);
    delegate void WriteLineCallback(String str);

    public partial class ConsoleView : SmartFrameWork.FrameWorkView
    {
        public static readonly  string NAME = "Console";

        public static readonly string ICON = "console.png";

        public override string Icon
        {
            get
            {
                return ICON;
            }
        }
        public ConsoleView()
        {
            InitializeComponent();
            ToolBarVisible = false;
        }
        private void barClear_Click(object sender, EventArgs e)
        {
            this.textBox.Text = "";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.textBox.AppendText(Console.GetString());
        }

        private void barSaveAs_Click(object sender, EventArgs e)
        {
            string fileName = MessageService.SaveFileDialog("Text file", "txt");
            if (fileName != null)
            {
                System.IO.File.WriteAllText(fileName, this.textBox.Text);
            }
        }
    }

    public class ConsoleViewCreator : SmartFrameWork.IViewCreator
    {
        public string Name
        {
            get { return ConsoleView.NAME; }
        }

        public string Icon
        {
            get { return ConsoleView.ICON; }
        }

        public SmartFrameWork.FrameWorkView CreateView(SmartFrameWork.FrameWorkWindow window)
        {
            ConsoleView view = new ConsoleView();
            return view;
        }
    }

}
