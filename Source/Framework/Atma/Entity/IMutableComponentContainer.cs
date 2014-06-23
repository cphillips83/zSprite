using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Entity
{
    public interface IMutableComponentContainer : IComponentContainer
    {
        T addComponent<T>(T t) where T: IComponent;
        void removeComponent<T>(T t) where T: IComponent;
    }
}
