using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;

namespace SmartFrameWork.Utils
{
    public class StringUtils
    {
        static Dictionary<Assembly, ResourceManager> resourceManager = new Dictionary<Assembly, ResourceManager>();

        public static string GetString(Type type, string name)
        {
          /*  
            ResourceManager rm;
            if (resourceManager.ContainsKey(type.Assembly))
            {
                rm = resourceManager[type.Assembly];
            }
            else
            {
                string assemblyname = type.Assembly.ManifestModule.Name.Replace(".dll","");
                rm = new ResourceManager(assemblyname+".Properties.Resources", type.Assembly);
                resourceManager.Add(type.Assembly, rm);
            }
            try
            {
                string text = rm.GetString(name);
                if (text != null)
                {
                    return text;
                }
            }catch(Exception e){}
            */ 
            return name;
        }
    }
}
