using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomSetLibrary
{
    public class CustomSet<T> : IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public int Count => _list.Count;

        public bool IsEmpty => Count == 0;

        public bool Add(T item)
        {
            if (_list.Contains(item))
                return false;

            _list.AddLast(item);
            return true;
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}