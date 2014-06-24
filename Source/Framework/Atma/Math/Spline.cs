#region GPLv3 License

/*
Atma
Copyright � 2014 Atma Project Team

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
using Atma.Utilities;
using System.Collections.Generic;
#endregion Namespace Declarations

namespace Atma
{
	abstract public class Spline<T>
	{
		#region Fields and Properties

		protected readonly Matrix4 hermitePoly = new Matrix4(
			2, -2, 1, 1,
			-3, 3, -2, -1,
			0, 0, 1, 0,
			1, 0, 0, 0 );

		/// <summary>Collection of control points.</summary>
		protected List<T> pointList;

		/// <summary>Collection of generated tangents for the spline controls points.</summary>
		protected List<T> tangentList;

		/// <summary>Specifies whether or not to recalculate tangents as each control point is added.</summary>
		protected bool autoCalculateTangents;

		/// <summary>
		///		Specifies whether or not to recalculate tangents as each control point is added.
		/// </summary>
		public bool AutoCalculate { get { return autoCalculateTangents; } set { autoCalculateTangents = value; } }

		/// <summary>
		///    Gets the number of control points in this spline.
		/// </summary>
		public int PointCount { get { return pointList.Count; } }

		#endregion Fields and Properties

		#region Construction and Destruction

		public Spline()
		{
			// intialize the vector collections
			pointList = new List<T>();
			tangentList = new List<T>();

			// do not auto calculate tangents by default
			autoCalculateTangents = false;
		}

		#endregion Construction and Destruction

		#region Methods

		/// <summary>
		///    Adds a new control point to the end of this spline.
		/// </summary>
		/// <param name="point"></param>
		public void AddPoint( T point )
		{
			pointList.Add( point );

			// recalc tangents if necessary
			if( autoCalculateTangents )
			{
				RecalculateTangents();
			}
		}

		/// <summary>
		///    Removes all current control points from this spline.
		/// </summary>
		public void Clear()
		{
			pointList.Clear();
			tangentList.Clear();
		}

		/// <summary>
		///     Returns the point at the specified index.
		/// </summary>
		/// <param name="index">Index at which to retrieve a point.</param>
		/// <returns>Vector3 containing the point data.</returns>
		public T GetPoint( int index )
		{
			Contract.Requires( index < pointList.Count );

			return pointList[ index ];
		}

		/// <summary>
		///		Recalculates the tangents associated with this spline. 
		/// </summary>
		/// <remarks>
		///		If you tell the spline not to update on demand by setting AutoCalculate to false,
		///		then you must call this after completing your updates to the spline points.
		/// </remarks>
		abstract public void RecalculateTangents();

		/// <summary>
		///		Returns an interpolated point based on a parametric value over the whole series.
		/// </summary>
		/// <remarks>
		///		Given a t value between 0 and 1 representing the parametric distance along the
		///		whole length of the spline, this method returns an interpolated point.
		/// </remarks>
		/// <param name="t">Parametric value.</param>
		/// <returns>An interpolated point along the spline.</returns>
		abstract public T Interpolate( Real t );

		/// <summary>
		///		Interpolates a single segment of the spline given a parametric value.
		/// </summary>
		/// <param name="index">The point index to treat as t=0. index + 1 is deemed to be t=1</param>
		/// <param name="t">Parametric value</param>
		/// <returns>An interpolated point along the spline.</returns>
		abstract public T Interpolate( int index, Real t );

		#endregion Methods
	}
}
