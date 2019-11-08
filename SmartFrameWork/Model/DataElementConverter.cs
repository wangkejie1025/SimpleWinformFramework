using System;
using System.ComponentModel;

namespace SmartFrameWork.Model
{
    class DataElementConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
        {

            if (t == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, t);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
            object value, Type destType)
        {
            if (destType == typeof(string) && value is IDataElement)
            {
                IDataElement p = (IDataElement)value;
                return p.Id;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
