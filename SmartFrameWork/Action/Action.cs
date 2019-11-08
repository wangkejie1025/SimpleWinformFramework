using System;
using System.Collections;
using System.Collections.Generic;
using SmartFrameWork.Project;
using SmartFrameWork.Utils;

namespace SmartFrameWork
{
    public class ActionContext
    {
        private FrameWorkWindow window;
        public FrameWorkWindow Window
        {
            get { return window; }
            set { window = value; }
        }
        public bool isportopen;
        private ISelectable selection;
        public ISelectable Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        private IProject project;
        public IProject Project
        {
            get { return project; }
            set { project = value; }
        }

        private object source;
        public object Source
        {
            get { return source; }
            set { source = value; }
        }

        public virtual void Clone(ActionContext context)
        {
            this.Window = context.Window;
            this.Selection = context.Selection;
            this.Project = context.Project;
            this.Source = context.Source;
        }
    }

    public class TreeActionContext : ActionContext
    {
        private List<object> path = new List<object>();
        public List<object> Path
        {
            get { return path; }
            set { path = value; }
        }
    }

    public class Action
    {
        public Action() { }

        public Action(string text, string icon = null)
        {
            this.text = text;
            this.icon = icon;
        }

        public Action(string text, bool beginGroup)
        {
            this.text = text;
            this.beginGroup = beginGroup;
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public virtual bool VisiblityOnDisable
        {
            get { return false; }
        }

        private string icon;
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        private bool beginGroup = false;
        public bool BeginGroup
        {
            get { return beginGroup; }
            set { beginGroup = value; }
        }

        private System.Windows.Forms.Keys shortCut;
        public System.Windows.Forms.Keys ShortCut
        {
            get { return shortCut; }
            set { shortCut = value; }
        }


        private string hint;
        public string Hint
        {
            get { return hint; }
            set { hint = value; }
        }

        private List<object> actions;
        public List<object> Actions
        {
            get
            {
                if (actions == null)
                    actions = new List<object>();
                return actions;
            }
        }

        private string exceptionMessage;
        public string ExceptionMessage
        {
            get { return exceptionMessage; }
            set { exceptionMessage = value; }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

       readonly MicroStopwatch watch = new MicroStopwatch();


        public void Excuete(ActionContext context)
        {
            watch.Reset();
            watch.Start();
            Perform(context);
            watch.Stop();
            //SmartFrameWork.Console.WriteLine(string.Format("Action {0} takes {1} Microseconds", this.Text, watch.ElapsedMicroseconds));
        }

        public virtual void Perform(ActionContext context) { }

        public virtual bool IsEnable(ActionContext context) { return true; }
    }

    public class CheckAction : Action
    {
        public CheckAction() { }

        public CheckAction(string text, string icon = null)
            : base(text, icon)
        {
        }

        public CheckAction(string text, bool beginGroup)
            : base(text, beginGroup)
        {
        }

        public virtual bool IsChecked(ActionContext context) { return false; }
    }

    public class MdiListAction : Action
    {

    }

    public class RecentFilesAction : Action
    {
        private Action openAction;
        public Action OpenAction
        {
            get { return openAction; }
            set { openAction = value; }
        }

        public RecentFilesAction()
        {
            this.Text = "Recent Files";
        }
    }

    public class RecentProjectsAction : Action
    {
        private Action openAction;
        public Action OpenAction
        {
            get { return openAction; }
            set { openAction = value; }
        }

        public RecentProjectsAction()
        {
            this.Text = "Recent Projects";
        }
    }

    public class ViewListAction : Action
    {

    }

    public class ActionGroup : Action
    {
        private int index = 1;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public ActionGroup()
        {
        }

        public ActionGroup(string text, string icon = null)
        {
            this.Text = text;
            this.Icon = icon;
        }

       readonly private List<Action> items = new List<Action>();
        public List<Action> Items
        {
            get { return items; }
        }

        public Action Add(Action action)
        {
            items.Add(action);
            return action;
        }
    }

    public class ProcessAction : Action
    {
        private string command;
        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        public ProcessAction() { }

        public ProcessAction(string text, string command, string icon = null)
            : base(text, icon)
        {
            this.command = command;
        }

        public ProcessAction(string text, string command, bool beginGroup)
            : base(text, beginGroup)
        {
            this.command = command;
        }

        public override bool IsEnable(ActionContext context)
        {
            string fullPath = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, command);
            return System.IO.File.Exists(fullPath);
        }

        public override void Perform(ActionContext context)
        {
            string fullPath = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, command);
            System.Diagnostics.Process.Start(fullPath);
        }
    }
}

