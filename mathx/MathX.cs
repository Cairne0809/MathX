using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathx
{
	public class MathX
	{
		public const double PI = Math.PI;
		public const double DoublePI = 2 * Math.PI;
		public const double HalfPI = 0.5 * Math.PI;
		public const double Rad2Deg = 180 / Math.PI;
		public const double Deg2Rad = Math.PI / 180;

		/// <summary>
		/// The accuracy is related to the number of significant digit.
		/// It should be altered only once at first.
		/// Default value: 1e-14
		/// </summary>
		public static double accuracy = 1e-14;
		
		public static double Sqr(double value)
		{
			return value * value;
		}

		public static int Clamp(int value, int min, int max)
		{
			return value < min ? min : value >= max ? max - 1 : value;
		}
		public static double Clamp(double value, double min = 0.0, double max = 1.0)
		{
			return value < min ? min : value > max ? max : value;
		}
		public static double ClampRadians(double value)
		{
			value = value >= 0.0 ? value - DoublePI * (int)(value / DoublePI) : value + DoublePI * (int)(-value / DoublePI);
			value = value < -PI ? value + DoublePI : value > PI ? value - DoublePI : value;
			return value;
		}

		public static double Lerp(double a, double b, double t)
		{
			return a + (b - a) * t;
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
		
		public static bool SegmentsIntersect(Vec2 p1, Vec2 p2, Vec2 q1, Vec2 q2)
		{
			Vec2 vp0 = p2 - p1;
			Vec2 vp1 = q1 - p1;
			Vec2 vp2 = q2 - p1;
			if (Vec2.Det(vp0, vp1) * Vec2.Det(vp0, vp2) > 0.0) return false;
			Vec2 vq0 = q2 - q1;
			Vec2 vq1 = p1 - q1;
			Vec2 vq2 = p2 - q1;
			if (Vec2.Det(vq0, vq1) * Vec2.Det(vq0, vq2) > 0.0) return false;
			return true;
		}
		
		public static Vec2 SegmentsIntersection(Vec2 p1, Vec2 p2, Vec2 q1, Vec2 q2)
		{
			if (!SegmentsIntersect(p1, p2, q1, q2)) return new Vec2(double.NaN, double.NaN);
			Vec2 pv = p2 - p1;
			Vec2 qv = q2 - q1;
			double det = Vec2.Det(pv, qv);
			double x = -(pv.x * qv.x * (q1.y - p1.y) + pv.y * qv.x * p1.x - qv.y * pv.x * q1.x) / det;
			double y = (pv.y * qv.y * (q1.x - p1.x) + pv.x * qv.y * p1.y - qv.x * pv.y * q1.y) / det;
			return new Vec2(x, y);
		}

		public static Vec3 LinePlaneIntersection(Vec3 lp, Vec3 ld, Vec3 pp, Vec3 pn)
		{
			double dot = ld * pn;
			if (dot == 0.0) return new Vec3(double.NaN, double.NaN, double.NaN);
			double t = ((pp.x - lp.x) * pn.x + (pp.y - lp.y) * pn.y + (pp.z - lp.z) * pn.z) / dot;
			double nx = lp.x + ld.x * t;
			double ny = lp.y + ld.y * t;
			double nz = lp.z + ld.z * t;
			return new Vec3(nx, ny, nz);
		}

		public static double[] SolveParaCurve(double x1, double y1, double x2, double y2, double x3, double y3, double[] opt = null)
		{
			if (opt == null) opt = new double[3];
			double de1 = x1 - x2;
			double de3 = de1 * (x2 - x3) * (x3 - x1);
			double a;
			double b;
			double c;
			opt[0] = a = ((x3 - x1) * (y1 - y2) - (x1 - x2) * (y3 - y1)) / de3;
			opt[1] = b = (y1 - y2 - a * (x1 * x1 - x2 * x2)) / de1;
			opt[2] = c = y1 - a * x1 * x1 - b * x1;
			return opt;
		}
		public static double[] SolveParaCurve(double x1, double y1, double x2, double y2, double[] opt = null)
		{
			if (opt == null) opt = new double[2];
			double de1 = x1 - x2;
			double de2 = -de1 * x2 * x1;
			double a;
			double b;
			opt[0] = a = (x1 * (y2 - y1) - (x2 - x1) * y1) / de2;
			opt[1] = b = (y1 - y2 - a * (x1 * x1 - x2 * x2)) / de1;
			return opt;
		}
		
	}
}
