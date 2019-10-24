using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork.Utils
{
    public class StatusManager
    {
        private static Status status = new Status();
        public static Status Status
        {
            get { return StatusManager.status; }
            set { StatusManager.status = value; }
        }
    }

    public class Status
    {
        public delegate void StatusChanged(int index, string text);
        public event StatusChanged StatusChange;

        public void SetStatus(int index,string text)
        {
            if (StatusChange != null)
            {
                StatusChange(index,text);
            }
        }
    }

    public class Observable
    {
        public delegate void PropertyChanged(Observable element, string name);
        public event PropertyChanged PropertyChange;
        protected void NofityPropertyChanged(string name)
        {
            if (this.PropertyChange != null)
            {
                PropertyChange(this, name);
            }
        }

        public delegate void ChildAdded(Observable element, string name, object item);
        public event ChildAdded ChildAdd;
        protected void NofityChildAdd(string name, object item)
        {
            if (this.ChildAdd != null)
            {
                ChildAdd(this, name, item);
            }
        }

        public delegate void ChildRemoved(Observable element, string name, object item);
        public event ChildRemoved ChildRemove;
        protected void NofityChildRemove(string name, object item)
        {
            if (this.ChildRemove != null)
            {
                ChildRemove(this, name, item);
            }
        }



        private object tag;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public virtual void Remove()
        {
            
        }
    }
}
