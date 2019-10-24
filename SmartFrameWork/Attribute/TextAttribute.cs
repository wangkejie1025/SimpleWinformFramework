using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{

    public class GroupAttribute : System.ComponentModel.CategoryAttribute
    {
        public GroupAttribute()
        {

        }

        public GroupAttribute(string category)
            : base(category)
        { }
    }

    public class TextAttribute : System.ComponentModel.DisplayNameAttribute
    {
        public TextAttribute()
        {

        }

        public TextAttribute(string displayName)
            : base(displayName)
        { }

        public override string DisplayName
        {
            get
            {
                return base.DisplayName;
            }
        }
    }
}
