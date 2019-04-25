using System;

namespace MathematicsX
{
	public static class VecX
	{
		public static bool IsNaV<T>(this T v) where T : IVector
		{
			int dim = v.Dimension;
			for (int i = 0; i < dim; i++)
				if (double.IsNaN(v[i])) return true;
			return false;
		}

		public static bool ValueEquals<T>(this T lhs, T rhs) where T : IVector
		{
			double pt = MathX.TOLERANCE, nt = -pt;
			int dim = lhs.Dimension;
			for (int i = 0; i < dim; i++)
			{
				double dx = lhs[i] - rhs[i];
				if (double.IsNaN(dx) || dx > pt || dx < nt) return false;
			}
			return true;
		}

		public static T Clone<T>(this T v) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = v[i];
			return nv;
		}

		public static T Neg<T>(this T v) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = -v[i];
			return nv;
		}
		public static T Inv<T>(this T v) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = 1 / v[i];
			return nv;
		}

		public static T Add<T>(this T lhs, double rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = lhs[i] + rhs;
			return nv;
		}
		public static T Add<T>(this T lhs, T rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = lhs[i] + rhs[i];
			return nv;
		}

		public static T Sub<T>(this T lhs, double rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = lhs[i] - rhs;
			return nv;
		}
		public static T Sub<T>(this T lhs, T rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = lhs[i] - rhs[i];
			return nv;
		}

		public static T Mul<T>(this T lhs, double rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = lhs[i] * rhs;
			return nv;
		}

		public static T Min<T>(T lhs, T rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = Math.Min(lhs[i], rhs[i]);
			return nv;
		}

		public static T Max<T>(T lhs, T rhs) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = Math.Max(lhs[i], rhs[i]);
			return nv;
		}

		public static int MinAxis<T>(this T v) where T : IVector
		{
			int n = 0, dim = v.Dimension;
			for (int i = 1; i < dim; i++)
				if (v[i] < v[n]) n = i;
			return n;
		}

		public static int MaxAxis<T>(this T v) where T : IVector
		{
			int n = 0, dim = v.Dimension;
			for (int i = 1; i < dim; i++)
				if (v[i] >= v[n]) n = i;
			return n;
		}

		public static double SqrLength<T>(this T v) where T : IVector
		{
			double sum = 0;
			int dim = v.Dimension;
			for (int i = 0; i < dim; i++)
				sum += v[i] * v[i];
			return sum;
		}

		public static double Length<T>(this T v) where T : IVector
		{
			return Math.Sqrt(SqrLength(v));
		}

		public static T Abs<T>(this T v) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = Math.Abs(v[i]);
			return nv;
		}

		public static T Normalize<T>(this T v) where T : IVector, new()
		{
			T nv = new T();
			double len = Length(v);
			if (len > 0)
			{
				int dim = nv.Dimension;
				for (int i = 0; i < dim; i++)
					nv[i] = v[i] / len;
			}
			return nv;
		}

		public static double Dot<T>(this T lhs, T rhs) where T : IVector
		{
			double sum = 0;
			int dim = lhs.Dimension;
			for (int i = 0; i < dim; i++)
				sum += lhs[i] * rhs[i];
			return sum;
		}

		public static double SqrDistance<T>(T lhs, T rhs) where T : IVector
		{
			double sum = 0;
			int dim = lhs.Dimension;
			for (int i = 0; i < dim; i++)
			{
				double sub = lhs[i] - rhs[i];
				sum += sub * sub;
			}
			return sum;
		}

		public static double Distance<T>(this T lhs, T rhs) where T : IVector
		{
			return Math.Sqrt(SqrDistance(lhs, rhs));
		}

		public static double Angle<T>(this T lhsNorm, T rhsNorm) where T : IVector
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}

		public static double Project01<T>(this T src, T dst) where T : IVector
		{
			double up = 0, down = 0;
			int dim = src.Dimension;
			for (int i = 0; i < dim; i++)
			{
				double dstx = dst[i];
				up += src[i] * dstx;
				down += dstx * dstx;
			}
			return down == 0 ? 0 : up / down;
		}

		public static T Project<T>(T src, T dst) where T : IVector, new()
		{
			T nv = new T();
			double pl = Project01(src, dst);
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = pl * dst[i];
			return nv;
		}

		public static T Orthogonalize<T>(T src, T dst) where T : IVector, new()
		{
			T nv = new T();
			double pl = Project01(src, dst);
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = src[i] - pl * dst[i];
			return nv;
		}

		public static T Reflect<T>(T src, T axis) where T : IVector, new()
		{
			T nv = new T();
			double lp = Project01(src, axis) * 2;
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = src[i] - lp * axis[i];
			return nv;
		}

		public static T Mirror<T>(T src, T axis) where T : IVector, new()
		{
			T nv = new T();
			double lp = Project01(src, axis) * 2;
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = lp * axis[i] - src[i];
			return nv;
		}

		public static T Clamp<T>(T v, double min = 0, double max = 1) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = MathX.Clamp(v[i], min, max);
			return nv;
		}

		public static T Clamp<T>(T v, T min, T max) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = MathX.Clamp(v[i], min[i], max[i]);
			return nv;
		}

		public static T Lerp<T>(T a, T b, double t) where T : IVector, new()
		{
			T nv = new T();
			double _t = 1 - t;
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = a[i] * _t + b[i] * t;
			return nv;
		}

		public static T Lerp<T>(T a, T b, T t) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = a[i] + (b[i] - a[i]) * t[i];
			return nv;
		}

		public static T Alerp<T>(T a, T b, T x) where T : IVector, new()
		{
			T nv = new T();
			int dim = nv.Dimension;
			for (int i = 0; i < dim; i++)
				nv[i] = (x[i] - a[i]) / (b[i] - a[i]);
			return nv;
		}

		public static T Refract<T>(T srcNorm, T axisNorm, double eta) where T : IVector, new()
		{
			
			double dot = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0)
			{
				k = eta * dot + Math.Sqrt(k);
				T nv = new T();
				int dim = nv.Dimension;
				for (int i = 0; i < dim; i++)
					nv[i] = eta * srcNorm[i] - k * axisNorm[i];
				return nv;
			}
			else
			{
				return srcNorm.Clone();
			}
		}

		//

		public static bool IsNaV(this Vec2 v)
		{
			return double.IsNaN(v.x) || double.IsNaN(v.y);
		}
		public static bool IsNaV(this Vec3 v)
		{
			return double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z);
		}
		public static bool IsNaV(this Vec4 v)
		{
			return double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z) || double.IsNaN(v.w);
		}
		
		public static bool ValueEquals(this Vec2 lhs, Vec2 rhs)
		{
			double pt = MathX.TOLERANCE, nt = -pt;
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt;
		}
		public static bool ValueEquals(this Vec3 lhs, Vec3 rhs)
		{
			double pt = MathX.TOLERANCE, nt = -pt;
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt
				&& dz <= pt && dz >= nt;
		}
		public static bool ValueEquals(this Vec4 lhs, Vec4 rhs)
		{
			double pt = MathX.TOLERANCE, nt = -pt;
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			double dw = lhs.w - rhs.w;
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt
				&& dz <= pt && dz >= nt
				&& dw <= pt && dw >= nt;
		}

		public static Vec2 Min(Vec2 lhs, Vec2 rhs)
		{
			Vec2 nv;
			nv.x = Math.Min(lhs.x, rhs.x);
			nv.y = Math.Min(lhs.y, rhs.y);
			return nv;
		}
		public static Vec3 Min(Vec3 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = Math.Min(lhs.x, rhs.x);
			nv.y = Math.Min(lhs.y, rhs.y);
			nv.z = Math.Min(lhs.z, rhs.z);
			return nv;
		}
		public static Vec4 Min(Vec4 lhs, Vec4 rhs)
		{
			Vec4 nv;
			nv.x = Math.Min(lhs.x, rhs.x);
			nv.y = Math.Min(lhs.y, rhs.y);
			nv.z = Math.Min(lhs.z, rhs.z);
			nv.w = Math.Min(lhs.w, rhs.w);
			return nv;
		}

		public static Vec2 Max(Vec2 lhs, Vec2 rhs)
		{
			Vec2 nv;
			nv.x = Math.Max(lhs.x, rhs.x);
			nv.y = Math.Max(lhs.y, rhs.y);
			return nv;
		}
		public static Vec3 Max(Vec3 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = Math.Max(lhs.x, rhs.x);
			nv.y = Math.Max(lhs.y, rhs.y);
			nv.z = Math.Max(lhs.z, rhs.z);
			return nv;
		}
		public static Vec4 Max(Vec4 lhs, Vec4 rhs)
		{
			Vec4 nv;
			nv.x = Math.Max(lhs.x, rhs.x);
			nv.y = Math.Max(lhs.y, rhs.y);
			nv.z = Math.Max(lhs.z, rhs.z);
			nv.w = Math.Max(lhs.w, rhs.w);
			return nv;
		}

		public static int MinAxis(this Vec2 v)
		{
			return v.x <= v.y ? 0 : 1;
		}
		public static int MinAxis(this Vec3 v)
		{
			if (v.x <= v.y && v.x <= v.z) return 0;
			if (v.y <= v.z) return 1;
			return 2;
		}
		public static int MinAxis(this Vec4 v)
		{
			if (v.x <= v.y && v.x <= v.z && v.x <= v.w) return 0;
			if (v.y <= v.z && v.y <= v.w) return 1;
			if (v.z <= v.w) return 2;
			return 3;
		}

		public static int MaxAxis(this Vec2 v)
		{
			return v.x > v.y ? 0 : 1;
		}
		public static int MaxAxis(this Vec3 v)
		{
			if (v.x > v.y && v.x > v.z) return 0;
			if (v.y > v.z) return 1;
			return 2;
		}
		public static int MaxAxis(this Vec4 v)
		{
			if (v.x > v.y && v.x > v.z && v.x > v.w) return 0;
			if (v.y > v.z && v.y > v.w) return 1;
			if (v.z > v.w) return 2;
			return 3;
		}

		public static double SqrLength(this Vec2 v)
		{
			return v.x * v.x + v.y * v.y;
		}
		public static double SqrLength(this Vec3 v)
		{
			return v.x * v.x + v.y * v.y + v.z * v.z;
		}
		public static double SqrLength(this Vec4 v)
		{
			return v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w;
		}

		public static double Length(this Vec2 v)
		{
			return Math.Sqrt(v.x * v.x + v.y * v.y);
		}
		public static double Length(this Vec3 v)
		{
			return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
		}
		public static double Length(this Vec4 v)
		{
			return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w);
		}

		public static Vec2 Abs(this Vec2 v)
		{
			return new Vec2(Math.Abs(v.x), Math.Abs(v.y));
		}
		public static Vec3 Abs(this Vec3 v)
		{
			return new Vec3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
		}
		public static Vec4 Abs(this Vec4 v)
		{
			return new Vec4(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z), Math.Abs(v.w));
		}

		public static Vec2 Normalize(this Vec2 v)
		{
			double len = v.Length();
			if (len == 0) return new Vec2();
			return new Vec2(v.x / len, v.y / len);
		}
		public static Vec3 Normalize(this Vec3 v)
		{
			double len = v.Length();
			if (len == 0) return new Vec3();
			return new Vec3(v.x / len, v.y / len, v.z / len);
		}
		public static Vec4 Normalize(this Vec4 v)
		{
			double len = v.Length();
			if (len == 0) return new Vec4();
			return new Vec4(v.x / len, v.y / len, v.z / len, v.w / len);
		}

		public static double SqrDistance(this Vec2 lhs, Vec2 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			return dx * dx + dy * dy;
		}
		public static double SqrDistance(this Vec3 lhs, Vec3 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			return dx * dx + dy * dy + dz * dz;
		}
		public static double SqrDistance(this Vec4 lhs, Vec4 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			double dw = lhs.w - rhs.w;
			return dx * dx + dy * dy + dz * dz + dw * dw;
		}

		public static double Distance(this Vec2 lhs, Vec2 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			return Math.Sqrt(dx * dx + dy * dy);
		}
		public static double Distance(this Vec3 lhs, Vec3 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			return Math.Sqrt(dx * dx + dy * dy + dz * dz);
		}
		public static double Distance(this Vec4 lhs, Vec4 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			double dw = lhs.w - rhs.w;
			return Math.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
		}

		public static double Dot(this Vec2 lhs, Vec2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}
		public static double Dot(this Vec3 lhs, Vec3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}
		public static double Dot(this Vec4 lhs, Vec4 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
		}

		public static double Angle(this Vec2 lhsNorm, Vec2 rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}
		public static double Angle(this Vec3 lhsNorm, Vec3 rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}
		public static double Angle(this Vec4 lhsNorm, Vec4 rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}

		public static double Project01(this Vec2 src, Vec2 dst) { return Dot(src, dst) / SqrLength(dst); }
		public static double Project01(this Vec3 src, Vec3 dst) { return Dot(src, dst) / SqrLength(dst); }
		public static double Project01(this Vec4 src, Vec4 dst) { return Dot(src, dst) / SqrLength(dst); }

		public static Vec2 Project(Vec2 src, Vec2 dst) { return Dot(src, dst) / SqrLength(dst) * dst; }
		public static Vec3 Project(Vec3 src, Vec3 dst) { return Dot(src, dst) / SqrLength(dst) * dst; }
		public static Vec4 Project(Vec4 src, Vec4 dst) { return Dot(src, dst) / SqrLength(dst) * dst; }

		public static Vec2 Orthogonalize(Vec2 src, Vec2 dst) { return src - Dot(src, dst) / SqrLength(dst) * dst; }
		public static Vec3 Orthogonalize(Vec3 src, Vec3 dst) { return src - Dot(src, dst) / SqrLength(dst) * dst; }
		public static Vec4 Orthogonalize(Vec4 src, Vec4 dst) { return src - Dot(src, dst) / SqrLength(dst) * dst; }

		public static Vec2 Reflect(Vec2 src, Vec2 axis) { return src - 2 * Project(src, axis); }
		public static Vec3 Reflect(Vec3 src, Vec3 axis) { return src - 2 * Project(src, axis); }
		public static Vec4 Reflect(Vec4 src, Vec4 axis) { return src - 2 * Project(src, axis); }

		public static Vec2 Mirror(Vec2 src, Vec2 axis) { return 2 * Project(src, axis) - src; }
		public static Vec3 Mirror(Vec3 src, Vec3 axis) { return 2 * Project(src, axis) - src; }
		public static Vec4 Mirror(Vec4 src, Vec4 axis) { return 2 * Project(src, axis) - src; }

		public static Vec2 Clamp(Vec2 v, double min = 0, double max = 1)
		{
			Vec2 nv;
			nv.x = v.x < min ? min : v.x > max ? max : v.x;
			nv.y = v.y < min ? min : v.y > max ? max : v.y;
			return nv;
		}
		public static Vec3 Clamp(Vec3 v, double min = 0, double max = 1)
		{
			Vec3 nv;
			nv.x = v.x < min ? min : v.x > max ? max : v.x;
			nv.y = v.y < min ? min : v.y > max ? max : v.y;
			nv.z = v.z < min ? min : v.z > max ? max : v.z;
			return nv;
		}
		public static Vec4 Clamp(Vec4 v, double min = 0, double max = 1)
		{
			Vec4 nv;
			nv.x = v.x < min ? min : v.x > max ? max : v.x;
			nv.y = v.y < min ? min : v.y > max ? max : v.y;
			nv.z = v.z < min ? min : v.z > max ? max : v.z;
			nv.w = v.w < min ? min : v.w > max ? max : v.w;
			return nv;
		}

		public static Vec2 Clamp(Vec2 v, Vec2 min, Vec2 max)
		{
			Vec2 nv;
			nv.x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x;
			nv.y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y;
			return nv;
		}
		public static Vec3 Clamp(Vec3 v, Vec3 min, Vec3 max)
		{
			Vec3 nv;
			nv.x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x;
			nv.y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y;
			nv.z = v.z < min.z ? min.z : v.z > max.z ? max.z : v.z;
			return nv;
		}
		public static Vec4 Clamp(Vec4 v, Vec4 min, Vec4 max)
		{
			Vec4 nv;
			nv.x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x;
			nv.y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y;
			nv.z = v.z < min.z ? min.z : v.z > max.z ? max.z : v.z;
			nv.w = v.w < min.w ? min.w : v.w > max.w ? max.w : v.w;
			return nv;
		}

		public static Vec2 Lerp(Vec2 a, Vec2 b, double t) { return a + (b - a) * t; }
		public static Vec3 Lerp(Vec3 a, Vec3 b, double t) { return a + (b - a) * t; }
		public static Vec4 Lerp(Vec4 a, Vec4 b, double t) { return a + (b - a) * t; }

		public static Vec2 Lerp(Vec2 a, Vec2 b, Vec2 t) { return a + (b - a) * t; }
		public static Vec3 Lerp(Vec3 a, Vec3 b, Vec3 t) { return a + (b - a) * t; }
		public static Vec4 Lerp(Vec4 a, Vec4 b, Vec4 t) { return a + (b - a) * t; }
		
		public static Vec2 Alerp(Vec2 a, Vec2 b, Vec2 x) { return (x - a) / (b - a); }
		public static Vec3 Alerp(Vec3 a, Vec3 b, Vec3 x) { return (x - a) / (b - a); }
		public static Vec4 Alerp(Vec4 a, Vec4 b, Vec4 x) { return (x - a) / (b - a); }

		public static Vec2 Refract(Vec2 srcNorm, Vec2 axisNorm, double eta)
		{
			double d = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - d * d);
			if (k < 0) return new Vec2(srcNorm.x, srcNorm.y);
			return eta * srcNorm - (eta * d + Math.Sqrt(k)) * axisNorm;
		}
		public static Vec3 Refract(Vec3 srcNorm, Vec3 axisNorm, double eta)
		{
			double d = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - d * d);
			if (k < 0) return new Vec3(srcNorm.x, srcNorm.y, srcNorm.z);
			return eta * srcNorm - (eta * d + Math.Sqrt(k)) * axisNorm;
		}
		public static Vec4 Refract(Vec4 srcNorm, Vec4 axisNorm, double eta)
		{
			double d = Dot(srcNorm, axisNorm);
			double k = 1 - eta * eta * (1 - d * d);
			if (k < 0) return new Vec4(srcNorm.x, srcNorm.y, srcNorm.z, srcNorm.w);
			return eta * srcNorm - (eta * d + Math.Sqrt(k)) * axisNorm;
		}

		//Specials

		public static Vec2 Skew(this Vec2 v)
		{
			Vec2 nv;
			nv.x = -v.y;
			nv.y = v.x;
			return nv;
		}

		public static double Cross(this Vec2 lhs, Vec2 rhs)
		{
			return lhs.x * rhs.y - lhs.y * rhs.x;
		}
		public static Vec3 Cross(this Vec3 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = lhs.y * rhs.z - lhs.z * rhs.y;
			nv.y = lhs.z * rhs.x - lhs.x * rhs.z;
			nv.z = lhs.x * rhs.y - lhs.y * rhs.x;
			return nv;
		}

		public static Vec2 Cross(Vec2 v0, Vec2 v1, Vec2 v2)
		{
			double z = v0.x * v1.y - v0.y * v1.x;
			Vec2 nv;
			nv.x = -z * v2.y;
			nv.y = z * v2.x;
			return nv;
		}
		public static Vec3 Cross(Vec3 v0, Vec3 v1, Vec3 v2)
		{
			Vec3 nv;
			nv.x = v0.y * v1.z - v0.z * v1.y;
			nv.y = v0.z * v1.x - v0.x * v1.z;
			nv.z = v0.x * v1.y - v0.y * v1.x;
			nv.x = nv.y * v2.z - nv.z * v2.y;
			nv.y = nv.z * v2.x - nv.x * v2.z;
			nv.z = nv.x * v2.y - nv.y * v2.x;
			return nv;
		}

		public static double Mixed(Vec3 v0, Vec3 v1, Vec3 v2)
		{
			return v0.x * v1.y * v2.z - v0.x * v2.y * v1.z
				 + v1.x * v2.y * v0.z - v1.x * v0.y * v2.z
				 + v2.x * v0.y * v1.z - v2.x * v1.y * v0.z;
		}

		public static Vec2 Rotate(this Vec2 src, double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			Vec2 nv;
			nv.x = cos * src.x - sin * src.y;
			nv.y = sin * src.x + cos * src.y;
			return nv;
		}
		public static Vec3 Rotate(this Vec3 src, double angle, Vec3 axisNorm)
		{
			double sx = src.x, sy = src.y, sz = src.z;
			double x = axisNorm.x, y = axisNorm.y, z = axisNorm.z;
			double xx = x * x, yy = y * y, zz = z * z;
			double xy = x * y, yz = y * z, xz = x * z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double _cos = 1 - cos;
			Vec3 nv;
			nv.x = (xx + (1 - xx) * cos) * sx + (xy * _cos - z * sin) * sy + (xz * _cos + y * sin) * sz;
			nv.y = (xy * _cos + z * sin) * sx + (yy + (1 - yy) * cos) * sy + (yz * _cos - x * sin) * sz;
			nv.z = (xz * _cos - y * sin) * sx + (yz * _cos + x * sin) * sy + (zz + (1 - zz) * cos) * sz;
			return nv;
		}
		
	}

}
