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
    /// <summary>
    /// 	Representation of a ray in space, ie a line with an origin and direction.
    /// </summary>
    public class Ray3
    {
        #region Fields

        internal Vector3 origin;
        internal Vector3 direction;

        #endregion

        #region Constructors

        /// <summary>
        ///    Default constructor.
        /// </summary>
        public Ray3()
        {
            origin = Vector3.Zero;
            direction = Vector3.UnitZ;
        }

        /// <summary>
        ///    Constructor.
        /// </summary>
        /// <param name="origin">Starting point of the ray.</param>
        /// <param name="direction">Direction the ray is pointing.</param>
        public Ray3(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the position of a point t units along the ray.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3 GetPoint(Real t)
        {
            return origin + (direction * t);
        }

        /// <summary>
        /// Gets the position of a point t units along the ray.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3 this[Real t] { get { return origin + (direction * t); } }

        #endregion Methods

        #region Intersection Methods

        /// <summary>
        ///    Tests whether this ray intersects the given box.
        /// </summary>
        /// <param name="box"></param>
        /// <returns>
        ///		Struct containing info on whether there was a hit, and the distance from the 
        ///		origin of this ray where the intersect happened.
        ///	</returns>
        public IntersectResult Intersects(AxisAlignedBox3 box)
        {
            return Utility.Intersects(this, box);
        }

        /// <summary>
        ///		Tests whether this ray intersects the given plane. 
        /// </summary>
        /// <param name="plane"></param>
        /// <returns>
        ///		Struct containing info on whether there was a hit, and the distance from the 
        ///		origin of this ray where the intersect happened.
        ///	</returns>
        public IntersectResult Intersects(Plane plane)
        {
            return Utility.Intersects(this, plane);
        }

        /// <summary>
        ///		Tests whether this ray intersects the given sphere. 
        /// </summary>
        /// <param name="sphere"></param>
        /// <returns>
        ///		Struct containing info on whether there was a hit, and the distance from the 
        ///		origin of this ray where the intersect happened.
        ///	</returns>
        public IntersectResult Intersects(Sphere sphere)
        {
            return Utility.Intersects(this, sphere);
        }

        /// <summary>
        ///		Tests whether this ray intersects the given PlaneBoundedVolume. 
        /// </summary>
        /// <param name="volume"></param>
        /// <returns>
        ///		Struct containing info on whether there was a hit, and the distance from the 
        ///		origin of this ray where the intersect happened.
        ///	</returns>
        public IntersectResult Intersects(PlaneBoundedVolume volume)
        {
            return Utility.Intersects(this, volume);
        }

        #endregion Intersection Methods

        #region Operator Overloads

        /// <summary>
        /// Gets the position of a point t units along the ray.
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 operator *(Ray3 ray, Real t)
        {
            return ray.origin + (ray.direction * t);
        }

        public static bool operator ==(Ray3 left, Ray3 right)
        {
            return left.direction == right.direction && left.origin == right.origin;
        }

        public static bool operator !=(Ray3 left, Ray3 right)
        {
            return left.direction != right.direction || left.origin != right.origin;
        }

        public override bool Equals(object obj)
        {
            return obj is Ray3 && this == (Ray3)obj;
        }

        public override int GetHashCode()
        {
            return direction.GetHashCode() ^ origin.GetHashCode();
        }

        #endregion Operator Overloads

        #region Properties

        /// <summary>
        ///    Gets/Sets the origin of the ray.
        /// </summary>
        public Vector3 Origin { get { return origin; } set { origin = value; } }

        /// <summary>
        ///    Gets/Sets the direction this ray is pointing.
        /// </summary>
        /// <remarks>
        ///    A ray has no length, so the direction goes to infinity.
        /// </remarks>
        public Vector3 Direction { get { return direction; } set { direction = value; } }

        #endregion
    }
}
