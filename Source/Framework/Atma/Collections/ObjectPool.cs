using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Collections
{
    public class ObjectPool<T> : IDisposable
        where T : new()
    {
        private List<T> _objects = new List<T>(64);
        private Stack<int> _freedObjects = new Stack<int>();

        public int activeItems { get { return _objects.Count; } }
        public int totalItems { get { return _objects.Count + _freedObjects.Count; } }

        public void free(int index)
        {
            _freedObjects.Push(index);
        }

        public T this[int index]
        {
            get { return _objects[index]; }
            set { _objects[index] = value; }
        }

        public int get()
        {
            if (_freedObjects.Count == 0)
            {
                var index = _objects.Count;
                _objects.Add(new T());
                return index;
            }

            return _freedObjects.Pop();
        }

        public void Dispose()
        {
            _freedObjects.Clear();
            _objects.Clear();
            _objects = null;
            _freedObjects = null;
        }
    }

}
