using System;
using System.Text;

namespace MathematicsX
{
	public struct Mat4x4 : IMatrix
	{
		public double c0r0;
		public double c1r0;
		public double c2r0;
		public double c3r0;
		public double c0r1;
		public double c1r1;
		public double c2r1;
		public double c3r1;
		public double c0r2;
		public double c1r2;
		public double c2r2;
		public double c3r2;
		public double c0r3;
		public double c1r3;
		public double c2r3;
		public double c3r3;

		public double this[int c, int r]
		{
			get
			{
				int index = 4 * r + c;
				switch (index)
				{
					case 0: return c0r0;
					case 1: return c1r0;
					case 2: return c2r0;
					case 3: return c3r0;
					case 4: return c0r1;
					case 5: return c1r1;
					case 6: return c2r1;
					case 7: return c3r1;
					case 8: return c0r2;
					case 9: return c1r2;
					case 10: return c2r2;
					case 11: return c3r2;
					case 12: return c0r3;
					case 13: return c1r3;
					case 14: return c2r3;
					case 15: return c3r3;
					default: throw new Exception("The index is out of range!");
				}
			}
			set
			{
				int index = 4 * r + c;
				switch (index)
				{
					case 0: c0r0 = value; break;
					case 1: c1r0 = value; break;
					case 2: c2r0 = value; break;
					case 3: c3r0 = value; break;
					case 4: c0r1 = value; break;
					case 5: c1r1 = value; break;
					case 6: c2r1 = value; break;
					case 7: c3r1 = value; break;
					case 8: c0r2 = value; break;
					case 9: c1r2 = value; break;
					case 10: c2r2 = value; break;
					case 11: c3r2 = value; break;
					case 12: c0r3 = value; break;
					case 13: c1r3 = value; break;
					case 14: c2r3 = value; break;
					case 15: c3r3 = value; break;
					default: throw new Exception("The index is out of range!");
				}
			}
		}
		public Vec4 this[int c]
		{
			get
			{
				if (c == 0) return new Vec4(c0r0, c0r1, c0r2, c0r3);
				if (c == 1) return new Vec4(c1r0, c1r1, c1r2, c1r3);
				if (c == 2) return new Vec4(c2r0, c2r1, c2r2, c2r3);
				if (c == 3) return new Vec4(c3r0, c3r1, c3r2, c3r3);
				throw new Exception("The index is out of range!");
			}
			set
			{
				if (c == 0) { c0r0 = value.x; c0r1 = value.y; c0r2 = value.z; c0r3 = value.w; }
				else if (c == 1) { c1r0 = value.x; c1r1 = value.y; c1r2 = value.z; c1r3 = value.w; }
				else if (c == 2) { c2r0 = value.x; c2r1 = value.y; c2r2 = value.z; c2r3 = value.w; }
				else if (c == 3) { c3r0 = value.x; c3r1 = value.y; c3r2 = value.z; c3r3 = value.w; }
				else throw new Exception("The index is out of range!");
			}
		}
		public int column { get { return 4; } }
		public int row { get { return 4; } }

		public bool isNaM
		{
			get
			{
				return
					double.IsNaN(c0r0) ||
					double.IsNaN(c1r0) ||
					double.IsNaN(c2r0) ||
					double.IsNaN(c3r0) ||
					double.IsNaN(c0r1) ||
					double.IsNaN(c1r1) ||
					double.IsNaN(c2r1) ||
					double.IsNaN(c3r1) ||
					double.IsNaN(c0r2) ||
					double.IsNaN(c1r2) ||
					double.IsNaN(c2r2) ||
					double.IsNaN(c3r2) ||
					double.IsNaN(c0r3) ||
					double.IsNaN(c1r3) ||
					double.IsNaN(c2r3) ||
					double.IsNaN(c3r3);
			}
		}
		
		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(c0r0.ToString(format)).Append("\t")
				.Append(c1r0.ToString(format)).Append("\t")
				.Append(c2r0.ToString(format)).Append("\t")
				.Append(c3r0.ToString(format)).Append("\t|\n|\t")
				.Append(c0r1.ToString(format)).Append("\t")
				.Append(c1r1.ToString(format)).Append("\t")
				.Append(c2r1.ToString(format)).Append("\t")
				.Append(c3r1.ToString(format)).Append("\t|\n|\t")
				.Append(c0r2.ToString(format)).Append("\t")
				.Append(c1r2.ToString(format)).Append("\t")
				.Append(c2r2.ToString(format)).Append("\t")
				.Append(c3r2.ToString(format)).Append("\t|\n|\t")
				.Append(c0r3.ToString(format)).Append("\t")
				.Append(c1r3.ToString(format)).Append("\t")
				.Append(c2r3.ToString(format)).Append("\t")
				.Append(c3r3.ToString(format)).Append("\t|");
			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString("");
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public Vec4 GetRow(int r)
		{
			if (r == 0) return new Vec4(c0r0, c1r0, c2r0, c3r0);
			if (r == 1) return new Vec4(c0r1, c1r1, c2r1, c3r1);
			if (r == 2) return new Vec4(c0r2, c1r2, c2r2, c3r2);
			if (r == 3) return new Vec4(c0r3, c1r3, c2r3, c3r3);
			throw new Exception("The index is out of range!");
		}
		public void SetRow(int r, Vec4 v)
		{
			if (r == 0) { c0r0 = v.x; c1r0 = v.y; c2r0 = v.z; c3r0 = v.w; }
			else if (r == 1) { c0r1 = v.x; c1r1 = v.y; c2r1 = v.z; c3r1 = v.w; }
			else if (r == 2) { c0r2 = v.x; c1r2 = v.y; c2r2 = v.z; c3r2 = v.w; }
			else if (r == 3) { c0r3 = v.x; c1r3 = v.y; c2r3 = v.z; c3r3 = v.w; }
			else throw new Exception("The index is out of range!");
		}


		public static bool operator ==(Mat4x4 lhs, Mat4x4 rhs)
		{
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Mat4x4 lhs, Mat4x4 rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Mat4x4 operator ~(Mat4x4 m)
		{
			Mat4x4 nm = new Mat4x4();
			nm.c1r0 = m.c0r1;
			nm.c2r0 = m.c0r2;
			nm.c3r0 = m.c0r3;
			nm.c0r1 = m.c1r0;
			nm.c2r1 = m.c1r2;
			nm.c3r1 = m.c1r3;
			nm.c0r2 = m.c2r0;
			nm.c1r2 = m.c2r1;
			nm.c3r2 = m.c2r3;
			nm.c0r3 = m.c3r0;
			nm.c1r3 = m.c3r1;
			nm.c2r3 = m.c3r2;
			return nm;
		}

		public static Mat4x4 operator +(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs.c0r0 + rhs.c0r0;
			m.c1r0 = lhs.c1r0 + rhs.c1r0;
			m.c2r0 = lhs.c2r0 + rhs.c2r0;
			m.c3r0 = lhs.c3r0 + rhs.c3r0;
			m.c0r1 = lhs.c0r1 + rhs.c0r1;
			m.c1r1 = lhs.c1r1 + rhs.c1r1;
			m.c2r1 = lhs.c2r1 + rhs.c2r1;
			m.c3r1 = lhs.c3r1 + rhs.c3r1;
			m.c0r2 = lhs.c0r2 + rhs.c0r2;
			m.c1r2 = lhs.c1r2 + rhs.c1r2;
			m.c2r2 = lhs.c2r2 + rhs.c2r2;
			m.c3r2 = lhs.c3r2 + rhs.c3r2;
			m.c0r3 = lhs.c0r3 + rhs.c0r3;
			m.c1r3 = lhs.c1r3 + rhs.c1r3;
			m.c2r3 = lhs.c2r3 + rhs.c2r3;
			m.c3r3 = lhs.c3r3 + rhs.c3r3;
			return m;
		}

		public static Mat4x4 operator -(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs.c0r0 - rhs.c0r0;
			m.c1r0 = lhs.c1r0 - rhs.c1r0;
			m.c2r0 = lhs.c2r0 - rhs.c2r0;
			m.c3r0 = lhs.c3r0 - rhs.c3r0;
			m.c0r1 = lhs.c0r1 - rhs.c0r1;
			m.c1r1 = lhs.c1r1 - rhs.c1r1;
			m.c2r1 = lhs.c2r1 - rhs.c2r1;
			m.c3r1 = lhs.c3r1 - rhs.c3r1;
			m.c0r2 = lhs.c0r2 - rhs.c0r2;
			m.c1r2 = lhs.c1r2 - rhs.c1r2;
			m.c2r2 = lhs.c2r2 - rhs.c2r2;
			m.c3r2 = lhs.c3r2 - rhs.c3r2;
			m.c0r3 = lhs.c0r3 - rhs.c0r3;
			m.c1r3 = lhs.c1r3 - rhs.c1r3;
			m.c2r3 = lhs.c2r3 - rhs.c2r3;
			m.c3r3 = lhs.c3r3 - rhs.c3r3;
			return m;
		}

		public static Mat4x4 operator *(double lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs * rhs.c0r0;
			m.c1r0 = lhs * rhs.c1r0;
			m.c2r0 = lhs * rhs.c2r0;
			m.c3r0 = lhs * rhs.c3r0;
			m.c0r1 = lhs * rhs.c0r1;
			m.c1r1 = lhs * rhs.c1r1;
			m.c2r1 = lhs * rhs.c2r1;
			m.c3r1 = lhs * rhs.c3r1;
			m.c0r2 = lhs * rhs.c0r2;
			m.c1r2 = lhs * rhs.c1r2;
			m.c2r2 = lhs * rhs.c2r2;
			m.c3r2 = lhs * rhs.c3r2;
			m.c0r3 = lhs * rhs.c0r3;
			m.c1r3 = lhs * rhs.c1r3;
			m.c2r3 = lhs * rhs.c2r3;
			m.c3r3 = lhs * rhs.c3r3;
			return m;
		}
		public static Mat4x4 operator *(Mat4x4 lhs, double rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs.c0r0 * rhs;
			m.c1r0 = lhs.c1r0 * rhs;
			m.c2r0 = lhs.c2r0 * rhs;
			m.c3r0 = lhs.c3r0 * rhs;
			m.c0r1 = lhs.c0r1 * rhs;
			m.c1r1 = lhs.c1r1 * rhs;
			m.c2r1 = lhs.c2r1 * rhs;
			m.c3r1 = lhs.c3r1 * rhs;
			m.c0r2 = lhs.c0r2 * rhs;
			m.c1r2 = lhs.c1r2 * rhs;
			m.c2r2 = lhs.c2r2 * rhs;
			m.c3r2 = lhs.c3r2 * rhs;
			m.c0r3 = lhs.c0r3 * rhs;
			m.c1r3 = lhs.c1r3 * rhs;
			m.c2r3 = lhs.c2r3 * rhs;
			m.c3r3 = lhs.c3r3 * rhs;
			return m;
		}
		
		public static Mat4x4 operator /(double lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs / rhs.c0r0;
			m.c1r0 = lhs / rhs.c1r0;
			m.c2r0 = lhs / rhs.c2r0;
			m.c3r0 = lhs / rhs.c3r0;
			m.c0r1 = lhs / rhs.c0r1;
			m.c1r1 = lhs / rhs.c1r1;
			m.c2r1 = lhs / rhs.c2r1;
			m.c3r1 = lhs / rhs.c3r1;
			m.c0r2 = lhs / rhs.c0r2;
			m.c1r2 = lhs / rhs.c1r2;
			m.c2r2 = lhs / rhs.c2r2;
			m.c3r2 = lhs / rhs.c3r2;
			m.c0r3 = lhs / rhs.c0r3;
			m.c1r3 = lhs / rhs.c1r3;
			m.c2r3 = lhs / rhs.c2r3;
			m.c3r3 = lhs / rhs.c3r3;
			return m;
		}
		public static Mat4x4 operator /(Mat4x4 lhs, double rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs.c0r0 / rhs;
			m.c1r0 = lhs.c1r0 / rhs;
			m.c2r0 = lhs.c2r0 / rhs;
			m.c3r0 = lhs.c3r0 / rhs;
			m.c0r1 = lhs.c0r1 / rhs;
			m.c1r1 = lhs.c1r1 / rhs;
			m.c2r1 = lhs.c2r1 / rhs;
			m.c3r1 = lhs.c3r1 / rhs;
			m.c0r2 = lhs.c0r2 / rhs;
			m.c1r2 = lhs.c1r2 / rhs;
			m.c2r2 = lhs.c2r2 / rhs;
			m.c3r2 = lhs.c3r2 / rhs;
			m.c0r3 = lhs.c0r3 / rhs;
			m.c1r3 = lhs.c1r3 / rhs;
			m.c2r3 = lhs.c2r3 / rhs;
			m.c3r3 = lhs.c3r3 / rhs;
			return m;
		}

		public static Mat4x4 operator *(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = lhs.c0r0 * rhs.c0r0 + lhs.c1r0 * rhs.c0r1 + lhs.c2r0 * rhs.c0r2 + lhs.c3r0 * rhs.c0r3;
			m.c1r0 = lhs.c0r0 * rhs.c1r0 + lhs.c1r0 * rhs.c1r1 + lhs.c2r0 * rhs.c1r2 + lhs.c3r0 * rhs.c1r3;
			m.c2r0 = lhs.c0r0 * rhs.c2r0 + lhs.c1r0 * rhs.c2r1 + lhs.c2r0 * rhs.c2r2 + lhs.c3r0 * rhs.c2r3;
			m.c3r0 = lhs.c0r0 * rhs.c3r0 + lhs.c1r0 * rhs.c3r1 + lhs.c2r0 * rhs.c3r2 + lhs.c3r0 * rhs.c3r3;
			m.c0r1 = lhs.c0r1 * rhs.c0r0 + lhs.c1r1 * rhs.c0r1 + lhs.c2r1 * rhs.c0r2 + lhs.c3r1 * rhs.c0r3;
			m.c1r1 = lhs.c0r1 * rhs.c1r0 + lhs.c1r1 * rhs.c1r1 + lhs.c2r1 * rhs.c1r2 + lhs.c3r1 * rhs.c1r3;
			m.c2r1 = lhs.c0r1 * rhs.c2r0 + lhs.c1r1 * rhs.c2r1 + lhs.c2r1 * rhs.c2r2 + lhs.c3r1 * rhs.c2r3;
			m.c3r1 = lhs.c0r1 * rhs.c3r0 + lhs.c1r1 * rhs.c3r1 + lhs.c2r1 * rhs.c3r2 + lhs.c3r1 * rhs.c3r3;
			m.c0r2 = lhs.c0r2 * rhs.c0r0 + lhs.c1r2 * rhs.c0r1 + lhs.c2r2 * rhs.c0r2 + lhs.c3r2 * rhs.c0r3;
			m.c1r2 = lhs.c0r2 * rhs.c1r0 + lhs.c1r2 * rhs.c1r1 + lhs.c2r2 * rhs.c1r2 + lhs.c3r2 * rhs.c1r3;
			m.c2r2 = lhs.c0r2 * rhs.c2r0 + lhs.c1r2 * rhs.c2r1 + lhs.c2r2 * rhs.c2r2 + lhs.c3r2 * rhs.c2r3;
			m.c3r2 = lhs.c0r2 * rhs.c3r0 + lhs.c1r2 * rhs.c3r1 + lhs.c2r2 * rhs.c3r2 + lhs.c3r2 * rhs.c3r3;
			m.c0r3 = lhs.c0r3 * rhs.c0r0 + lhs.c1r3 * rhs.c0r1 + lhs.c2r3 * rhs.c0r2 + lhs.c3r3 * rhs.c0r3;
			m.c1r3 = lhs.c0r3 * rhs.c1r0 + lhs.c1r3 * rhs.c1r1 + lhs.c2r3 * rhs.c1r2 + lhs.c3r3 * rhs.c1r3;
			m.c2r3 = lhs.c0r3 * rhs.c2r0 + lhs.c1r3 * rhs.c2r1 + lhs.c2r3 * rhs.c2r2 + lhs.c3r3 * rhs.c2r3;
			m.c3r3 = lhs.c0r3 * rhs.c3r0 + lhs.c1r3 * rhs.c3r1 + lhs.c2r3 * rhs.c3r2 + lhs.c3r3 * rhs.c3r3;
			return m;
		}

		public static Vec4 operator *(Mat4x4 lhs, Vec4 rhs)
		{
			Vec4 v = new Vec4();
			v.x = lhs.c0r0 * rhs.x + lhs.c1r0 * rhs.y + lhs.c2r0 * rhs.z + lhs.c3r0 * rhs.w;
			v.y = lhs.c0r1 * rhs.x + lhs.c1r1 * rhs.y + lhs.c2r1 * rhs.z + lhs.c3r1 * rhs.w;
			v.z = lhs.c0r2 * rhs.x + lhs.c1r2 * rhs.y + lhs.c2r2 * rhs.z + lhs.c3r2 * rhs.w;
			v.w = lhs.c0r3 * rhs.x + lhs.c1r3 * rhs.y + lhs.c2r3 * rhs.z + lhs.c3r3 * rhs.w;
			return v;
		}
		public static Vec3 operator *(Mat4x4 lhs, Vec3 rhs)
		{
			Vec3 v = new Vec3();
			v.x = lhs.c0r0 * rhs.x + lhs.c1r0 * rhs.y + lhs.c2r0 * rhs.z + lhs.c3r0;
			v.y = lhs.c0r1 * rhs.x + lhs.c1r1 * rhs.y + lhs.c2r1 * rhs.z + lhs.c3r1;
			v.z = lhs.c0r2 * rhs.x + lhs.c1r2 * rhs.y + lhs.c2r2 * rhs.z + lhs.c3r2;
			return v;
		}


		public static Mat4x4 Translate(Vec3 delta)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = m.c1r1 = m.c2r2 = m.c3r3 = 1;
			m.c3r0 = delta.x;
			m.c3r1 = delta.y;
			m.c3r2 = delta.z;
			return m;
		}

		public static Mat4x4 RotateX(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.c0r0 = m.c3r3 = 1;
			double cx = Math.Cos(angle);
			double sx = Math.Sin(angle);
			m.c1r1 = cx;
			m.c2r1 = -sx;
			m.c1r2 = sx;
			m.c2r2 = cx;
			return m;
		}
		public static Mat4x4 RotateY(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.c1r1 = m.c3r3 = 1;
			double cy = Math.Cos(angle);
			double sy = Math.Sin(angle);
			m.c0r0 = cy;
			m.c2r0 = sy;
			m.c0r2 = -sy;
			m.c2r2 = cy;
			return m;
		}
		public static Mat4x4 RotateZ(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.c2r2 = m.c3r3 = 1;
			double cz = Math.Cos(angle);
			double sz = Math.Sin(angle);
			m.c0r0 = cz;
			m.c1r0 = -sz;
			m.c0r1 = sz;
			m.c1r1 = cz;
			return m;
		}

		public static Mat4x4 RotateZYX(Vec3 euler)
		{
			Mat4x4 m = new Mat4x4();
			m.c3r3 = 1;
			double cx = Math.Cos(euler.x);
			double sx = Math.Sin(euler.x);
			double cy = Math.Cos(euler.y);
			double sy = Math.Sin(euler.y);
			double cz = Math.Cos(euler.z);
			double sz = Math.Sin(euler.z);
			m.c0r0 = cy * cz;
			m.c1r0 = -cx * sz + sx * sy * cz;
			m.c2r0 = sx * sz + cx * sy * cz;
			m.c0r1 = cy * sz;
			m.c1r1 = cx * cz + sx * sy * sz;
			m.c2r1 = -sx * cz + cx * sy * sz;
			m.c0r2 = -sy;
			m.c1r2 = sx * cy;
			m.c2r2 = cx * cy;
			return m;
		}
		public static Mat4x4 RotateYXZ(Vec3 euler)
		{
			Mat4x4 m = new Mat4x4();
			m.c3r3 = 1;
			double cz = Math.Cos(euler.z);
			double sz = Math.Sin(euler.z);
			double cx = Math.Cos(euler.x);
			double sx = Math.Sin(euler.x);
			double cy = Math.Cos(euler.y);
			double sy = Math.Sin(euler.y);
			m.c0r0 = cy * cz + sx * sy * sz;
			m.c1r0 = -cy * sz + sx * sy * cz;
			m.c2r0 = cx * sy;
			m.c0r1 = cx * sz;
			m.c1r1 = cx * cz;
			m.c2r1 = -sx;
			m.c0r2 = -sy * cz + sx * cy * sz;
			m.c1r2 = sy * sz + sx * cy * cz;
			m.c2r2 = cx * cy;
			return m;
		}

		public static Mat4x4 Rotate(Vec3 euler)
		{
			return RotateYXZ(euler);
		}
		public static Mat4x4 Rotate(double angle, Vec3 normalAxis)
		{
			Mat4x4 m = new Mat4x4();
			m.c3r3 = 1;
			double ax = normalAxis.x, ay = normalAxis.y, az = normalAxis.z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			m.c0r0 = ax * ax + (1 - ax * ax) * cos;
			m.c1r0 = ax * ay * (1 - cos) - az * sin;
			m.c2r0 = ax * az * (1 - cos) + ay * sin;
			m.c0r1 = ay * ax * (1 - cos) + az * sin;
			m.c1r1 = ay * ay + (1 - ay * ay) * cos;
			m.c2r1 = ay * az * (1 - cos) - ax * sin;
			m.c0r2 = az * ax * (1 - cos) - ay * sin;
			m.c1r2 = az * ay * (1 - cos) + ax * sin;
			m.c2r2 = az * az + (1 - az * az) * cos;
			return m;
		}
		public static Mat4x4 Rotate(Quat rotation)
		{
			double angle;
			Vec3 normalAxis;
			Quat.ToAngleAxis(rotation, out angle, out normalAxis);
			return Rotate(angle, normalAxis);
		}

		public static Mat4x4 Scale(Vec3 scale)
		{
			Mat4x4 m = new Mat4x4();
			m.c3r3 = 1;
			m.c0r0 = scale.x;
			m.c1r1 = scale.y;
			m.c2r2 = scale.z;
			return m;
		}

		public static Mat4x4 TRzyxS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * RotateZYX(euler) * Scale(scale);
		}
		public static Mat4x4 TRyxzS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * RotateYXZ(euler) * Scale(scale);
		}

		public static Mat4x4 TRS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * Rotate(euler) * Scale(scale);
		}
		public static Mat4x4 TRS(Vec3 delta, double angle, Vec3 normalAxis, Vec3 scale)
		{
			return Translate(delta) * Rotate(angle, normalAxis) * Scale(scale);
		}
		public static Mat4x4 TRS(Vec3 delta, Quat rotation, Vec3 scale)
		{
			return Translate(delta) * Rotate(rotation) * Scale(scale);
		}

	}
}
