﻿using Atma.Core;
using Atma.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Engine.Subsystems
{
    public abstract class GraphicsSubsystem : ISubsystem
    {
        public static readonly GameUri Uri = "subsystem:graphics";
        private static readonly Logger logger = Logger.getLogger(typeof(GraphicsSubsystem));

        public GameUri uri { get { return Uri; } }

        private GameEngine _engine;

        public virtual void init()
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

        public virtual void shutdown()
        {
            logger.info("shutdown");
        }

    }
}
