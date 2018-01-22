using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Mat3x3 : IMatrix
	{
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
		public unsafe double this[int row, int column]
		{
			get { return this[3 * row + column]; }
			set { this[3 * row + column] = value; }
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
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }

		public unsafe Vec3 GetRow(int index)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = 3 * index + ptr;
					return new Vec3(*(p++), *(p++), *(p++));
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetRow(int index, Vec3 row)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = 3 * index + ptr;
					*(p++) = row.x; *(p++) = row.y; *(p++) = row.z;
				}
			else throw new IndexOutOfRangeException();
		}

		public unsafe Vec3 GetColumn(int index)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					return new Vec3(*p, *(p + 3), *(p + 6));
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetColumn(int index, Vec3 column)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					*p = column.x; *(p + 3) = column.y; *(p + 6) = column.z;
				}
			else throw new IndexOutOfRangeException();
		}


		public static bool operator ==(Mat3x3 lhs, Mat3x3 rhs) { return lhs.Equals(rhs); }
		public static bool operator !=(Mat3x3 lhs, Mat3x3 rhs) { return !lhs.Equals(rhs); }

		public static bool IsNaM(Mat3x3 m)
		{
			return double.IsNaN(m.m00) || double.IsNaN(m.m01) || double.IsNaN(m.m02)
				|| double.IsNaN(m.m10) || double.IsNaN(m.m11) || double.IsNaN(m.m12)
				|| double.IsNaN(m.m20) || double.IsNaN(m.m21) || double.IsNaN(m.m22);
		}

		public static Mat3x3 operator ~(Mat3x3 m)
		{
			Mat3x3 nm = new Mat3x3();
			nm.m00 = m.m00; nm.m01 = m.m10; nm.m02 = m.m20;
			nm.m10 = m.m01; nm.m11 = m.m11; nm.m12 = m.m21;
			nm.m20 = m.m02; nm.m21 = m.m12; nm.m22 = m.m22;
			return nm;
		}

		public static Mat3x3 operator *(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
			m.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
			m.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;
			m.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
			m.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
			m.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;
			m.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
			m.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
			m.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;
			return m;
		}

		public static Vec3 operator *(Mat3x3 lhs, Vec3 rhs)
		{
			Vec3 v = new Vec3();
			v.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			v.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			v.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
			return v;
		}
		public static Vec2 operator *(Mat3x3 lhs, Vec2 rhs)
		{
			Vec2 v = new Vec2();
			v.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02;
			v.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12;
			return v;
		}

		public static double Determinant(Mat3x3 m)
		{
			return m.m00 * m.m11 * m.m22 - m.m00 * m.m12 * m.m21
				 + m.m01 * m.m12 * m.m20 - m.m01 * m.m10 * m.m22
				 + m.m02 * m.m10 * m.m21 - m.m02 * m.m11 * m.m20;
		}

		public static Mat3x3 Translate(Vec2 delta)
		{
			Mat3x3 m = new Mat3x3();
			m.m00 = m.m11 = m.m22 = 1;
			m.m02 = delta.x;
			m.m12 = delta.y;
			return m;
		}

		public static Mat3x3 Rotate(double angle)
		{
			Mat3x3 m = new Mat3x3();
			m.m22 = 1;
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);
			m.m00 = cos;
			m.m01 = -sin;
			m.m10 = sin;
			m.m11 = cos;
			return m;
		}

		public static Mat3x3 Scale(Vec2 scale)
		{
			Mat3x3 m = new Mat3x3();
			m.m22 = 1;
			m.m00 = scale.x;
			m.m11 = scale.y;
			return m;
		}

		public static Mat3x3 TRS(Vec2 delta, double angle, Vec2 scale)
		{
			return Translate(delta) * Rotate(angle) * Scale(scale);
		}

		public static Mat3x3 zero { get { return new Mat3x3(); } }
		public static Mat3x3 identity { get { return new Mat3x3(1, 0, 0, 0, 1, 0, 0, 0, 1); } }
	}
}
