using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;

namespace Atma.Asteroids.Engine.Subsystems
{
    public abstract class ViewportSubsystem : ISubsystem
    {
        public static readonly GameUri Uri = "subsystem:viewport";
        private static readonly Logger logger = Logger.getLogger(typeof(DisplayDevice));

        public GameUri uri { get { return Uri; } }

        private GameEngine _engine;

        public void init()
        {
            logger.info("initialise");
            _engine = CoreRegistry.require<GameEngine>(GameEngine.Uri);
        }

        public virtual void preUpdate(float delta)
        {

        }

        public virtual void postUpdate(float delta)
        {
            _engine.currentState.render();
        }

        public void shutdown()
        {
            logger.info("shutdown");
        }
    }
}
