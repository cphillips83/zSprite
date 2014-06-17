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
    public struct Projection
    {
        public double min;
        public double max;
        public double center;

        public Projection(double min, double max)
        {
            this.min = min;
            this.max = max;
            this.center = (min + max) / 2;
        }

        public bool overlap(Projection other)
        {
            return (other.min >= min && other.min <= max) || (other.max >= min && other.max <= max) ||
                (min >= other.min && min <= other.max) || (max >= other.min && max <= other.max);
        }

        public double getOverlap(Projection other)
        {
            if (min == other.min && max == other.max)
                return max - min;
            else if (min < other.min && max > other.max) //contains other
                return other.max - other.min;
            else if (other.min < min && other.max > max)
                return max - min;
            else if (min < other.min)
                return max - other.min;

            return other.max - min;
        }

        public override string ToString()
        {
            return string.Format("min: {0}, max: {1}", min, max);
        }
    }
}
