using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SmartFrameWork
{
    public interface ISelectable
    {
    }

    public interface IDragable
    {
    }

    public interface ICloneable
    {
    }

    public class StructureSelectable : ISelectable
    {
        public StructureSelectable(object data)
        {
            this.Data = data;
        }

        object data;
        [System.ComponentModel.Browsable(false)]
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
