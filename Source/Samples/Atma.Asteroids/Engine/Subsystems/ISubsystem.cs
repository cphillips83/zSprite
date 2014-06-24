using Atma.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Engine.Subsystems
{
    public interface ISubsystem 
    {
        GameUri uri { get; }

        void init();

        void preUpdate(float delta);
        //void update(float delta);
        void postUpdate(float delta);

        //void preRender();
        //void render();
        //void postRender();

        void shutdown();

        //void dispose();

        //void registerSystems(ComponentSystemManager componentSystemManager);
    }
}
