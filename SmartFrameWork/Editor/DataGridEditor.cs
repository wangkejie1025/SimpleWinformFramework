using System;
using System.Collections.Generic;
using System.Reflection;

namespace SmartFrameWork.Editor
{
    public partial class DataGridEditor : FrameWorkEditor
    {
        private object data;
        public object Data
        {
            get { return data; }
            set
            {
                // 处理历史数据
                if (data is Utils.Extension.IList)
                {
                    Utils.Extension.IList list = data as Utils.Extension.IList;
                    list.ChildAdd -= new Utils.Extension.ChildAdded(list_ChildAdd);
                    list.ChildRemove -= new Utils.Extension.ChildRemoved(list_ChildRemove);
                }

                data = value;
                this.bindingSource.DataSource = Data;
                this.gridView.RefreshData();

                if (data is Utils.Extension.IList)
                {
                    Utils.Extension.IList list = data as Utils.Extension.IList;
                    list.ChildAdd += new Utils.Extension.ChildAdded(list_ChildAdd);
                    list.ChildRemove += new Utils.Extension.ChildRemoved(list_ChildRemove);
                }
            }
        }

        void list_ChildRemove(object parent, object item)
        {
            this.gridView.RefreshData();
        }

        void list_ChildAdd(object parent, object item)
        {
            this.gridView.RefreshData();
        }

        public override void Refresh()
        {
            this.gridView.RefreshData();
        }

        public bool HasAttrubute(PropertyInfo property, Type attribute)
        {
            object[] attributes = property.GetCustomAttributes(attribute, false);
            if (attributes != null && attributes.Length == 1)
            {
                return true;
            }
            return false;
        }

        public object GetAttrubute(PropertyInfo property, Type attribute)
        {
            object[] attributes = property.GetCustomAttributes(attribute, false);
            if (attributes != null && attributes.Length == 1)
            {
                return attributes[0];
            }
            return null;
        }

        private List<PropertyInfo> properties = new List<PropertyInfo>();

        private Dictionary<int, PropertyInfo> columns = new Dictionary<int, PropertyInfo>();

        private Type dataType;
        public Type DataType
        {
            get { return dataType; }
            set
            {
                if (dataType != value)
                {
                    dataType = value;
                    properties.Clear();
                    columns.Clear();
                    foreach (PropertyInfo property in DataType.GetProperties())
                    {
                        object attri = GetAttrubute(property, typeof(System.ComponentModel.BrowsableAttribute));
                        if (attri != null && (attri as System.ComponentModel.BrowsableAttribute).Browsable == false)
                        {
                            continue;
                        }
                        if (HasAttrubute(property, typeof(NoDisplayAttribute)))
                        {
                            continue;
                        }

                        object columnattri = GetAttrubute(property, typeof(ColumnAttribute));
                        if (columnattri != null)
                        {
                            int index = (columnattri as ColumnAttribute).index;
                            columns.Add(index, property);
                        }
                        properties.Add(property);
                    }
                    {
                        this.gridView.Columns.Clear();
                        foreach (PropertyInfo property in properties)
                        {
                            DevExpress.XtraGrid.Columns.GridColumn column = this.gridView.Columns.AddVisible(property.Name);
                            FormatAttribute fromatAttr = GetAttrubute(property, typeof(FormatAttribute)) as FormatAttribute;
                            if (fromatAttr != null)
                            {
                                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                                column.DisplayFormat.FormatString = fromatAttr.Format;
                            }
                        }

                        for (int columnIndex = 0; columnIndex < properties.Count; columnIndex++)
                        {
                            if (columns.ContainsKey(columnIndex))
                            {
                                this.gridView.Columns.ColumnByFieldName(columns[columnIndex].Name).VisibleIndex = columnIndex;
                            }
                        }
                    }
                }
            }
        }

        public DataGridEditor()
        {
            InitializeComponent();
        }


        private void gridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int[] rows = this.gridView.GetSelectedRows();
            if (rows != null && rows.Length == 1)
            {
                object value = this.gridView.GetRow(rows[0]);
                SelectionManager.Selection = value as SmartFrameWork.ISelectable;
            }
        }
    }
}
