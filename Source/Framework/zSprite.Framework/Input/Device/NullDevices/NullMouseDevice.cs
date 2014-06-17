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

using System.Collections.Generic;

#endregion Namespace Declarations

namespace zSprite
{
    public class NullMouseDevice : MouseDevice
    {
        public override Vector2i getPosition()
        {
            return new Vector2i();
        }

        public override Vector2i getDelta()
        {
            return new Vector2i();
        }

        public override bool isButtonDown(int button)
        {
            return false;
        }

        public override bool isVisible()
        {
            return false;
        }

        public override Queue<InputAction> getInputQueue()
        {
            return new Queue<InputAction>();
        }
    }
}
