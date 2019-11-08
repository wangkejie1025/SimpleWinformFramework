using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

namespace SmartFrameWork
{
    public class XmlSerializer
    {
   
        private Dictionary<string, Type> types = new Dictionary<string, Type>();
        public Dictionary<string, Type> Types
        {
            get { return types; }
            set { types = value; }
        }

        public void Serialize(string fileName, object o)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("File");
            doc.AppendChild(root);
            Serialize(doc, root, o);
            doc.Save(fileName);
        }

        protected void Serialize(XmlDocument doc, XmlElement parent, object data)
        {
            //显示projectFile
            XmlElement ele = doc.CreateElement(data.GetType().Name);
            parent.AppendChild(ele);
            Type type = data.GetType();


            if (type == typeof(bool) ||
                type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(long) ||
                type == typeof(double) ||
                type == typeof(float) ||
                type == typeof(string) ||
                type.IsEnum
                )
            {
                if (data != null)
                {
                    ele.InnerText = data.ToString();
                }
            }
            else
            {

                foreach (PropertyInfo info in type.GetProperties())
                {
                    try
                    {
                        object[] attrs = info.GetCustomAttributes(typeof(System.Xml.Serialization.XmlIgnoreAttribute), true);
                        if (attrs != null && attrs.Length > 0)
                        {
                            continue;
                        }

                        Type ptype = info.PropertyType;
                        if (typeof(System.Collections.IList).IsAssignableFrom(ptype))
                        {
                            System.Collections.IList sublist = info.GetValue(data, null) as System.Collections.IList;
                            XmlElement listele = doc.CreateElement(info.Name);
                            ele.AppendChild(listele);
                            foreach (object subitem in sublist)
                            {
                                Serialize(doc, listele, subitem);
                            }
                        }
                        else if (
                            ptype == typeof(bool) ||
                            ptype == typeof(int) ||
                            ptype == typeof(uint) ||
                            ptype == typeof(long) ||
                            ptype == typeof(double) ||
                            ptype == typeof(float) ||
                            ptype == typeof(string) ||
                            ptype.IsEnum
                            )
                        {
                            XmlAttribute itemattr = doc.CreateAttribute(info.Name);
                            object v = info.GetValue(data, null);
                            if (v != null)
                            {
                                itemattr.Value = info.GetValue(data, null).ToString();
                            }
                            ele.Attributes.Append(itemattr);
                        }
                        else
                        {
                            object value = info.GetValue(data, null);
                            if (value != null)
                            {
                                XmlElement listele = doc.CreateElement(info.Name);
                                ele.AppendChild(listele);
                                Serialize(doc, listele, value);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Services.LoggingService.Error(ex);
                    }
                }
            }
        }

        public object DeSerialize(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode ele = doc.DocumentElement.FirstChild;

            return DeSerialize(ele);
        }

        protected void GetProperty(object data, PropertyInfo info, XmlAttribute attr)
        {

            try
            {
                string value = attr.Value as string;

                if (info.PropertyType == typeof(bool))
                {
                    bool v = bool.Parse(value);
                    info.SetValue(data, v, null);
                }

                if (info.PropertyType == typeof(string))
                {
                    info.SetValue(data, value, null);
                }
                if (info.PropertyType == typeof(int))
                {
                    int v = 0;
                    if (value != null && value != "")
                    {
                        v = Int32.Parse(value);
                    }
                    info.SetValue(data, v, null);
                }
                if (info.PropertyType == typeof(uint))
                {
                    uint v = 0;
                    if (value != null && value != "")
                    {
                        v = System.UInt32.Parse(value);
                    }
                    info.SetValue(data, v, null);
                }
                if (info.PropertyType == typeof(long))
                {
                    long v = 0;
                    if (value != null && value != "")
                    {
                        v = long.Parse(value);
                    }
                    info.SetValue(data, v, null);
                }
                if (info.PropertyType == typeof(float))
                {
                    float v = 0;
                    if (value != null && value != "")
                    {
                        v = float.Parse(value);
                    }
                    info.SetValue(data, v, null);
                }
                if (info.PropertyType == typeof(double))
                {
                    double v = 0;
                    if (value != null && value != "")
                    {
                        v = double.Parse(value);
                    }
                    info.SetValue(data, v, null);
                }
                if (info.PropertyType == typeof(DateTime))
                {
                    DateTime v = DateTime.Now;
                    if (value != null && value != "")
                    {
                        v = DateTime.Parse(value);
                    }
                    info.SetValue(data, v, null);
                }

                if (info.PropertyType.IsEnum)
                {
                    ;
                    info.SetValue(data, Enum.Parse(info.PropertyType, value), null);
                }
            }
            catch (Exception ex)
            {
                Services.LoggingService.Error("属性" + info.Name + "错误", ex);
            }
        }


        protected object DeSerialize(XmlNode ele)
        {
            if (ele.Name == "String")
            {
                return ele.InnerText;
            }
            string typeName = ele.Name;
            if (!types.ContainsKey(typeName))
            {
                return null;
            }

            Type type = types[typeName];

            object data = Activator.CreateInstance(type);


            foreach (XmlAttribute attr in ele.Attributes)
            {
                PropertyInfo p = type.GetProperty(attr.Name);
                if (p != null)
                {
                    GetProperty(data, p, attr);
                }
            }


            foreach (XmlNode subnode in ele.ChildNodes)
            {
                PropertyInfo property = type.GetProperty(subnode.Name);
                if (property != null)
                {
                    if (typeof(System.Collections.IList).IsAssignableFrom(property.PropertyType))
                    {
                        System.Collections.IList list = property.GetValue(data, null) as System.Collections.IList;
                        foreach (XmlNode sublistnode in subnode.ChildNodes)
                        {
                            object sublistobj = DeSerialize(sublistnode);
                            if (list != null && sublistobj != null)
                            {
                                list.Add(sublistobj);
                            }
                        }
                    }
                    else
                    {
                        object value = DeSerialize(subnode.FirstChild);
                        property.SetValue(data, value, null);
                    }
                }
            }
            return data;
        }
    }
}
