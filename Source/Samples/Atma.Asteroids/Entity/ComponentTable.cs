using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Entity
{
    public class ComponentTable
    {
        private Dictionary<Type, ComponentMap> _componentMap = new Dictionary<Type, ComponentMap>();
        private Dictionary<int, List<Type>> _idLookup = new Dictionary<int, List<Type>>();

        public void add<T>(EntityRef e, T t)
            where T : Component
        {
            var type = typeof(T);
            ComponentMap components;
            if (!_componentMap.TryGetValue(type, out components))
            {
                components = new ComponentMap(type);
                _componentMap.Add(type, components);
            }

            components.add(e.id, t);

            List<Type> idLookup;
            if (!_idLookup.TryGetValue(e.id, out idLookup))
            {
                idLookup = new List<Type>();
                _idLookup.Add(e.id, idLookup);
            }

            if (idLookup.Contains(type))
                idLookup.Remove(type);

            idLookup.Add(type);
        }

        public void remove<T>(EntityRef e, T t)
            where T : Component
        {
            var type = typeof(T);
            ComponentMap components;
            if (_componentMap.TryGetValue(type, out components))
                components.remove(e.id);

            List<Type> idLookup;
            if (_idLookup.TryGetValue(e.id, out idLookup))
            {
                if (idLookup.Contains(type))
                    idLookup.Remove(type);
            }
        }

        public T get<T>(EntityRef e)
            where T : Component
        {
            var type = typeof(T);
            ComponentMap components;
            if (_componentMap.TryGetValue(type, out components))
                return (T)components.get(e.id);

            return null;
        }

        public void clear(EntityRef e)
        {
            clear(e.id);
        }

        private void clear(int id)
        {
            List<Type> idLookup;
            if (_idLookup.TryGetValue(id, out idLookup))
            {
                foreach (var type in idLookup)
                {
                    ComponentMap components;
                    if (_componentMap.TryGetValue(type, out components))
                        components.remove(id);

                }
                idLookup.Clear();

                _idLookup.Remove(id);
            }
        }

        public void clear()
        {
            var maps = _componentMap.Values.ToArray();
            foreach (var m in maps)
                m.clear();

            _componentMap.Clear();
            _idLookup.Clear();
        }
    }
}
