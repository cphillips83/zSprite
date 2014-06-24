#region GPLv3 License

/*
Atma
Copyright © 2014 Atma Project Team

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

namespace Atma
{
    public struct RectOffset
    {
        //public Vector2 offset;
        public Vector2 min;
        public Vector2 max;

        public RectOffset(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public RectOffset(float left, float right, float top, float bottom)
        {
            this.min = new Vector2(left, top);
            this.max = new Vector2(right, bottom);
        }

        public float left { get { return min.X; } set { min.X = value; } }
        public float right { get { return max.X; } set { max.X = value; } }
        public float top { get { return min.Y; } set { min.Y = value; } }
        public float bottom { get { return max.Y; } set { max.Y = value; } }

        public float horizontal { get { return left + right; } }
        public float vertical { get { return top + bottom; } }

        public Vector2 size { get { return new Vector2(horizontal, vertical); } }
        //public Vector2 right { get { return new Vector2(max.X, 0); } }

        //public Vector2 top { get { return new Vector2(0, min.Y); } }
        //public Vector2 bottom { get { return new Vector2(0, max.Y); } }

        //public Vector2 sizeX { get { return new Vector2(min.X + max.X, 0); } }
        //public Vector2 sizeY { get { return new Vector2(0, min.Y + max.Y); } }
        //public float xWidth { get { return left.X + right.X; } }
        //public float yHeight { get { return top.Y + bottom.Y; } }

        //public Vector2 size { get { return new Vector2(xWidth, yHeight); } }

        //public AxisAlignedBox topLeft { get { return AxisAlignedBox.FromRect(Vector2.Zero + offset, min); } }
        //public AxisAlignedBox left { get { return AxisAlignedBox.FromRect(new Vector2(0, min.Y) Vector2.Zero + offset, min); } }
        //public AxisAlignedBox add(AxisAlignedBox box)
        //{
        //    return new AxisAlignedBox(box.minVector , box.maxVector + min + max);
        //}
    }
}

