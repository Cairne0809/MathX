using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Vec2 : IVector
	{
		public int dimension { get { return 2; } }

		public double x;
		public double y;

		public Vec2 xy { get { return new Vec2(x, y); } set { x = value.x; y = value.y; } }
		public Vec2 yx { get { return new Vec2(y, x); } set { y = value.x; x = value.y; } }

		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return x;
					case 1: return y;
					default: throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					default: throw new IndexOutOfRangeException();
				}
			}
		}
		
		public Vec2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
		public Vec2(Vec2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		public Vec2 S2(string swizzle)
		{
			Vec2 nv;
			nv.x = this[swizzle[0] - 120];
			nv.y = this[swizzle[1] - 120];
			return nv;
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
			return Math.Abs(x - v.x) <= MathX.Tolerance
				&& Math.Abs(y - v.y) <= MathX.Tolerance;
		}


		public static explicit operator Vec2(Vec3 v) { return new Vec2(v.x, v.y); }
		public static explicit operator Vec2(Vec4 v) { return new Vec2(v.x, v.y); }

		public static bool operator ==(Vec2 lhs, Vec2 rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Vec2 lhs, Vec2 rhs) { return !lhs.ValueEquals(rhs); }

		public static Vec2 operator -(Vec2 v) { return new Vec2(-v.x, -v.y); }

		public static Vec2 operator +(double lhs, Vec2 rhs) { return new Vec2(lhs + rhs.x, lhs + rhs.y); }
		public static Vec2 operator +(Vec2 lhs, double rhs) { return new Vec2(lhs.x + rhs, lhs.y + rhs); }
		public static Vec2 operator +(Vec2 lhs, Vec2 rhs) { return new Vec2(lhs.x + rhs.x, lhs.y + rhs.y); }

		public static Vec2 operator -(double lhs, Vec2 rhs) { return new Vec2(lhs - rhs.x, lhs - rhs.y); }
		public static Vec2 operator -(Vec2 lhs, double rhs) { return new Vec2(lhs.x - rhs, lhs.y - rhs); }
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

		public static double Cross(Vec2 lhs, Vec2 rhs)
		{
			return lhs.x * rhs.y - lhs.y * rhs.x;
		}

		public static Vec2 Skew(Vec2 v)
		{
			return new Vec2(-v.y, v.x);
		}

		public static Vec2 Rotate(Vec2 src, double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double vx = cos * src.x - sin * src.y;
			double vy = sin * src.x + cos * src.y;
			return new Vec2(vx, vy);
		}

		public static readonly Vec2 zero = new Vec2();
		public static readonly Vec2 one = new Vec2(1, 1);
		public static readonly Vec2 right = new Vec2(1, 0);
		public static readonly Vec2 up = new Vec2(0, 1);
		public static readonly Vec2 NaV = new Vec2(double.NaN, double.NaN);
	}
}
