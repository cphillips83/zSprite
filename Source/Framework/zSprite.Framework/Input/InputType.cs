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


    /// <summary>
    /// The type of an input
    /// </summary>
    public abstract class InputType
    {
        //MOUSE_BUTTON {
        //    @Override
        //    public Input getInput(int id) {
        //        return MouseInput.find(this, id);
        //    }

        //    @Override
        //    public Input getInput(String name) {
        //        return MouseInput.find(name);
        //    }
        //},
        //MOUSE_WHEEL {
        //    @Override
        //    public Input getInput(int id) {
        //        return MouseInput.find(this, id);
        //    }

        //    @Override
        //    public Input getInput(String name) {
        //        return MouseInput.find(name);
        //    }
        //},
       
        //};

        #region Input Types

        protected class NoneInputType : InputType
        {
            private UnknownInput noneNone;

            public NoneInputType()
            {
                noneNone = new UnknownInput(this, 0);
            }

            public override Input getInput(int id)
            {
                return noneNone;
            }

            public override Input getInput(string name)
            {
                return null;
            }
        }

        protected class KeyInputType : InputType
        {
            public override Input getInput(int id)
            {
                return Keyboard.find(id);
            }

            public override Input getInput(string name)
            {
                return Keyboard.find(name);
            }
        }

        protected class MouseInputType : InputType
        {
            public override Input getInput(int id)
            {
                return MouseInput.find(this, id);
            }

            public override Input getInput(string name)
            {
                return MouseInput.find(name);
            }

        }
        //MOUSE_BUTTON {
        //    @Override
        //    public Input getInput(int id) {
        //        return MouseInput.find(this, id);
        //    }

        //    @Override
        //    public Input getInput(String name) {
        //        return MouseInput.find(name);
        //    }
        //},

        protected class ControllerInputType : InputType
        {
            public override Input getInput(int id)
            {
                return null;
            }

            public override Input getInput(string name)
            {
                return null;
            }
        }

        #endregion Input Types

        #region Members

        public static readonly InputType NONE = new NoneInputType();
        public static readonly InputType KEY = new KeyInputType();
        public static readonly InputType MOUSE_BUTTON = new MouseInputType();
        public static readonly InputType MOUSE_WHEEL = new MouseInputType();
        public static readonly InputType CONTROLLER_1 = new ControllerInputType();
        public static readonly InputType CONTROLLER_2 = new ControllerInputType();
        public static readonly InputType CONTROLLER_3 = new ControllerInputType();
        public static readonly InputType CONTROLLER_4 = new ControllerInputType();

        #endregion Members

        #region Constructors

        protected InputType()
        {

        }

        #endregion Constructors

        #region Methods

        public abstract Input getInput(int id);
        public abstract Input getInput(String name);

        public static Input parse(string inputName)
        {
            foreach (var type in values())
            {
                var result = type.getInput(inputName);
                if (result != null)
                    return result;
            }

            return UnknownInput.tryParse(inputName);
        }

        public static IEnumerable<InputType> values()
        {
            yield return NONE;
            yield return KEY;
            yield return MOUSE_BUTTON;
            yield return MOUSE_WHEEL;
            yield return CONTROLLER_1;
            yield return CONTROLLER_2;
            yield return CONTROLLER_3;
            yield return CONTROLLER_4;
        }

        #endregion Methods
    }
}
