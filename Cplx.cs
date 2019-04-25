using System;
using System.Text;

namespace MathematicsX
{
	public struct Cplx
	{
		public double a;
		public double b;

		public Cplx(double a, double b)
		{
			this.a = a;
			this.b = b;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(a.ToString(format));
			if (b != 0)
			{
				if (b > 0) sb.Append("+");
				sb.Append(b.ToString(format)).Append("i");
			}
			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString(MathX.ToleranceFormat);
		}


		public static implicit operator Cplx(double x) { return new Cplx(x, 0); }
		public static explicit operator double(Cplx x) { return x.a; }
		public static explicit operator Cplx(Vec2 v) { return new Cplx(v.x, v.y); }

		public static bool IsNaN(Cplx x)
		{
			return double.IsNaN(x.a) || double.IsNaN(x.b);
		}

		public static Cplx operator -(Cplx x)
		{
			x.a = -x.a;
			x.b = -x.b;
			return x;
		}

		public static Cplx operator +(Cplx lhs, Cplx rhs)
		{
			lhs.a += rhs.a;
			lhs.b += rhs.b;
			return lhs;
		}

		public static Cplx operator -(Cplx lhs, Cplx rhs)
		{
			lhs.a -= rhs.a;
			lhs.b -= rhs.b;
			return lhs;
		}

		public static Cplx operator *(Cplx lhs, Cplx rhs)
		{
			if (lhs.b == 0 && rhs.b == 0)
			{
				return new Cplx(lhs.a * rhs.a, 0);
			}
			Cplx nc;
			nc.a = lhs.a * rhs.a - lhs.b * rhs.b;
			nc.b = lhs.b * rhs.a + lhs.a * rhs.b;
			return nc;
		}

		public static Cplx operator /(Cplx lhs, Cplx rhs)
		{
			if (lhs.b == 0 && rhs.b == 0)
			{
				return new Cplx(lhs.a / rhs.a, 0);
			}
			double div = rhs.a * rhs.a + rhs.b * rhs.b;
			Cplx nc;
			nc.a = (lhs.a * rhs.a + lhs.b * rhs.b) / div;
			nc.b = (lhs.b * rhs.a - lhs.a * rhs.b) / div;
			return nc;
		}

		public static Cplx operator ~(Cplx x)
		{
			x.b = -x.b;
			return x;
		}

		public static double SqrLength(Cplx x)
		{
			return x.a * x.a + x.b * x.b;
		}

		public static double Length(Cplx x)
		{
			return Math.Sqrt(x.a * x.a + x.b * x.b);
		}

		public static void GetArgument(Cplx x, out double rho, out double theta)
		{
			rho = Length(x);
			theta = Math.Atan2(x.b, x.a);
		}

		public static Cplx FromArgument(double rho, double theta)
		{
			Cplx nc;
			nc.a = Math.Cos(theta) * rho;
			nc.b = Math.Sin(theta) * rho;
			return nc;
		}
		
		public static Cplx Pow(Cplx x, double y)
		{
			double rho = Length(x);
			double theta = Math.Atan2(x.b, x.a);
			rho = Math.Pow(rho, y);
			theta *= y;
			x.a = Math.Cos(theta) * rho;
			x.b = Math.Sin(theta) * rho;
			return x;
		}

		public static Cplx Sqrt(Cplx x)
		{
			double rho = Length(x);
			double theta = Math.Atan2(x.a, x.b);
			rho = Math.Sqrt(rho);
			theta *= 0.5;
			x.a = Math.Cos(theta) * rho;
			x.b = Math.Sin(theta) * rho;
			return x;
		}
		
		public static readonly Cplx zero = new Cplx();
		public static readonly Cplx i = new Cplx(0, 1);
		public static readonly Cplx NaN = new Cplx(double.NaN, double.NaN);
	}
}
