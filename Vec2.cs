using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Vec2 : IVector
	{
		public int Dimension { get { return 2; } }

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
		public override string ToString()
		{
			return ToString(MathX.ToleranceFormat);
		}


		public static explicit operator Vec2(Cplx c) { return new Vec2(c.a, c.b); }
		public static explicit operator Vec2(Vec3 v) { return new Vec2(v.x, v.y); }
		public static explicit operator Vec2(Vec4 v) { return new Vec2(v.x, v.y); }

		public static Vec2 operator -(Vec2 v)
		{
			return new Vec2(-v.x, -v.y);
		}

		public static Vec2 operator +(double lhs, Vec2 rhs)
		{
			return new Vec2(lhs + rhs.x, lhs + rhs.y);
		}
		public static Vec2 operator +(Vec2 lhs, double rhs)
		{
			return new Vec2(lhs.x + rhs, lhs.y + rhs);
		}
		public static Vec2 operator +(Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		public static Vec2 operator -(double lhs, Vec2 rhs)
		{
			return new Vec2(lhs - rhs.x, lhs - rhs.y);
		}
		public static Vec2 operator -(Vec2 lhs, double rhs)
		{
			return new Vec2(lhs.x - rhs, lhs.y - rhs);
		}
		public static Vec2 operator -(Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		public static Vec2 operator *(double lhs, Vec2 rhs)
		{
			return new Vec2(lhs * rhs.x, lhs * rhs.y);
		}
		public static Vec2 operator *(Vec2 lhs, double rhs)
		{
			return new Vec2(lhs.x * rhs, lhs.y * rhs);
		}
		public static Vec2 operator *(Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		public static Vec2 operator /(double lhs, Vec2 rhs)
		{
			return new Vec2(lhs / rhs.x, lhs / rhs.y);
		}
		public static Vec2 operator /(Vec2 lhs, double rhs)
		{
			return new Vec2(lhs.x / rhs, lhs.y / rhs);
		}
		public static Vec2 operator /(Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.x / rhs.x, lhs.y / rhs.y);
		}
		
		public static Vec2 GetRandom()
		{
			double theta = MathX.TWO_PI * MathX.GetRandom();
			Vec2 nv;
			nv.x = Math.Sin(theta);
			nv.y = Math.Cos(theta);
			return nv;
		}

		public static readonly Vec2 zero = new Vec2();
		public static readonly Vec2 one = new Vec2(1, 1);
		public static readonly Vec2 right = new Vec2(1, 0);
		public static readonly Vec2 up = new Vec2(0, 1);
		public static readonly Vec2 NaV = new Vec2(double.NaN, double.NaN);
	}
}
