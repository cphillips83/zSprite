using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Entity
{
    public interface IComponentContainer : IEnumerable<IComponent>
    {
        bool hasComponent<T>() where T: IComponent;
        T getComponent<T>() where T: IComponent;
    }
}
