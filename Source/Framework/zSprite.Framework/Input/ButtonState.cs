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
    public struct ButtonState
    {
        public static readonly ButtonState DOWN = new ButtonState(true);
        public static readonly ButtonState UP = new ButtonState(false);
        public static readonly ButtonState REPEAT = new ButtonState(true);

        private bool down;

        private ButtonState(bool down)
        {
            this.down = down;
        }

        public bool isDown()
        {
            return down;
        }
    }

}
