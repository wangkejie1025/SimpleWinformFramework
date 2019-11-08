using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Xml;
using System.IO;

namespace SmartFrameWork.Services
{
    /// <summary>
    /// This interface flags an object beeing "mementocapable". This means that the
    /// state of the object could be saved to an <see cref="Properties"/> object
    /// and set from a object from the same class.
    /// This is used to save and restore the state of GUI objects.
    /// </summary>
    public interface IMementoCapable
    {
        /// <summary>
        /// Creates a new memento from the state.
        /// </summary>
        Properties CreateMemento();

        /// <summary>
        /// Sets the state to the given memento.
        /// </summary>
        void SetMemento(Properties memento);
    }
    /// <summary>
    /// Description of PropertyGroup.The class can acess like Properties[propertyName]
    /// </summary>
    public class Properties
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();
        //access through object[propertyName],索引的访问方式
        public string this[string property]
        {
            get
            {
                //如果包含key则返回value
                return Convert.ToString(Get(property));
            }
            set
            {
                //设置Key value
                Set(property, value);
            }
        }

        public object Get(string property)
        {
            if (!properties.ContainsKey(property))
            {
                return null;
            }
            return properties[property];
        }

        public void Set<T>(string property, T value)
        {
            //set会触发OnPropertyChanged事件更新对应属性的值
            T oldValue = default(T);
            if (!properties.ContainsKey(property))
            {
                properties.Add(property, value);
            }
            else
            {
                oldValue = Get<T>(property, value);
                properties[property] = value;
            }
            //触发事件，通知属性字典进行更新
            OnPropertyChanged(new PropertyChangedEventArgs(this, property, oldValue, value));
        }

        public bool Contains(string property)
        {
            return properties.ContainsKey(property);
        }

        public bool Remove(string property)
        {
            return properties.Remove(property);
        }

        public Properties()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Properties:{");
            foreach (KeyValuePair<string, object> entry in properties)
            {
                sb.Append(entry.Key);
                sb.Append("=");
                sb.Append(entry.Value);
                sb.Append(",");
            }
            sb.Append("}]");
            return sb.ToString();
        }

        public static Properties ReadFromAttributes(XmlReader reader)
        {
            Properties properties = new Properties();
            //xml中的必须为Attribute的方式
            if (reader.HasAttributes)
            {
                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    properties[reader.Name] = reader.Value;
                }
                reader.MoveToElement(); //Moves the reader back to the element node.
            }
            return properties;
        }

        public void ReadProperties(XmlReader reader, string endElement)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == endElement)
                        {
                            return;
                        }
                        break;
                    case XmlNodeType.Element:
                        string propertyName = reader.LocalName;
                        //如果是嵌套的Properties
                        if (propertyName == "Properties")
                        {
                            propertyName = reader.GetAttribute(0);
                            Properties p = new Properties();
                            p.ReadProperties(reader, "Properties");
                            properties[propertyName] = p;
                        }
                        //如果是数组的类型
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            properties[propertyName] = ReadArray(reader);
                        }
                        else
                        {
                            //普通的属性加入到字典中
                            properties[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                        }
                        break;
                }
            }
        }
        //这里就需要改造对象了
        IList<string> ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return new List<string>();
            List<string> temp = new List<string>();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == "Array")
                        {
                            return temp;
                        }
                        break;
                    case XmlNodeType.Element:
                        temp.Add((reader.HasAttributes ? reader.GetAttribute(0) : null));
                        break;
                }
            }
            return temp;
        }

        public void WriteProperties(XmlTextWriter writer)
        {
            foreach (KeyValuePair<string, object> entry in properties)
            {
                //key有三种形式，一种是Propertie，一种是Array还有一种就是普通的string
                object val = entry.Value;
                if (val is Properties)
                {
                    writer.WriteStartElement("Properties");
                    writer.WriteAttributeString("name", entry.Key);
                    ((Properties)val).WriteProperties(writer);
                    writer.WriteEndElement();
                }
                //else if (val is Array || val is ArrayList)
                else if (val is IList<string>)
                {
                    writer.WriteStartElement("Array");
                    writer.WriteAttributeString("name", entry.Key);
                    foreach (object o in (IEnumerable)val)
                    {
                        writer.WriteStartElement("Element");
                        WriteValue(writer, o);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteStartElement(entry.Key);
                    WriteValue(writer, val);
                    writer.WriteEndElement();
                }
            }
        }

        void WriteValue(XmlTextWriter writer, object val)
        {
            if (val != null)
            {
                if (val is string)
                {
                    writer.WriteAttributeString("value", val.ToString());
                }
                else
                {
                    TypeConverter c = TypeDescriptor.GetConverter(val.GetType());
                    writer.WriteAttributeString("value", c.ConvertToInvariantString(val));
                }
            }
        }

        public void Save(string fileName)
        {
            using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("Properties");
                WriteProperties(writer);
                writer.WriteEndElement();
            }
        }
        public static Properties Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Properties":
                                Properties properties = new Properties();
                                properties.ReadProperties(reader, "Properties");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        public T Get<T>(string property, T defaultValue)
        {
            if (!properties.ContainsKey(property))
            {
                properties.Add(property, defaultValue);
                return defaultValue;
            }
            object o = properties[property];

            if (o is string && typeof(T) != typeof(string))
            {
                TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                try
                {
                    o = c.ConvertFromInvariantString(o.ToString());
                }
                catch (NotSupportedException ex)
                {
                    MessageService.ShowWarning("Error loading property '" + property + "': " + ex.Message);
                    o = defaultValue;
                }
                properties[property] = o; // store for future look up
            }
            //else if (o is ArrayList && typeof(T).IsArray)
            else if(o is IList<T>)
            {
                IList<T> list = (IList<T>)o;
                Type elementType = typeof(T).GetElementType();
                Array arr = System.Array.CreateInstance(elementType, list.Count);
                TypeConverter c = TypeDescriptor.GetConverter(elementType);
                for (int i = 0; i < arr.Length; ++i)
                {
                    if (list[i] != null)
                    {
                        arr.SetValue(c.ConvertFromInvariantString(list[i].ToString()), i);
                    }
                }
                o = arr;
                properties[property] = o; // store for future look up
            }
            try
            {
                return (T)o;
            }
            catch (NullReferenceException)
            {
                // can happen when configuration is invalid -> o is null and a value type is expected
                return defaultValue;
            }
        }
        //事件触发函数，通知绑定事件的处理函数
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        //通知所有订阅事件的处理函数
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
