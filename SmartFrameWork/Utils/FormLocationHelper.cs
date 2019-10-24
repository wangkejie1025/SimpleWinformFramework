// Copyright (c) 2005 Daniel Grunwald
// Licensed under the terms of the "BSD License", see doc/license.txt

using System;
using System.Drawing;
using System.Windows.Forms;
using SmartFrameWork.Services;

namespace SmartFrameWork.Utils
{
	/// <summary>
	/// Static helper class that loads and stores the position and size of a Form in the
	/// PropertyService.
	/// </summary>
	public static class FormLocationHelper
	{
        /// <summary>
        /// save laction parameters into xml file
        /// </summary>
        /// <param name="form">winform</param>
        /// <param name="propertyName">propertyName saved in xml file</param>
        /// <param name="isResizable">winform isResizable</param>
		public static void Apply(Form form, string propertyName, bool isResizable)
		{
            //之前在PropertyService.Load();方法中已经导入所有属性值，并存储为字典的方式
            //暂时只保存为位置信息
			form.StartPosition = FormStartPosition.Manual;
			if (isResizable) {
                //Get方法要根据具体的类型从object->T
				form.Bounds = Validate(PropertyService.Get(propertyName, GetDefaultBounds(form)));
			} else {
				form.Location = Validate(PropertyService.Get(propertyName, GetDefaultLocation(form)), form.Size);
			}
            //同时绑定了窗体关闭时的自动保存属性的事件,如果要
			form.Closing += delegate {
				if (isResizable) {
                    //这里的key是窗体名称，value是form.bounds对象，保存的时候调用相应的toString()方法保存成字符串的形式
                    //set写的时候会触发属性更新的事件，更新value值
					PropertyService.Set(propertyName, form.Bounds);
				} else {
					PropertyService.Set(propertyName, form.Location);
				}
			};
		}
		
		static Rectangle Validate(Rectangle bounds)
		{
			// Check if form is outside the screen and get it back if necessary.
			// This is important when the user uses multiple screens, a window stores its location
			// on the secondary monitor and then the secondary monitor is removed.
			Rectangle screen1 = Screen.FromPoint(new Point(bounds.X, bounds.Y)).WorkingArea;
			Rectangle screen2 = Screen.FromPoint(new Point(bounds.X + bounds.Width, bounds.Y)).WorkingArea;
			if (bounds.Y < screen1.Y - 5 && bounds.Y < screen2.Y - 5)
				bounds.Y = screen1.Y - 5;
			if (bounds.X < screen1.X - bounds.Width / 2)
				bounds.X = screen1.X - bounds.Width / 2;
			else if (bounds.X > screen2.Right - bounds.Width / 2)
				bounds.X = screen2.Right - bounds.Width / 2;
			return bounds;
		}
		
		static Point Validate(Point location, Size size)
		{
			return Validate(new Rectangle(location, size)).Location;
		}
		
		static Rectangle GetDefaultBounds(Form form)
		{
			return new Rectangle(GetDefaultLocation(form), form.Size);
		}
		
		static Point GetDefaultLocation(Form form)
		{
			Rectangle parent = (form.Owner != null) ? form.Owner.Bounds : Screen.FromPoint(Control.MousePosition).WorkingArea;
			Size size = form.Size;
			return new Point(parent.Left + (parent.Width - size.Width) / 2,
			                 parent.Top + (parent.Height - size.Height) / 2);
		}
	}
}
