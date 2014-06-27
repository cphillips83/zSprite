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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
#endregion Namespace Declarations

namespace Atma
{
	/// <summary>
	///		Class encapsulating a standard 4x4 homogenous matrix.
	/// </summary>
	/// <remarks>
	///		The engine uses column vectors when applying matrix multiplications,
	///		This means a vector is represented as a single column, 4-row
	///		matrix. This has the effect that the tranformations implemented
	///		by the matrices happens right-to-left e.g. if vector V is to be
	///		transformed by M1 then M2 then M3, the calculation would be
	///		M3 * M2 * M1 * V. The order that matrices are concatenated is
	///		vital since matrix multiplication is not cummatative, i.e. you
	///		can get a different result if you concatenate in the wrong order.
	/// 		<p/>
	///		The use of column vectors and right-to-left ordering is the
	///		standard in most mathematical texts, and is the same as used in
	///		OpenGL. It is, however, the opposite of Direct3D, which has
	///		inexplicably chosen to differ from the accepted standard and uses
	///		row vectors and left-to-right matrix multiplication.
	///		<p/>
	///		The engine deals with the differences between D3D and OpenGL etc.
	///		internally when operating through different render systems. The engine
	///		users only need to conform to standard maths conventions, i.e.
	///		right-to-left matrix multiplication, (The engine transposes matrices it
	///		passes to D3D to compensate).
	///		<p/>
	///		The generic form M * V which shows the layout of the matrix 
	///		entries is shown below:
	///		<p/>
	///		| m[0][0]  m[0][1]  m[0][2]  m[0][3] |   {x}
	///		| m[1][0]  m[1][1]  m[1][2]  m[1][3] |   {y}
	///		| m[2][0]  m[2][1]  m[2][2]  m[2][3] |   {z}
	///		| m[3][0]  m[3][1]  m[3][2]  m[3][3] |   {1}
	///	</remarks>
	///	<ogre headerVersion="1.18" sourceVersion="1.8" />
	[StructLayout( LayoutKind.Sequential )]
	public struct Matrix4
	{
		#region Member variables

		public Real m00, m01, m02, m03;
		public Real m10, m11, m12, m13;
		public Real m20, m21, m22, m23;
		public Real m30, m31, m32, m33;

		private static readonly Matrix4 zeroMatrix = new Matrix4(
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0 );

		private static readonly Matrix4 identityMatrix = new Matrix4(
			1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1 );

		// NOTE: This is different from what is in OGRE. Not sure why this is the case ATM, however, do not change it.
		private static readonly Matrix4 clipSpace2dToImageSpace = new Matrix4(
			//0.5f,  0.0f, 0.0f, -0.5f,
			//0.0f, -0.5f, 0.0f, -0.5f,
			//0.0f,  0.0f, 0.0f,  1.0f,
			//0.0f,  0.0f, 0.0f,  1.0f );
			0.5f, 0.0f, 0.0f, 0.5f,
			0.0f, -0.5f, 0.0f, 0.5f,
			0.0f, 0.0f, 1.0f, 0.0f,
			0.0f, 0.0f, 0.0f, 1.0f );

		#endregion

		#region Constructors

		/// <summary>
		///		Creates a new Matrix4 with all the specified parameters.
		/// </summary>
		public Matrix4( Real m00, Real m01, Real m02, Real m03,
		                Real m10, Real m11, Real m12, Real m13,
		                Real m20, Real m21, Real m22, Real m23,
		                Real m30, Real m31, Real m32, Real m33 )
		{
			this.m00 = m00;
			this.m01 = m01;
			this.m02 = m02;
			this.m03 = m03;
			this.m10 = m10;
			this.m11 = m11;
			this.m12 = m12;
			this.m13 = m13;
			this.m20 = m20;
			this.m21 = m21;
			this.m22 = m22;
			this.m23 = m23;
			this.m30 = m30;
			this.m31 = m31;
			this.m32 = m32;
			this.m33 = m33;
		}

		#endregion

		#region Static properties

		/// <summary>
		///    Returns a matrix with the following form:
		///    | 1,0,0,0 |
		///    | 0,1,0,0 |
		///    | 0,0,1,0 |
		///    | 0,0,0,1 |
		/// </summary>
		public static Matrix4 Identity { get { return identityMatrix; } }

		/// <summary>
		///    Returns a matrix with all elements set to 0.
		/// </summary>
		public static Matrix4 Zero { get { return zeroMatrix; } }

		public static Matrix4 ClipSpace2DToImageSpace { get { return clipSpace2dToImageSpace; } }

        public static Matrix4 CreateTranslation(float xPosition, float yPosition, float zPosition)
        {
            Matrix4 result;
            CreateTranslation(xPosition, yPosition, zPosition, out result);
            return result;
        }


        public static void CreateTranslation(ref Vector3 position, out Matrix4 result)
        {
            result.m00 = 1;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = 1;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = 1;
            result.m23 = 0;
            result.m30 = position.X;
            result.m31 = position.Y;
            result.m32 = position.Z;
            result.m33 = 1;
        }


        public static Matrix4 CreateTranslation(Vector3 position)
        {
            Matrix4 result;
            CreateTranslation(ref position, out result);
            return result;
        }


        public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix4 result)
        {
            result.m00 = 1;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = 1;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = 1;
            result.m23 = 0;
            result.m30 = xPosition;
            result.m31 = yPosition;
            result.m32 = zPosition;
            result.m33 = 1;
        }

        public static Matrix4 CreateRotationX(float radians)
        {
            Matrix4 result;
            CreateRotationX(radians, out result);
            return result;
        }


        public static void CreateRotationX(float radians, out Matrix4 result)
        {
            result = Matrix4.Identity;

            var val1 = Utility.Cos(radians);
            var val2 = Utility.Sin(radians);

            result.m11 = val1;
            result.m12 = val2;
            result.m21 = -val2;
            result.m22 = val1;
        }

        public static Matrix4 CreateRotationY(float radians)
        {
            Matrix4 result;
            CreateRotationY(radians, out result);
            return result;
        }


        public static void CreateRotationY(float radians, out Matrix4 result)
        {
            result = Matrix4.Identity;

            var val1 = Utility.Cos(radians);
            var val2 = Utility.Sin(radians);

            result.m00 = val1;
            result.m02 = -val2;
            result.m20 = val2;
            result.m22 = val1;
        }


        public static Matrix4 CreateRotationZ(float radians)
        {
            Matrix4 result;
            CreateRotationZ(radians, out result);
            return result;
        }


        public static void CreateRotationZ(float radians, out Matrix4 result)
        {
            result = Matrix4.Identity;

            var val1 = Utility.Cos(radians);
            var val2 = Utility.Sin(radians);

            result.m00 = val1;
            result.m01 = val2;
            result.m10 = -val2;
            result.m11 = val1;
        }

        public static Matrix4 CreateScale(float scale)
        {
            Matrix4 result;
            CreateScale(scale, scale, scale, out result);
            return result;
        }


        public static void CreateScale(float scale, out Matrix4 result)
        {
            CreateScale(scale, scale, scale, out result);
        }


        public static Matrix4 CreateScale(float xScale, float yScale, float zScale)
        {
            Matrix4 result;
            CreateScale(xScale, yScale, zScale, out result);
            return result;
        }


        public static void CreateScale(float xScale, float yScale, float zScale, out Matrix4 result)
        {
            result = new Matrix4();
            result.m00 = xScale;
            result.m02 = 0;
            result.m03 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = yScale;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = zScale;
            result.m23 = 0;
            result.m30 = 0;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1;
        }


        public static Matrix4 CreateScale(Vector3 scales)
        {
            Matrix4 result;
            CreateScale(ref scales, out result);
            return result;
        }


        public static void CreateScale(ref Vector3 scales, out Matrix4 result)
        {
            result.m00 = scales.X;
            result.m01 = 0;
            result.m02 = 0;
            result.m03 = 0;
            result.m10 = 0;
            result.m11 = scales.Y;
            result.m12 = 0;
            result.m13 = 0;
            result.m20 = 0;
            result.m21 = 0;
            result.m22 = scales.Z;
            result.m23 = 0;
            result.m30 = 0;
            result.m31 = 0;
            result.m32 = 0;
            result.m33 = 1;
        }
		#endregion

		#region Public properties

		/// <summary>
		///		Gets/Sets the Translation portion of the matrix.
		///		| 0 0 0 Tx|
		///		| 0 0 0 Ty|
		///		| 0 0 0 Tz|
		///		| 0 0 0  1 |
		/// </summary>
		public Vector3 Translation
		{
			get { return new Vector3( this.m03, this.m13, this.m23 ); }
			set
			{
				this.m03 = value.X;
				this.m13 = value.Y;
				this.m23 = value.Z;
			}
		}

		/// <summary>
		///		Gets/Sets the Scale portion of the matrix.
		///		|Sx 0  0  0 |
		///		| 0 Sy 0  0 |
		///		| 0  0 Sz 0 |
		///		| 0  0  0  0 |
		/// </summary>
		/// <remarks>
		///     Note that the property reflects the real scale only when there isn't any rotation, 
		/// i.e. the 3x3 rotation portion of the matrix was a <see cref="Matrix3.Identiy"/> before a scale was set.
		/// If you need to obtain the current scale of a rotated matrix, use the more expensive <see cref="ExtractRotation"/> method.
		/// </remarks>
		public Vector3 Scale
		{
			get { return new Vector3( this.m00, this.m11, this.m22 ); }
			set
			{
				this.m00 = value.X;
				this.m11 = value.Y;
				this.m22 = value.Z;
			}
		}

		/// <summary>
		/// Check whether or not the matrix is affine matrix.
		/// </summary>
		/// <remarks>
		/// An affine matrix is a 4x4 matrix with row 3 equal to (0, 0, 0, 1),
		/// e.g. no projective coefficients.
		/// </remarks>
		public bool IsAffine { get { return m30 == 0 && m31 == 0 && m32 == 0 && m33 == 1; } }

		/// <summary>
		///    Gets the determinant of this matrix.
		/// </summary>
		public Real Determinant
		{
			get
			{
				// note: this is an expanded version of the Ogre determinant() method, to give better performance in C#. Generated using a script
				Real result = m00 * ( m11 * ( m22 * m33 - m32 * m23 ) - m12 * ( m21 * m33 - m31 * m23 ) + m13 * ( m21 * m32 - m31 * m22 ) ) -
				              m01 * ( m10 * ( m22 * m33 - m32 * m23 ) - m12 * ( m20 * m33 - m30 * m23 ) + m13 * ( m20 * m32 - m30 * m22 ) ) +
				              m02 * ( m10 * ( m21 * m33 - m31 * m23 ) - m11 * ( m20 * m33 - m30 * m23 ) + m13 * ( m20 * m31 - m30 * m21 ) ) -
				              m03 * ( m10 * ( m21 * m32 - m31 * m22 ) - m11 * ( m20 * m32 - m30 * m22 ) + m12 * ( m20 * m31 - m30 * m21 ) );

				return result;
			}
		}

		#endregion

		#region Static methods

		/// <summary>
		///		Used to allow assignment from a Matrix3 to a Matrix4 object.
		/// </summary>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 FromMatrix3( Matrix3 right )
		{
			return right;
		}

		/// <summary>
		/// Creates a translation Matrix
		/// </summary>
		/// <param name="position"></param>
		/// <param name="scale"></param>
		/// <param name="orientation"></param>
		/// <returns></returns>
		public static Matrix4 Compose( Vector3 translation, Vector3 scale, Quaternion orientation )
		{
			// Ordering:
			//    1. Scale
			//    2. Rotate
			//    3. Translate

			Matrix3 rot3x3, scale3x3;
			rot3x3 = orientation.ToRotationMatrix();
			scale3x3 = Matrix3.Zero;
			scale3x3.m00 = scale.X;
			scale3x3.m11 = scale.Y;
			scale3x3.m22 = scale.Z;

			// Set up final matrix with scale, rotation and translation
			Matrix4 result = rot3x3 * scale3x3;
			result.Translation = translation;

			return result;
		}

		/// <summary>
		/// Creates an inverse translation Matrix
		/// </summary>
		/// <param name="translation"></param>
		/// <param name="scale"></param>
		/// <param name="orientation"></param>
		/// <returns></returns>
		public static Matrix4 ComposeInverse( Vector3 translation, Vector3 scale, Quaternion orientation )
		{
			// Invert the parameters
			Vector3 invTranslate = -translation;
			Vector3 invScale = new Vector3( 1f / scale.X, 1f / scale.Y, 1f / scale.Z );
			Quaternion invRot = orientation.Inverse();

			// Because we're inverting, order is translation, rotation, scale
			// So make translation relative to scale & rotation
			invTranslate *= invScale; // scale
			invTranslate = invRot * invTranslate; // rotate

			// Next, make a 3x3 rotation matrix and apply inverse scale
			Matrix3 rot3x3, scale3x3;
			rot3x3 = invRot.ToRotationMatrix();
			scale3x3 = Matrix3.Zero;
			scale3x3.m00 = invScale.X;
			scale3x3.m11 = invScale.Y;
			scale3x3.m22 = invScale.Z;

			// Set up final matrix with scale, rotation and translation
			Matrix4 result = scale3x3 * rot3x3;
			result.Translation = invTranslate;

			return result;
		}

		#endregion

		#region Public methods

		/// <summary>
		///    Returns a 3x3 portion of this 4x4 matrix.
		/// </summary>
		/// <returns></returns>
		public Matrix3 GetMatrix3()
		{
			return
				new Matrix3(
					this.m00, this.m01, this.m02,
					this.m10, this.m11, this.m12,
					this.m20, this.m21, this.m22 );
		}

		/// <summary>
		///    Returns an inverted matrix.
		/// </summary>
		/// <returns></returns>
		public Matrix4 Inverse()
		{
			return Adjoint() * ( 1.0f / this.Determinant );
		}

		/// <summary>
		///     Returns an inverted affine matrix.
		/// </summary>
		/// <returns></returns>
		public Matrix4 InverseAffine()
		{
			Debug.Assert( IsAffine );

			Real t00 = m22 * m11 - m21 * m12;
			Real t10 = m20 * m12 - m22 * m10;
			Real t20 = m21 * m10 - m20 * m11;

			Real invDet = 1 / ( m00 * t00 + m01 * t10 + m02 * t20 );

			t00 *= invDet;
			t10 *= invDet;
			t20 *= invDet;

			m00 *= invDet;
			m01 *= invDet;
			m02 *= invDet;

			Real r00 = t00;
			Real r01 = m02 * m21 - m01 * m22;
			Real r02 = m01 * m12 - m02 * m11;

			Real r10 = t10;
			Real r11 = m00 * m22 - m02 * m20;
			Real r12 = m02 * m10 - m00 * m12;

			Real r20 = t20;
			Real r21 = m01 * m20 - m00 * m21;
			Real r22 = m00 * m11 - m01 * m10;

			Real r03 = -( r00 * m03 + r01 * m13 + r02 * m23 );
			Real r13 = -( r10 * m03 + r11 * m13 + r12 * m23 );
			Real r23 = -( r20 * m03 + r21 * m13 + r22 * m23 );

			return new Matrix4(
				r00, r01, r02, r03,
				r10, r11, r12, r13,
				r20, r21, r22, r23,
				0, 0, 0, 1 );
		}

		/// <summary>
		///    Swap the rows of the matrix with the columns.
		/// </summary>
		/// <returns>A transposed Matrix.</returns>
		public Matrix4 Transpose()
		{
			return new Matrix4( this.m00, this.m10, this.m20, this.m30,
			                    this.m01, this.m11, this.m21, this.m31,
			                    this.m02, this.m12, this.m22, this.m32,
			                    this.m03, this.m13, this.m23, this.m33 );
		}

		/// <summary>
		/// 3-D Vector transformation specially for affine matrix.
		/// </summary>
		/// <remarks>
		/// Transforms the given 3-D vector by the matrix, projecting the
		/// result back into <i>w</i> = 1.
		/// The matrix must be an affine matrix. <see cref="Matrix4.IsAffine"/>.
		/// </remarks>
		/// <param name="v"></param>
		/// <returns></returns>
		public Vector3 TransformAffine( Vector3 v )
		{
			Debug.Assert( IsAffine );

			return new Vector3(
				m00 * v.X + m01 * v.Y + m02 * v.Z + m03,
				m10 * v.X + m11 * v.Y + m12 * v.Z + m13,
				m20 * v.X + m21 * v.Y + m22 * v.Z + m23 );
		}

		/// <summary>
		/// 4-D Vector transformation specially for affine matrix.
		/// </summary>
		/// <remarks>
		/// The matrix must be an affine matrix. <see cref="Matrix4.IsAffine"/>.
		/// </remarks>
		/// <param name="v"></param>
		/// <returns></returns>
		public Vector4 TransformAffine( Vector4 v )
		{
			Debug.Assert( IsAffine );

			return new Vector4(
				m00 * v.X + m01 * v.Y + m02 * v.Z + m03 * v.W,
				m10 * v.X + m11 * v.Y + m12 * v.Z + m13 * v.W,
				m20 * v.X + m21 * v.Y + m22 * v.Z + m23 * v.W,
				v.W );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public void MakeRealArray( Real[] reals )
		{
			reals[ 0 ] = this.m00;
			reals[ 1 ] = this.m01;
			reals[ 2 ] = this.m02;
			reals[ 3 ] = this.m03;
			reals[ 4 ] = this.m10;
			reals[ 5 ] = this.m11;
			reals[ 6 ] = this.m12;
			reals[ 7 ] = this.m13;
			reals[ 8 ] = this.m20;
			reals[ 9 ] = this.m21;
			reals[ 10 ] = this.m22;
			reals[ 11 ] = this.m23;
			reals[ 12 ] = this.m30;
			reals[ 13 ] = this.m31;
			reals[ 14 ] = this.m32;
			reals[ 15 ] = this.m33;
		}

		public void MakeFloatArray( float[] floats )
		{
			floats[ 0 ] = this.m00;
			floats[ 1 ] = this.m01;
			floats[ 2 ] = this.m02;
			floats[ 3 ] = this.m03;
			floats[ 4 ] = this.m10;
			floats[ 5 ] = this.m11;
			floats[ 6 ] = this.m12;
			floats[ 7 ] = this.m13;
			floats[ 8 ] = this.m20;
			floats[ 9 ] = this.m21;
			floats[ 10 ] = this.m22;
			floats[ 11 ] = this.m23;
			floats[ 12 ] = this.m30;
			floats[ 13 ] = this.m31;
			floats[ 14 ] = this.m32;
			floats[ 15 ] = this.m33;
		}

		/// <summary>
		///     Extract the 3x3 matrix representing the current rotation. 
		/// </summary>
		/// <param name="rotation"></param>
		public Matrix3 ExtractRotation()
		{
			Vector3 axis = Vector3.Zero;
			Matrix3 rotation = Matrix3.Identity;

			axis.X = this.m00;
			axis.Y = this.m10;
			axis.Z = this.m20;
			axis.Normalize();
			rotation.m00 = axis.X;
			rotation.m10 = axis.Y;
			rotation.m20 = axis.Z;

			axis.X = this.m01;
			axis.Y = this.m11;
			axis.Z = this.m21;
			axis.Normalize();
			rotation.m01 = axis.X;
			rotation.m11 = axis.Y;
			rotation.m21 = axis.Z;

			axis.X = this.m02;
			axis.Y = this.m12;
			axis.Z = this.m22;
			axis.Normalize();
			rotation.m02 = axis.X;
			rotation.m12 = axis.Y;
			rotation.m22 = axis.Z;

			return rotation;
		}

		/// <summary>
		///     Extract scaling information.
		/// </summary>
		/// <returns></returns>
		public Vector3 ExtractScale()
		{
			Vector3 scale = Vector3.UnitScale;
			Vector3 axis = Vector3.Zero;

			axis.X = this.m00;
			axis.Y = this.m10;
			axis.Z = this.m20;
			scale.X = axis.Length;

			axis.X = this.m01;
			axis.Y = this.m11;
			axis.Z = this.m21;
			scale.Y = axis.Length;

			axis.X = this.m02;
			axis.Y = this.m12;
			axis.Z = this.m22;
			scale.Z = axis.Length;

			return scale;
		}

		/// <summary>
		///    Decompose the matrix.
		/// </summary>
		/// <param name="translation"></param>
		/// <param name="scale"></param>
		/// <param name="orientation"></param>
		public void Decompose( out Vector3 translation, out Vector3 scale, out Quaternion orientation )
		{
			scale = Vector3.UnitScale;
			Matrix3 rotation = Matrix3.Identity;
			Vector3 axis = Vector3.Zero;

			axis.X = this.m00;
			axis.Y = this.m10;
			axis.Z = this.m20;
			scale.X = axis.Normalize(); // Normalize() returns the vector's length before it was normalized
			rotation.m00 = axis.X;
			rotation.m10 = axis.Y;
			rotation.m20 = axis.Z;

			axis.X = this.m01;
			axis.Y = this.m11;
			axis.Z = this.m21;
			scale.Y = axis.Normalize();
			rotation.m01 = axis.X;
			rotation.m11 = axis.Y;
			rotation.m21 = axis.Z;

			axis.X = this.m02;
			axis.Y = this.m12;
			axis.Z = this.m22;
			scale.Z = axis.Normalize();
			rotation.m02 = axis.X;
			rotation.m12 = axis.Y;
			rotation.m22 = axis.Z;

			orientation = Quaternion.FromRotationMatrix( rotation );
			translation = this.Translation;
		}

        public static Matrix4 Invert(Matrix4 matrix)
        {
            Invert(ref matrix, out matrix);
            return matrix;
        }


        public static void Invert(ref Matrix4 matrix, out Matrix4 result)
        {
            float num1 = matrix.m00;
            float num2 = matrix.m01;
            float num3 = matrix.m02;
            float num4 = matrix.m03;
            float num5 = matrix.m10;
            float num6 = matrix.m11;
            float num7 = matrix.m12;
            float num8 = matrix.m13;
            float num9 = matrix.m20;
            float num10 = matrix.m21;
            float num11 = matrix.m22;
            float num12 = matrix.m23;
            float num13 = matrix.m30;
            float num14 = matrix.m31;
            float num15 = matrix.m32;
            float num16 = matrix.m33;
            float num17 = (float)((double)num11 * (double)num16 - (double)num12 * (double)num15);
            float num18 = (float)((double)num10 * (double)num16 - (double)num12 * (double)num14);
            float num19 = (float)((double)num10 * (double)num15 - (double)num11 * (double)num14);
            float num20 = (float)((double)num9 * (double)num16 - (double)num12 * (double)num13);
            float num21 = (float)((double)num9 * (double)num15 - (double)num11 * (double)num13);
            float num22 = (float)((double)num9 * (double)num14 - (double)num10 * (double)num13);
            float num23 = (float)((double)num6 * (double)num17 - (double)num7 * (double)num18 + (double)num8 * (double)num19);
            float num24 = (float)-((double)num5 * (double)num17 - (double)num7 * (double)num20 + (double)num8 * (double)num21);
            float num25 = (float)((double)num5 * (double)num18 - (double)num6 * (double)num20 + (double)num8 * (double)num22);
            float num26 = (float)-((double)num5 * (double)num19 - (double)num6 * (double)num21 + (double)num7 * (double)num22);
            float num27 = (float)(1.0 / ((double)num1 * (double)num23 + (double)num2 * (double)num24 + (double)num3 * (double)num25 + (double)num4 * (double)num26));

            result.m00 = num23 * num27;
            result.m10 = num24 * num27;
            result.m20 = num25 * num27;
            result.m30 = num26 * num27;
            result.m01 = (float)-((double)num2 * (double)num17 - (double)num3 * (double)num18 + (double)num4 * (double)num19) * num27;
            result.m11 = (float)((double)num1 * (double)num17 - (double)num3 * (double)num20 + (double)num4 * (double)num21) * num27;
            result.m21 = (float)-((double)num1 * (double)num18 - (double)num2 * (double)num20 + (double)num4 * (double)num22) * num27;
            result.m31 = (float)((double)num1 * (double)num19 - (double)num2 * (double)num21 + (double)num3 * (double)num22) * num27;
            float num28 = (float)((double)num7 * (double)num16 - (double)num8 * (double)num15);
            float num29 = (float)((double)num6 * (double)num16 - (double)num8 * (double)num14);
            float num30 = (float)((double)num6 * (double)num15 - (double)num7 * (double)num14);
            float num31 = (float)((double)num5 * (double)num16 - (double)num8 * (double)num13);
            float num32 = (float)((double)num5 * (double)num15 - (double)num7 * (double)num13);
            float num33 = (float)((double)num5 * (double)num14 - (double)num6 * (double)num13);
            result.m02 = (float)((double)num2 * (double)num28 - (double)num3 * (double)num29 + (double)num4 * (double)num30) * num27;
            result.m12 = (float)-((double)num1 * (double)num28 - (double)num3 * (double)num31 + (double)num4 * (double)num32) * num27;
            result.m22 = (float)((double)num1 * (double)num29 - (double)num2 * (double)num31 + (double)num4 * (double)num33) * num27;
            result.m32 = (float)-((double)num1 * (double)num30 - (double)num2 * (double)num32 + (double)num3 * (double)num33) * num27;
            float num34 = (float)((double)num7 * (double)num12 - (double)num8 * (double)num11);
            float num35 = (float)((double)num6 * (double)num12 - (double)num8 * (double)num10);
            float num36 = (float)((double)num6 * (double)num11 - (double)num7 * (double)num10);
            float num37 = (float)((double)num5 * (double)num12 - (double)num8 * (double)num9);
            float num38 = (float)((double)num5 * (double)num11 - (double)num7 * (double)num9);
            float num39 = (float)((double)num5 * (double)num10 - (double)num6 * (double)num9);
            result.m03 = (float)-((double)num2 * (double)num34 - (double)num3 * (double)num35 + (double)num4 * (double)num36) * num27;
            result.m13 = (float)((double)num1 * (double)num34 - (double)num3 * (double)num37 + (double)num4 * (double)num38) * num27;
            result.m23 = (float)-((double)num1 * (double)num35 - (double)num2 * (double)num37 + (double)num4 * (double)num39) * num27;
            result.m33 = (float)((double)num1 * (double)num36 - (double)num2 * (double)num38 + (double)num3 * (double)num39) * num27;


            /*
                        
                        
///
// Use Laplace expansion theorem to calculate the inverse of a 4x4 matrix
// 
// 1. Calculate the 2x2 determinants needed the 4x4 determinant based on the 2x2 determinants 
// 3. Create the adjugate matrix, which satisfies: A * adj(A) = det(A) * I
// 4. Divide adjugate matrix with the determinant to find the inverse
            
float det1, det2, det3, det4, det5, det6, det7, det8, det9, det10, det11, det12;
float detMatrix;
FindDeterminants(ref matrix, out detMatrix, out det1, out det2, out det3, out det4, out det5, out det6, 
                 out det7, out det8, out det9, out det10, out det11, out det12);
            
float invDetMatrix = 1f / detMatrix;
            
Matrix ret; // Allow for matrix and result to point to the same structure
            
ret.M11 = (matrix.M22*det12 - matrix.M23*det11 + matrix.M24*det10) * invDetMatrix;
ret.M12 = (-matrix.M12*det12 + matrix.M13*det11 - matrix.M14*det10) * invDetMatrix;
ret.M13 = (matrix.M42*det6 - matrix.M43*det5 + matrix.M44*det4) * invDetMatrix;
ret.M14 = (-matrix.M32*det6 + matrix.M33*det5 - matrix.M34*det4) * invDetMatrix;
ret.M21 = (-matrix.M21*det12 + matrix.M23*det9 - matrix.M24*det8) * invDetMatrix;
ret.M22 = (matrix.M11*det12 - matrix.M13*det9 + matrix.M14*det8) * invDetMatrix;
ret.M23 = (-matrix.M41*det6 + matrix.M43*det3 - matrix.M44*det2) * invDetMatrix;
ret.M24 = (matrix.M31*det6 - matrix.M33*det3 + matrix.M34*det2) * invDetMatrix;
ret.M31 = (matrix.M21*det11 - matrix.M22*det9 + matrix.M24*det7) * invDetMatrix;
ret.M32 = (-matrix.M11*det11 + matrix.M12*det9 - matrix.M14*det7) * invDetMatrix;
ret.M33 = (matrix.M41*det5 - matrix.M42*det3 + matrix.M44*det1) * invDetMatrix;
ret.M34 = (-matrix.M31*det5 + matrix.M32*det3 - matrix.M34*det1) * invDetMatrix;
ret.M41 = (-matrix.M21*det10 + matrix.M22*det8 - matrix.M23*det7) * invDetMatrix;
ret.M42 = (matrix.M11*det10 - matrix.M12*det8 + matrix.M13*det7) * invDetMatrix;
ret.M43 = (-matrix.M41*det4 + matrix.M42*det2 - matrix.M43*det1) * invDetMatrix;
ret.M44 = (matrix.M31*det4 - matrix.M32*det2 + matrix.M33*det1) * invDetMatrix;
            
result = ret;
*/
        }
		#endregion

		#region Private methods

		/// <summary>
		///    Used to generate the adjoint of this matrix.
		/// </summary>
		/// <returns>The adjoint matrix of the current instance.</returns>
		private Matrix4 Adjoint()
		{
			// note: this is an expanded version of the Ogre adjoint() method, to give better performance in C#. Generated using a script
			Real val0 = m11 * ( m22 * m33 - m32 * m23 ) - m12 * ( m21 * m33 - m31 * m23 ) + m13 * ( m21 * m32 - m31 * m22 );
			Real val1 = -( m01 * ( m22 * m33 - m32 * m23 ) - m02 * ( m21 * m33 - m31 * m23 ) + m03 * ( m21 * m32 - m31 * m22 ) );
			Real val2 = m01 * ( m12 * m33 - m32 * m13 ) - m02 * ( m11 * m33 - m31 * m13 ) + m03 * ( m11 * m32 - m31 * m12 );
			Real val3 = -( m01 * ( m12 * m23 - m22 * m13 ) - m02 * ( m11 * m23 - m21 * m13 ) + m03 * ( m11 * m22 - m21 * m12 ) );
			Real val4 = -( m10 * ( m22 * m33 - m32 * m23 ) - m12 * ( m20 * m33 - m30 * m23 ) + m13 * ( m20 * m32 - m30 * m22 ) );
			Real val5 = m00 * ( m22 * m33 - m32 * m23 ) - m02 * ( m20 * m33 - m30 * m23 ) + m03 * ( m20 * m32 - m30 * m22 );
			Real val6 = -( m00 * ( m12 * m33 - m32 * m13 ) - m02 * ( m10 * m33 - m30 * m13 ) + m03 * ( m10 * m32 - m30 * m12 ) );
			Real val7 = m00 * ( m12 * m23 - m22 * m13 ) - m02 * ( m10 * m23 - m20 * m13 ) + m03 * ( m10 * m22 - m20 * m12 );
			Real val8 = m10 * ( m21 * m33 - m31 * m23 ) - m11 * ( m20 * m33 - m30 * m23 ) + m13 * ( m20 * m31 - m30 * m21 );
			Real val9 = -( m00 * ( m21 * m33 - m31 * m23 ) - m01 * ( m20 * m33 - m30 * m23 ) + m03 * ( m20 * m31 - m30 * m21 ) );
			Real val10 = m00 * ( m11 * m33 - m31 * m13 ) - m01 * ( m10 * m33 - m30 * m13 ) + m03 * ( m10 * m31 - m30 * m11 );
			Real val11 = -( m00 * ( m11 * m23 - m21 * m13 ) - m01 * ( m10 * m23 - m20 * m13 ) + m03 * ( m10 * m21 - m20 * m11 ) );
			Real val12 = -( m10 * ( m21 * m32 - m31 * m22 ) - m11 * ( m20 * m32 - m30 * m22 ) + m12 * ( m20 * m31 - m30 * m21 ) );
			Real val13 = m00 * ( m21 * m32 - m31 * m22 ) - m01 * ( m20 * m32 - m30 * m22 ) + m02 * ( m20 * m31 - m30 * m21 );
			Real val14 = -( m00 * ( m11 * m32 - m31 * m12 ) - m01 * ( m10 * m32 - m30 * m12 ) + m02 * ( m10 * m31 - m30 * m11 ) );
			Real val15 = m00 * ( m11 * m22 - m21 * m12 ) - m01 * ( m10 * m22 - m20 * m12 ) + m02 * ( m10 * m21 - m20 * m11 );

			return new Matrix4( val0, val1, val2, val3, val4, val5, val6, val7, val8, val9, val10, val11, val12, val13, val14, val15 );
		}

		#endregion

		#region Operator overloads + CLS compliant method equivalents

		/// <summary>
		///		Used to multiply (concatenate) two 4x4 Matrices.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 Multiply( Matrix4 left, Matrix4 right )
		{
			return left * right;
		}

		/// <summary>
		///		Used to multiply (concatenate) two 4x4 Matrices.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 operator *( Matrix4 left, Matrix4 right )
		{
			Matrix4 result = new Matrix4();

			result.m00 = left.m00 * right.m00 + left.m01 * right.m10 + left.m02 * right.m20 + left.m03 * right.m30;
			result.m01 = left.m00 * right.m01 + left.m01 * right.m11 + left.m02 * right.m21 + left.m03 * right.m31;
			result.m02 = left.m00 * right.m02 + left.m01 * right.m12 + left.m02 * right.m22 + left.m03 * right.m32;
			result.m03 = left.m00 * right.m03 + left.m01 * right.m13 + left.m02 * right.m23 + left.m03 * right.m33;

			result.m10 = left.m10 * right.m00 + left.m11 * right.m10 + left.m12 * right.m20 + left.m13 * right.m30;
			result.m11 = left.m10 * right.m01 + left.m11 * right.m11 + left.m12 * right.m21 + left.m13 * right.m31;
			result.m12 = left.m10 * right.m02 + left.m11 * right.m12 + left.m12 * right.m22 + left.m13 * right.m32;
			result.m13 = left.m10 * right.m03 + left.m11 * right.m13 + left.m12 * right.m23 + left.m13 * right.m33;

			result.m20 = left.m20 * right.m00 + left.m21 * right.m10 + left.m22 * right.m20 + left.m23 * right.m30;
			result.m21 = left.m20 * right.m01 + left.m21 * right.m11 + left.m22 * right.m21 + left.m23 * right.m31;
			result.m22 = left.m20 * right.m02 + left.m21 * right.m12 + left.m22 * right.m22 + left.m23 * right.m32;
			result.m23 = left.m20 * right.m03 + left.m21 * right.m13 + left.m22 * right.m23 + left.m23 * right.m33;

			result.m30 = left.m30 * right.m00 + left.m31 * right.m10 + left.m32 * right.m20 + left.m33 * right.m30;
			result.m31 = left.m30 * right.m01 + left.m31 * right.m11 + left.m32 * right.m21 + left.m33 * right.m31;
			result.m32 = left.m30 * right.m02 + left.m31 * right.m12 + left.m32 * right.m22 + left.m33 * right.m32;
			result.m33 = left.m30 * right.m03 + left.m31 * right.m13 + left.m32 * right.m23 + left.m33 * right.m33;

			return result;
		}

		/// <summary>
		///		Transforms the given 3-D vector by the matrix, projecting the 
		///		result back into <i>w</i> = 1.
		///		<p/>
		///		This means that the initial <i>w</i> is considered to be 1.0,
		///		and then all the tree elements of the resulting 3-D vector are
		///		divided by the resulting <i>w</i>.
		/// </summary>
		/// <param name="matrix">A Matrix4.</param>
		/// <param name="vector">A Vector3.</param>
		/// <returns>A new vector.</returns>
		public static Vector3 Multiply( Matrix4 matrix, Vector3 vector )
		{
			return matrix * vector;
		}

		/// <summary>
		///		Transforms a plane using the specified transform.
		/// </summary>
		/// <param name="matrix">Transformation matrix.</param>
		/// <param name="plane">Plane to transform.</param>
		/// <returns>A transformed plane.</returns>
		public static Plane Multiply( Matrix4 matrix, Plane plane )
		{
			return matrix * plane;
		}

		/// <summary>
		///		Transforms the given 3-D vector by the matrix, projecting the 
		///		result back into <i>w</i> = 1.
		///		<p/>
		///		This means that the initial <i>w</i> is considered to be 1.0,
		///		and then all the tree elements of the resulting 3-D vector are
		///		divided by the resulting <i>w</i>.
		/// </summary>
		/// <param name="matrix">A Matrix4.</param>
		/// <param name="vector">A Vector3.</param>
		/// <returns>A new vector.</returns>
		public static Vector3 operator *( Matrix4 matrix, Vector3 vector )
		{
			Vector3 result = new Vector3();

			Real inverseW = 1.0f / ( matrix.m30 * vector.X + matrix.m31 * vector.Y + matrix.m32 * vector.Z + matrix.m33 );

			result.X = ( ( matrix.m00 * vector.X ) + ( matrix.m01 * vector.Y ) + ( matrix.m02 * vector.Z ) + matrix.m03 ) * inverseW;
			result.Y = ( ( matrix.m10 * vector.X ) + ( matrix.m11 * vector.Y ) + ( matrix.m12 * vector.Z ) + matrix.m13 ) * inverseW;
			result.Z = ( ( matrix.m20 * vector.X ) + ( matrix.m21 * vector.Y ) + ( matrix.m22 * vector.Z ) + matrix.m23 ) * inverseW;

			return result;
		}

		/// <summary>
		///		Used to multiply a Matrix4 object by a scalar value..
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 operator *( Matrix4 left, Real scalar )
		{
			Matrix4 result = new Matrix4();

			result.m00 = left.m00 * scalar;
			result.m01 = left.m01 * scalar;
			result.m02 = left.m02 * scalar;
			result.m03 = left.m03 * scalar;

			result.m10 = left.m10 * scalar;
			result.m11 = left.m11 * scalar;
			result.m12 = left.m12 * scalar;
			result.m13 = left.m13 * scalar;

			result.m20 = left.m20 * scalar;
			result.m21 = left.m21 * scalar;
			result.m22 = left.m22 * scalar;
			result.m23 = left.m23 * scalar;

			result.m30 = left.m30 * scalar;
			result.m31 = left.m31 * scalar;
			result.m32 = left.m32 * scalar;
			result.m33 = left.m33 * scalar;

			return result;
		}

		/// <summary>
		///		Used to multiply a transformation to a Plane.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="plane"></param>
		/// <returns></returns>
		public static Plane operator *( Matrix4 left, Plane plane )
		{
			Plane result = new Plane();

			Vector3 planeNormal = plane.Normal;

			result.Normal = new Vector3(
				left.m00 * planeNormal.X + left.m01 * planeNormal.Y + left.m02 * planeNormal.Z,
				left.m10 * planeNormal.X + left.m11 * planeNormal.Y + left.m12 * planeNormal.Z,
				left.m20 * planeNormal.X + left.m21 * planeNormal.Y + left.m22 * planeNormal.Z );

			Vector3 pt = planeNormal * -plane.D;
			pt = left * pt;

			result.D = -pt.Dot( result.Normal );

			return result;
		}

		/// <summary>
		///		Used to add two matrices together.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 Add( Matrix4 left, Matrix4 right )
		{
			return left + right;
		}

		/// <summary>
		///		Used to add two matrices together.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 operator +( Matrix4 left, Matrix4 right )
		{
			Matrix4 result = new Matrix4();

			result.m00 = left.m00 + right.m00;
			result.m01 = left.m01 + right.m01;
			result.m02 = left.m02 + right.m02;
			result.m03 = left.m03 + right.m03;

			result.m10 = left.m10 + right.m10;
			result.m11 = left.m11 + right.m11;
			result.m12 = left.m12 + right.m12;
			result.m13 = left.m13 + right.m13;

			result.m20 = left.m20 + right.m20;
			result.m21 = left.m21 + right.m21;
			result.m22 = left.m22 + right.m22;
			result.m23 = left.m23 + right.m23;

			result.m30 = left.m30 + right.m30;
			result.m31 = left.m31 + right.m31;
			result.m32 = left.m32 + right.m32;
			result.m33 = left.m33 + right.m33;

			return result;
		}

		/// <summary>
		///		Used to subtract two matrices.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 Subtract( Matrix4 left, Matrix4 right )
		{
			return left - right;
		}

		/// <summary>
		///		Used to subtract two matrices.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static Matrix4 operator -( Matrix4 left, Matrix4 right )
		{
			Matrix4 result = new Matrix4();

			result.m00 = left.m00 - right.m00;
			result.m01 = left.m01 - right.m01;
			result.m02 = left.m02 - right.m02;
			result.m03 = left.m03 - right.m03;

			result.m10 = left.m10 - right.m10;
			result.m11 = left.m11 - right.m11;
			result.m12 = left.m12 - right.m12;
			result.m13 = left.m13 - right.m13;

			result.m20 = left.m20 - right.m20;
			result.m21 = left.m21 - right.m21;
			result.m22 = left.m22 - right.m22;
			result.m23 = left.m23 - right.m23;

			result.m30 = left.m30 - right.m30;
			result.m31 = left.m31 - right.m31;
			result.m32 = left.m32 - right.m32;
			result.m33 = left.m33 - right.m33;

			return result;
		}

		/// <summary>
		/// Compares two Matrix4 instances for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns>true if the Matrix 4 instances are equal, false otherwise.</returns>
		public static bool operator ==( Matrix4 left, Matrix4 right )
		{
			if(
				left.m00 == right.m00 && left.m01 == right.m01 && left.m02 == right.m02 && left.m03 == right.m03 &&
				left.m10 == right.m10 && left.m11 == right.m11 && left.m12 == right.m12 && left.m13 == right.m13 &&
				left.m20 == right.m20 && left.m21 == right.m21 && left.m22 == right.m22 && left.m23 == right.m23 &&
				left.m30 == right.m30 && left.m31 == right.m31 && left.m32 == right.m32 && left.m33 == right.m33 )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Compares two Matrix4 instances for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns>true if the Matrix 4 instances are not equal, false otherwise.</returns>
		public static bool operator !=( Matrix4 left, Matrix4 right )
		{
			return !( left == right );
		}

		/// <summary>
		///		Used to allow assignment from a Matrix3 to a Matrix4 object.
		/// </summary>
		/// <param name="right"></param>
		/// <returns></returns>
		public static implicit operator Matrix4( Matrix3 right )
		{
			Matrix4 result = Matrix4.Identity;

			result.m00 = right.m00;
			result.m01 = right.m01;
			result.m02 = right.m02;
			result.m10 = right.m10;
			result.m11 = right.m11;
			result.m12 = right.m12;
			result.m20 = right.m20;
			result.m21 = right.m21;
			result.m22 = right.m22;

			return result;
		}

		/// <summary>
		///    Allows the Matrix to be accessed like a 2d array (i.e. matrix[2,3])
		/// </summary>
		/// <remarks>
		///    This indexer is only provided as a convenience, and is <b>not</b> recommended for use in
		///    intensive applications.  
		/// </remarks>
		public Real this[ int row, int col ]
		{
			get
			{
				//Debug.Assert((row >= 0 && row < 4) && (col >= 0 && col < 4), "Attempt to access Matrix4 indexer out of bounds.");

				unsafe
				{
					fixed( Real* pM = &m00 )
					{
						return *( pM + ( ( 4 * row ) + col ) );
					}
				}
			}
			set
			{
				//Debug.Assert((row >= 0 && row < 4) && (col >= 0 && col < 4), "Attempt to access Matrix4 indexer out of bounds.");

				unsafe
				{
					fixed( Real* pM = &m00 )
					{
						*( pM + ( ( 4 * row ) + col ) ) = value;
					}
				}
			}
		}

		/// <summary>
		///		Allows the Matrix to be accessed linearly (m[0] -> m[15]).  
		/// </summary>
		/// <remarks>
		///    This indexer is only provided as a convenience, and is <b>not</b> recommended for use in
		///    intensive applications.  
		/// </remarks>
		public Real this[ int index ]
		{
			get
			{
				//Debug.Assert(index >= 0 && index < 16, "Attempt to access Matrix4 linear indexer out of bounds.");

				unsafe
				{
					fixed( Real* pMatrix = &this.m00 )
					{
						return *( pMatrix + index );
					}
				}
			}
			set
			{
				//Debug.Assert(index >= 0 && index < 16, "Attempt to access Matrix4 linear indexer out of bounds.");

				unsafe
				{
					fixed( Real* pMatrix = &this.m00 )
					{
						*( pMatrix + index ) = value;
					}
				}
			}
		}

		#endregion

		#region Object overloads

		/// <summary>
		///		Overrides the Object.ToString() method to provide a text representation of 
		///		a Matrix4.
		/// </summary>
		/// <returns>A string representation of a vector3.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat( " | {0} {1} {2} {3} |\n", this.m00, this.m01, this.m02, this.m03 );
			sb.AppendFormat( " | {0} {1} {2} {3} |\n", this.m10, this.m11, this.m12, this.m13 );
			sb.AppendFormat( " | {0} {1} {2} {3} |\n", this.m20, this.m21, this.m22, this.m23 );
			sb.AppendFormat( " | {0} {1} {2} {3} |\n", this.m30, this.m31, this.m32, this.m33 );

			return sb.ToString();
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
			int hashCode = 0;

			unsafe
			{
				fixed( Real* pM = &m00 )
				{
					for( int i = 0; i < 16; i++ )
					{
						hashCode ^= (int)( *( pM + i ) );
					}
				}
			}

			return hashCode;
		}

		/// <summary>
		///		Compares this Matrix to another object.  This should be done because the 
		///		equality operators (==, !=) have been overriden by this class.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return obj is Matrix4 && this == (Matrix4)obj;
		}

		#endregion
	}
}
