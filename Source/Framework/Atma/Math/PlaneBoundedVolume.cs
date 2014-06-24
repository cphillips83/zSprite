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

using Atma.Collections;
namespace Atma
{
	/// <summary>
	///		Represents a convex volume bounded by planes.
	/// </summary>
	public class PlaneBoundedVolume
	{
		#region Fields

		/// <summary>
		///		Publicly accessible plane list, you can modify this direct.
		/// </summary>
		public PlaneList planes = new PlaneList();

		/// <summary>
		///		Side of the plane to be considered 'outside'.
		/// </summary>
		public PlaneSide outside;

		#endregion Fields

		#region Constructors

		/// <summary>
		///		Default constructor.
		/// </summary>
		public PlaneBoundedVolume()
			: this( PlaneSide.Negative ) {}

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="outside">Side of the plane to be considered 'outside'.</param>
		public PlaneBoundedVolume( PlaneSide outside )
		{
			this.outside = outside;
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		///		Intersection test with an <see cref="AxisAlignedBox2"/>.
		/// </summary>
		/// <remarks>
		///		May return false positives but will never miss an intersection.
		/// </remarks>
		/// <param name="box">Box to test.</param>
		/// <returns>True if interesecting, false otherwise.</returns>
		public bool Intersects( AxisAlignedBox3 box )
		{
			if( box.IsNull )
			{
				return false;
			}

			if( box.IsInfinite )
			{
				return true;
			}

			// Get centre of the box
			Vector3 center = box.Center;
			// Get the half-size of the box
			Vector3 halfSize = box.HalfSize;

			// If all points are on outside of any plane, we fail
			Vector3[] points = box.Corners;

			for( int i = 0; i < planes.Count; i++ )
			{
				Plane plane = (Plane)planes[ i ];

				PlaneSide side = plane.GetSide( center, halfSize );
				if( side == outside )
				{
					// Found a splitting plane therefore return not intersecting
					return false;
				}
			}

			// couldn't find a splitting plane, assume intersecting
			return true;
		}

		/// <summary>
		///		Intersection test with <see cref="Sphere"/>.
		/// </summary>
		/// <param name="sphere">Sphere to test.</param>
		/// <returns>True if the sphere intersects this volume, and false otherwise.</returns>
		public bool Intersects( Sphere sphere )
		{
			for( int i = 0; i < planes.Count; i++ )
			{
				Plane plane = (Plane)planes[ i ];

				// Test which side of the plane the sphere is
				Real d = plane.GetDistance( sphere.Center );

				// Negate d if planes point inwards
				if( outside == PlaneSide.Negative )
				{
					d = -d;
				}

				if( ( d - sphere.Radius ) > 0 )
				{
					return false;
				}
			}

			// assume intersecting
			return true;
		}

		#endregion Methods
	}
}
