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
    /// <summary>
    ///		A standard Circle, used mostly for bounds checking.
    /// </summary>
    /// <remarks>
    ///		A Circle in math texts is normally represented by the function
    ///		x^2 + y^2 + z^2 = r^2 (for Circle's centered on the origin). We store spheres
    ///		simply as a center point and a radius.
    /// </remarks>
    public sealed class Circle
    {
        #region Protected member variables

        private float radius;
        private Vector2 center;

        #endregion

        #region Constructors

        /// <summary>
        ///		Creates a unit Circle centered at the origin.
        /// </summary>
        public Circle()
        {
            radius = 1.0f;
            center = Vector2.Zero;
        }

        /// <summary>
        /// Creates an arbitrary spehere.
        /// </summary>
        /// <param name="center">Center point of the Circle.</param>
        /// <param name="radius">Radius of the Circle.</param>
        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        #endregion

        #region Properties

        /// <summary>
        ///		Gets/Sets the center of the Circle.
        /// </summary>
        public Vector2 Center { get { return center; } set { center = value; } }

        /// <summary>
        ///		Gets/Sets the radius of the Circle.
        /// </summary>
        public float Radius { get { return radius; } set { radius = value; } }

        #endregion

        #region Intersection methods

        public static bool operator ==(Circle sphere1, Circle sphere2)
        {
            return sphere1.center == sphere2.center && sphere1.radius == sphere2.radius;
        }

        public static bool operator !=(Circle sphere1, Circle sphere2)
        {
            return sphere1.center != sphere2.center || sphere1.radius != sphere2.radius;
        }

        public override bool Equals(object obj)
        {
            return obj is Circle && this == (Circle)obj;
        }

        public override int GetHashCode()
        {
            return center.GetHashCode() ^ radius.GetHashCode();
        }

        public bool Intersects(LineSegment line)
        {
            var radiusSq = radius * radius;
            var cp = line.closest(center);
            return (cp - center).LengthSquared <= radiusSq;
        }

        /// <summary>
        ///		Tests for intersection between this Circle and another Circle.
        /// </summary>
        /// <param name="Circle">Other Circle.</param>
        /// <returns>True if the spheres intersect, false otherwise.</returns>
        public bool Intersects(Circle Circle)
        {
            return ((Circle.center - center).Length <= (Circle.radius + radius));
        }

        /// <summary>
        ///		Returns whether or not this Circle interects a box.
        /// </summary>
        /// <param name="box"></param>
        /// <returns>True if the box intersects, false otherwise.</returns>
        public bool Intersects(AxisAlignedBox2 box)
        {
            return Utility.Intersects(this, box);
        }

        ///// <summary>
        /////		Returns whether or not this Circle interects a plane.
        ///// </summary>
        ///// <param name="plane"></param>
        ///// <returns>True if the plane intersects, false otherwise.</returns>
        //public bool Intersects(Plane plane)
        //{
        //    return Utility.Intersects(this, plane);
        //}

        /// <summary>
        ///		Returns whether or not this Circle interects a Vector2.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>True if the vector intersects, false otherwise.</returns>
        public bool Intersects(Vector2 vector)
        {
            return (vector - center).Length <= radius;
        }

        #endregion Intersection methods
    }
}