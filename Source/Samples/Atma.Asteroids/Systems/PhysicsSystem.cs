using Atma.Asteroids.Components;
using Atma.Asteroids.Engine.Subsystems;
using Atma.Asteroids.Entity;
using Atma.Core;
using Atma.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Systems
{
    public class PhysicsSystem : IComponentSystem, IUpdateSubscriber
    {
        public readonly static GameUri Uri = "system:phsyics";

        private readonly static Logger logger = Logger.getLogger(typeof(PhysicsSystem));
        private EntityManager _manager;

        public void init()
        {
            logger.info("initialise");
            _manager = CoreRegistry.require<EntityManager>(EntityManager.Uri);
        }

        public void update(float delta)
        {
            foreach (var id in _manager.getWithComponents("position", "velocity"))
            {
                var e = _manager.createRef(id);
                var position = e.getComponent<Position>("position");
                var velocity = e.getComponent<Velocity>("velocity");

                position.x += velocity.x * delta;
                position.y += velocity.y * delta;
                //logger.info("updating {0} to {1}, {2}", id, position.x, position.y);
            }
        }

        public void shutdown()
        {
            logger.info("shutdown");

        }
    }
}
