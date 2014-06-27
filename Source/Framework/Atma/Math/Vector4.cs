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
using System.Diagnostics;
using System.Runtime.InteropServices;
#endregion Namespace Declarations

namespace Atma
{
	/// <summary>
	/// 4D homogeneous vector.
	/// </summary>
	[StructLayout( LayoutKind.Sequential )]
	public struct Vector4
	{
		#region Member variables

		public Real X, Y, Z, W;

		private static readonly Vector4 zeroVector = new Vector4( 0.0f, 0.0f, 0.0f, 0.0f );

		#endregion

		#region Constructors

		/// <summary>
		///		Creates a new 4 dimensional Vector.
		/// </summary>
		public Vector4( Real x, Real y, Real z, Real w )
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		#endregion

		#region Properties

		/// <summary>
		///		Gets a Vector4 with all components set to 0.
		/// </summary>
		public static Vector4 Zero { get { return zeroVector; } }

		#endregion Properties

		#region Methods

		/// <summary>
		///     Calculates the dot (scalar) product of this vector with another.
		/// </summary>
		/// <param name="vec">
		///     Vector with which to calculate the dot product (together with this one).
		/// </param>
		/// <returns>A Real representing the dot product value.</returns>
		public Real Dot( Vector4 vec )
		{
			return X * vec.X + Y * vec.Y + Z * vec.Z + W * vec.W;
		}

		#endregion Methods

		#region Operator overloads + CLS compliant method equivalents

		/// <summary>
		///		
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static Vector4 Multiply( Vector4 vector, Matrix4 matrix )
		{
			return vector * matrix;
		}

		/// <summary>
		///		
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static Vector4 operator *( Matrix4 matrix, Vector4 vector )
		{
			Vector4 result = new Vector4();

			result.X = vector.X * matrix.m00 + vector.Y * matrix.m01 + vector.Z * matrix.m02 + vector.W * matrix.m03;
			result.Y = vector.X * matrix.m10 + vector.Y * matrix.m11 + vector.Z * matrix.m12 + vector.W * matrix.m13;
			result.Z = vector.X * matrix.m20 + vector.Y * matrix.m21 + vector.Z * matrix.m22 + vector.W * matrix.m23;
			result.W = vector.X * matrix.m30 + vector.Y * matrix.m31 + vector.Z * matrix.m32 + vector.W * matrix.m33;

			return result;
		}

		// TODO: Find the signifance of having 2 overloads with opposite param lists that do transposed operations
		public static Vector4 operator *( Vector4 vector, Matrix4 matrix )
		{
			Vector4 result = new Vector4();

			result.X = vector.X * matrix.m00 + vector.Y * matrix.m10 + vector.Z * matrix.m20 + vector.W * matrix.m30;
			result.Y = vector.X * matrix.m01 + vector.Y * matrix.m11 + vector.Z * matrix.m21 + vector.W * matrix.m31;
			result.Z = vector.X * matrix.m02 + vector.Y * matrix.m12 + vector.Z * matrix.m22 + vector.W * matrix.m32;
			result.W = vector.X * matrix.m03 + vector.Y * matrix.m13 + vector.Z * matrix.m23 + vector.W * matrix.m33;

			return result;
		}

		/// <summary>
		///		Multiplies a Vector4 by a scalar value.
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="scalar"></param>
		/// <returns></returns>
		public static Vector4 operator *( Vector4 vector, Real scalar )
		{
			Vector4 result = new Vector4();

			result.X = vector.X * scalar;
			result.Y = vector.Y * scalar;
			result.Z = vector.Z * scalar;
			result.W = vector.W * scalar;

			return result;
		}

		/// <summary>
		///		User to compare two Vector4 instances for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns>true or false</returns>
		public static bool operator ==( Vector4 left, Vector4 right )
		{
			return ( left.X == right.X &&
			         left.Y == right.Y &&
			         left.Z == right.Z &&
			         left.W == right.W );
		}

		/// <summary>
		///		Used to add a Vector4 to another Vector4.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Vector4 operator +( Vector4 left, Vector4 right )
		{
			return new Vector4( left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W );
		}

		/// <summary>
		///		Used to subtract a Vector4 from another Vector4.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Vector4 operator -( Vector4 left, Vector4 right )
		{
			return new Vector4( left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W );
		}

		/// <summary>
		///		Used to negate the elements of a vector.
		/// </summary>
		/// <param name="left"></param>
		/// <returns></returns>
		public static Vector4 operator -( Vector4 left )
		{
			return new Vector4( -left.X, -left.Y, -left.Z, -left.W );
		}

		/// <summary>
		///		User to compare two Vector4 instances for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns>true or false</returns>
		public static bool operator !=( Vector4 left, Vector4 right )
		{
			return ( left.X != right.X ||
			         left.Y != right.Y ||
			         left.Z != right.Z ||
			         left.W != right.W );
		}

        public static implicit operator Vector4(Color c)
        {
            return new Vector4(c.r, c.g, c.b, c.a);
        }

        public static implicit operator Color(Vector4 v)
        {
            return new Color(v.X, v.Y, v.Z, v.W);
        }

		/// <summary>
		///		Used to access a Vector by index 0 = this.x, 1 = this.y, 2 = this.z, 3 = this.w.  
		/// </summary>
		/// <remarks>
		///		Uses unsafe pointer arithmetic to reduce the code required.
		///	</remarks>
		public Real this[ int index ]
		{
			get
			{
				Debug.Assert( index >= 0 && index < 4, "Indexer boundaries overrun in Vector4." );

				// using pointer arithmetic here for less code.  Otherwise, we'd have a big switch statement.
				unsafe
				{
					fixed( Real* pX = &X )
					{
						return *( pX + index );
					}
				}
			}
			set
			{
				Debug.Assert( index >= 0 && index < 4, "Indexer boundaries overrun in Vector4." );

				// using pointer arithmetic here for less code.  Otherwise, we'd have a big switch statement.
				unsafe
				{
					fixed( Real* pX = &X )
					{
						*( pX + index ) = value;
					}
				}
			}
		}

		#endregion

		#region Object overloads

		/// <summary>
		///		Overrides the Object.ToString() method to provide a text representation of 
		///		a Vector4.
		/// </summary>
		/// <returns>A string representation of a Vector4.</returns>
		public override string ToString()
		{
			return string.Format( "<{0},{1},{2},{3}>", this.X, this.Y, this.Z, this.W );
		}

		/// <summary>
		///		Provides a unique hash code based on the member variables of this
		///		class.  This should be done because the equality operators (==, !=)
		///		have been overriden by this class.
		///		<p/>
		///		The standard implementation is a simple XOR operation between all local
		///		member variables.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}

		/// <summary>
		///		Compares this Vector to another object.  This should be done because the 
		///		equality operators (==, !=) have been overriden by this class.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return obj is Vector4 && this == (Vector4)obj;
		}

		#endregion

		#region Parse method, implemented for factories

		/// <summary>
		///		Parses a string and returns Vector4.
		/// </summary>
		/// <param name="vector">
		///     A string representation of a Vector4. ( as its returned from Vector4.ToString() )
		/// </param>
		/// <returns>
		///     A new Vector4.
		/// </returns>
		public static Vector4 Parse( string vector )
		{
			string[] vals = vector.TrimStart( '<' ).TrimEnd( '>' ).Split( ',' );

			return new Vector4( Real.Parse( vals[ 0 ].Trim() ), Real.Parse( vals[ 1 ].Trim() ), Real.Parse( vals[ 2 ].Trim() ), Real.Parse( vals[ 3 ].Trim() ) );
		}

		#endregion
	}
}
