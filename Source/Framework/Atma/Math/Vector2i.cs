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
using System;
using System.Globalization;
using System.Runtime.InteropServices;
#endregion Namespace Declarations

namespace Atma
{
	/// <summary>
	///     2 dimensional vector.
	/// </summary>
	[StructLayout( LayoutKind.Sequential )]
	public struct Vector2i
	{
		#region Fields

		public int X, Y;

		#endregion Fields

		#region Properties

		/// <summary>
		/// Gets length of this vector
		/// </summary>
		public Real Length { get { return Utility.Sqrt( X * X + Y * Y ); } }

		/// <summary>
		/// Gets the squared length of this vector
		/// </summary>
		public Real LengthSquared { get { return X * X + Y * Y; } }

		/// <summary>
		/// Gets a vector perpendicular to this, which has the same magnitude.
		/// </summary>
		public Vector2i Perpendicular { get { return new Vector2i( this.Y, -this.X ); } }

		#endregion

		#region Static

		private static readonly Vector2i zeroVector = new Vector2i( 0, 0 );

		/// <summary>
		///		Gets a Vector2i with all components set to 0.
		/// </summary>
        public static Vector2i Zero { get { return zeroVector; } }

        public static Vector2i One { get { return new Vector2i(1, 1); } }
        
        public static Vector2i forward { get { return new Vector2i(1, 0); } }

		#endregion

		#region Constructors

		/// <summary>
		///     Constructor.
		/// </summary>
		/// <param name="x">X position.</param>
		/// <param name="y">Y position</param>
        public Vector2i(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		#endregion Constructors

		#region Methods

        ///// <summary>
        /////		Normalizes the vector.
        ///// </summary>
        ///// <remarks>
        /////		This method normalises the vector such that it's
        /////		length / magnitude is 1. The result is called a unit vector.
        /////		<p/>
        /////		This function will not crash for zero-sized vectors, but there
        /////		will be no changes made to their components.
        /////	</remarks>
        /////	<returns>The previous length of the vector.</returns>
        //public Real Normalize()
        //{
        //    Real length = Utility.Sqrt( this.X * this.X + this.Y * this.Y );

        //    // Will also work for zero-sized vectors, but will change nothing
        //    if( length > Real.Epsilon )
        //    {
        //        Real inverseLength = 1.0f / length;

        //        this.X = (int)(this.X * inverseLength);
        //        this.Y = (int)(this.Y * inverseLength);
				
        //    }

        //    return length;
        //}

		/// <summary>
		/// Gets a normalized (unit length) vector of this vector
		/// </summary>
		/// <returns></returns>
		public Vector2 ToNormalized()
		{
			Vector2 vec = new Vector2(this.X, this.Y);
			vec.Normalize();

			return vec;
		}

		/// <summary>
		/// Calculates the 2 dimensional cross-product of 2 vectors, which results
		/// in a Real value which is 2 times the area of the triangle
		/// defined by the two vectors. It also is the magnitude of the 3D vector that is perpendicular
		/// to the 2D vectors if the 2D vectors are projected to 3D space.
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public Real Cross( Vector2i vector )
		{
			return this.X * vector.Y - this.Y * vector.X;
		}

		/// <summary>
		/// Calculates the 2 dimensional dot-product of 2 vectors, 
		/// which is equal to the cosine of the angle between the vectors, times the lengths of each of the vectors.
		/// A.Dot(B) == |A| * |B| * cos(fi)
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public Real Dot( Vector2i vector )
		{
			return this.X * vector.X + this.Y * vector.Y;
		}

        public static Real Dot(Vector2i p0, Vector2i p1)
        {
            return p0.Dot(p1);
        }

        //public static Vector2i Transform(Vector2i position, Matrix4 matrix)
        //{
        //    Transform(ref position, ref matrix, out position);
        //    return position;
        //}

        //public static void Transform(ref Vector2i position, ref Matrix4 matrix, out Vector2i result)
        //{
        //    result = new Vector2i((int)(position.X * matrix.m00) + (position.Y * matrix.m10) + matrix.m30,
        //                         (position.X * matrix.m01) + (position.Y * matrix.m11) + matrix.m31);
        //}
		#endregion

        #region Overloaded operators + CLS compliant method equivalents

        /// <summary>
        ///		User to compare two Vector2ii instances for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Vector2i left, Vector2i right)
        {
            return (left.X == right.X && left.Y == right.Y);
        }

        /// <summary>
        ///		User to compare two Vector2ii instances for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Vector2i left, Vector2i right)
        {
            return (left.X != right.X || left.Y != right.Y);
        }

        /// <summary>
        ///		Used when a Vector2ii is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i Multiply(Vector2i left, Vector2i right)
        {
            return left * right;
        }

        /// <summary>
        ///		Used when a Vector2ii is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i operator *(Vector2i left, Vector2i right)
        {
            //return new Vector2ii( left.x * right.x, left.y * right.y, left.z * right.z );
            Vector2i retVal;
            retVal.X = left.X * right.X;
            retVal.Y = left.Y * right.Y;
            return retVal;
        }

        /// <summary>
        /// Used to divide a vector by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2i Divide(Vector2i left, int scalar)
        {
            return left / scalar;
        }

        /// <summary>
        ///		Used when a Vector2ii is divided by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i operator /(Vector2i left, Vector2i right)
        {
            Vector2i vector;

            vector.X = left.X / right.X;
            vector.Y = left.Y / right.Y;

            return vector;
        }

        /// <summary>
        ///		Used when a Vector2ii is divided by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i Divide(Vector2i left, Vector2i right)
        {
            return left / right;
        }

        /// <summary>
        /// Used to divide a vector by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2i operator /(Vector2i left, Real scalar)
        {
            //Debug.Assert(scalar != 0, "Cannot divide a Vector2ii by zero.");

            Vector2i vector;

            // get the inverse of the scalar up front to avoid doing multiple divides later
            float inverse = 1f / scalar;

            vector.X = (int)(left.X * inverse);
            vector.Y = (int)(left.Y * inverse);

            return vector;
        }


        /// <summary>
        ///		Used when a Vector2ii is added to another Vector2ii.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i Add(Vector2i left, Vector2i right)
        {
            return left + right;
        }

        /// <summary>
        ///		Used when a Vector2ii is added to another Vector2ii.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        ///		Used when a Vector2ii is multiplied by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2i Multiply(Vector2i left, int scalar)
        {
            return left * scalar;
        }

        /// <summary>
        ///		Used when a Vector2ii is multiplied by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector2i operator *(Vector2i left, int scalar)
        {
            //return new Vector2ii( left.x * scalar, left.y * scalar, left.z * scalar );
            Vector2i retVal;
            retVal.X = left.X * scalar;
            retVal.Y = left.Y * scalar;
            return retVal;
        }

        /// <summary>
        ///		Used when a scalar value is multiplied by a Vector2ii.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i Multiply(int scalar, Vector2i right)
        {
            return scalar * right;
        }

        /// <summary>
        ///		Used when a scalar value is multiplied by a Vector2ii.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i operator *(int scalar, Vector2i right)
        {
            //return new Vector2ii( right.x * scalar, right.y * scalar, right.z * scalar );
            Vector2i retVal;
            retVal.X = right.X * scalar;
            retVal.Y = right.Y * scalar;
            return retVal;
        }

        /// <summary>
        ///		Used to subtract a Vector2ii from another Vector2ii.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i Subtract(Vector2i left, Vector2i right)
        {
            return left - right;
        }

        /// <summary>
        ///		Used to subtract a Vector2ii from another Vector2ii.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2i operator -(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.X - right.X, left.Y - right.Y);
        }


        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector2i Negate(Vector2i left)
        {
            return -left;
        }

        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector2i operator -(Vector2i left)
        {
            return new Vector2i(-left.X, -left.Y);
        }

        /// <summary>
        ///    Returns true if the vector's scalar components are all smaller
        ///    that the ones of the vector it is compared against.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Vector2i left, Vector2i right)
        {
            if (left.X > right.X && left.Y > right.Y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///    Returns true if the vector's scalar components are all greater
        ///    that the ones of the vector it is compared against.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Vector2i left, Vector2i right)
        {
            if (left.X < right.X && left.Y < right.Y)
            {
                return true;
            }

            return false;
        }

        public static Vector2i operator %(Vector2i left, int right)
        {
            return new Vector2i(left.X % right, left.Y % right);
        }


        ///// <summary>
        /////		Used to access a Vector by index 0 = x, 1 = y, 2 = z.  
        ///// </summary>
        ///// <remarks>
        /////		Uses unsafe pointer arithmetic to reduce the code required.
        /////	</remarks>
        //public int this[int index]
        //{
        //    get
        //    {
        //        Debug.Assert(index >= 0 && index < 3, "Indexer boundaries overrun in Vector2ii.");

        //        // using pointer arithmetic here for less code.  Otherwise, we'd have a big switch statement.
        //        unsafe
        //        {
        //            fixed (int* pX = &x)
        //                return *(pX + index);
        //        }
        //    }
        //    set
        //    {
        //        Debug.Assert(index >= 0 && index < 3, "Indexer boundaries overrun in Vector2ii.");

        //        // using pointer arithmetic here for less code.  Otherwise, we'd have a big switch statement.
        //        unsafe
        //        {
        //            fixed (int* pX = &x)
        //                *(pX + index) = value;
        //        }
        //    }
        //}

        #endregion

		#region Object overrides

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2i))
                return false;

            var other = (Vector2i)obj;
            return this == other;
        }

		public override int GetHashCode()
		{
			return X ^ Y;
		}

		public override string ToString()
		{
			return String.Format( CultureInfo.InvariantCulture, "Vector2i({0}, {1})", this.X, this.Y );
		}

		#endregion

		#region Parse from string

		public Vector2i Parse( string s )
		{
			// the format is "Vector2i(x, y)"
			if( !s.StartsWith( "Vector2i(" ) )
			{
				throw new FormatException();
			}

			string[] values = s.Substring( 8 ).TrimEnd( '}' ).Split( ',' );

			return new Vector2i( int.Parse( values[ 0 ], CultureInfo.InvariantCulture ),
                                int.Parse(values[1], CultureInfo.InvariantCulture));
		}

		#endregion
	}
}
