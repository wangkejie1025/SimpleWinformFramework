
namespace SmartFrameWork.Utils.Extension
{
    public delegate void ChildAdded(object parent, object item);
    public delegate void ChildRemoved(object parent, object item);
    public interface IList
    {

        event ChildAdded ChildAdd;
        event ChildRemoved ChildRemove;

        object Tag
        {
            get;
            set;
        }
    }

    public class List<T> : System.Collections.Generic.List<T>, ISelectable, IList
    {
        public event ChildAdded ChildAdd;
        protected void NofityChildAdd(object item)
        {
            if (this.ChildAdd != null)
            {
                ChildAdd(this, item);
            }
        }

        public event ChildRemoved ChildRemove;
        protected void NofityChildRemove(object item)
        {
            if (this.ChildRemove != null)
            {
                ChildRemove(this, item);
            }
        }

        public void Add(T item)
        {
            base.Add(item);
            NofityChildAdd(item);
        }

        public void Remove(T item)
        {
            base.Remove(item);
            NofityChildRemove(item);
        }

        public void RemoveAt(int index)
        {
            T item = base[index];
            base.RemoveAt(index);
            NofityChildRemove(item);
        }

        private object tag;
        [System.ComponentModel.Browsable(false)]
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }


        public object[] ToArray()
        {
            object[] items = new object[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                items[i] = this[i];
            }
            return items;
        }
    }
}
