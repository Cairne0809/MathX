using System;

namespace MathematicsX
{
	public static class VecX
	{
		public static bool ValueEquals(IVector lhs, IVector rhs)
		{
			double pt = MathX.Tolerance;
			double nt = -pt;
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
			{
				double dx = lhs[i] - rhs[i];
				if (double.IsNaN(dx) || dx > pt || dx < nt) return false;
			}
			return true;
		}

		public static T Copy<T>(IVector v) where T : struct, IVector
		{
			T nv = new T();
			int dim = nv.dimension;
			if (dim > v.dimension) dim = v.dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = v[i];
			return nv;
		}

		public static T Negate<T>(T v) where T : struct, IVector
		{
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				v[i] = -v[i];
			return v;
		}

		public static T Add<T>(T lhs, double rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] += rhs;
			return lhs;
		}
		public static T Add<T>(T lhs, T rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] += rhs[i];
			return lhs;
		}

		public static T Sub<T>(double lhs, T rhs) where T : struct, IVector
		{
			int dim = rhs.dimension;
			for (int i = 0; i < dim; i++)
				rhs[i] = lhs - rhs[i];
			return rhs;
		}
		public static T Sub<T>(T lhs, double rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] -= rhs;
			return lhs;
		}
		public static T Sub<T>(T lhs, T rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] -= rhs[i];
			return lhs;
		}

		public static T Mul<T>(T lhs, double rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] *= rhs;
			return lhs;
		}
		public static T Mul<T>(T lhs, T rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] *= rhs[i];
			return lhs;
		}

		public static T Div<T>(double lhs, T rhs) where T : struct, IVector
		{
			int dim = rhs.dimension;
			for (int i = 0; i < dim; i++)
				rhs[i] = lhs / rhs[i];
			return rhs;
		}
		public static T Div<T>(T lhs, double rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] /= rhs;
			return lhs;
		}
		public static T Div<T>(T lhs, T rhs) where T : struct, IVector
		{
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				lhs[i] /= rhs[i];
			return lhs;
		}

		public static bool IsNaV(Vec2 v) { return double.IsNaN(v.x) || double.IsNaN(v.y); }
		public static bool IsNaV(Vec3 v) { return double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z); }
		public static bool IsNaV(IVector v)
		{
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				if (double.IsNaN(v[i])) return true;
			return false;
		}

		public static int MinI(Vec2 v) { return v.x <= v.y ? 0 : 1; }
		public static int MinI(Vec3 v)
		{
			if (v.x <= v.y) { if (v.x <= v.z) return 0; }
			else if (v.y <= v.z) return 1;
			return 2;
		}
		public static int MinI(IVector v)
		{
			int n = 0, dim = v.dimension;
			for (int i = 1; i < dim; i++)
				if (v[i] < v[n]) n = i;
			return n;
		}

		public static int MaxI(Vec2 v) { return v.x > v.y ? 0 : 1; }
		public static int MaxI(Vec3 v)
		{
			if (v.x > v.y) { if (v.x > v.z) return 0; }
			else if (v.y > v.z) return 1;
			return 2;
		}
		public static int MaxI(IVector v)
		{
			int n = 0, dim = v.dimension;
			for (int i = 1; i < dim; i++)
				if (v[i] >= v[n]) n = i;
			return n;
		}

		public static double SqrLength(Vec2 v) { return v.x * v.x + v.y * v.y; }
		public static double SqrLength(Vec3 v) { return v.x * v.x + v.y * v.y + v.z * v.z; }
		public static double SqrLength(IVector v)
		{
			double sum = 0;
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				sum += v[i] * v[i];
			return sum;
		}

		public static double Length(Vec2 v) { return Math.Sqrt(v.x * v.x + v.y * v.y); }
		public static double Length(Vec3 v) { return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z); }
		public static double Length(IVector v)
		{
			double sum = 0;
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				sum += v[i] * v[i];
			return Math.Sqrt(sum);
		}

		public static Vec2 Abs(Vec2 v)
		{
			v.x = Math.Abs(v.x);
			v.y = Math.Abs(v.y);
			return v;
		}
		public static Vec3 Abs(Vec3 v)
		{
			v.x = Math.Abs(v.x);
			v.y = Math.Abs(v.y);
			v.z = Math.Abs(v.z);
			return v;
		}
		public static T Abs<T>(T v) where T : struct, IVector
		{
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				v[i] = Math.Abs(v[i]);
			return v;
		}

		public static Vec2 Normalize(Vec2 v)
		{
			double len = SqrLength(v);
			if (len > 0)
			{
				len = Math.Sqrt(len);
				v.x /= len;
				v.y /= len;
			}
			return v;
		}
		public static Vec3 Normalize(Vec3 v)
		{
			double len = SqrLength(v);
			if (len > 0)
			{
				len = Math.Sqrt(len);
				v.x /= len;
				v.y /= len;
				v.z /= len;
			}
			return v;
		}
		public static T Normalize<T>(T v) where T : struct, IVector
		{
			double len = SqrLength(v);
			if (len > 0)
			{
				len = Math.Sqrt(len);
				int dim = v.dimension;
				for (int i = 0; i < dim; i++)
					v[i] /= len;
			}
			return v;
		}

		public static double Dot(Vec2 lhs, Vec2 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }
		public static double Dot(Vec3 lhs, Vec3 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }
		public static double Dot(IVector lhs, IVector rhs)
		{
			double sum = 0;
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
				sum += lhs[i] * rhs[i];
			return sum;
		}

		public static double SqrDistance(Vec2 lhs, Vec2 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			return lhs.x * lhs.x + lhs.y * lhs.y;
		}
		public static double SqrDistance(Vec3 lhs, Vec3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs.x * lhs.x + lhs.y * lhs.y + lhs.z * lhs.z;
		}
		public static double SqrDistance(IVector lhs, IVector rhs)
		{
			double sum = 0;
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
			{
				double sub = lhs[i] - rhs[i];
				sum += sub * sub;
			}
			return sum;
		}

		public static double Distance(Vec2 lhs, Vec2 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			return Math.Sqrt(lhs.x * lhs.x + lhs.y * lhs.y);
		}
		public static double Distance(Vec3 lhs, Vec3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return Math.Sqrt(lhs.x * lhs.x + lhs.y * lhs.y + lhs.z * lhs.z);
		}
		public static double Distance(IVector lhs, IVector rhs)
		{
			double sum = 0;
			int dim = lhs.dimension;
			for (int i = 0; i < dim; i++)
			{
				double sub = lhs[i] - rhs[i];
				sum += sub * sub;
			}
			return Math.Sqrt(sum);
		}

		public static double Angle(Vec2 lhsNorm, Vec2 rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}
		public static double Angle(Vec3 lhsNorm, Vec3 rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}
		public static double Angle(IVector lhsNorm, IVector rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}
		
		public static Vec2 Project(Vec2 src, Vec2 dstNorm) { return Dot(src, dstNorm) * dstNorm; }
		public static Vec3 Project(Vec3 src, Vec3 dstNorm) { return Dot(src, dstNorm) * dstNorm; }
		public static T Project<T>(T src, T dstNorm) where T : struct, IVector
		{
			double dot = Dot(src, dstNorm);
			int dim = src.dimension;
			for (int i = 0; i < dim; i++)
				src[i] = dot * dstNorm[i];
			return src;
		}

		public static Vec2 Mirror(Vec2 src, Vec2 axisNorm) { return 2 * Project(src, axisNorm) - src; }
		public static Vec3 Mirror(Vec3 src, Vec3 axisNorm) { return 2 * Project(src, axisNorm) - src; }
		public static T Mirror<T>(T src, T axisNorm) where T : struct, IVector
		{
			double dot2 = Dot(src, axisNorm) * 2;
			int dim = src.dimension;
			for (int i = 0; i < dim; i++)
				src[i] = dot2 * axisNorm[i] - src[i];
			return src;
		}

		public static Vec2 Reflect(Vec2 src, Vec2 axisNorm) { return src - 2 * Project(src, axisNorm); }
		public static Vec3 Reflect(Vec3 src, Vec3 axisNorm) { return src - 2 * Project(src, axisNorm); }
		public static T Reflect<T>(T src, T axisNorm) where T : struct, IVector
		{
			double dot2 = Dot(src, axisNorm) * 2;
			int dim = src.dimension;
			for (int i = 0; i < dim; i++)
				src[i] -= dot2 * axisNorm[i];
			return src;
		}

		public static Vec2 Refract(Vec2 srcNorm, Vec2 axisNorm, double eta)
		{
			double dot = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0) return eta * srcNorm - (eta * dot + Math.Sqrt(k)) * axisNorm;
			return srcNorm;
		}
		public static Vec3 Refract(Vec3 srcNorm, Vec3 axisNorm, double eta)
		{
			double dot = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0) return eta * srcNorm - (eta * dot + Math.Sqrt(k)) * axisNorm;
			return srcNorm;
		}
		public static T Refract<T>(T srcNorm, T axisNorm, double eta) where T : struct, IVector
		{
			double dot = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0)
			{
				k = eta * dot + Math.Sqrt(k);
				int dim = srcNorm.dimension;
				for (int i = 0; i < dim; i++)
					srcNorm[i] = eta * srcNorm[i] - k * axisNorm[i];
			}
			return srcNorm;
		}

		public static Vec2 Clamp(Vec2 v, double min = 0, double max = 1)
		{
			v.x = MathX.Clamp(v.x, min, max);
			v.y = MathX.Clamp(v.y, min, max);
			return v;
		}
		public static Vec3 Clamp(Vec3 v, double min = 0, double max = 1)
		{
			v.x = MathX.Clamp(v.x, min, max);
			v.y = MathX.Clamp(v.y, min, max);
			v.z = MathX.Clamp(v.z, min, max);
			return v;
		}
		public static T Clamp<T>(T v, double min = 0, double max = 1) where T : struct, IVector
		{
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				v[i] = MathX.Clamp(v[i], min, max);
			return v;
		}

		public static Vec2 Clamp(Vec2 v, Vec2 min, Vec2 max)
		{
			v.x = MathX.Clamp(v.x, min.x, max.x);
			v.y = MathX.Clamp(v.y, min.y, max.y);
			return v;
		}
		public static Vec3 Clamp(Vec3 v, Vec3 min, Vec3 max)
		{
			v.x = MathX.Clamp(v.x, min.x, max.x);
			v.y = MathX.Clamp(v.y, min.y, max.y);
			v.z = MathX.Clamp(v.z, min.z, max.z);
			return v;
		}
		public static T Clamp<T>(T v, T min, T max) where T : struct, IVector
		{
			int dim = v.dimension;
			for (int i = 0; i < dim; i++)
				v[i] = MathX.Clamp(v[i], min[i], max[i]);
			return v;
		}

		public static Vec2 Lerp(Vec2 a, Vec2 b, double t) { return a + (b - a) * t; }
		public static Vec3 Lerp(Vec3 a, Vec3 b, double t) { return a + (b - a) * t; }
		public static T Lerp<T>(T a, T b, double t) where T : struct, IVector
		{
			int dim = a.dimension;
			for (int i = 0; i < dim; i++)
				a[i] += (b[i] - a[i]) * t;
			return a;
		}

		public static Vec2 Lerp(Vec2 a, Vec2 b, Vec2 t) { return a + (b - a) * t; }
		public static Vec3 Lerp(Vec3 a, Vec3 b, Vec3 t) { return a + (b - a) * t; }
		public static T Lerp<T>(T a, T b, T t) where T : struct, IVector
		{
			int dim = a.dimension;
			for (int i = 0; i < dim; i++)
				a[i] += (b[i] - a[i]) * t[i];
			return a;
		}

		public static Vec2 Alerp(Vec2 a, Vec2 b, Vec2 x)
		{
			Vec2 nv;
			Vec2 div = b - a;
			nv.x = div.x == 0 ? 0 : (x.x - a.x) / div.x;
			nv.y = div.y == 0 ? 0 : (x.y - a.y) / div.y;
			return nv;
		}
		public static Vec3 Alerp(Vec3 a, Vec3 b, Vec3 x)
		{
			Vec3 nv;
			Vec3 div = b - a;
			nv.x = div.x == 0 ? 0 : (x.x - a.x) / div.x;
			nv.y = div.y == 0 ? 0 : (x.y - a.y) / div.y;
			nv.z = div.z == 0 ? 0 : (x.z - a.z) / div.z;
			return nv;
		}
		public static T Alerp<T>(T a, T b, T x) where T : struct, IVector
		{
			int dim = a.dimension;
			for (int i = 0; i < dim; i++)
			{
				double div = b[i] - a[i];
				a[i] = div == 0 ? 0 : (x[i] - a[i]) / div;
			}
			return a;
		}

	}
}
