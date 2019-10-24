using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class SelectionManager
    {
        public delegate void SelectionChange(object selection);
        public static event SelectionChange SelectionChanged;
        private static object selection;
        //写Selection会触发SelectionChanged的事件
        public static object Selection
        {
            get { return selection; }
            set
            {
                selection = value;
                if (SelectionChanged != null)
                {
                    SelectionChanged(selection);
                }
            }
        }
    }
}
