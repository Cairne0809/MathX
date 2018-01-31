using System;

namespace MathematicsX
{
	public static class MathX
	{
		public const string ToleranceFormat = "F14";
		public const double Tolerance = 1e-14;
		public const double E = Math.E;
		public const double PI = Math.PI;
		public const double DoublePI = 2 * PI;
		public const double HalfPI = 0.5 * PI;
		public const double Rad2Deg = 180 / PI;
		public const double Deg2Rad = PI / 180;
		public static readonly double MaxValue = Math.Sqrt(double.MaxValue);

		static Random m_random = new Random();

		public static double GetRandom()
		{
			return m_random.NextDouble();
		}
		public static double GetRandom(double min, double max)
		{
			return min + (max - min) * m_random.NextDouble();
		}
		public static void SetRandom(Random random)
		{
			m_random = random;
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
			value = value >= 0 ? value - DoublePI * (int)(value / DoublePI) : value + DoublePI * (int)(-value / DoublePI);
			value = value < -PI ? value + DoublePI : value > PI ? value - DoublePI : value;
			return value;
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
			a = b-a > PI? a + DoublePI : a-b > PI? a - DoublePI : a;
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
			src = diff > PI ? src + DoublePI : -diff > PI ? src - DoublePI : src;
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

		public static double PointLineDistance(Vec2 p, Vec2 lnP, Vec2 lnDir)
		{
			Vec2 lpp = p - lnP;
			Vec2 proj = VecX.Project(lpp, VecX.Normalize(lnDir));
			return Math.Sqrt(VecX.SqrLength(lpp) - VecX.SqrLength(proj));
		}
		public static double PointLineDistance(Vec3 p, Vec3 lnP, Vec3 lnDir)
		{
			Vec3 lpp = p - lnP;
			Vec3 proj = VecX.Project(lpp, VecX.Normalize(lnDir));
			return Math.Sqrt(VecX.SqrLength(lpp) - VecX.SqrLength(proj));
		}
		public static double PointLineDistance(Vec4 p, Vec4 lnP, Vec4 lnDir)
		{
			Vec4 lpp = p - lnP;
			Vec4 proj = VecX.Project(lpp, VecX.Normalize(lnDir));
			return Math.Sqrt(VecX.SqrLength(lpp) - VecX.SqrLength(proj));
		}

		public static bool SegmentsIntersect(Vec2 p0, Vec2 p1, Vec2 q0, Vec2 q1)
		{
			Vec2 vp0 = p1 - p0;
			Vec2 vp1 = q0 - p0;
			Vec2 vp2 = q1 - p0;
			if (Vec2.Cross(vp0, vp1) * Vec2.Cross(vp0, vp2) > 0.0) return false;
			Vec2 vq0 = q1 - q0;
			Vec2 vq1 = p0 - q0;
			Vec2 vq2 = p1 - q0;
			if (Vec2.Cross(vq0, vq1) * Vec2.Cross(vq0, vq2) > 0.0) return false;
			return true;
		}
		
		public static Vec2 SegmentsIntersection(Vec2 p0, Vec2 p1, Vec2 q0, Vec2 q1)
		{
			if (!SegmentsIntersect(p0, p1, q0, q1)) return Vec2.NaV;
			Vec2 pv = p1 - p0;
			Vec2 qv = q1 - q0;
			double det = Vec2.Cross(pv, qv);
			double x = -(pv.x * qv.x * (q0.y - p0.y) + pv.y * qv.x * p0.x - qv.y * pv.x * q0.x) / det;
			double y = (pv.y * qv.y * (q0.x - p0.x) + pv.x * qv.y * p0.y - qv.x * pv.y * q0.y) / det;
			return new Vec2(x, y);
		}

		public static Vec3 LinePlaneIntersection(Vec3 lnP, Vec3 lnDir, Vec3 plnP, Vec3 norm)
		{
			double dot = VecX.Dot(lnDir, norm);
			if (dot == 0.0) return Vec3.NaV;
			double t = VecX.Dot((plnP - lnP), norm) / dot;
			Vec3 nv = lnP + lnDir * t;
			return nv;
		}

		public static Vec3 SegmentTriangleIntersection(Vec3 p0, Vec3 p1, Vec3 v0, Vec3 v1, Vec3 v2)
		{
			Vec3 cp = LinePlaneIntersection(p0, p1 - p0, v0, Vec3.Cross(v0 - v1, v0 - v2));
			if (VecX.IsNaV(cp)) return Vec3.NaV;
			if (VecX.Dot(cp - p0, cp - p1) > 0) return Vec3.NaV;
			Vec3 c0 = Vec3.Cross(cp - v0, cp - v1);
			Vec3 c1 = Vec3.Cross(cp - v1, cp - v2);
			Vec3 c2 = Vec3.Cross(cp - v2, cp - v0);
			if (VecX.Dot(c0, c1) < 0 || VecX.Dot(c1, c2) < 0 || VecX.Dot(c2, c0) < 0) return Vec3.NaV;
			return cp;
		}

		public static int LineSphereIntersection(Vec3 lnP, Vec3 lnDir, Vec3 center, double radius, out Vec3 cp0, out Vec3 cp1)
		{
			//SqrDistance(sc, lp + ld * t) = sr * sr, solve t
			//ld*ld * t*t + 2*(sc-lp)*ld * t + (sc-lp)*(sc-lp) - sr*sr = 0

			lnDir = VecX.Normalize(lnDir);
			Vec3 pc = center - lnP;
			double a = VecX.SqrLength(lnDir);
			double b = VecX.Dot(pc, lnDir) * -2;
			double c = VecX.SqrLength(pc) - radius * radius;
			double delta = b * b - 4 * a * c;
			if (delta > 0)
			{
				delta = Math.Sqrt(delta);
				cp0 = lnP + (b + delta) / a * 0.5 * lnDir;
				cp1 = lnP + (b - delta) / a * 0.5 * lnDir;
				return 2;
			}
			else if (delta == 0)
			{
				cp0 = lnP + b / a * 0.5 * lnDir;
				cp1 = Vec3.NaV;
				return 1;
			}
			else
			{
				cp0 = cp1 = Vec3.NaV;
				return 0;
			}
		}

		public static int SegmentSphereIntersection(Vec3 p0, Vec3 p1, Vec3 sphereCenter, double radius, out Vec3 cp0, out Vec3 cp1)
		{
			int count = LineSphereIntersection(p0, p1 - p0, sphereCenter, radius, out cp0, out cp1);
			if (count == 2 && VecX.Dot(cp1 - p0, cp1 - p1) > 0)
			{
				cp1 = Vec3.NaV;
				count--;
			}
			if (count >= 1 && VecX.Dot(cp0 - p0, cp0 - p1) > 0)
			{
				cp0 = cp1;
				count--;
			}
			return count;
		}
		
	}
}
