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
        private Dictionary<int, EntityRef> _entities = new Dictionary<int, EntityRef>();
        //private List<int> _freeIds = new List<int>();

        public IEntityRef create()
        {
            var e = new EntityRef(++_nextId, this);
            _entities.Add(e.id, e);

            if (onEntityAdd != null)
                onEntityAdd(e);

            return e;
        }

        public IEntityRef create(params Component[] components)
        {
            var e = new EntityRef(++_nextId, this);
            _entities.Add(e.id, e);

            foreach (var c in components)
                _componentTable.add(e, c);

            if (onEntityAdd != null)
                onEntityAdd(e);

            return e;
        }

        public IEntityRef get(int id)
        {
            EntityRef e;
            if (!_entities.TryGetValue(id, out e))
                return null;

            return e;
        }

        public T addComponent<T>(EntityRef e, T t)
            where T : Component
        {
            _componentTable.add(e, t);

            if (onEntityChange != null)
                onEntityChange(e);

            return t;
        }

        public void removeComponent<T>(EntityRef e, T t)
            where T: Component
        {
            _componentTable.remove(e, t);

            if (onEntityChange != null)
                onEntityChange(e);
        }

        public T getComponent<T>(EntityRef e)
            where T : Component
        {
            return _componentTable.get<T>(e);
        }

        public IEnumerable<Component> getComponents(EntityRef e)
        {
            yield break;
        }

        public void destroy(IEntityRef e)
        {
            throw new NotImplementedException();
        }

        public int count
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<IEntityRef> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
