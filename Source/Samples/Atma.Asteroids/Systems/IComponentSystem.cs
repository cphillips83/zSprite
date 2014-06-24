using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Systems
{
    public interface IUpdateSubscriber
    {
        void update(float delta);
    }

    public interface IRenderSubscriber
    {
        void render();
    }

    public interface IComponentSystem
    {
        void init();
        void shutdown();
    }
}
