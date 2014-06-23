using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Entity
{
    public class ComponentMap
    {
        public Type type;
        public List<Component> components;

    }

    public class ComponentTable
    {
        private Dictionary<int, List<Component>> _componentMap = new Dictionary<int, List<Component>>();

        public void add(EntityRef e, Component c)
        {
            List<Component> components;
            if (!_componentMap.TryGetValue(e.id, out components))
            {
                components = new List<Component>();
            }
        }
    }
}
