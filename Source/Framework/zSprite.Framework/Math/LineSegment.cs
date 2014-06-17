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
    public struct LineSegment
    {
        public Vector2 p0;
        public Vector2 p1;

        public LineSegment(Vector2 p0, Vector2 p1)
        {
            this.p0 = p0;
            this.p1 = p1;
        }

        public Vector2 closest(Vector2 p)
        {
            var l2 = (p0 - p1).LengthSquared;
            if (l2 == 0)
                return p0;

            var t = Vector2.Dot(p - p0, p1 - p0) / l2;
            if (t < 0)
                return p0;
            else if (t > 1)
                return p1;

            return p0 + t * (p1 - p0);
        }


        public float distance(Vector2 p)
        {
            var l2 = (p0 - p1).LengthSquared;
            if (l2 == 0)
                return (p0 - p).Length;

            var t = Vector2.Dot(p - p0, p1 - p0) / l2;
            if (t < 0)
                return (p0 - p).Length;
            else if (t > 1)
                return (p1 - p).Length;

            var projection = p0 + t * (p1 - p0);
            return (p - projection).Length;
        }

        public float distanceSquared(Vector2 p)
        {
            var l2 = (p0 - p1).LengthSquared;
            if (l2 == 0)
                return (p0 - p).LengthSquared;

            var t = Vector2.Dot(p - p0, p1 - p0) / l2;
            if (t < 0)
                return (p0 - p).LengthSquared;
            else if (t > 1)
                return (p1 - p).LengthSquared;

            var projection = p0 + t * (p1 - p0);
            return (p - projection).LengthSquared;
        }
    }

}