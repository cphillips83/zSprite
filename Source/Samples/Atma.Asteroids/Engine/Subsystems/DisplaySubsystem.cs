using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;
using TK = OpenTK;

namespace Atma.Asteroids.Engine.Subsystems
{
    public abstract class DisplayDevice
    {
        public static readonly GameUri Uri = "subsystem:display";
        private static readonly Logger logger = Logger.getLogger(typeof(DisplayDevice));

        public GameUri uri { get { return Uri; } }

        public virtual void init()
        {
            logger.info("initialise");
        }

        public abstract void processmMessage();

        public abstract void setFullscreen(bool state, bool resizable);

        public abstract void setTitle(string title);

        public abstract void setVSync(bool vsync);

        public abstract bool closeRequest { get; protected set; }

        public abstract void swap();

        public virtual void shutdown()
        {
            logger.info("shutdown");
        }
    }
}
