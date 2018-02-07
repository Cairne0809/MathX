using System;

namespace MathematicsX
{
	public static class MathX
	{
		public const string ToleranceFormat = "F14";
		public const double Tolerance = 1e-14;
		public const double E = 2.7182818284590452353602874713526625;
		public const double PI = 3.1415926535897932384626433832795;
		public const double DoublePI = 6.283185307179586476925286766559;
		public const double HalfPI = 1.5707963267948966192313216916398;
		public const double Rad2Deg = 57.295779513082320876798154814105;
		public const double Deg2Rad = 0.01745329251994329576923690768489;
		public static readonly double MaxValue = Math.Sqrt(float.MaxValue);

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

		public static bool ValueEquals(double lhs, double rhs)
		{
			double delta = lhs - rhs;
			return delta <= Tolerance && delta >= -Tolerance;
		}

		public static double Fract(double value)
		{
			return value - Math.Floor(value);
		}

		public static int Clamp(int value, int min, int max)
		{
			return value < min ? min : value > max ? max : value;
		}
		public static double Clamp(double value, double min = 0, double max = 1)
		{
			return value < min ? min : value > max ? max : value;
		}
		public static double ClampRadians(double value)
		{
			value = value / DoublePI;
			value -= (int)value;
			return value * DoublePI;
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
			double div = b - a;
			return div == 0 ? 0 : (x - a) / div;
		}

		public static double LerpRadians(double a, double b, double t)
		{
			a = ClampRadians(a);
			b = ClampRadians(b);
			a = b - a > PI ? a + DoublePI : a - b > PI ? a - DoublePI : a;
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

		public static double PointLineDistance(Vec2 dp, Vec2 lnDir)
		{
			Vec2 proj = VecX.Project(dp, lnDir);
			return Math.Sqrt(VecX.SqrLength(dp) - VecX.SqrLength(proj));
		}
		public static double PointLineDistance(Vec3 dp, Vec3 lnDir)
		{
			Vec3 proj = VecX.Project(dp, lnDir);
			return Math.Sqrt(VecX.SqrLength(dp) - VecX.SqrLength(proj));
		}
		public static double PointLineDistance<T>(T dp, T lnDir) where T : struct, IVector
		{
			T proj = VecX.Project(dp, lnDir);
			return Math.Sqrt(VecX.SqrLength(dp) - VecX.SqrLength(proj));
		}

		public static bool SegmentsIntersect(Vec2 p0, Vec2 p1, Vec2 q0, Vec2 q1)
		{
			Vec2 vp0 = p1 - p0;
			Vec2 vp1 = q0 - p0;
			Vec2 vp2 = q1 - p0;
			if (Vec2.Cross(vp0, vp1) * Vec2.Cross(vp0, vp2) > 0) return false;
			Vec2 vq0 = q1 - q0;
			Vec2 vq1 = p0 - q0;
			Vec2 vq2 = p1 - q0;
			if (Vec2.Cross(vq0, vq1) * Vec2.Cross(vq0, vq2) > 0) return false;
			return true;
		}
		
		public static bool SegmentsIntersection(Vec2 p0, Vec2 p1, Vec2 q0, Vec2 q1, out Vec2 cp)
		{
			if (SegmentsIntersect(p0, p1, q0, q1))
			{
				Vec2 pv = p1 - p0;
				Vec2 qv = q1 - q0;
				double det = Vec2.Cross(pv, qv);
				cp.x = -(pv.x * qv.x * (q0.y - p0.y) + pv.y * qv.x * p0.x - qv.y * pv.x * q0.x) / det;
				cp.y = (pv.y * qv.y * (q0.x - p0.x) + pv.x * qv.y * p0.y - qv.x * pv.y * q0.y) / det;
				return true;
			}
			else
			{
				cp = default(Vec2);
				return false;
			}
		}

		public static bool LinePlaneIntersection(Vec3 lnP, Vec3 lnDir, Vec3 plnP, Vec3 norm, out Vec3 cp)
		{
			double dot = VecX.Dot(lnDir, norm);
			if (dot == 0)
			{
				cp = default(Vec3);
				return false;
			}
			else
			{
				double t = VecX.Dot((plnP - lnP), norm) / dot;
				cp = lnP + lnDir * t;
				return true;
			}
		}

		public static bool SegmentTriangleIntersection(Vec3 p0, Vec3 p1, Vec3 v0, Vec3 v1, Vec3 v2, out Vec3 cp)
		{
			if (LinePlaneIntersection(p0, p1 - p0, v0, Vec3.Cross(v0 - v1, v0 - v2), out cp))
			{
				if (VecX.Dot(cp - p0, cp - p1) <= 0)
				{
					Vec3 c0 = Vec3.Cross(cp - v0, cp - v1);
					Vec3 c1 = Vec3.Cross(cp - v1, cp - v2);
					Vec3 c2 = Vec3.Cross(cp - v2, cp - v0);
					if (VecX.Dot(c0, c1) >= 0 && VecX.Dot(c1, c2) >= 0 && VecX.Dot(c2, c0) >= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static int LineSphereIntersection(Vec3 lnP, Vec3 lnDir, Vec3 center, double radius, out Vec3 cp0, out Vec3 cp1)
		{
			//SqrDistance(sc, lp + ld * t) = sr * sr, solve t
			//ld*ld * t*t + 2*(sc-lp)*ld * t + (sc-lp)*(sc-lp) - sr*sr = 0

			lnDir = VecX.Normalize(lnDir);
			Vec3 pc = center - lnP;
			double da = VecX.SqrLength(lnDir) * 2;
			double b = VecX.Dot(pc, lnDir) * -2;
			double c = VecX.SqrLength(pc) - radius * radius;
			double delta = b * b - 2 * da * c;
			if (delta > 0)
			{
				delta = Math.Sqrt(delta);
				cp0 = lnP + (b + delta) / da * lnDir;
				cp1 = lnP + (b - delta) / da * lnDir;
				return 2;
			}
			else if (delta == 0)
			{
				cp0 = lnP + b / da * lnDir;
				cp1 = default(Vec3);
				return 1;
			}
			else
			{
				cp0 = cp1 = default(Vec3);
				return 0;
			}
		}

		public static int SegmentSphereIntersection(Vec3 p0, Vec3 p1, Vec3 sphereCenter, double radius, out Vec3 cp0, out Vec3 cp1)
		{
			int count = LineSphereIntersection(p0, p1 - p0, sphereCenter, radius, out cp0, out cp1);
			if (count == 2 && VecX.Dot(cp1 - p0, cp1 - p1) > 0)
			{
				cp1 = default(Vec3);
				count--;
			}
			if (count >= 1 && VecX.Dot(cp0 - p0, cp0 - p1) > 0)
			{
				cp0 = cp1;
				count--;
			}
			return count;
		}

		public static bool LineIntersection(Vec2 p0, Vec2 v0, Vec2 p1, Vec2 v1, out Vec2 cp)
		{
			Vec3 dp = p0 - p1;
			double v0v0 = VecX.SqrLength(v0);
			double v1v1 = VecX.SqrLength(v1);
			double v0v1 = VecX.Dot(v0, v1);
			double div = v0v0 * v1v1 - v0v1 * v0v1;
			if (div == 0)
			{
				cp = default(Vec2);
				return false;
			}
			else
			{
				double dpv0 = VecX.Dot(dp, v0);
				double dpv1 = VecX.Dot(dp, v1);
				double t = (v0v1 * dpv1 - v1v1 * dpv0) / div;
				cp = p0 + v0 * t;
				return true;
			}
		}
		public static bool CommonPerpendicular(Vec3 p0, Vec3 v0, Vec3 p1, Vec3 v1, out Vec3 pp0, out Vec3 pp1)
		{
			Vec3 dp = p0 - p1;
			double v0v0 = VecX.SqrLength(v0);
			double v1v1 = VecX.SqrLength(v1);
			double v0v1 = VecX.Dot(v0, v1);
			double div = v0v0 * v1v1 - v0v1 * v0v1;
			if (div == 0)
			{
				pp0 = default(Vec3);
				pp1 = default(Vec3);
				return false;
			}
			else
			{
				double dpv0 = VecX.Dot(dp, v0);
				double dpv1 = VecX.Dot(dp, v1);
				double t0 = (v0v1 * dpv1 - v1v1 * dpv0) / div;
				double t1 = (v0v0 * dpv1 - v0v1 * dpv0) / div;
				pp0 = p0 + v0 * t0;
				pp1 = p1 + v1 * t1;
				return true;
			}
		}
		public static bool CommonPerpendicular<T>(T p0, T v0, T p1, T v1, out T pp0, out T pp1) where T : struct, IVector
		{
			T dp = VecX.Sub(p0, p1);
			double v0v0 = VecX.SqrLength(v0);
			double v1v1 = VecX.SqrLength(v1);
			double v0v1 = VecX.Dot(v0, v1);
			double div = v0v0 * v1v1 - v0v1 * v0v1;
			if (div == 0)
			{
				pp0 = default(T);
				pp1 = default(T);
				return false;
			}
			else
			{
				double dpv0 = VecX.Dot(dp, v0);
				double dpv1 = VecX.Dot(dp, v1);
				double t0 = (v0v1 * dpv1 - v1v1 * dpv0) / div;
				double t1 = (v0v0 * dpv1 - v0v1 * dpv0) / div;
				pp0 = VecX.Add(p0, VecX.Mul(v0, t0));
				pp1 = VecX.Add(p1, VecX.Mul(v1, t1));
				return true;
			}
		}
		
	}
}
