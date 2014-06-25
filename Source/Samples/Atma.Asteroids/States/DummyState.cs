using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Core;
using Atma.Engine;
using Atma.Asteroids.Engine.Subsystems;
using Atma.Asteroids.Entity;
using Atma.Asteroids.Systems;
using Atma.Asteroids.Components;
using Atma.Asteroids.Assets;
using Atma.Asteroids.Assets.Types;

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
            _components.register(RenderSystem.Uri, new RenderSystem());

            _components.init();

            var id = _entity.create();
            
            var position = _entity.addComponent(id, "position", new Position());
            position.x = 1f;
            position.y = 1f;

            //var velocity = _entity.addComponent(id, "velocity", new Velocity());
            //velocity.x = 2;
            //velocity.y = 1.5f;


            var meshdata = new MeshData();
            meshdata.vertices = new Vector3[] { new Vector3(-1, 1, 0), new Vector3(0, -1, 0), new Vector3(1, 1, 0) };
            meshdata.indices = new int[] { 0, 1, 2 };

            var mesh = new Mesh() { mesh = Assets.Assets.generateAsset(AssetType.MESH, meshdata) };
            _entity.addComponent(id, "mesh", mesh);
            //OpenGL.GL.Vertex2(-1.0f, 1.0f);
            //OpenGL.GL.Color3(Color.SpringGreen.r, Color.SpringGreen.g, Color.SpringGreen.b);
            //OpenGL.GL.Vertex2(0.0f, -1.0f);
            //OpenGL.GL.Color3(Color.Ivory.r, Color.Ivory.g, Color.Ivory.b);
            //OpenGL.GL.Vertex2(1.0f, 1.0f);

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
