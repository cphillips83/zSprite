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
    public struct MouseInput : Input
    {
        #region MouseInputs
        
        public static readonly MouseInput NONE = new MouseInput(InputType.MOUSE_BUTTON, -1, "MOUSE_NONE", "");
        public static readonly MouseInput MOUSE_LEFT = new MouseInput(InputType.MOUSE_BUTTON, 0, "MOUSE_LEFT", "Left Click", "M_LEFT", "M_1");
        public static readonly MouseInput MOUSE_RIGHT = new MouseInput(InputType.MOUSE_BUTTON, 1, "MOUSE_RIGHT", "Right Click", "M_2");
        public static readonly MouseInput MOUSE_3 = new MouseInput(InputType.MOUSE_BUTTON, 2, "MOUSE_3", "Mouse 3", "M_3");
        public static readonly MouseInput MOUSE_4 = new MouseInput(InputType.MOUSE_BUTTON, 3, "MOUSE_4", "Mouse 4", "M_4");
        public static readonly MouseInput MOUSE_5 = new MouseInput(InputType.MOUSE_BUTTON, 4, "MOUSE_5", "Mouse 5", "M_5");
        public static readonly MouseInput WHEEL_UP = new MouseInput(InputType.MOUSE_WHEEL, 1, "MOUSE_WHEEL_UP", "Mouse Wheel Up", "MWHEEL_UP");
        public static readonly MouseInput WHEEL_DOWN = new MouseInput(InputType.MOUSE_WHEEL, -1, "MOUSE_WHEEL_DOWN", "Mouse Wheel Down", "MWHEEL_DOWN");
        
        #endregion MouseInputs

        #region Members

        private static Dictionary<String, MouseInput> lookup = new Dictionary<string, MouseInput>();

        private InputType type;
        private int id;
        private string displayName;
        private string name;
        private HashSet<string> identifiers;

        #endregion Members

        #region Constructor

        static MouseInput()
        {
            foreach (var type in values())
                foreach (var identifier in type.identifiers1())
                    lookup.Add(identifier, type);
        }

        public MouseInput(InputType type, int id, string name, string displayName, params string[] alternateStrings)
        {
            this.type = type;
            this.id = (int)id;
            this.name = name;
            this.displayName = displayName;
            this.identifiers = new HashSet<string>(alternateStrings);
        }

        #endregion Constructor

        #region Methods

        public InputType getType()
        {
            return type;
        }

        public IEnumerable<string> identifiers1()
        {
            foreach (var i in identifiers)
                yield return i;
        }

        public int getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }

        public string getDisplayName()
        {
            return displayName;
        }

        #endregion Methods

        #region Static Methods

        public static IEnumerable<MouseInput> values()
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

        public static MouseInput find(InputType type, int id)
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
            MouseInput input;
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

        #endregion Static Methods

    }
}
