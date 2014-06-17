using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zSprite.EntitySystem.Systems
{
    public abstract class BaseComponentSystem : ComponentSystem
    {

        public abstract void initialise();

        public abstract void preBegin();

        public abstract void postBegin();

        public abstract void preSave();

        public abstract void postSave();

        public abstract void shutdown();

    }
}
