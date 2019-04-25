using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct SqMat3 : ISquareMatrix
	{
		public int Row { get { return 3; } }
		public int Column { get { return 3; } }
		public int Length { get { return 9; } }

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
					fixed (double* ptr = &m00) return ptr[index];
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 9)
					fixed (double* ptr = &m00) ptr[index] = value;
				else throw new IndexOutOfRangeException();
			}
		}
		public double this[int row, int column]
		{
			get { return this[column + row * 3]; }
			set { this[column + row * 3] = value; }
		}

		public SqMat3(
			double m00, double m01, double m02,
			double m10, double m11, double m12,
			double m20, double m21, double m22)
		{
			this.m00 = m00; this.m01 = m01; this.m02 = m02;
			this.m10 = m10; this.m11 = m11; this.m12 = m12;
			this.m20 = m20; this.m21 = m21; this.m22 = m22;
		}
		public SqMat3(double[] m)
		{
			this.m00 = m[0]; this.m01 = m[1]; this.m02 = m[2];
			this.m10 = m[3]; this.m11 = m[4]; this.m12 = m[5];
			this.m20 = m[6]; this.m21 = m[7]; this.m22 = m[8];
		}
		public SqMat3(Vec3 r0, Vec3 r1, Vec3 r2)
		{
			this.m00 = r0.x; this.m01 = r0.y; this.m02 = r0.z;
			this.m10 = r1.x; this.m11 = r1.y; this.m12 = r1.z;
			this.m20 = r2.x; this.m21 = r2.y; this.m22 = r2.z;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("| ")
				.Append(m00.ToString(format)).Append(", ")
				.Append(m01.ToString(format)).Append(", ")
				.Append(m02.ToString(format)).Append(" |\n| ")
				.Append(m10.ToString(format)).Append(", ")
				.Append(m11.ToString(format)).Append(", ")
				.Append(m12.ToString(format)).Append(" |\n| ")
				.Append(m20.ToString(format)).Append(", ")
				.Append(m21.ToString(format)).Append(", ")
				.Append(m22.ToString(format)).Append(" |");
			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString(MathX.ToleranceFormat);
		}

		public unsafe Vec3 GetRow(int index)
		{
			if (index >= 0 && index < 3)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index * 3;
					Vec3 nv;
					nv.x = p[0];
					nv.y = p[1];
					nv.z = p[2];
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
					p[0] = row.x;
					p[1] = row.y;
					p[2] = row.z;
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
					nv.x = p[0];
					nv.y = p[3];
					nv.z = p[6];
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
					p[0] = column.x;
					p[3] = column.y;
					p[6] = column.z;
				}
			else throw new IndexOutOfRangeException();
		}

		public SqMat3 Inverse()
		{
			return new SqMat3(
				this[0, 0], this[0, 1], -VecX.Dot(GetColumn(0).xy, GetColumn(2).xy),
				this[1, 0], this[1, 1], -VecX.Dot(GetColumn(1).xy, GetColumn(2).xy),
				0, 0, 1);
		}


		public static SqMat3 operator *(SqMat3 lhs, SqMat3 rhs)
		{
			SqMat3 nm;
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

		public static Vec3 operator *(SqMat3 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			nv.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
			return nv;
		}
		public static Vec2 operator *(SqMat3 lhs, Vec2 rhs)
		{
			Vec2 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12;
			return nv;
		}

		public static double Determinant(SqMat3 m)
		{
			return m.m00 * m.m11 * m.m22 - m.m00 * m.m12 * m.m21
				 + m.m01 * m.m12 * m.m20 - m.m01 * m.m10 * m.m22
				 + m.m02 * m.m10 * m.m21 - m.m02 * m.m11 * m.m20;
		}

		public static SqMat3 Translate(Vec2 delta)
		{
			return new SqMat3(
				1, 0, delta.x,
				0, 1, delta.y,
				0, 0, 1);
		}

		public static SqMat3 Rotate(double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			return new SqMat3(
				cos, -sin, 0,
				sin, cos, 0,
				0, 0, 1);
		}

		public static SqMat3 Scale(Vec2 scale)
		{
			return new SqMat3(
				scale.x, 0, 0,
				0, scale.y, 0,
				0, 0, 1);
		}

		public static SqMat3 TRS(Vec2 delta, double angle, Vec2 scale)
		{
			return Translate(delta) * Rotate(angle) * Scale(scale);
		}

		public static readonly SqMat3 zero = new SqMat3();
		public static readonly SqMat3 identity = new SqMat3(1, 0, 0, 0, 1, 0, 0, 0, 1);
	}
}
