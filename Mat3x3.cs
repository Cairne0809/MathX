using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Mat3x3 : IMatrix
	{
		public int row { get { return 3; } }
		public int column { get { return 3; } }

		public double m00;
		public double m01;
		public double m02;
		public double m10;
		public double m11;
		public double m12;
		public double m20;
		public double m21;
		public double m22;

		public unsafe double this[int index]
		{
			get
			{
				if (index >= 0 && index < 9)
					fixed (double* ptr = &m00) return *(ptr + index);
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 9)
					fixed (double* ptr = &m00) *(ptr + index) = value;
				else throw new IndexOutOfRangeException();
			}
		}
		public double this[int row, int column]
		{
			get { return this[column + row * 3]; }
			set { this[column + row * 3] = value; }
		}

		public Mat3x3(double m00, double m01, double m02,
			double m10, double m11, double m12,
			double m20, double m21, double m22)
		{
			this.m00 = m00; this.m01 = m01; this.m02 = m02;
			this.m10 = m10; this.m11 = m11; this.m12 = m12;
			this.m20 = m20; this.m21 = m21; this.m22 = m22;
		}
		public Mat3x3(double[] m)
		{
			this.m00 = m[0]; this.m01 = m[1]; this.m02 = m[2];
			this.m10 = m[3]; this.m11 = m[4]; this.m12 = m[5];
			this.m20 = m[6]; this.m21 = m[7]; this.m22 = m[8];
		}
		public Mat3x3(Vec3 column0, Vec3 column1, Vec3 column2)
		{
			this.m00 = column0.x; this.m01 = column1.x; this.m02 = column2.x;
			this.m10 = column0.y; this.m11 = column1.y; this.m12 = column2.y;
			this.m20 = column0.z; this.m21 = column1.z; this.m22 = column2.z;
		}
		public Mat3x3(Mat3x3 m)
		{
			this.m00 = m.m00; this.m01 = m.m01; this.m02 = m.m02;
			this.m10 = m.m10; this.m11 = m.m11; this.m12 = m.m12;
			this.m20 = m.m20; this.m21 = m.m21; this.m22 = m.m22;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(m00.ToString(format)).Append("\t")
				.Append(m01.ToString(format)).Append("\t")
				.Append(m02.ToString(format)).Append("\t|\n|\t")
				.Append(m10.ToString(format)).Append("\t")
				.Append(m11.ToString(format)).Append("\t")
				.Append(m12.ToString(format)).Append("\t|\n|\t")
				.Append(m20.ToString(format)).Append("\t")
				.Append(m21.ToString(format)).Append("\t")
				.Append(m22.ToString(format)).Append("\t|");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }

		public unsafe Vec3 GetRow(int index)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index * 3;
					Vec3 nv;
					nv.x = *p;
					nv.y = *(p + 1);
					nv.z = *(p + 2);
					return nv;
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetRow(int index, Vec3 row)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index * 3;
					*p = row.x;
					*(p + 1) = row.y;
					*(p + 2) = row.z;
				}
			else throw new IndexOutOfRangeException();
		}

		public unsafe Vec3 GetColumn(int index)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					Vec3 nv;
					nv.x = *p;
					nv.y = *(p + 3);
					nv.z = *(p + 6);
					return nv;
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetColumn(int index, Vec3 column)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					*p = column.x;
					*(p + 3) = column.y;
					*(p + 6) = column.z;
				}
			else throw new IndexOutOfRangeException();
		}

		
		public static bool IsNaM(Mat3x3 m)
		{
			return double.IsNaN(m.m00) || double.IsNaN(m.m01) || double.IsNaN(m.m02)
				|| double.IsNaN(m.m10) || double.IsNaN(m.m11) || double.IsNaN(m.m12)
				|| double.IsNaN(m.m20) || double.IsNaN(m.m21) || double.IsNaN(m.m22);
		}

		public static Mat3x3 operator *(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 nm;
			nm.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
			nm.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
			nm.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;
			nm.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
			nm.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
			nm.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;
			nm.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
			nm.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
			nm.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;
			return nm;
		}

		public static Vec3 operator *(Mat3x3 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			nv.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
			return nv;
		}
		public static Vec2 operator *(Mat3x3 lhs, Vec2 rhs)
		{
			Vec2 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12;
			return nv;
		}

		public static Mat3x3 Transpose(Mat3x3 m)
		{
			Mat3x3 nm;
			nm.m00 = m.m00; nm.m01 = m.m10; nm.m02 = m.m20;
			nm.m10 = m.m01; nm.m11 = m.m11; nm.m12 = m.m21;
			nm.m20 = m.m02; nm.m21 = m.m12; nm.m22 = m.m22;
			return nm;
		}

		public static double Determinant(Mat3x3 m)
		{
			return m.m00 * m.m11 * m.m22 - m.m00 * m.m12 * m.m21
				 + m.m01 * m.m12 * m.m20 - m.m01 * m.m10 * m.m22
				 + m.m02 * m.m10 * m.m21 - m.m02 * m.m11 * m.m20;
		}

		public static Mat3x3 Translate(Vec2 delta)
		{
			return new Mat3x3(
				1, 0, delta.x,
				0, 1, delta.y,
				0, 0, 1);
		}

		public static Mat3x3 Rotate(double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			return new Mat3x3(
				cos, -sin, 0,
				sin, cos, 0,
				0, 0, 1);
		}

		public static Mat3x3 Scale(Vec2 scale)
		{
			return new Mat3x3(
				scale.x, 0, 0,
				0, scale.y, 0,
				0, 0, 1);
		}

		public static Mat3x3 TRS(Vec2 delta, double angle, Vec2 scale)
		{
			return Translate(delta) * Rotate(angle) * Scale(scale);
		}

		public static readonly Mat3x3 zero = new Mat3x3();
		public static readonly Mat3x3 identity = new Mat3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);
	}
}
