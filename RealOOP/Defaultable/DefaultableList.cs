using System;
using System.Collections;
using System.Collections.Generic;

namespace RealOOP.Defaultable
{
    public class DefaultableList<T> : IList<T>
    {
        private readonly List<T> _list = new List<T>();
        private readonly T _defaultValue;
        public DefaultableList(T defaultValue)
        {
            _defaultValue = defaultValue;
        } 

        public T this[int index]
        {
            get
            {
                try
                {
                    return _list[index];
                }
                catch (Exception)
                {
                    return _defaultValue;
                }
            }

            set { _list[index] = value; }
        }

        public int Count => _list.Count;
        public bool IsReadOnly => false;
        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
