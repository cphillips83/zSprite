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
	///		A standard sphere, used mostly for bounds checking.
	/// </summary>
	/// <remarks>
	///		A sphere in math texts is normally represented by the function
	///		x^2 + y^2 + z^2 = r^2 (for sphere's centered on the origin). We store spheres
	///		simply as a center point and a radius.
	/// </remarks>
	public sealed class Sphere
	{
		#region Protected member variables

		private Real radius;
		private Vector3 center;

		#endregion

		#region Constructors

		/// <summary>
		///		Creates a unit sphere centered at the origin.
		/// </summary>
		public Sphere()
		{
			radius = 1.0f;
			center = Vector3.Zero;
		}

		/// <summary>
		/// Creates an arbitrary spehere.
		/// </summary>
		/// <param name="center">Center point of the sphere.</param>
		/// <param name="radius">Radius of the sphere.</param>
		public Sphere( Vector3 center, Real radius )
		{
			this.center = center;
			this.radius = radius;
		}

		#endregion

		#region Properties

		/// <summary>
		///		Gets/Sets the center of the sphere.
		/// </summary>
		public Vector3 Center { get { return center; } set { center = value; } }

		/// <summary>
		///		Gets/Sets the radius of the sphere.
		/// </summary>
		public Real Radius { get { return radius; } set { radius = value; } }

		#endregion

		#region Intersection methods

		public static bool operator ==( Sphere sphere1, Sphere sphere2 )
		{
			return sphere1.center == sphere2.center && sphere1.radius == sphere2.radius;
		}

		public static bool operator !=( Sphere sphere1, Sphere sphere2 )
		{
			return sphere1.center != sphere2.center || sphere1.radius != sphere2.radius;
		}

		public override bool Equals( object obj )
		{
			return obj is Sphere && this == (Sphere)obj;
		}

		public override int GetHashCode()
		{
			return center.GetHashCode() ^ radius.GetHashCode();
		}

		/// <summary>
		///		Tests for intersection between this sphere and another sphere.
		/// </summary>
		/// <param name="sphere">Other sphere.</param>
		/// <returns>True if the spheres intersect, false otherwise.</returns>
		public bool Intersects( Sphere sphere )
		{
			return ( ( sphere.center - center ).Length <= ( sphere.radius + radius ) );
		}

		/// <summary>
		///		Returns whether or not this sphere interects a box.
		/// </summary>
		/// <param name="box"></param>
		/// <returns>True if the box intersects, false otherwise.</returns>
		public bool Intersects( AxisAlignedBox3 box )
		{
			return Utility.Intersects( this, box );
		}

		/// <summary>
		///		Returns whether or not this sphere interects a plane.
		/// </summary>
		/// <param name="plane"></param>
		/// <returns>True if the plane intersects, false otherwise.</returns>
		public bool Intersects( Plane plane )
		{
			return Utility.Intersects( this, plane );
		}

		/// <summary>
		///		Returns whether or not this sphere interects a Vector3.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns>True if the vector intersects, false otherwise.</returns>
		public bool Intersects( Vector3 vector )
		{
			return ( vector - center ).Length <= radius;
		}

		#endregion Intersection methods
	}
}
