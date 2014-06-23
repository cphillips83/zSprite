using Atma.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Entity
{
    public class EntityRef : IEntityRef
    {
        private EntityManager _entityManager;

        public EntityRef(int id, EntityManager em)
        {
            this.id = id;
            _entityManager = em;
        }

        public int id { get; private set; }

        public T addComponent<T>(T t)
            where T : Component
        {
            return _entityManager.addComponent(t);
        }

        public void removeComponent<T>(T t)
            where T : Component
        {
            _entityManager.removeComponent(t);
        }

        public bool hasComponent<T>()
            where T : Component
        {
            return getComponent<T>() != null;
        }

        public T getComponent<T>()
            where T : Component
        {
            return _entityManager.getComponent<T>();
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach (var c in _entityManager.getComponents(this))
                yield return c;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var c in _entityManager.getComponents(this))
                yield return c;
        }
    }
}
