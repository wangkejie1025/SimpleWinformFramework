using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using Microsoft.Win32;

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
    }
}
