using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Mat4x4 : IMatrix
	{
		public double m00;
		public double m01;
		public double m02;
		public double m03;
		public double m10;
		public double m11;
		public double m12;
		public double m13;
		public double m20;
		public double m21;
		public double m22;
		public double m23;
		public double m30;
		public double m31;
		public double m32;
		public double m33;

		public unsafe double this[int index]
		{
			get
			{
				if (index >= 0 && index < 16)
					fixed (double* ptr = &m00) return *(ptr + index);
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 16)
					fixed (double* ptr = &m00) *(ptr + index) = value;
				else throw new IndexOutOfRangeException();
			}
		}
		public unsafe double this[int row, int column]
		{
			get { return this[4 * row + column]; }
			set { this[4 * row + column] = value; }
		}

		public Mat4x4(double m00, double m01, double m02, double m03,
			double m10, double m11, double m12, double m13,
			double m20, double m21, double m22, double m23,
			double m30, double m31, double m32, double m33)
		{
			this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
			this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
			this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
			this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
		}
		public Mat4x4(double[] m)
		{
			this.m00 = m[0]; this.m01 = m[1]; this.m02 = m[2]; this.m03 = m[3];
			this.m10 = m[4]; this.m11 = m[5]; this.m12 = m[6]; this.m13 = m[7];
			this.m20 = m[8]; this.m21 = m[9]; this.m22 = m[10]; this.m23 = m[11];
			this.m30 = m[12]; this.m31 = m[13]; this.m32 = m[14]; this.m33 = m[15];
		}
		public Mat4x4(Vec4 column0, Vec4 column1, Vec4 column2, Vec4 column3)
		{
			this.m00 = column0.x; this.m01 = column1.x; this.m02 = column2.x; this.m03 = column3.x;
			this.m10 = column0.y; this.m11 = column1.y; this.m12 = column2.y; this.m13 = column3.y;
			this.m20 = column0.z; this.m21 = column1.z; this.m22 = column2.z; this.m23 = column3.z;
			this.m30 = column0.w; this.m31 = column1.w; this.m32 = column2.w; this.m33 = column3.w;
		}
		
		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(m00.ToString(format)).Append("\t")
				.Append(m01.ToString(format)).Append("\t")
				.Append(m02.ToString(format)).Append("\t")
				.Append(m03.ToString(format)).Append("\t|\n|\t")
				.Append(m10.ToString(format)).Append("\t")
				.Append(m11.ToString(format)).Append("\t")
				.Append(m12.ToString(format)).Append("\t")
				.Append(m13.ToString(format)).Append("\t|\n|\t")
				.Append(m20.ToString(format)).Append("\t")
				.Append(m21.ToString(format)).Append("\t")
				.Append(m22.ToString(format)).Append("\t")
				.Append(m23.ToString(format)).Append("\t|\n|\t")
				.Append(m30.ToString(format)).Append("\t")
				.Append(m31.ToString(format)).Append("\t")
				.Append(m32.ToString(format)).Append("\t")
				.Append(m33.ToString(format)).Append("\t|");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }

		public unsafe Vec4 GetRow(int index)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = 4 * index + ptr;
					return new Vec4(*(p++), *(p++), *(p++), *(p++));
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetRow(int index, Vec4 row)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = 4 * index + ptr;
					*(p++) = row.x; *(p++) = row.y; *(p++) = row.z; *(p++) = row.w;
				}
			else throw new IndexOutOfRangeException();
		}

		public unsafe Vec4 GetColumn(int index)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					return new Vec4(*p, *(p + 4), *(p + 8), *(p + 12));
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetColumn(int index, Vec4 column)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					*p = column.x; *(p + 4) = column.y; *(p + 8) = column.z; *(p + 12) = column.w;
				}
			else throw new IndexOutOfRangeException();
		}


		public static bool operator ==(Mat4x4 lhs, Mat4x4 rhs) { return lhs.Equals(rhs); }
		public static bool operator !=(Mat4x4 lhs, Mat4x4 rhs) { return !lhs.Equals(rhs); }

		public static bool IsNaM(Mat4x4 m)
		{
			return double.IsNaN(m.m00) || double.IsNaN(m.m01) || double.IsNaN(m.m02) || double.IsNaN(m.m03)
				|| double.IsNaN(m.m10) || double.IsNaN(m.m11) || double.IsNaN(m.m12) || double.IsNaN(m.m13)
				|| double.IsNaN(m.m20) || double.IsNaN(m.m21) || double.IsNaN(m.m22) || double.IsNaN(m.m23)
				|| double.IsNaN(m.m30) || double.IsNaN(m.m31) || double.IsNaN(m.m32) || double.IsNaN(m.m33);
		}

		/// <summary>
		/// Transpose
		/// </summary>
		public static Mat4x4 operator ~(Mat4x4 m)
		{
			Mat4x4 nm = new Mat4x4();
			nm.m00 = m.m00; nm.m01 = m.m10; nm.m02 = m.m20; nm.m03 = m.m30;
			nm.m10 = m.m01; nm.m11 = m.m11; nm.m12 = m.m21; nm.m13 = m.m31;
			nm.m20 = m.m02; nm.m21 = m.m12; nm.m22 = m.m22; nm.m23 = m.m32;
			nm.m30 = m.m03; nm.m31 = m.m13; nm.m32 = m.m23; nm.m33 = m.m33;
			return nm;
		}

		public static Mat4x4 operator *(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			m.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			m.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			m.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			m.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			m.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			m.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			m.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			m.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			m.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			m.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			m.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			m.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			m.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			m.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			m.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return m;
		}

		public static Vec4 operator *(Mat4x4 lhs, Vec4 rhs)
		{
			Vec4 v = new Vec4();
			v.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z + lhs.m03 * rhs.w;
			v.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z + lhs.m13 * rhs.w;
			v.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z + lhs.m23 * rhs.w;
			v.w = lhs.m30 * rhs.x + lhs.m31 * rhs.y + lhs.m32 * rhs.z + lhs.m33 * rhs.w;
			return v;
		}
		public static Vec3 operator *(Mat4x4 lhs, Vec3 rhs)
		{
			Vec3 v = new Vec3();
			v.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z + lhs.m03;
			v.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z + lhs.m13;
			v.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z + lhs.m23;
			return v;
		}

		public static double Determinant(Mat4x4 m)
		{
			return m.m00 * m.m11 * m.m22 * m.m33 - m.m00 * m.m13 * m.m22 * m.m31 + m.m01 * m.m10 * m.m23 * m.m32 - m.m01 * m.m12 * m.m23 * m.m30
				 + m.m02 * m.m13 * m.m20 * m.m31 - m.m02 * m.m11 * m.m20 * m.m33 + m.m03 * m.m12 * m.m21 * m.m30 - m.m03 * m.m10 * m.m21 * m.m32
				 + m.m00 * m.m12 * m.m23 * m.m31 - m.m00 * m.m11 * m.m23 * m.m32 + m.m02 * m.m10 * m.m21 * m.m33 - m.m02 * m.m13 * m.m21 * m.m30
				 + m.m03 * m.m11 * m.m20 * m.m32 - m.m03 * m.m12 * m.m20 * m.m31 + m.m01 * m.m13 * m.m22 * m.m30 - m.m01 * m.m10 * m.m22 * m.m33
				 + m.m00 * m.m13 * m.m21 * m.m32 - m.m00 * m.m12 * m.m21 * m.m33 + m.m03 * m.m10 * m.m22 * m.m31 - m.m03 * m.m11 * m.m22 * m.m30
				 + m.m01 * m.m12 * m.m20 * m.m33 - m.m01 * m.m13 * m.m20 * m.m32 + m.m02 * m.m11 * m.m23 * m.m30 - m.m02 * m.m10 * m.m23 * m.m31;
		}

		public static Mat4x4 Translate(Vec3 delta)
		{
			Mat4x4 m = new Mat4x4();
			m.m00 = m.m11 = m.m22 = m.m33 = 1;
			m.m03 = delta.x;
			m.m13 = delta.y;
			m.m23 = delta.z;
			return m;
		}

		public static Mat4x4 RotateX(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.m00 = m.m33 = 1;
			double cx = Math.Cos(angle);
			double sx = Math.Sin(angle);
			m.m11 = cx;
			m.m12 = -sx;
			m.m21 = sx;
			m.m22 = cx;
			return m;
		}
		public static Mat4x4 RotateY(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.m11 = m.m33 = 1;
			double cy = Math.Cos(angle);
			double sy = Math.Sin(angle);
			m.m00 = cy;
			m.m02 = sy;
			m.m20 = -sy;
			m.m22 = cy;
			return m;
		}
		public static Mat4x4 RotateZ(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.m22 = m.m33 = 1;
			double cz = Math.Cos(angle);
			double sz = Math.Sin(angle);
			m.m00 = cz;
			m.m01 = -sz;
			m.m10 = sz;
			m.m11 = cz;
			return m;
		}

		/// <summary>
		/// RY * RX * RZ
		/// </summary>
		public static Mat4x4 Rotate(Vec3 euler)
		{
			Mat4x4 m = new Mat4x4();
			m.m33 = 1;
			double cz = Math.Cos(euler.z);
			double sz = Math.Sin(euler.z);
			double cx = Math.Cos(euler.x);
			double sx = Math.Sin(euler.x);
			double cy = Math.Cos(euler.y);
			double sy = Math.Sin(euler.y);
			m.m00 = cy * cz + sx * sy * sz;
			m.m01 = -cy * sz + sx * sy * cz;
			m.m02 = cx * sy;
			m.m10 = cx * sz;
			m.m11 = cx * cz;
			m.m12 = -sx;
			m.m20 = -sy * cz + sx * cy * sz;
			m.m21 = sy * sz + sx * cy * cz;
			m.m22 = cx * cy;
			return m;
		}
		public static Mat4x4 Rotate(double angle, Vec3 axisNorm)
		{
			Mat4x4 m = new Mat4x4();
			m.m33 = 1;
			double ax = axisNorm.x, ay = axisNorm.y, az = axisNorm.z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			m.m00 = ax * ax + (1 - ax * ax) * cos;
			m.m01 = ax * ay * (1 - cos) - az * sin;
			m.m02 = ax * az * (1 - cos) + ay * sin;
			m.m10 = ay * ax * (1 - cos) + az * sin;
			m.m11 = ay * ay + (1 - ay * ay) * cos;
			m.m12 = ay * az * (1 - cos) - ax * sin;
			m.m20 = az * ax * (1 - cos) - ay * sin;
			m.m21 = az * ay * (1 - cos) + ax * sin;
			m.m22 = az * az + (1 - az * az) * cos;
			return m;
		}
		public static Mat4x4 Rotate(Quat rotation)
		{
			double angle;
			Vec3 axisNorm;
			Quat.ToAngleAxis(rotation, out angle, out axisNorm);
			return Rotate(angle, axisNorm);
		}

		public static Mat4x4 Scale(Vec3 scale)
		{
			Mat4x4 m = new Mat4x4();
			m.m33 = 1;
			m.m00 = scale.x;
			m.m11 = scale.y;
			m.m22 = scale.z;
			return m;
		}

		public static Mat4x4 TRS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * Rotate(euler) * Scale(scale);
		}
		public static Mat4x4 TRS(Vec3 delta, double angle, Vec3 axisNorm, Vec3 scale)
		{
			return Translate(delta) * Rotate(angle, axisNorm) * Scale(scale);
		}
		public static Mat4x4 TRS(Vec3 delta, Quat rotation, Vec3 scale)
		{
			return Translate(delta) * Rotate(rotation) * Scale(scale);
		}

		public static Mat4x4 zero { get { return new Mat4x4(); } }
		public static Mat4x4 identity { get { return new Mat4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1); } }
	}
}
