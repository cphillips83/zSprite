using Atma.Entity;
using Atma.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Entity
{
    public class EntityManager : IEntityManager
    {
        public event OnEntity onEntityChange;
        public event OnEntity onEntityAdd;
        public event OnEntity onEntityRemove;

        private ComponentTable _componentTable = new ComponentTable();

        private int _nextId = 0;
        private List<int> _entities = new List<int>(1024);
        private HashSet<int> _entityMap = new HashSet<int>();
        //private List<int> _freeIds = new List<int>();

        public int create()
        {
            _nextId++;
            var id = _nextId;

            _entityMap.Add(id);
            _entities.Add(id);

            if (onEntityAdd != null)
                onEntityAdd(id);

            return id;
        }

        //public int create(params Component[] components)
        //{
        //    _nextId++;
        //    var id = _nextId;

        //    _entityMap.Add(id);
        //    _entities.Add(id);

        //    foreach (var c in components)
        //        _componentTable.add(id, c);

        //    if (onEntityAdd != null)
        //        onEntityAdd(id);

        //    return id;
        //}

        public T addComponent<T>(int id, string component, T t)
            where T : Component
        {
            _componentTable.add(id, component, t);

            if (onEntityChange != null)
                onEntityChange(id);

            return t;
        }

        public void removeComponent(int id, string component)
        //where T: Component
        {
            _componentTable.remove(id, component);

            if (onEntityChange != null)
                onEntityChange(id);
        }

        public bool hasetComponent(int id, string component)
        //where T : Component
        {
            return _componentTable.has(id, component);
        }

        public T getComponent<T>(int id, string component)
            where T : Component
        {
            return _componentTable.get<T>(id, component);
        }

        public IEnumerable<Component> getComponents(int id)
        {
            return _componentTable.getAll(id);
        }

        public bool exists(int id)
        {
            return _entityMap.Contains(id);
        }

        public void destroy(int id)
        {
            if (exists(id))
            {
                var index = _entities.IndexOf(id);
                _entities[index] = _entities[_entities.Count - 1];
                _entities.RemoveAt(_entities.Count - 1);
                
                _entityMap.Remove(id);

                if (onEntityRemove != null)
                    onEntityRemove(id);
            }
        }

        public EntityRef createRef(int id)
        {
            return new EntityRef(id, this);
        }

        public int count
        {
            get { return _entities.Count; }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }
    }
}
