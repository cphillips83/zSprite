using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;

namespace Atma.Asteroids.Engine.Subsystems.Xna
{
    public abstract class XnaSubsystem : ISubsystem
    {
        public abstract GameUri uri { get; }

        public virtual void init()
        {
        }

        public virtual void preUpdate(float delta)
        {
        }

        public virtual void postUpdate(float delta)
        {
        }

        public virtual void shutdown()
        {
        }
    }
}
