using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathx
{
	public struct Vec2 : IVec
	{
		public double x;
		public double y;

		public double this[int index]
		{
			get
			{
				if (index == 0) return x;
				else if (index == 1) return y;
				else throw (new Exception("The index is out of range!"));
			}
			set
			{
				if (index == 0) x = value;
				else if (index == 1) y = value;
				else throw (new Exception("The index is out of range!"));
			}
		}
		public int dimension { get { return 2; } }
		public bool isNaN { get { return double.IsNaN(x) || double.IsNaN(y); } }
		public double sqrMagnitude { get { return x * x + y * y; } }
		public double magnitude { get { return Math.Sqrt(x * x + y * y); } }

		public Vec2 normalized
		{
			get
			{
				double mag = Math.Sqrt(x * x + y * y);
				if (mag > 0) return new Vec2(x / mag, y / mag);
				return new Vec2();
			}
		}

		public Vec2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public string ToString(string format)
		{
			return "(" + x.ToString(format) + ", " + y.ToString(format) + ")";
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
		public bool ValueEquals(Vec2 v)
		{
			return this == v;
		}


		public static explicit operator Vec2(Vec3 v)
		{
			return new Vec2(v.x, v.y);
		}
		public static explicit operator Vec2(Vec4 v)
		{
			return new Vec2(v.x, v.y);
		}

		public static bool operator ==(Vec2 lhs, Vec2 rhs)
		{
			bool bx = Math.Abs(lhs.x - rhs.x) <= MathX.accuracy;
			bool by = Math.Abs(lhs.y - rhs.y) <= MathX.accuracy;
			return bx && by;
		}
		public static bool operator !=(Vec2 lhs, Vec2 rhs)
		{
			bool bx = Math.Abs(lhs.x - rhs.x) > MathX.accuracy;
			bool by = Math.Abs(lhs.y - rhs.y) > MathX.accuracy;
			return bx || by;
		}

		public static Vec2 operator -(Vec2 v)
		{
			return new Vec2(-v.x, -v.y);
		}
		public static Vec2 operator +(Vec2 lhs, Vec2 rhs)
		{
			return new Vec2(lhs.x + rhs.x, lhs.y + rhs.y);
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
		public static Vec2 operator /(Vec2 lhs, double rhs)
		{
			return new Vec2(lhs.x / rhs, lhs.y / rhs);
		}

		public static double operator *(Vec2 lhs, Vec2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		public static double Det(Vec2 lhs, Vec2 rhs)
		{
			return lhs.y * rhs.x - lhs.x * rhs.y;
		}

		public static double SqrDistance(Vec2 lhs, Vec2 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			return dx * dx + dy * dy;
		}
		public static double Distance(Vec2 lhs, Vec2 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		public static double Angle(Vec2 lhs, Vec2 rhs)
		{
			double x1 = lhs.x, y1 = lhs.y;
			double x2 = rhs.x, y2 = rhs.y;
			double m1 = Math.Sqrt(x1 * x1 + y1 * y1);
			double m2 = Math.Sqrt(x2 * x2 + y2 * y2);
			if (m1 == 0 || m2 == 0) return 0;
			double dot = x1 * x2 + y1 * y2;
			double cos = dot / m1 / m2;
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}

		public static Vec2 Rotate(Vec2 src, double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double vx = cos * src.x - sin * src.y;
			double vy = sin * src.x + cos * src.y;
			return new Vec2(vx, vy);
		}

		public static Vec2 Project(Vec2 src, Vec2 dst)
		{
			double x = dst.x, y = dst.y;
			double sqrMag = x * x + y * y;
			if (sqrMag > 0)
			{
				double DdSM = (src.x * x + src.y * y) / sqrMag;
				return new Vec2(x * DdSM, y * DdSM);
			}
			return new Vec2();
		}

		public static Vec2 Mirror(Vec2 src, Vec2 axis)
		{
			Vec2 pjt = Project(src, axis);
			return pjt + pjt - src;
		}

		public static Vec2 zero { get { return new Vec2(); } }
		public static Vec2 one { get { return new Vec2(1, 1); } }
		public static Vec2 right { get { return new Vec2(1, 0); } }
		public static Vec2 up { get { return new Vec2(0, 1); } }
	}
}
