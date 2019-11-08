using System;

namespace SmartFrameWork
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(int i)
        {
            index = i;
        }
        public readonly int index;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NoDisplayAttribute : Attribute
    {
        public NoDisplayAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FormatAttribute : Attribute
    {
        string format;
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        public FormatAttribute(string format)
        {
            this.Format = format;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class AdapterAttribute : Attribute
    {
        public AdapterAttribute(Type t)
        {
            adapterType = t;
        }
        public readonly Type adapterType;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DataSourceAttribute : Attribute
    {
        private Type type;
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        public DataSourceAttribute(Type type)
        {
            this.Type = type;
        }

    }
}
