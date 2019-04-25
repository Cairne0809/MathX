using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public static class GeometryX
	{
		public static Vec2 Distance(Vec2 dp, Vec2 sd)
		{
			double lp = VecX.Project01(dp, sd);
			lp = MathX.Clamp(lp, 0.0, 1.0);
			return dp - lp * sd;
		}
		public static Vec3 Distance(Vec3 dp, Vec3 sd)
		{
			double lp = VecX.Project01(dp, sd);
			lp = MathX.Clamp(lp, 0.0, 1.0);
			return dp - lp * sd;
		}
		public static T Distance<T>(T dp, T sd) where T : IVector, new()
		{
			double lp = VecX.Project01(dp, sd);
			lp = MathX.Clamp(lp, 0.0, 1.0);
			return VecX.Sub(dp, VecX.Mul(sd, lp));
		}

		public static Vec2 Distance(Vec2 p, Triangle<Vec2> tri)
		{
			Vec2 dn = Distance(p - tri.p0, tri.p1 - tri.p0);
			Vec2 n1 = Distance(p - tri.p1, tri.p2 - tri.p1);
			Vec2 n2 = Distance(p - tri.p2, tri.p0 - tri.p2);
			if (VecX.SqrLength(n1) < VecX.SqrLength(dn)) dn = n1;
			if (VecX.SqrLength(n2) < VecX.SqrLength(dn)) dn = n2;
			return dn;
		}
		public static bool InTriangle(Vec2 p, Triangle<Vec2> tri)
		{
			double c0 = VecX.Cross(p - tri.p0, p - tri.p1);
			double c1 = VecX.Cross(p - tri.p1, p - tri.p2);
			double c2 = VecX.Cross(p - tri.p2, p - tri.p0);
			return c0 * c1 >= 0.0 && c1 * c2 >= 0.0 && c2 * c0 >= 0.0;
		}

		public static Vec2 Distance(Vec2 p, IList<Vec2> poly)
		{
			int lastI = poly.Count - 1;
			Vec2 dn, n;
			dn = Distance(p - poly[lastI], poly[0] - poly[lastI]);
			for (int i = 0; i < lastI; i++)
			{
				n = Distance(p - poly[i], poly[i + 1] - poly[i]);
				if (VecX.SqrLength(n) < VecX.SqrLength(dn)) dn = n;
			}
			return dn;
		}
		public static bool InConvexPolygon(Vec2 p, IList<Vec2> poly)
		{
			int lastI = poly.Count - 1;
			double lc = VecX.Cross(p - poly[lastI], p - poly[0]);
			for (int i = 0; i < lastI; i++)
			{
				double c = VecX.Cross(p - poly[i], p - poly[i + 1]);
				if (c * lc < 0.0) return false;
				lc = c;
			}
			return true;
		}

		public static Vec3 Distance(Vec3 p, Tetrahedron<Vec3> tet)
		{
			//incomplete...
			Vec3 v0 = p - tet.p0;
			Vec3 v1 = p - tet.p1;
			Vec3 v2 = p - tet.p2;
			Vec3 v3 = p - tet.p3;
			return default(Vec3);
		}

		public static bool LineSegsIntersect(LineSeg<Vec2> s0, LineSeg<Vec2> s1)
		{
			Vec2 vp0 = s0.p1 - s0.p0;
			Vec2 vp1 = s1.p0 - s0.p0;
			Vec2 vp2 = s1.p1 - s0.p0;
			if (VecX.Cross(vp0, vp1) * VecX.Cross(vp0, vp2) > 0) return false;
			Vec2 vq0 = s1.p1 - s1.p0;
			Vec2 vq1 = s0.p0 - s1.p0;
			Vec2 vq2 = s0.p1 - s1.p0;
			if (VecX.Cross(vq0, vq1) * VecX.Cross(vq0, vq2) > 0) return false;
			return true;
		}
		public static bool LineSegsIntersection(LineSeg<Vec2> s0, LineSeg<Vec2> s1, out Vec2 cp)
		{
			if (LineSegsIntersect(s0, s1))
			{
				double p0x = s0.p0.x;
				double p0y = s0.p0.y;
				double q0x = s1.p0.x;
				double q0y = s1.p0.y;
				double pvx = s0.p1.x - p0x;
				double pvy = s0.p1.y - p0y;
				double qvx = s1.p1.x - q0x;
				double qvy = s1.p1.y - q0y;
				double det = VecX.Cross(new Vec2(pvx, pvy), new Vec2(qvx, qvy));
				cp.x = -(pvx * qvx * (q0y - p0y) + pvy * qvx * p0x - qvy * pvx * q0x) / det;
				cp.y = (pvy * qvy * (q0x - p0x) + pvx * qvy * p0y - qvx * pvy * q0y) / det;
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
			double dot = VecX.Dot(norm, lnDir);
			if (dot == 0)
			{
				cp = default(Vec3);
				return false;
			}
			else
			{
				double t = VecX.Dot(norm, plnP - lnP) / dot;
				cp = lnP + lnDir * t;
				return true;
			}
		}

		public static bool LineSegTriangleIntersection(LineSeg<Vec3> s, Triangle<Vec3> t, out Vec3 cp)
		{
			if (LinePlaneIntersection(s.p0, s.p1 - s.p0, t.p0, VecX.Cross(t.p0 - t.p1, t.p0 - t.p2), out cp))
			{
				if (VecX.Dot(cp - s.p0, cp - s.p1) <= 0)
				{
					Vec3 c0 = VecX.Cross(cp - t.p0, cp - t.p1);
					Vec3 c1 = VecX.Cross(cp - t.p1, cp - t.p2);
					Vec3 c2 = VecX.Cross(cp - t.p2, cp - t.p0);
					if (VecX.Dot(c0, c1) >= 0 && 
						VecX.Dot(c1, c2) >= 0 && 
						VecX.Dot(c2, c0) >= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static int LineSphereIntersection(Vec3 lnD, Vec3 sC, double r, out double s, out double t)
		{
			double a = lnD.SqrLength() * 2;
			double b = VecX.Dot(sC, lnD) * 2;
			double c = sC.SqrLength() - r*r;
			double delta = b * b - 2 * a * c;
			if (delta < 0)
			{
				s = t = 0;
				return 0;
			}
			else if (delta > 0)
			{
				delta = Math.Sqrt(delta);
				s = (b - delta) / a;
				t = (b + delta) / a;
				return 2;
			}
			else
			{
				s = t = b / a;
				return 1;
			}
		}
		public static int LineSphereIntersection<T>(T lnD, T sC, double r, out double s, out double t) where T : IVector, new()
		{
			//SqrDistance(sc, lp + ld * t) = sr * sr, solve t
			//ld*ld * t*t + 2*(sc-lp)*ld * t + (sc-lp)*(sc-lp) - sr*sr = 0
			
			double a = lnD.SqrLength() * 2;
			double b = VecX.Dot(sC, lnD) * 2;
			double c = sC.SqrLength() - r * r;
			double delta = b * b - 2 * a * c;
			if (delta < 0)
			{
				s = t = 0;
				return 0;
			}
			else if (delta > 0)
			{
				delta = Math.Sqrt(delta);
				s = (b - delta) / a;
				t = (b + delta) / a;
				return 2;
			}
			else
			{
				s = t = b / a;
				return 1;
			}
		}

		public static bool CommonPerpendicular(DirLineSeg<Vec3> u, DirLineSeg<Vec3> v, out double s, out double t)
		{
			double A = VecX.Dot(u.d, v.d);
			double B = VecX.SqrLength(u.d);
			double C = VecX.SqrLength(v.d);
			double div = A * A - B * C;
			if (div == 0.0)
			{
				s = t = 0;
				return false;
			}
			Vec3 dp = v.p - u.p;
			double D = VecX.Dot(dp, u.d);
			double E = VecX.Dot(dp, v.d);
			s = (C * D - A * E) / div;
			t = (A * D - B * E) / div;
			return true;
		}
		public static bool CommonPerpendicular<T>(DirLineSeg<T> u, DirLineSeg<T> v, out double s, out double t) where T : IVector, new()
		{
			double A = VecX.Dot(u.d, v.d);
			double B = VecX.SqrLength(u.d);
			double C = VecX.SqrLength(v.d);
			double div = A * A - B * C;
			if (div == 0.0)
			{
				s = t = 0;
				return false;
			}
			T dp = VecX.Sub(v.p, u.p);
			double D = VecX.Dot(dp, u.d);
			double E = VecX.Dot(dp, v.d);
			s = (A * E - C * D) / div;
			t = (B * E - A * D) / div;
			return true;
		}

		public static double Distance(DirLineSeg<Vec2> u, DirLineSeg<Vec2> v)
		{
			double d1 = VecX.Length(Distance(v.p - u.p, u.d));
			double d2 = VecX.Length(Distance(v.p + v.d - u.p, u.d));
			double d3 = VecX.Length(Distance(u.p - v.p, v.d));
			double d4 = VecX.Length(Distance(u.p + u.d - v.p, v.d));
			double d = Math.Min(Math.Min(d1, d2), Math.Min(d3, d4));
			return d;
		}
		public static LineSeg<Vec3> Distance(DirLineSeg<Vec3> u, DirLineSeg<Vec3> v)
		{
			LineSeg<Vec3> r;
			Vec3 dn1 = Distance(v.p - u.p, u.d);
			Vec3 dn2 = Distance(v.p + v.d - u.p, u.d);
			Vec3 dn3 = Distance(u.p - v.p, v.d);
			Vec3 dn4 = Distance(u.p + u.d - v.p, v.d);
			double d1 = VecX.SqrLength(dn1);
			double d2 = VecX.SqrLength(dn2);
			double d3 = VecX.SqrLength(dn3);
			double d4 = VecX.SqrLength(dn4);
			if (d1 < d2 && d1 < d3 && d1 < d4) { r.p1 = v.p; r.p0 = r.p1 - dn1; }
			else if (d2 < d3 && d2 < d4) { r.p1 = v.p + v.d; r.p0 = r.p1 - dn2; }
			else if (d3 < d4) { r.p0 = u.p; r.p1 = r.p0 - dn3; }
			else { r.p0 = u.p + u.d; r.p1 = r.p0 - dn4; }
			double s, t;
			if (CommonPerpendicular(u, v, out s, out t) &&
				s > 0.0 && s < 1.0 && t > 0.0 && t < 1.0)
			{
				r.p0 = u.p + s * u.d;
				r.p1 = v.p + t * v.d;
			}
			return r;
		}
		public static LineSeg<T> Distance<T>(DirLineSeg<T> u, DirLineSeg<T> v) where T : IVector, new()
		{
			LineSeg<T> r;
			T dn1 = Distance(v.p.Sub(u.p), u.d);
			T dn2 = Distance(v.p.Add(v.d).Sub(u.p), u.d);
			T dn3 = Distance(u.p.Sub(v.p), v.d);
			T dn4 = Distance(u.p.Add(u.d).Sub(v.p), v.d);
			double d1 = VecX.SqrLength(dn1);
			double d2 = VecX.SqrLength(dn2);
			double d3 = VecX.SqrLength(dn3);
			double d4 = VecX.SqrLength(dn4);
			if (d1 < d2 && d1 < d3 && d1 < d4) { r.p1 = v.p; r.p0 = r.p1.Sub(dn1); }
			else if (d2 < d3 && d2 < d4) { r.p1 = v.p.Add(v.d); r.p0 = r.p1.Sub(dn2); }
			else if (d3 < d4) { r.p0 = u.p; r.p1 = r.p0.Sub(dn3); }
			else { r.p0 = u.p.Add(u.d); r.p1 = r.p0.Sub(dn4); }
			double s, t;
			if (CommonPerpendicular(u, v, out s, out t) &&
				s > 0.0 && s < 1.0 && t > 0.0 && t < 1.0)
			{
				r.p0 = u.d.Mul(s).Add(u.p);
				r.p1 = v.d.Mul(t).Add(v.p);
			}
			return r;
		}

		public static void MinkowskiDiff(IEnumerable<Vec2> s0, IEnumerable<Vec2> s1, HashSet<Vec2> result)
		{
			foreach (Vec2 p0 in s0)
				foreach (Vec2 p1 in s1)
					result.Add(p0 - p1);
		}
		public static void MinkowskiDiff(IEnumerable<Vec3> s0, IEnumerable<Vec3> s1, HashSet<Vec3> result)
		{
			foreach (Vec3 p0 in s0)
				foreach (Vec3 p1 in s1)
					result.Add(p0 - p1);
		}
		public static void MinkowskiDiff<T>(IEnumerable<T> s0, IEnumerable<T> s1, HashSet<T> result) where T : IVector, new()
		{
			foreach (T p0 in s0)
				foreach (T p1 in s1)
					result.Add(VecX.Sub(p0, p1));
		}
	}
}
