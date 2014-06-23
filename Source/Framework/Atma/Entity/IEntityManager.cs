using Atma.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Entity
{
    public interface IEntityManager : IEnumerable<IEntityRef>
    {
        event OnEntity onEntityChange;
        event OnEntity onEntityAdd;
        event OnEntity onEntityRemove;

        IEntityRef create();
        //IEntityRef create(params IComponent[] components);
        //IEntityRef create(string prefab);
        IEntityRef get(int id);
        void destroy(IEntityRef e);
        int count { get; }
    }
}
