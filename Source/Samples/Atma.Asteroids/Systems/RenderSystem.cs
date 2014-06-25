using Atma.Asteroids.Components;
using Atma.Asteroids.Engine.Subsystems;
using Atma.Asteroids.Entity;
using Atma.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Systems
{
    public class RenderSystem : IComponentSystem, IRenderSubscriber
    {
        public static readonly GameUri Uri = "system:render";

        //private GraphicsSubsystem _graphic;
        private EntityManager _manager;

        public void init()
        {
            //_graphic = CoreRegistry.require<GraphicsSubsystem>(GraphicsSubsystem.Uri);
            _manager = CoreRegistry.require<EntityManager>(EntityManager.Uri);
        }

        public void render()
        {
            //_graphic.preUpdate(0);
            foreach (var id in _manager.getWithComponents("position", "mesh"))
            {
                var e = _manager.createRef(id);
                var position = e.getComponent<Position>("position");
                var mesh = e.getComponent<Mesh>("mesh");

                mesh.mesh.render();
                //position.x += velocity.x * delta;
                //position.y += velocity.y * delta;
                //logger.info("updating {0} to {1}, {2}", id, position.x, position.y);
            }
            //_graphic.postUpdate(0);
        }

        public void shutdown()
        {
        }
    }
}
