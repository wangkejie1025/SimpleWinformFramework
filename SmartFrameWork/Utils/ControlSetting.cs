using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using Microsoft.Win32;
using DevExpress.XtraNavBar;

namespace SmartFrameWork.Utils
{
    public class ControlSetting
    {
        public static void GridViewSetting(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            gridView.OptionsMenu.EnableColumnMenu = false;
            gridView.OptionsMenu.EnableFooterMenu = false;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsView.ShowGroupPanel = false;
        }
        public static void BindingPortName(System.Windows.Forms.ComboBox comboBox)
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                comboBox.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    comboBox.Items.Add(sValue);
                }
            }
        }
        public static string[] GetCOMPortName()
        {
            //string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey(@"Hardware\DeviceMap\SerialComm");
            if (keyCom != null)
            {
                return keyCom.GetValueNames();
            }
            return new string[0];
        }
        /// <summary>
        /// 创建barGroup
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="caption">标题</param>
        private DevExpress.XtraNavBar.NavBarGroup CreatNavigationBarGroup(NavBarControl navBarControl,string name, string caption)
        {
            DevExpress.XtraNavBar.NavBarGroup navBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
            navBarGroup.Name = name;
            navBarGroup.Caption = caption;
            navBarGroup.Expanded = true;
            navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
           navBarGroup});
            return navBarGroup;
        }
        /// <summary>
        /// 创建barGroup的子项
        /// </summary>
        /// <param name="navBarGroup">父group</param>
        /// <param name="name">名称</param>
        /// <param name="caption">标题</param>
        private void CreatNavigationBarItem(NavBarControl navBarControl, NavBarGroup navBarGroup, string name, string caption, string SmallImageIndex,Action<object, NavBarLinkEventArgs> action)
        {
            DevExpress.XtraNavBar.NavBarItem navBarItem = new DevExpress.XtraNavBar.NavBarItem();
            navBarItem.Name = name;
            navBarItem.Caption = caption;
            navBarItem.SmallImageIndex = int.Parse(SmallImageIndex);
            navBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(action);
            //把barItem加入到ItemGroup中
            navBarGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
           new NavBarItemLink(navBarItem)});
            //把barItem加入到navBarControl里
            navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
           navBarItem});
        }
    }
}
