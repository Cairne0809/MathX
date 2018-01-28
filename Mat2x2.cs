using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Mat2x2 : IMatrix
	{
		public int row { get { return 2; } }
		public int column { get { return 2; } }

		public double m00;
		public double m01;
		public double m10;
		public double m11;

		public unsafe double this[int index]
		{
			get
			{
				if (index >= 0 && index < 4)
					fixed (double* ptr = &m00) return *(ptr + index);
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 4)
					fixed (double* ptr = &m00) *(ptr + index) = value;
				else throw new IndexOutOfRangeException();
			}
		}
		public double this[int row, int column]
		{
			get { return this[2 * row + column]; }
			set { this[2 * row + column] = value; }
		}

		public Mat2x2(double m00, double m01, double m10, double m11)
		{
			this.m00 = m00; this.m01 = m01;
			this.m10 = m10; this.m11 = m11;
		}
		public Mat2x2(double[] m)
		{
			this.m00 = m[0]; this.m01 = m[1];
			this.m10 = m[2]; this.m11 = m[3];
		}
		public Mat2x2(Vec2 column0, Vec2 column1)
		{
			this.m00 = column0.x; this.m01 = column1.x;
			this.m10 = column0.y; this.m11 = column1.y;
		}
		public Mat2x2(Mat2x2 m)
		{
			this.m00 = m.m00; this.m01 = m.m01;
			this.m10 = m.m10; this.m11 = m.m11;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(m00.ToString(format)).Append("\t")
				.Append(m01.ToString(format)).Append("\t|\n|\t")
				.Append(m10.ToString(format)).Append("\t")
				.Append(m11.ToString(format)).Append("\t|");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }

		public Vec2 GetRow(int index)
		{
			switch (index)
			{
				case 0: return new Vec2(m00, m01);
				case 1: return new Vec2(m10, m11);
				default: throw new IndexOutOfRangeException();
			}
		}
		public void SetRow(int index, Vec2 row)
		{
			switch (index)
			{
				case 0: m00 = row.x; m01 = row.y; break;
				case 1: m10 = row.x; m11 = row.y; break;
				default: throw new IndexOutOfRangeException();
			}
		}

		public Vec2 GetColumn(int index)
		{
			switch (index)
			{
				case 0: return new Vec2(m00, m10);
				case 1: return new Vec2(m01, m11);
				default: throw new IndexOutOfRangeException();
			}
		}
		public void SetColumn(int index, Vec2 column)
		{
			switch (index)
			{
				case 0: m00 = column.x; m10 = column.y; break;
				case 1: m01 = column.x; m11 = column.y; break;
				default: throw new IndexOutOfRangeException();
			}
		}

		
		public static bool IsNaM(Mat2x2 m)
		{
			return double.IsNaN(m.m00) || double.IsNaN(m.m01)
				|| double.IsNaN(m.m10) || double.IsNaN(m.m11);
		}

		public static Mat2x2 operator *(Mat2x2 lhs, Mat2x2 rhs)
		{
			Mat2x2 nm;
			nm.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10;
			nm.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11;
			nm.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10;
			nm.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11;
			return nm;
		}

		public static Vec2 operator *(Mat2x2 lhs, Vec2 rhs)
		{
			Vec2 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y;
			return nv;
		}

		public static Mat2x2 Transpose(Mat2x2 m)
		{
			Mat2x2 nm;
			nm.m00 = m.m00; nm.m01 = m.m10;
			nm.m10 = m.m01; nm.m11 = m.m11;
			return nm;
		}

		public static double Determinant(Mat2x2 m)
		{
			return m.m00 * m.m11 - m.m01 * m.m10;
		}

		public static Mat2x2 Rotate(double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			return new Mat2x2(
				cos, -sin,
				sin, cos);
		}

		public static Mat2x2 Scale(Vec2 scale)
		{
			return new Mat2x2(
				scale.x, 0,
				0, scale.y);
		}

		public static readonly Mat2x2 zero = new Mat2x2();
		public static readonly Mat2x2 identity = new Mat2x2(1, 0, 0, 1);
	}
}
