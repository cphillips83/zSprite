using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TK = OpenTK;
using GL11 = OpenTK.Graphics.ES11.GL;
using Atma.Engine;

namespace Atma.Asteroids.Engine.Subsystems.OpenTK
{
    public class OpenTKDisplaySubsystem : DisplayDevice
    {
        internal TK.GameWindow _window;

        public override void init()
        {
            base.init();

            _window = new TK.GameWindow();
            _window.Visible = true;

            _window.Closing += _window_Closing;
        }

        public override bool closeRequest { get; protected set; }

        void _window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            closeRequest = true;
        }

        public override void processmMessage()
        {
            _window.ProcessEvents();
        }

        public override void swap()
        {
            _window.SwapBuffers();
        }

        public override void setFullscreen(bool state, bool resizable)
        {
            var display = TK.DisplayDevice.Default;

            display.ChangeResolution(display.Width, display.Height, display.BitsPerPixel, display.RefreshRate);
            if (state)
            {
                _window.WindowState = TK.WindowState.Fullscreen;
                _window.WindowBorder = TK.WindowBorder.Hidden;
            }
            else
            {
                _window.WindowState = TK.WindowState.Normal;
                if (resizable)
                    _window.WindowBorder = TK.WindowBorder.Resizable;
                else
                    _window.WindowBorder = TK.WindowBorder.Fixed;
            }

            GL11.Viewport(0, 0, _window.Width, _window.Height);
        }

        public override void setTitle(string title)
        {
            _window.Title = title;
        }

        public override void setVSync(bool vsync)
        {
            if (vsync)
                _window.VSync = TK.VSyncMode.On;
            else
                _window.VSync = TK.VSyncMode.Off;
        }

        public override void shutdown()
        {
            base.shutdown();

            _window.Close();
            _window.Dispose();
        }
    }
}
