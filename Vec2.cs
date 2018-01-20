using System;
using System.Text;

namespace MathematicsX
{
	public struct Vec2 : IVector
	{
		public double x;
		public double y;

		public Vec2 xy { get { return new Vec2(x, y); } set { x = value.x; y = value.y; } }
		public Vec2 yx { get { return new Vec2(y, x); } set { y = value.x; x = value.y; } }

		public Vec2 s2(string swizzle)
		{
			if (swizzle.Length < 2) throw new Exception("The swizzle.Length is not enough!");
			Vec2 nv = new Vec2();
			nv.x = this[swizzle[0] - 120];
			nv.y = this[swizzle[1] - 120];
			return nv;
		}

		public double this[int index]
		{
			get
			{
				if (index == 0) return x;
				else if (index == 1) return y;
				else throw new Exception("The index is out of range!");
			}
			set
			{
				if (index == 0) x = value;
				else if (index == 1) y = value;
				else throw new Exception("The index is out of range!");
            }
		}
		public int dimension { get { return 2; } }

		public Vec2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
		public Vec2(Vec2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("V(")
				.Append(x.ToString(format)).Append(", ")
				.Append(y.ToString(format)).Append(")");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }
		public bool ValueEquals(Vec2 v)
		{
			bool bx = Math.Abs(x - v.x) <= MathX.accuracy;
			bool by = Math.Abs(y - v.y) <= MathX.accuracy;
			return bx && by;
		}


		public static implicit operator Vec2(double v) { return new Vec2(v, 0); }
		public static explicit operator Vec2(Vec3 v) { return new Vec2(v.x, v.y); }
		public static explicit operator Vec2(Vec4 v) { return new Vec2(v.x, v.y); }

		public static bool operator ==(Vec2 lhs, Vec2 rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Vec2 lhs, Vec2 rhs) { return !lhs.ValueEquals(rhs); }

		public static Vec2 operator -(Vec2 v) { return new Vec2(-v.x, -v.y); }
		public static Vec2 operator +(Vec2 lhs, Vec2 rhs) { return new Vec2(lhs.x + rhs.x, lhs.y + rhs.y); }
		public static Vec2 operator -(Vec2 lhs, Vec2 rhs) { return new Vec2(lhs.x - rhs.x, lhs.y - rhs.y); }
		public static Vec2 operator *(double lhs, Vec2 rhs) { return new Vec2(lhs * rhs.x, lhs * rhs.y); }
		public static Vec2 operator *(Vec2 lhs, double rhs) { return new Vec2(lhs.x * rhs, lhs.y * rhs); }
		public static Vec2 operator *(Vec2 lhs, Vec2 rhs) { return new Vec2(lhs.x * rhs.x, lhs.y * rhs.y); }
		public static Vec2 operator /(double lhs, Vec2 rhs) { return new Vec2(lhs / rhs.x, lhs / rhs.y); }
		public static Vec2 operator /(Vec2 lhs, double rhs) { return new Vec2(lhs.x / rhs, lhs.y / rhs); }
		public static Vec2 operator /(Vec2 lhs, Vec2 rhs) { return new Vec2(lhs.x / rhs.x, lhs.y / rhs.y); }

		public static Vec2 GetRandom()
		{
			double theta = MathX.DoublePI * MathX.GetRandom();
			double x = Math.Sin(theta);
			double y = Math.Cos(theta);
			return new Vec2(x, y);
		}

		public static Vec2 zero { get { return new Vec2(); } }
		public static Vec2 one { get { return new Vec2(1, 1); } }
		public static Vec2 right { get { return new Vec2(1, 0); } }
		public static Vec2 up { get { return new Vec2(0, 1); } }
		public static Vec2 NaV { get { return new Vec2(double.NaN, double.NaN); } }
	}
}
