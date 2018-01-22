﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public static class VecX
	{
		public static bool IsNaV(Vec2 v) { return double.IsNaN(v.x) || double.IsNaN(v.y); }
		public static bool IsNaV(Vec3 v) { return double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z); }
		public static bool IsNaV(Vec4 v) { return double.IsNaN(v.x) || double.IsNaN(v.y) || double.IsNaN(v.z) || double.IsNaN(v.w); }

		public static double SqrLength(Vec2 v) { return v.x * v.x + v.y * v.y; }
		public static double SqrLength(Vec3 v) { return v.x * v.x + v.y * v.y + v.z * v.z; }
		public static double SqrLength(Vec4 v) { return v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w; }

		public static double Length(Vec2 v) { return Math.Sqrt(v.x * v.x + v.y * v.y); }
		public static double Length(Vec3 v) { return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z); }
		public static double Length(Vec4 v) { return Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w); }

		public static Vec2 Abs(Vec2 v) { return new Vec2(Math.Abs(v.x), Math.Abs(v.y)); }
		public static Vec3 Abs(Vec3 v) { return new Vec3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z)); }
		public static Vec4 Abs(Vec4 v) { return new Vec4(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z), Math.Abs(v.w)); }

		public static int MinI(Vec2 v) { return v.x <= v.y ? 0 : 1; }
		public static int MinI(Vec3 v)
		{
			if (v.x <= v.y) { if (v.x <= v.z) return 0; }
			else if (v.y <= v.z) return 1;
			return 2;
		}
		public static int MinI(Vec4 v)
		{
			int i = 0;
			if (v.y < v.x) i = 1;
			if (v.z < v[i]) i = 2;
			if (v.w < v[i]) return 3;
			return i;
		}

		public static int MaxI(Vec2 v) { return v.x > v.y ? 0 : 1; }
		public static int MaxI(Vec3 v)
		{
			if (v.x > v.y) { if (v.x > v.z) return 0; }
			else if (v.y > v.z) return 1;
			return 2;
		}
		public static int MaxI(Vec4 v)
		{
			int i = 0;
			if (v.y >= v.x) i = 1;
			if (v.z >= v[i]) i = 2;
			if (v.w >= v[i]) return 3;
			return i;
		}

		public static Vec2 Normalize(Vec2 v)
		{
			double len = SqrLength(v);
			if (len > 0 && len != 1) return v / Math.Sqrt(len);
			return new Vec2(v);
		}
		public static Vec3 Normalize(Vec3 v)
		{
			double len = SqrLength(v);
			if (len > 0 && len != 1) return v / Math.Sqrt(len);
			return new Vec3(v);
		}
		public static Vec4 Normalize(Vec4 v)
		{
			double len = SqrLength(v);
			if (len > 0 && len != 1) return v / Math.Sqrt(len);
			return new Vec4(v);
		}

		public static double Dot(Vec2 lhs, Vec2 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y; }
		public static double Dot(Vec3 lhs, Vec3 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }
		public static double Dot(Vec4 lhs, Vec4 rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w; }

		public static double Determinant(Vec2 lhs, Vec2 rhs) { return lhs.x * rhs.y - lhs.y * rhs.x; }
		public static double Determinant(Vec3 v0, Vec3 v1, Vec3 v2) { return Mat3x3.Determinant(new Mat3x3(v0, v1, v2)); }
		public static double Determinant(Vec4 v0, Vec4 v1, Vec4 v2, Vec4 v3) { return Mat4x4.Determinant(new Mat4x4(v0, v1, v2, v3)); }

		public static Vec3 Cross(Vec3 lhs, Vec3 rhs)
		{
			double x = lhs.y * rhs.z - lhs.z * rhs.y;
			double y = lhs.z * rhs.x - lhs.x * rhs.z;
			double z = lhs.x * rhs.y - lhs.y * rhs.x;
			return new Vec3(x, y, z);
		}

		public static double SqrDistance(Vec2 lhs, Vec2 rhs) { return SqrLength(lhs - rhs); }
		public static double SqrDistance(Vec3 lhs, Vec3 rhs) { return SqrLength(lhs - rhs); }
		public static double SqrDistance(Vec4 lhs, Vec4 rhs) { return SqrLength(lhs - rhs); }

		public static double Distance(Vec2 lhs, Vec2 rhs) { return Length(lhs - rhs); }
		public static double Distance(Vec3 lhs, Vec3 rhs) { return Length(lhs - rhs); }
		public static double Distance(Vec4 lhs, Vec4 rhs) { return Length(lhs - rhs); }

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
		public static double Angle(Vec4 lhsNorm, Vec4 rhsNorm)
		{
			double cos = Dot(lhsNorm, rhsNorm);
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
		public static Vec3 Rotate(Vec3 src, double angle, Vec3 axisNorm)
		{
			double sx = src.x, sy = src.y, sz = src.z;
			double ax = axisNorm.x, ay = axisNorm.y, az = axisNorm.z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double vx = (ax * ax + (1 - ax * ax) * cos) * sx + (ax * ay * (1 - cos) - az * sin) * sy + (ax * az * (1 - cos) + ay * sin) * sz;
			double vy = (ay * ax * (1 - cos) + az * sin) * sx + (ay * ay + (1 - ay * ay) * cos) * sy + (ay * az * (1 - cos) - ax * sin) * sz;
			double vz = (az * ax * (1 - cos) - ay * sin) * sx + (az * ay * (1 - cos) + ax * sin) * sy + (az * az + (1 - az * az) * cos) * sz;
			return new Vec3(vx, vy, vz);
		}

		public static Vec2 Project(Vec2 src, Vec2 dstNorm) { return Dot(src, dstNorm) * dstNorm; }
		public static Vec3 Project(Vec3 src, Vec3 dstNorm) { return Dot(src, dstNorm) * dstNorm; }
		public static Vec4 Project(Vec4 src, Vec4 dstNorm) { return Dot(src, dstNorm) * dstNorm; }

		public static Vec3 ProjectOnPlane(Vec3 src, Vec3 norm) { return Project(src, Cross(Cross(norm, src), norm)); }

		public static Vec2 Mirror(Vec2 src, Vec2 axisNorm) { return 2 * Project(src, axisNorm) - src; }
		public static Vec3 Mirror(Vec3 src, Vec3 axisNorm) { return 2 * Project(src, axisNorm) - src; }
		public static Vec4 Mirror(Vec4 src, Vec4 axisNorm) { return 2 * Project(src, axisNorm) - src; }

		public static Vec2 Reflect(Vec2 src, Vec2 axisNorm) { return src - 2 * Project(src, axisNorm); }
		public static Vec3 Reflect(Vec3 src, Vec3 axisNorm) { return src - 2 * Project(src, axisNorm); }
		public static Vec4 Reflect(Vec4 src, Vec4 axisNorm) { return src - 2 * Project(src, axisNorm); }

		public static Vec2 Refract(Vec2 src, Vec2 axisNorm, double eta)
		{
			double dot = Dot(src, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0) return eta * src - (eta * dot + Math.Sqrt(k)) * axisNorm;
			return new Vec2();
		}
		public static Vec3 Refract(Vec3 src, Vec3 axisNorm, double eta)
		{
			double dot = Dot(src, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0) return eta * src - (eta * dot + Math.Sqrt(k)) * axisNorm;
			return new Vec3();
		}
		public static Vec4 Refract(Vec4 src, Vec4 axisNorm, double eta)
		{
			double dot = Dot(src, axisNorm);
			double k = 1 - eta * eta * (1 - dot * dot);
			if (k >= 0) return eta * src - (eta * dot + Math.Sqrt(k)) * axisNorm;
			return new Vec4();
		}

		public static Vec2 Lerp(Vec2 a, Vec2 b, double t) { return a + t * (b - a); }
		public static Vec3 Lerp(Vec3 a, Vec3 b, double t) { return a + t * (b - a); }
		public static Vec4 Lerp(Vec4 a, Vec4 b, double t) { return a + t * (b - a); }
		public static Vec2 Lerp(Vec2 a, Vec2 b, Vec2 t) { return a + t * (b - a); }
		public static Vec3 Lerp(Vec3 a, Vec3 b, Vec3 t) { return a + t * (b - a); }
		public static Vec4 Lerp(Vec4 a, Vec4 b, Vec4 t) { return a + t * (b - a); }

		public static Vec2 Alerp(Vec2 a, Vec2 b, Vec2 x) { return (x - a) / (b - a); }
		public static Vec3 Alerp(Vec3 a, Vec3 b, Vec3 x) { return (x - a) / (b - a); }
		public static Vec4 Alerp(Vec4 a, Vec4 b, Vec4 x) { return (x - a) / (b - a); }
	}
}
