using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SmartFrameWork.Model
{
    public enum Direction
    {
        Input, Output, Both
    }

    public interface Identifier
    {
        string Name { get; set; }
    }

    public interface ISingleElementContainer
    {
        string ElementName { get; set; }

        IDataElement Element { get; set; }
    }

    public interface IMultiElementContainer
    {
        List<string> ElementNames { get; set; }

        void AddElement(string name);

        void RemoveElement(int index);

        List<IDataElement> Elements { get; set; }
    }

    public interface ISingleInputContainer : ISingleElementContainer
    {
    }

    public interface IMultiInputContainer : IMultiElementContainer
    {
        List<string> Names { get; set; }
    }

    public interface ISingleOutputContainer : IMultiElementContainer
    {
    }


    public delegate void ValueUpdateEvent(object sender, EventArgs e);
    public delegate void ValueChangedEvent(object sender, EventArgs e);

    [TypeConverter(typeof(DataElementConverter))]
    public interface IDataElement : Identifier, SmartFrameWork.ISelectable
    {
        event ValueUpdateEvent ValueUpdate;

        event ValueChangedEvent ValueChange;

        string Name { get; set; }
        string Id { get; set; }
        string Type { get; }
        object Value { get; set; }
        bool Dirty { get; set; }
        string Description { get; set; }
        List<ValueEnum> Values { get; set; }
        double MaxValue { get; set; }
        double MinValue { get; set; }
        string Unit { get; set; }
        int Reference { get; set; }
        Direction Direction { get; set; }
        DateTime Time { get; set; }
        bool RefreshFromMemery { get; set; }

        void UpdateValue(object value, DateTime time);
    }

    public class DataElement : SmartFrameWork.Utils.Observable, IDataElement, Identifier
    {

        public event ValueUpdateEvent ValueUpdate;

        public event ValueChangedEvent ValueChange;


        public void NotifyValueUpdate()
        {
            ValueUpdate?.Invoke(this, new EventArgs());
        }

        public void NotifyValueChange()
        {
            ValueChange?.Invoke(this, new EventArgs());
        }

        private string id;
        [System.ComponentModel.Browsable(false)]
        public virtual string Id
        {
            get { return id; }
            set
            {
                id = value;
                NofityPropertyChanged("Id");
            }
        }

        [System.ComponentModel.Browsable(false)]
        public virtual string Type
        {
            get { return "GENERAL"; }
        }

        private Direction direction = Direction.Both;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private bool refreshFromMemery = false;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public bool RefreshFromMemery
        {
            get { return refreshFromMemery; }
            set { refreshFromMemery = value; }
        }

        private string name;
        [SmartFrameWork.Group("Basic")]
        [SmartFrameWork.Text("Name")]
        [SmartFrameWork.Column(0)]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public virtual string Name
        {
            get { return name; }
            set
            {
                name = value;
                NofityPropertyChanged("Name");
            }
        }

        private string description;
        [SmartFrameWork.Group("Basic")]
        [SmartFrameWork.Text("Description")]
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        private object value;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public virtual object Value
        {
            get { return this.value; }
            set
            {
                bool changed = (this.value != value);
                this.value = value;
                this.Time = DateTime.Now;
                Dirty = true;
                NotifyValueUpdate();
                if (changed)
                {
                    NotifyValueChange();
                }
            }
        }

        public virtual void UpdateValue(object value, DateTime time)
        {
            bool changed = (this.value != value);
            this.value = value;
            this.Time = time;
            Dirty = true;
            NotifyValueUpdate();
            if (changed)
            {
                NotifyValueChange();
            }
        }

        private DateTime time;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public virtual DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                NofityPropertyChanged("Time");
            }
        }


        private bool dirty;
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public virtual bool Dirty
        {
            get { return dirty; }
            set { dirty = value; }
        }

        private int reference;
        [SmartFrameWork.Group("Basic")]
        [SmartFrameWork.Text("Reference")]
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public int Reference
        {
            get { return reference; }
            set { reference = value; }
        }


        private string unit;
        [SmartFrameWork.Group("Basic")]
        [SmartFrameWork.Text("Unit")]
        [System.Xml.Serialization.XmlAttribute()]
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }


        private List<ValueEnum> values = new List<ValueEnum>();
        [SmartFrameWork.Group("Value")]
        [SmartFrameWork.Text("Values")]
        [System.ComponentModel.Bindable(false)]
        [SmartFrameWork.NoDisplay]
        public List<ValueEnum> Values
        {
            get { return values; }
            set { values = value; }
        }

        private double maxValue;
        [SmartFrameWork.Group("Value")]
        [SmartFrameWork.Text("Max Value")]
        [System.Xml.Serialization.XmlAttribute()]
        public double MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }


        private double minValue;
        [SmartFrameWork.Group("Value")]
        [SmartFrameWork.Text("Min Value")]
        [System.Xml.Serialization.XmlAttribute()]
        public double MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }


        public virtual void Clone(object org)
        {
            if (org is DataElement)
            {
                DataElement element = org as DataElement;
                this.id = element.Id;
                this.direction = element.Direction;
                this.name = element.Name;
                this.description = element.Description;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public partial class ValueEnum
    {
        private string name;
        [SmartFrameWork.Text("Name")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double value;
        [SmartFrameWork.Text("Value")]
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
