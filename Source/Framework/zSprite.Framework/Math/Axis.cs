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
    public struct Axis
    {
        public Vector2 normal;
        public Vector2 unit;
        public Vector2 edge;

        public readonly static Axis Zero = new Axis(Vector2.Zero, Vector2.Zero, Vector2.Zero);

        public Axis(Vector2 n, Vector2 u, Vector2 e)
        {
            this.normal = n;
            this.unit = u;
            this.edge = e;
        }

        public override string ToString()
        {
            return string.Format("N:{0}, U:{1}, E:{2}", normal, unit, edge);
        }
    }
}
