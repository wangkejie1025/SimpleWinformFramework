using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartFrameWork
{
    public class ConfigurationAction : Action
    {
        string configFile;

        public ConfigurationAction(string configFile)
        {
            this.Text = "Options ...";
            this.configFile = configFile;
        }


        public override void Perform(SmartFrameWork.ActionContext context)
        {
            //ConfigDialog dlg = new ConfigDialog();
            //dlg.Config = ConfigManager.GetConfig(configFile);
            //dlg.ShowDialog();
        }
    }
}
