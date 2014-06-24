using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atma.Asteroids.Engine
{
    public interface IDisplayDevice
    {
        bool hasFocus { get; }
        bool setFullscreen(bool fullscreen);
        bool isHeadless { get; }

        void startRender();
        void endRender();
        
    }
}
