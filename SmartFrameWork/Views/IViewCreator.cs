using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public interface IViewCreator
    {
        string Name { get; }

        string Icon { get; }

        FrameWorkView CreateView(FrameWorkWindow window);
    }
}
