#region GPLv3 License

/*
zSprite
Copyright © 2014 zSprite Project Team

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License V3
as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License V3 for more details.

You should have received a copy of the GNU General Public License V3
along with this library; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

#endregion

#region Namespace Declarations
using System;
using System.Collections.Generic;
#endregion Namespace Declarations

namespace zSprite
{
    public class MouseInput
    {
        #region MouseButtons
        
        public static readonly MouseButton NONE = new MouseButton(InputType.MOUSE_BUTTON, -1, "MOUSE_NONE", "");
        public static readonly MouseButton MOUSE_LEFT = new MouseButton(InputType.MOUSE_BUTTON, 0, "MOUSE_LEFT", "Left Click", "M_LEFT", "M_1");
        public static readonly MouseButton MOUSE_RIGHT = new MouseButton(InputType.MOUSE_BUTTON, 1, "MOUSE_RIGHT", "Right Click", "M_2");
        public static readonly MouseButton MOUSE_3 = new MouseButton(InputType.MOUSE_BUTTON, 2, "MOUSE_3", "Mouse 3", "M_3");
        public static readonly MouseButton MOUSE_4 = new MouseButton(InputType.MOUSE_BUTTON, 3, "MOUSE_4", "Mouse 4", "M_4");
        public static readonly MouseButton MOUSE_5 = new MouseButton(InputType.MOUSE_BUTTON, 4, "MOUSE_5", "Mouse 5", "M_5");
        public static readonly MouseButton WHEEL_UP = new MouseButton(InputType.MOUSE_WHEEL, 1, "MOUSE_WHEEL_UP", "Mouse Wheel Up", "MWHEEL_UP");
        public static readonly MouseButton WHEEL_DOWN = new MouseButton(InputType.MOUSE_WHEEL, -1, "MOUSE_WHEEL_DOWN", "Mouse Wheel Down", "MWHEEL_DOWN");
        
        #endregion MouseButtons

        #region Members

        private static Dictionary<String, MouseButton> lookup = new Dictionary<string, MouseButton>();

        #endregion Members

        #region Constructor

        static MouseInput()
        {
            foreach (var type in values())
                foreach (var identifier in type.identifiers1())
                    lookup.Add(identifier, type);
        }

        #endregion Constructor

        #region Methods

        public static IEnumerable<MouseButton> values()
        {
            yield return NONE;
            yield return MOUSE_LEFT;
            yield return MOUSE_RIGHT;
            yield return MOUSE_3;
            yield return MOUSE_4;
            yield return MOUSE_5;
            yield return WHEEL_UP;
            yield return WHEEL_DOWN;
        }

        public static MouseButton find(InputType type, int id)
        {
            foreach (var input in values())
            {
                if (input.getType() == type && input.getId() == id)
                {
                    return input;
                }
            }
            return NONE;
        }

        public static Input find(String name)
        {
            MouseButton input;
            if (lookup.TryGetValue(name.ToUpperInvariant(), out input))
                return input;

            return null;
        }

        //public static Vector2 getPosition()
        //{
        //    //return CoreRegistry.get(InputSystem.class).getMouseDevice().getPosition();
        //}

        //public static bool getButtonState(int button)
        //{
        //    //return CoreRegistry.get(InputSystem.class).getMouseDevice().isButtonDown(button);
        //}

        //public static bool isVisible()
        //{
        //    //return CoreRegistry.get(InputSystem.class).getMouseDevice().isVisible();
        //}

        #endregion Methods
    }
}
