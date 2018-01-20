using System;
using System.Text;

namespace MathematicsX
{
	public struct Mat3x3 : IMatrix
	{
		public double c0r0;
		public double c1r0;
		public double c2r0;
		public double c0r1;
		public double c1r1;
		public double c2r1;
		public double c0r2;
		public double c1r2;
		public double c2r2;

		public double this[int c, int r]
		{
			get
			{
				int index = 3 * r + c;
				switch (index)
				{
					case 0: return c0r0;
					case 1: return c1r0;
					case 2: return c2r0;
					case 3: return c0r1;
					case 4: return c1r1;
					case 5: return c2r1;
					case 6: return c0r2;
					case 7: return c1r2;
					case 8: return c2r2;
					default: throw new Exception("The index is out of range!");
				}
			}
			set
			{
				int index = 3 * r + c;
				switch (index)
				{
					case 0: c0r0 = value; break;
					case 1: c1r0 = value; break;
					case 2: c2r0 = value; break;
					case 3: c0r1 = value; break;
					case 4: c1r1 = value; break;
					case 5: c2r1 = value; break;
					case 6: c0r2 = value; break;
					case 7: c1r2 = value; break;
					case 8: c2r2 = value; break;
					default: throw new Exception("The index is out of range!");
				}
			}
		}
		public Vec3 this[int c]
		{
			get
			{
				if (c == 0) return new Vec3(c0r0, c0r1, c0r2);
				if (c == 1) return new Vec3(c1r0, c1r1, c1r2);
				if (c == 2) return new Vec3(c2r0, c2r1, c2r2);
				throw new Exception("The index is out of range!");
			}
			set
			{
				if (c == 0) { c0r0 = value.x; c0r1 = value.y; c0r2 = value.z; }
				else if (c == 1) { c1r0 = value.x; c1r1 = value.y; c1r2 = value.z; }
				else if (c == 2) { c2r0 = value.x; c2r1 = value.y; c2r2 = value.z; }
				else throw new Exception("The index is out of range!");
			}
		}
		public int column { get { return 3; } }
		public int row { get { return 3; } }

		public bool isNaM
		{
			get
			{
				return
					double.IsNaN(c0r0) ||
					double.IsNaN(c1r0) ||
					double.IsNaN(c2r0) ||
					double.IsNaN(c0r1) ||
					double.IsNaN(c1r1) ||
					double.IsNaN(c2r1) ||
					double.IsNaN(c0r2) ||
					double.IsNaN(c1r2) ||
					double.IsNaN(c2r2);
			}
		}
		
		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(c0r0.ToString(format)).Append("\t")
				.Append(c1r0.ToString(format)).Append("\t")
				.Append(c2r0.ToString(format)).Append("\t|\n|\t")
				.Append(c0r1.ToString(format)).Append("\t")
				.Append(c1r1.ToString(format)).Append("\t")
				.Append(c2r1.ToString(format)).Append("\t|\n|\t")
				.Append(c0r2.ToString(format)).Append("\t")
				.Append(c1r2.ToString(format)).Append("\t")
				.Append(c2r2.ToString(format)).Append("\t|");
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

		public Vec3 GetRow(int r)
		{
			if (r == 0) return new Vec3(c0r0, c1r0, c2r0);
			if (r == 1) return new Vec3(c0r1, c1r1, c2r1);
			if (r == 2) return new Vec3(c0r2, c1r2, c2r2);
			throw new Exception("The index is out of range!");
		}
		public void SetRow(int r, Vec3 v)
		{
			if (r == 0) { c0r0 = v.x; c1r0 = v.y; c2r0 = v.z; }
			else if (r == 1) { c0r1 = v.x; c1r1 = v.y; c2r1 = v.z; }
			else if (r == 2) { c0r2 = v.x; c1r2 = v.y; c2r2 = v.z; }
			else throw new Exception("The index is out of range!");
		}


		public static bool operator ==(Mat3x3 lhs, Mat3x3 rhs)
		{
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Mat3x3 lhs, Mat3x3 rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Mat3x3 operator ~(Mat3x3 m)
		{
			Mat3x3 nm = new Mat3x3();
			nm.c1r0 = m.c0r1;
			nm.c2r0 = m.c0r2;
			nm.c0r1 = m.c1r0;
			nm.c2r1 = m.c1r2;
			nm.c0r2 = m.c2r0;
			nm.c1r2 = m.c2r1;
			return nm;
		}

		public static Mat3x3 operator +(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs.c0r0 + rhs.c0r0;
			m.c1r0 = lhs.c1r0 + rhs.c1r0;
			m.c2r0 = lhs.c2r0 + rhs.c2r0;
			m.c0r1 = lhs.c0r1 + rhs.c0r1;
			m.c1r1 = lhs.c1r1 + rhs.c1r1;
			m.c2r1 = lhs.c2r1 + rhs.c2r1;
			m.c0r2 = lhs.c0r2 + rhs.c0r2;
			m.c1r2 = lhs.c1r2 + rhs.c1r2;
			m.c2r2 = lhs.c2r2 + rhs.c2r2;
			return m;
		}

		public static Mat3x3 operator -(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs.c0r0 - rhs.c0r0;
			m.c1r0 = lhs.c1r0 - rhs.c1r0;
			m.c2r0 = lhs.c2r0 - rhs.c2r0;
			m.c0r1 = lhs.c0r1 - rhs.c0r1;
			m.c1r1 = lhs.c1r1 - rhs.c1r1;
			m.c2r1 = lhs.c2r1 - rhs.c2r1;
			m.c0r2 = lhs.c0r2 - rhs.c0r2;
			m.c1r2 = lhs.c1r2 - rhs.c1r2;
			m.c2r2 = lhs.c2r2 - rhs.c2r2;
			return m;
		}

		public static Mat3x3 operator *(double lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs * rhs.c0r0;
			m.c1r0 = lhs * rhs.c1r0;
			m.c2r0 = lhs * rhs.c2r0;
			m.c0r1 = lhs * rhs.c0r1;
			m.c1r1 = lhs * rhs.c1r1;
			m.c2r1 = lhs * rhs.c2r1;
			m.c0r2 = lhs * rhs.c0r2;
			m.c1r2 = lhs * rhs.c1r2;
			m.c2r2 = lhs * rhs.c2r2;
			return m;
		}
		public static Mat3x3 operator *(Mat3x3 lhs, double rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs.c0r0 * rhs;
			m.c1r0 = lhs.c1r0 * rhs;
			m.c2r0 = lhs.c2r0 * rhs;
			m.c0r1 = lhs.c0r1 * rhs;
			m.c1r1 = lhs.c1r1 * rhs;
			m.c2r1 = lhs.c2r1 * rhs;
			m.c0r2 = lhs.c0r2 * rhs;
			m.c1r2 = lhs.c1r2 * rhs;
			m.c2r2 = lhs.c2r2 * rhs;
			return m;
		}

		public static Mat3x3 operator /(double lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs / rhs.c0r0;
			m.c1r0 = lhs / rhs.c1r0;
			m.c2r0 = lhs / rhs.c2r0;
			m.c0r1 = lhs / rhs.c0r1;
			m.c1r1 = lhs / rhs.c1r1;
			m.c2r1 = lhs / rhs.c2r1;
			m.c0r2 = lhs / rhs.c0r2;
			m.c1r2 = lhs / rhs.c1r2;
			m.c2r2 = lhs / rhs.c2r2;
			return m;
		}
		public static Mat3x3 operator /(Mat3x3 lhs, double rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs.c0r0 / rhs;
			m.c1r0 = lhs.c1r0 / rhs;
			m.c2r0 = lhs.c2r0 / rhs;
			m.c0r1 = lhs.c0r1 / rhs;
			m.c1r1 = lhs.c1r1 / rhs;
			m.c2r1 = lhs.c2r1 / rhs;
			m.c0r2 = lhs.c0r2 / rhs;
			m.c1r2 = lhs.c1r2 / rhs;
			m.c2r2 = lhs.c2r2 / rhs;
			return m;
		}

		public static Mat3x3 operator *(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = lhs.c0r0 * rhs.c0r0 + lhs.c1r0 * rhs.c0r1 + lhs.c2r0 * rhs.c0r2;
			m.c1r0 = lhs.c0r0 * rhs.c1r0 + lhs.c1r0 * rhs.c1r1 + lhs.c2r0 * rhs.c1r2;
			m.c2r0 = lhs.c0r0 * rhs.c2r0 + lhs.c1r0 * rhs.c2r1 + lhs.c2r0 * rhs.c2r2;
			m.c0r1 = lhs.c0r1 * rhs.c0r0 + lhs.c1r1 * rhs.c0r1 + lhs.c2r1 * rhs.c0r2;
			m.c1r1 = lhs.c0r1 * rhs.c1r0 + lhs.c1r1 * rhs.c1r1 + lhs.c2r1 * rhs.c1r2;
			m.c2r1 = lhs.c0r1 * rhs.c2r0 + lhs.c1r1 * rhs.c2r1 + lhs.c2r1 * rhs.c2r2;
			m.c0r2 = lhs.c0r2 * rhs.c0r0 + lhs.c1r2 * rhs.c0r1 + lhs.c2r2 * rhs.c0r2;
			m.c1r2 = lhs.c0r2 * rhs.c1r0 + lhs.c1r2 * rhs.c1r1 + lhs.c2r2 * rhs.c1r2;
			m.c2r2 = lhs.c0r2 * rhs.c2r0 + lhs.c1r2 * rhs.c2r1 + lhs.c2r2 * rhs.c2r2;
			return m;
		}

		public static Vec3 operator *(Mat3x3 lhs, Vec3 rhs)
		{
			Vec3 v = new Vec3();
			v.x = lhs.c0r0 * rhs.x + lhs.c1r0 * rhs.y + lhs.c2r0 * rhs.z;
			v.y = lhs.c0r1 * rhs.x + lhs.c1r1 * rhs.y + lhs.c2r1 * rhs.z;
			v.z = lhs.c0r2 * rhs.x + lhs.c1r2 * rhs.y + lhs.c2r2 * rhs.z;
			return v;
		}
		public static Vec2 operator *(Mat3x3 lhs, Vec2 rhs)
		{
			Vec2 v = new Vec2();
			v.x = lhs.c0r0 * rhs.x + lhs.c1r0 * rhs.y + lhs.c2r0;
			v.y = lhs.c0r1 * rhs.x + lhs.c1r1 * rhs.y + lhs.c2r1;
			return v;
		}


		public static Mat3x3 Translate(Vec2 delta)
		{
			Mat3x3 m = new Mat3x3();
			m.c0r0 = m.c1r1 = m.c2r2 = 1;
			m.c2r0 = delta.x;
			m.c2r1 = delta.y;
			return m;
		}

		public static Mat3x3 Rotate(double angle)
		{
			Mat3x3 m = new Mat3x3();
			m.c2r2 = 1;
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);
			m.c0r0 = cos;
			m.c1r0 = -sin;
			m.c0r1 = sin;
			m.c1r1 = cos;
			return m;
		}

		public static Mat3x3 Scale(Vec2 scale)
		{
			Mat3x3 m = new Mat3x3();
			m.c2r2 = 1;
			m.c0r0 = scale.x;
			m.c1r1 = scale.y;
			return m;
		}

		public static Mat3x3 TRS(Vec2 delta, double angle, Vec2 scale)
		{
			return Translate(delta) * Rotate(angle) * Scale(scale);
		}

	}
}
