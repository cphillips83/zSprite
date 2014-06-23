using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;

namespace Atma.Asteroids.States
{
    public class DummyState : IGameState
    {
        private readonly static Logger logger = Logger.getLogger(typeof(DummyState));

        public void init()
        {
            
        }

        public void update(float dt)
        {
        }

        public void input(float dt)
        {
        }

        public void render()
        {
        }

        public void Dispose()
        {
        }
    }
}
