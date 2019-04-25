using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct SqMat4 : ISquareMatrix
	{
		public int Row { get { return 4; } }
		public int Column { get { return 4; } }
		public int Length { get { return 16; } }

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
					fixed (double* ptr = &m00) return ptr[index];
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 16)
					fixed (double* ptr = &m00) ptr[index] = value;
				else throw new IndexOutOfRangeException();
			}
		}
		public double this[int row, int column]
		{
			get { return this[column + row * 4]; }
			set { this[column + row * 4] = value; }
		}

		public SqMat4(double m00, double m01, double m02, double m03,
			double m10, double m11, double m12, double m13,
			double m20, double m21, double m22, double m23,
			double m30, double m31, double m32, double m33)
		{
			this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
			this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
			this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
			this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
		}
		public SqMat4(double[] m)
		{
			this.m00 = m[0]; this.m01 = m[1]; this.m02 = m[2]; this.m03 = m[3];
			this.m10 = m[4]; this.m11 = m[5]; this.m12 = m[6]; this.m13 = m[7];
			this.m20 = m[8]; this.m21 = m[9]; this.m22 = m[10]; this.m23 = m[11];
			this.m30 = m[12]; this.m31 = m[13]; this.m32 = m[14]; this.m33 = m[15];
		}
		public SqMat4(Vec4 r0, Vec4 r1, Vec4 r2, Vec4 r3)
		{
			this.m00 = r0.x; this.m01 = r0.y; this.m02 = r0.z; this.m03 = r0.w;
			this.m10 = r1.x; this.m11 = r1.y; this.m12 = r1.z; this.m13 = r1.w;
			this.m20 = r2.x; this.m21 = r2.y; this.m22 = r2.z; this.m23 = r2.w;
			this.m30 = r3.x; this.m31 = r3.y; this.m32 = r3.z; this.m33 = r3.w;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("| ")
				.Append(m00.ToString(format)).Append(", ")
				.Append(m01.ToString(format)).Append(", ")
				.Append(m02.ToString(format)).Append(", ")
				.Append(m03.ToString(format)).Append(" |\n| ")
				.Append(m10.ToString(format)).Append(", ")
				.Append(m11.ToString(format)).Append(", ")
				.Append(m12.ToString(format)).Append(", ")
				.Append(m13.ToString(format)).Append(" |\n| ")
				.Append(m20.ToString(format)).Append(", ")
				.Append(m21.ToString(format)).Append(", ")
				.Append(m22.ToString(format)).Append(", ")
				.Append(m23.ToString(format)).Append(" |\n| ")
				.Append(m30.ToString(format)).Append(", ")
				.Append(m31.ToString(format)).Append(", ")
				.Append(m32.ToString(format)).Append(", ")
				.Append(m33.ToString(format)).Append(" |");
			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString(MathX.ToleranceFormat);
		}

		public unsafe Vec4 GetRow(int index)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index * 4;
					Vec4 nv;
					nv.x = p[0];
					nv.y = p[1];
					nv.z = p[2];
					nv.w = p[3];
					return nv;
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetRow(int index, Vec4 row)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index * 4;
					p[0] = row.x;
					p[1] = row.y;
					p[2] = row.z;
					p[3] = row.w;
				}
			else throw new IndexOutOfRangeException();
		}

		public unsafe Vec4 GetColumn(int index)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					Vec4 nv;
					nv.x = p[0];
					nv.y = p[4];
					nv.z = p[8];
					nv.w = p[12];
					return nv;
				}
			else throw new IndexOutOfRangeException();
		}
		public unsafe void SetColumn(int index, Vec4 column)
		{
			if (index >= 0 && index < 4)
				fixed (double* ptr = &m00)
				{
					double* p = ptr + index;
					p[0] = column.x;
					p[4] = column.y;
					p[8] = column.z;
					p[12] = column.w;
				}
			else throw new IndexOutOfRangeException();
		}

		public SqMat4 Inverse()
		{
			Vec4 r0 = LinearEquation.SolveEquations(
				new LnEqn<Vec4>(GetColumn(0), 1),
				new LnEqn<Vec4>(GetColumn(1), 0),
				new LnEqn<Vec4>(GetColumn(2), 0),
				new LnEqn<Vec4>(GetColumn(3), 0));
			Vec4 r1 = LinearEquation.SolveEquations(
				new LnEqn<Vec4>(GetColumn(0), 0),
				new LnEqn<Vec4>(GetColumn(1), 1),
				new LnEqn<Vec4>(GetColumn(2), 0),
				new LnEqn<Vec4>(GetColumn(3), 0));
			Vec4 r2 = LinearEquation.SolveEquations(
				new LnEqn<Vec4>(GetColumn(0), 0),
				new LnEqn<Vec4>(GetColumn(1), 0),
				new LnEqn<Vec4>(GetColumn(2), 1),
				new LnEqn<Vec4>(GetColumn(3), 0));
			return new SqMat4(r0, r1, r2, new Vec4(0, 0, 0, 1));
		}


		public static SqMat4 operator *(SqMat4 lhs, SqMat4 rhs)
		{
			SqMat4 nm;
			nm.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			nm.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			nm.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			nm.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			nm.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			nm.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			nm.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			nm.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			nm.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			nm.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			nm.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			nm.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			nm.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			nm.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			nm.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			nm.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return nm;
		}

		public static Vec4 operator *(SqMat4 lhs, Vec4 rhs)
		{
			Vec4 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z + lhs.m03 * rhs.w;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z + lhs.m13 * rhs.w;
			nv.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z + lhs.m23 * rhs.w;
			nv.w = lhs.m30 * rhs.x + lhs.m31 * rhs.y + lhs.m32 * rhs.z + lhs.m33 * rhs.w;
			return nv;
		}
		public static Vec3 operator *(SqMat4 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z + lhs.m03;
			nv.y = lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z + lhs.m13;
			nv.z = lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z + lhs.m23;
			return nv;
		}

		public static double Determinant(SqMat4 m)
		{
			return m.m00 * m.m11 * m.m22 * m.m33 - m.m00 * m.m13 * m.m22 * m.m31
				 + m.m01 * m.m10 * m.m23 * m.m32 - m.m01 * m.m12 * m.m23 * m.m30
				 + m.m02 * m.m13 * m.m20 * m.m31 - m.m02 * m.m11 * m.m20 * m.m33
				 + m.m03 * m.m12 * m.m21 * m.m30 - m.m03 * m.m10 * m.m21 * m.m32
				 + m.m00 * m.m12 * m.m23 * m.m31 - m.m00 * m.m11 * m.m23 * m.m32
				 + m.m02 * m.m10 * m.m21 * m.m33 - m.m02 * m.m13 * m.m21 * m.m30
				 + m.m03 * m.m11 * m.m20 * m.m32 - m.m03 * m.m12 * m.m20 * m.m31
				 + m.m01 * m.m13 * m.m22 * m.m30 - m.m01 * m.m10 * m.m22 * m.m33
				 + m.m00 * m.m13 * m.m21 * m.m32 - m.m00 * m.m12 * m.m21 * m.m33
				 + m.m03 * m.m10 * m.m22 * m.m31 - m.m03 * m.m11 * m.m22 * m.m30
				 + m.m01 * m.m12 * m.m20 * m.m33 - m.m01 * m.m13 * m.m20 * m.m32
				 + m.m02 * m.m11 * m.m23 * m.m30 - m.m02 * m.m10 * m.m23 * m.m31;
		}

		public static SqMat4 Translate(Vec3 delta)
		{
			return new SqMat4(
				1, 0, 0, delta.x,
				0, 1, 0, delta.y,
				0, 0, 1, delta.z,
				0, 0, 0, 1);
		}

		public static SqMat4 RotateX(double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			return new SqMat4(
				1, 0, 0, 0,
				0, cos, -sin, 0,
				0, sin, cos, 0,
				0, 0, 0, 1);
		}
		public static SqMat4 RotateY(double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			return new SqMat4(
				cos, 0, sin, 0,
				0, 1, 0, 0,
				-sin, 0, cos, 0,
				0, 0, 0, 1);
		}
		public static SqMat4 RotateZ(double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			return new SqMat4(
				cos, -sin, 0, 0,
				sin, cos, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1);
		}

		/// <summary>
		/// RY * RX * RZ
		/// </summary>
		public static SqMat4 Rotate(Vec3 euler)
		{
			double cz = Math.Cos(euler.z);
			double sz = Math.Sin(euler.z);
			double cx = Math.Cos(euler.x);
			double sx = Math.Sin(euler.x);
			double cy = Math.Cos(euler.y);
			double sy = Math.Sin(euler.y);
			return new SqMat4(
				cy * cz + sx * sy * sz, -cy * sz + sx * sy * cz, cx * sy, 0,
				cx * sz, cx * cz, -sx, 0,
				-sy * cz + sx * cy * sz, sy * sz + sx * cy * cz, cx * cy, 0,
				0, 0, 0, 1);
		}
		public static SqMat4 Rotate(double angle, Vec3 axisNorm)
		{
			double x = axisNorm.x, y = axisNorm.y, z = axisNorm.z;
			double xx = x * x, yy = y * y, zz = z * z;
			double xy = x * y, yz = y * z, xz = x * z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double _cos = 1 - cos;
			return new SqMat4(
				xx + (1 - xx) * cos, xy * _cos - z * sin, xz * _cos + y * sin, 0,
				xy * _cos + z * sin, yy + (1 - yy) * cos, yz * _cos - x * sin, 0,
				xz * _cos - y * sin, yz * _cos + x * sin, zz + (1 - zz) * cos, 0,
				0, 0, 0, 1);
		}

		public static SqMat4 Scale(Vec3 scale)
		{
			return new SqMat4(
				scale.x, 0, 0, 0,
				0, scale.y, 0, 0,
				0, 0, scale.z, 0,
				0, 0, 0, 1);
		}

		public static SqMat4 TRS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * Rotate(euler) * Scale(scale);
		}
		public static SqMat4 TRS(Vec3 delta, double angle, Vec3 axisNorm, Vec3 scale)
		{
			return Translate(delta) * Rotate(angle, axisNorm) * Scale(scale);
		}
		public static SqMat4 TRS(Vec3 delta, Quat rotation, Vec3 scale)
		{
			return Translate(delta) * Quat.ToMat4x4(rotation) * Scale(scale);
		}

		public static readonly SqMat4 zero = new SqMat4();
		public static readonly SqMat4 identity = new SqMat4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
	}
}
