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

#endregion Namespace Declarations

namespace zSprite
{
    public sealed class InputAction
    {
        private readonly Input input;
        private readonly ButtonState state;
        private readonly int delta;
        private readonly char inputChar;
        private readonly Vector2i mousePosition;

        public InputAction(Input input, ButtonState state, Vector2i mousePosition)
        {
            this.mousePosition = mousePosition;
            this.input = input;
            this.state = state;
            this.delta = 0;
            this.inputChar = '\0';
        }

        public InputAction(Input input, int delta, Vector2i mousePosition)
        {
            this.mousePosition = mousePosition;
            this.input = input;
            this.state = ButtonState.DOWN;
            this.delta = delta;
            this.inputChar = '\0';
        }

        public InputAction(Input input, ButtonState state, char inputChar)
        {
            this.mousePosition = Vector2i.Zero;
            this.input = input;
            this.state = state;
            this.delta = 0;
            this.inputChar = inputChar;
        }

        public InputAction(Input input, int delta, char inputChar)
        {
            this.mousePosition = Vector2i.Zero;
            this.input = input;
            this.state = ButtonState.DOWN;
            this.delta = delta;
            this.inputChar = inputChar;
        }

        /// <returns>Whether this is an axis action (e.g. a mouse wheel or volume knob)</returns>
        public bool isAxisAction()
        {
            return delta != 0;
        }

        /// <returns>The type of input involved in this action (mouse button/mouse wheel)</returns>
        public Input getInput()
        {
            return input;
        }

        /// <returns>The state of that input button</returns>
        public ButtonState getState()
        {
            return state;
        }

        /// <returns>For axis actions, the change in value</returns>
        public int getTurns()
        {
            return delta;
        }

        public char getInputChar()
        {
            return inputChar;
        }

        public Vector2i getMousePosition()
        {
            return mousePosition;
        }

        public override string ToString()
        {
            return "InputAction [" + this.input + " \'" + inputChar + "' (" + state + "), mouse: " + mousePosition + "]";
        }
    }
}
