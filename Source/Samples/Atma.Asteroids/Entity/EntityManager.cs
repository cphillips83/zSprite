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

        public IEntityRef create()
        {
            throw new NotImplementedException();
        }

        public IEntityRef create(params IComponent[] components)
        {
            throw new NotImplementedException();
        }

        public IEntityRef get(int id)
        {
            throw new NotImplementedException();
        }

        public T addComponent<T>(T t)
            where T : Component
        {

            return t;
        }

        public void removeComponent<T>(T t)
        {

        }

        public T getComponent<T>()
            where T : Component
        {
            return null;
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
