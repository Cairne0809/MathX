using System;
using System.Text;

namespace MathematicsX
{
	public struct Cplx
	{
		public double x;
		public double y;

		public Cplx(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
		public Cplx(Cplx c)
		{
			this.x = c.x;
			this.y = c.y;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("C(")
				.Append(x.ToString(format)).Append(", ")
				.Append(y.ToString(format)).Append(")");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }
		public bool ValueEquals(Cplx c)
		{
			double pt = MathX.Tolerance;
			double nt = -pt;
			double dx = x - c.x;
			double dy = y - c.y;
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt;
		}


		public static implicit operator Cplx(double n) { return new Cplx(n, 0); }
		public static explicit operator Cplx(Vec2 v) { return new Cplx(v.x, v.y); }

		public static bool operator ==(Cplx lhs, Cplx rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Cplx lhs, Cplx rhs) { return !lhs.ValueEquals(rhs); }

		public static Cplx operator -(Cplx c)
		{
			c.x = -c.x;
			c.y = -c.y;
			return c;
		}

		public static Cplx operator +(Cplx lhs, Cplx rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			return lhs;
		}

		public static Cplx operator -(Cplx lhs, Cplx rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			return lhs;
		}

		public static Cplx operator *(Cplx lhs, Cplx rhs)
		{
			Cplx nc;
			nc.x = lhs.x * rhs.x - lhs.y * rhs.y;
			nc.y = lhs.y * rhs.x + lhs.x * rhs.y;
			return nc;
		}

		public static Cplx operator /(Cplx lhs, Cplx rhs)
		{
			double div = rhs.x * rhs.x + rhs.y * rhs.y;
			Cplx nc;
			nc.x = (lhs.x * rhs.x + lhs.y * rhs.y) / div;
			nc.y = (lhs.y * rhs.x - lhs.x * rhs.y) / div;
			return nc;
		}

		public static Cplx operator ~(Cplx c)
		{
			c.y = -c.y;
			return c;
		}

		public static double SqrLength(Cplx c)
		{
			return c.x * c.x + c.y * c.y;
		}

		public static double Length(Cplx c)
		{
			return Math.Sqrt(c.x * c.x + c.y * c.y);
		}

		public static void GetArgument(Cplx c, out double rho, out double theta)
		{
			rho = Length(c);
			theta = Math.Atan2(c.y, c.x);
		}

		public static Cplx FromArgument(double rho, double theta)
		{
			Cplx nc;
			nc.x = Math.Cos(theta) * rho;
			nc.y = Math.Sin(theta) * rho;
			return nc;
		}

		public static Cplx Pow(Cplx c, double n)
		{
			double rho = Length(c);
			double theta = Math.Atan2(c.y, c.x);
			rho = Math.Pow(rho, n);
			theta *= n;
			c.x = Math.Cos(theta) * rho;
			c.y = Math.Sin(theta) * rho;
			return c;
		}

		public static Cplx GetRandom()
		{
			double theta = MathX.DoublePI * MathX.GetRandom();
			Cplx nc;
			nc.x = Math.Sin(theta);
			nc.y = Math.Cos(theta);
			return nc;
		}
		
		public static readonly Cplx zero = new Cplx();
		public static readonly Cplx i = new Cplx(0, 1);
		public static readonly Cplx NaC = new Cplx(double.NaN, double.NaN);

	}
}
