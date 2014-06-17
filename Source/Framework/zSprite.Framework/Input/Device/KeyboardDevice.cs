using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zSprite
{
    public interface KeyboardDevice : InputDevice
    {
        /// <returns>The current state of the given key</returns>
        bool isKeyDown(int key);
    }
}
