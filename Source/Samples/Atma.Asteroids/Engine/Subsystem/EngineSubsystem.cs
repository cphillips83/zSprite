using Atma.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Engine.Subsystem
{    
    public interface EngineSubsystem
    {
        void preInitialise();

        //void postInitialise(Config config);

        void preUpdate(IGameState currentState, float delta);

        void postUpdate(IGameState currentState, float delta);

        //void shutdown(Config config);

        void dispose();

        //void registerSystems(ComponentSystemManager componentSystemManager);
    }

}
