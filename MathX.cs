using System;

namespace MathematicsX
{
	public static class MathX
	{
		public const double TOLERANCE = 1e-14;
		public const double E = 2.7182818284590452353602874713526625;
		public const double PI = 3.1415926535897932384626433832795;
		public const double TWO_PI = 6.283185307179586476925286766559;
		public const double HALF_PI = 1.5707963267948966192313216916398;
		public const double R2D = 57.295779513082320876798154814105;
		public const double D2R = 0.01745329251994329576923690768489;
		public static readonly double MaxValue = Math.Sqrt(float.MaxValue);
		/// <summary>
		/// Default: "f14"
		/// </summary>
		public static string ToleranceFormat = "f14";

		private static Random m_random = new Random();

		public static void SetRandom(Random random)
		{
			m_random = random;
		}

		public static double GetRandom()
		{
			return m_random.NextDouble();
		}
		public static double GetRandom(double min, double max)
		{
			return min + (max - min) * m_random.NextDouble();
		}
		
		public static bool ValueEquals(double lhs, double rhs)
		{
			double diff = lhs - rhs;
			return diff <= TOLERANCE && diff >= -TOLERANCE;
		}

		public static double Trunc(double value)
		{
			return Math.Truncate(value);
		}

		public static double Frac(double value)
		{
			return value - Math.Truncate(value);
		}

		public static double Factorial(int x)
		{
			double last = 1;
			for (int i = 2; i <= x; i++)
				last *= i;
			return last;
		}

		public static int Clamp(int value, int min, int max)
		{
			return value < min ? min : value > max ? max : value;
		}
		public static double Clamp(double value, double min, double max)
		{
			return value < min ? min : value > max ? max : value;
		}
		public static double Clamp(double value)
		{
			return value < 0 ? 0 : value > 1 ? 1 : value;
		}
		public static double ClampRadians(double value)
		{
			value = value / TWO_PI;
			value -= Math.Floor(value);
			return value * TWO_PI;
		}

		public static double Weight(double a, double b, double value)
		{
			if (a == b) return 0;
			if (value < a && value < b) return -1;
			if (value > a && value > b) return 1;
			return (a + b - value * 2) / (a - b);
		}
		public static int WeightI(double a, double b, double value)
		{
			if (value < a && value < b) return -1;
			if (value > a && value > b) return 1;
			return 0;
		}

		public static double Lerp(double a, double b, double t)
		{
			return a + (b - a) * t;
		}
		public static double Alerp(double a, double b, double x)
		{
			return (x - a) / (b - a);
		}

		public static double LerpRadians(double a, double b, double t)
		{
			a = ClampRadians(a);
			b = ClampRadians(b);
			a = b - a > PI ? a + TWO_PI : a - b > PI ? a - TWO_PI : a;
			return a + (b - a) * t;
		}

		public static double MoveTowards(double src, double dst, double delta)
		{
			double diff = dst - src;
			diff = Math.Abs(diff) < delta ? diff : diff > 0.0 ? delta : -delta;
			return src + diff;
		}
		public static double MoveTowardsRadians(double src, double dst, double delta)
		{
			src = ClampRadians(src);
			dst = ClampRadians(dst);
			double diff = dst - src;
			src = diff > PI ? src + TWO_PI : -diff > PI ? src - TWO_PI : src;
			return MoveTowards(src, dst, delta);
		}

		public static double SmoothDamp(double src, double dst, ref double curSpeed, double smoothTime, double deltaTime, double maxSpeed = double.MaxValue)
		{
			if (smoothTime>0)
			{
				double targetSpeed = Clamp(Math.Abs(dst - src) / smoothTime, 0.0, maxSpeed);
				curSpeed = Clamp(curSpeed + targetSpeed * deltaTime / smoothTime, 0.0, targetSpeed);
				return MoveTowards(src, dst, curSpeed * deltaTime);
			}
			return dst;
		}
		
		public static Vec3 SolveParaCurve(double x0, double y0, double x1, double y1, double x2, double y2)
		{
			double de1 = x0 - x1;
			double de3 = de1 * (x1 - x2) * (x2 - x0);
			double a = ((x2 - x0) * (y0 - y1) - (x0 - x1) * (y2 - y0)) / de3;
			double b = (y0 - y1 - a * (x0 * x0 - x1 * x1)) / de1;
			double c = y0 - a * x0 * x0 - b * x0;
			return new Vec3(a, b, c);
		}
		public static Vec3 SolveParaCurve(Vec2 p0, Vec2 p1, Vec2 p2)
		{
			return SolveParaCurve(p0.x, p0.y, p1.x, p1.y, p2.x, p2.y);
		}
	}
}
