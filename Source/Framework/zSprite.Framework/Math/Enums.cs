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
	///    Type of intersection detected between 2 object.
	/// </summary>
	public enum Intersection
	{
		/// <summary>
		///    The objects are not intersecting.
		/// </summary>
		None,

		/// <summary>
		///    An object is fully contained within another object.
		/// </summary>
		Contained,

		/// <summary>
		///    An object fully contains another object.
		/// </summary>
		Contains,

		/// <summary>
		///    The objects are partially intersecting each other.
		/// </summary>
		Partial
	}

	/// <summary>
	/// The "positive side" of the plane is the half space to which the
	/// plane normal points. The "negative side" is the other half
	/// space. The flag "no side" indicates the plane itself.
	/// </summary>
	public enum PlaneSide
	{
		None,
		Positive,
		Negative,
		Both
	}
}
