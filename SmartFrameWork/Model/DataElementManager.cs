using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFrameWork.Model
{
    public interface IDataElementManager
    {
        bool Contains(string instance);

        IDataElement GetElementByID(string type, string instance, string ID);

        string[] GetVariableNames(string instance);
    }

    public class DataElementMapping
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        Dictionary<string, string> mapping = new Dictionary<string, string>();
        public Dictionary<string, string> Mapping
        {
            get { return mapping; }
            set { mapping = value; }
        }
    }

    public class DataElementManager : IDataElementManager
    {
        Dictionary<string, Dictionary<string, IDataElement>> elements = new Dictionary<string, Dictionary<string, IDataElement>>();
        public Dictionary<string, Dictionary<string, IDataElement>> Elements
        {
            get { return elements; }
            set { elements = value; }
        }

        public virtual bool Contains(string instance)
        {
            return Elements.ContainsKey(instance);
        }


        public virtual string GetID(string ID)
        {
            return ID.Substring(ID.IndexOf('/') + 1);
        }

        public IDataElement GetElementByID(string type, string instance, string ID)
        {
            ID = GetID(ID);
            if (elements.ContainsKey(instance))
            {
                Dictionary<string, IDataElement> model = elements[instance];
                if (model.ContainsKey(ID))
                {
                    return model[ID];
                }
            }
            return null;
        }
        public string[] GetVariableNames(string instance)
        {
            if (elements.ContainsKey(instance))
            {
                Dictionary<string, IDataElement> model = elements[instance];
                string[] ids = new string[model.Keys.Count];
                model.Keys.CopyTo(ids, 0);
                return ids;
            }
            return null;
        }
    }
}
