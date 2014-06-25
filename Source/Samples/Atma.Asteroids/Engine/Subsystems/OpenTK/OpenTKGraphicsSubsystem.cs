using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atma.Engine;

using TK = OpenTK;
using OpenGL = OpenTK.Graphics.OpenGL;
using GL11 = OpenTK.Graphics.ES11.GL;
using Atma.Core;
using Atma.Asteroids.Assets;
using Atma.Asteroids.Assets.Types;

namespace Atma.Asteroids.Engine.Subsystems.OpenTK
{
    public class OpenTKGraphicsSubsystem : GraphicsSubsystem
    {
        private static readonly Logger logger = Logger.getLogger(typeof(OpenTKGraphicsSubsystem));
        private OpenTKDisplaySubsystem _display;
        private GameEngine _engine;

        public override void preUpdate(float delta)
        {
            _display.processmMessage();
            OpenGL.GL.Clear(OpenGL.ClearBufferMask.ColorBufferBit | OpenGL.ClearBufferMask.DepthBufferBit);
            OpenGL.GL.MatrixMode(OpenGL.MatrixMode.Projection);
            OpenGL.GL.LoadIdentity();
            OpenGL.GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

         
            base.preUpdate(delta);
        }

        public override void postUpdate(float delta)
        {
            base.postUpdate(delta);
            _display.swap();
        }

        public override void init()
        {
            base.init();
            //CoreRegistry.putPermanently(RenderingSubsystemFactory.class, new LwjglRenderingSubsystemFactory(bufferPool));

            _engine = CoreRegistry.require<GameEngine>(GameEngine.Uri);
            initDisplay();
            initOpenGL();
        }

        private void initDisplay()
        {
            _display = CoreRegistry.putPermanently(DisplayDevice.Uri, new OpenTKDisplaySubsystem());
            _display.init();

            _display.setFullscreen(false, true);
            _display.setTitle("Atma.Asteroids | Pre Alpha");

            //String root = "org/terasology/icons/";
            //ClassLoader classLoader = getClass().getClassLoader();

            //BufferedImage icon16 = ImageIO.read(classLoader.getResourceAsStream(root + "gooey_sweet_16.png"));
            //BufferedImage icon32 = ImageIO.read(classLoader.getResourceAsStream(root + "gooey_sweet_32.png"));
            //BufferedImage icon64 = ImageIO.read(classLoader.getResourceAsStream(root + "gooey_sweet_64.png"));
            //BufferedImage icon128 = ImageIO.read(classLoader.getResourceAsStream(root + "gooey_sweet_128.png"));

            //Display.setIcon(new ByteBuffer[]{
            //        new ImageIOImageData().imageToByteBuffer(icon16, false, false, null),
            //        new ImageIOImageData().imageToByteBuffer(icon32, false, false, null),
            //        new ImageIOImageData().imageToByteBuffer(icon64, false, false, null),
            //        new ImageIOImageData().imageToByteBuffer(icon128, false, false, null)
            //});

            //Display.create(rc.getPixelFormat());
            //Display.setVSyncEnabled(rc.isVSync());
            _display.setVSync(true);
        }

        private void initOpenGL()
        {
            checkOpenGL();
            GL11.Viewport(0, 0, _display._window.Width, _display._window.Height);
            initOpenGLParams();

        }

        private void checkOpenGL()
        {
            var canRunGame = true;/*GLContext.getCapabilities().OpenGL11
                    & GLContext.getCapabilities().OpenGL12
                    & GLContext.getCapabilities().OpenGL14
                    & GLContext.getCapabilities().OpenGL15
                    & GLContext.getCapabilities().GL_ARB_framebuffer_object
                    & GLContext.getCapabilities().GL_ARB_texture_float
                    & GLContext.getCapabilities().GL_ARB_half_float_pixel
                    & GLContext.getCapabilities().GL_ARB_shader_objects*/
            ;

            if (!canRunGame)
            {
                String message = "Your GPU driver is not supporting the mandatory versions or extensions of OpenGL. Considered updating your GPU drivers? Exiting...";
                logger.error(message);
                return;
            }

            var assetManager = CoreRegistry.require<AssetManager>(AssetManager.Uri);
            assetManager.setFactory2<MeshData, IMesh>(AssetType.MESH, new AssetFactory2((uri, data) =>
            {
                return new OpenTKMesh(uri, (MeshData)data);
            }));

            ///var mesh = assetManager.generateAsset<Mesh, MeshData>("mesh:asdf:asdf", new MeshData());
        }


        public void initOpenGLParams()
        {
            GL11.Enable(TK.Graphics.ES11.EnableCap.CullFace);
            GL11.Enable(TK.Graphics.ES11.EnableCap.DepthTest);
            GL11.Enable(TK.Graphics.ES11.EnableCap.Normalize);
            GL11.DepthFunc(TK.Graphics.ES11.DepthFunction.Lequal);
        }

        public override void shutdown()
        {
            _display.shutdown();
        }
    }
}
