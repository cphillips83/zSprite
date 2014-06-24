using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;
using Atma.Asteroids.Engine.Subsystems;
using Atma.Asteroids.Entity;
using Atma.Asteroids.Systems;

namespace Atma.Asteroids.States
{
    public class DummyState : IGameState
    {
        private readonly static Logger logger = Logger.getLogger(typeof(DummyState));

        private GraphicsSubsystem _graphics;
        private EntityManager _entity;
        private ComponentSystemManager _components;

        public void begin()
        {
            logger.info("begin");
            _graphics = CoreRegistry.require<GraphicsSubsystem>(GraphicsSubsystem.Uri);
            
            _entity = CoreRegistry.put(EntityManager.Uri, new EntityManager());
            
            _components = CoreRegistry.put(ComponentSystemManager.Uri, new ComponentSystemManager());
            _components.register(PhysicsSystem.Uri, new PhysicsSystem());

            _components.init();
        }

        public void end()
        {
            logger.info("end");
            _components.shutdown();
            //_entity.clear();
            CoreRegistry.clear();
        }

        public void update(float dt)
        {
            _components.update(dt);
        }

        public void input(float dt)
        {

        }

        public void render()
        {
            //graphics.beginRender()
            _components.render();

        }

    }
}
