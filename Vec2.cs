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
				if (index == 0) return x;
				else if (index == 1) return y;
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0) x = value;
				else if (index == 1) y = value;
				else throw new IndexOutOfRangeException();
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
			double pt = MathX.Tolerance;
			double nt = -pt;
			double dx = x - v.x;
			double dy = y - v.y;
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt;
		}


		public static explicit operator Vec2(Cplx c) { return new Vec2(c.x, c.y); }
		public static explicit operator Vec2(Vec3 v) { return new Vec2(v.x, v.y); }
		public static explicit operator Vec2(Vec4 v) { return new Vec2(v.x, v.y); }

		public static bool operator ==(Vec2 lhs, Vec2 rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Vec2 lhs, Vec2 rhs) { return !lhs.ValueEquals(rhs); }

		public static Vec2 operator -(Vec2 v)
		{
			v.x = -v.x;
			v.y = -v.y;
			return v;
		}

		public static Vec2 operator +(double lhs, Vec2 rhs)
		{
			rhs.x += lhs;
			rhs.y += lhs;
			return rhs;
		}
		public static Vec2 operator +(Vec2 lhs, double rhs)
		{
			lhs.x += rhs;
			lhs.y += rhs;
			return lhs;
		}
		public static Vec2 operator +(Vec2 lhs, Vec2 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			return lhs;
		}

		public static Vec2 operator -(double lhs, Vec2 rhs)
		{
			rhs.x = lhs - rhs.x;
			rhs.y = lhs - rhs.y;
			return rhs;
		}
		public static Vec2 operator -(Vec2 lhs, double rhs)
		{
			lhs.x -= rhs;
			lhs.y -= rhs;
			return lhs;
		}
		public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			return lhs;
		}

		public static Vec2 operator *(double lhs, Vec2 rhs)
		{
			rhs.x *= lhs;
			rhs.y *= lhs;
			return rhs;
		}
		public static Vec2 operator *(Vec2 lhs, double rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			return lhs;
		}
		public static Vec2 operator *(Vec2 lhs, Vec2 rhs)
		{
			lhs.x *= rhs.x;
			lhs.y *= rhs.y;
			return lhs;
		}

		public static Vec2 operator /(double lhs, Vec2 rhs)
		{
			rhs.x = lhs / rhs.x;
			rhs.y = lhs / rhs.y;
			return rhs;
		}
		public static Vec2 operator /(Vec2 lhs, double rhs)
		{
			lhs.x /= rhs;
			lhs.y /= rhs;
			return lhs;
		}
		public static Vec2 operator /(Vec2 lhs, Vec2 rhs)
		{
			lhs.x /= rhs.x;
			lhs.y /= rhs.y;
			return lhs;
		}
		
		public static Vec2 GetRandom()
		{
			double theta = MathX.DoublePI * MathX.GetRandom();
			Vec2 nv;
			nv.x = Math.Sin(theta);
			nv.y = Math.Cos(theta);
			return nv;
		}

		public static double Cross(Vec2 lhs, Vec2 rhs)
		{
			return lhs.x * rhs.y - lhs.y * rhs.x;
		}

		public static Vec2 Skew(Vec2 v)
		{
			Vec2 nv;
			nv.x = -v.y;
			nv.y = v.x;
			return nv;
		}

		public static Vec2 Rotate(Vec2 src, double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			Vec2 nv;
			nv.x = cos * src.x - sin * src.y;
			nv.y = sin * src.x + cos * src.y;
			return nv;
		}

		public static readonly Vec2 zero = new Vec2();
		public static readonly Vec2 one = new Vec2(1, 1);
		public static readonly Vec2 right = new Vec2(1, 0);
		public static readonly Vec2 up = new Vec2(0, 1);
		public static readonly Vec2 NaV = new Vec2(double.NaN, double.NaN);
	}
}
