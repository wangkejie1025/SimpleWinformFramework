using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFrameWork.Model
{
    public class Repository
    {
        private Dictionary<string, IDataElementManager> managers = new Dictionary<string, IDataElementManager>();
        public Dictionary<string, IDataElementManager> Managers
        {
            get { return managers; }
            set { managers = value; }
        }

        private IDataElementManager defaultManager;
        public IDataElementManager DefaultManager
        {
            get { return defaultManager; }
            set { defaultManager = value; }
        }

        private Dictionary<string, DataElementMapping> mapping = new Dictionary<string, DataElementMapping>();
        public Dictionary<string, DataElementMapping> Mapping
        {
            get { return mapping; }
            set { mapping = value; }
        }

        public string GetID(string name)
        {
            foreach (DataElementMapping mapping in Mapping.Values)
            {
                if (mapping.Mapping.ContainsKey(name))
                {
                    return mapping.Mapping[name];
                }
            }
            return null;
        }

        public string GetTypeName(string id)
        {
            if (id != null)
            {
                string[] sect = id.Split('/');
                if (sect.Length >= 1)
                {
                    return sect[0];
                }
            }
            return null;
        }

        public string GetInstanceName(string id)
        {
            string[] sect = id.Split('/');
            if (sect.Length >= 2)
            {
                return sect[1];
            }
            return null;
        }

        public IDataElement GetElementByID(string ID)
        {
            if (ID == null)
            {
                return null;
            }
            if (ID.StartsWith("SEMATIC") && ID.Contains('/'))
            {
                string id = ID.Split('/')[1];
                ID = GetID(id);
            }
            string type = GetTypeName(ID);
            string instance = GetInstanceName(ID);
            if (managers.ContainsKey(type))
            {
                IDataElementManager manager = managers[type];
                return manager.GetElementByID(type, instance, ID); ;

            }
            return null;
        }

        public string[] GetVariableNames(string type, string instance)
        {
            if (managers.ContainsKey(type))
            {
                IDataElementManager manager = managers[type];
                return manager.GetVariableNames(instance); ;

            }
            return null;
        }
    }
}
