using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;

namespace Atma.Asteroids.Engine.Subsystems.Xna
{
    public class XnaGraphicsSubystem : XnaSubsystem
    {
        public static readonly GameUri Uri = "subsystem:graphics";
        private static readonly Logger logger = Logger.getLogger(typeof(GraphicsSubsystem));

        private GameEngine _engine;

        public override void init()
        {
            logger.info("initialise");
            _engine = CoreRegistry.require<GameEngine>(GameEngine.Uri);
        }

        public override void preUpdate(float delta)
        {

        }

        public override void postUpdate(float delta)
        {
            _engine.currentState.render();
        }

        public override void shutdown()
        {
            logger.info("shutdown");
        }

        public override GameUri uri { get { return Uri; } }
        
    }
}
