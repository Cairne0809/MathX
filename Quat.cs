using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Quat
	{
		public double x;
		public double y;
		public double z;
		public double w;

		public Quat(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		public Quat(Vec3 v, double w)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = w;
		}
		public Quat(Quat q)
		{
			this.x = q.x;
			this.y = q.y;
			this.z = q.z;
			this.w = q.w;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Q(")
				.Append(x.ToString(format)).Append(", ")
				.Append(y.ToString(format)).Append(", ")
				.Append(z.ToString(format)).Append(", ")
				.Append(w.ToString(format)).Append(")");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }
		public bool ValueEquals(Quat q)
		{
			double pt = MathX.Tolerance;
			double nt = -pt;
			double dx, dy, dz, dw;
			if (w > 0 ^ q.w > 0)
			{
				dx = x + q.x;
				dy = y + q.y;
				dz = z + q.z;
				dw = w + q.w;
			}
			else
			{
				dx = x - q.x;
				dy = y - q.y;
				dz = z - q.z;
				dw = w - q.w;
			}
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt
				&& dz <= pt && dz >= nt
				&& dw <= pt && dw >= nt;
		}


		public static explicit operator Quat(Vec3 v) { return new Quat(v.x, v.y, v.z, 0); }
		public static explicit operator Quat(Vec4 v) { return new Quat(v.x, v.y, v.z, v.w); }

		public static bool operator ==(Quat lhs, Quat rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Quat lhs, Quat rhs) { return !lhs.ValueEquals(rhs); }

		/// <summary>
		/// Conjugate: ~Q = (-x, -y, -z, w)
		/// </summary>
		public static Quat operator ~(Quat q)
		{
			q.x = -q.x;
			q.y = -q.y;
			q.z = -q.z;
			return q;
		}

		/// <summary>
		/// Q1 * Q2 = (w1 * V2 + w2 * V1 + V1 x V2, w1 * w2 - V1 • V2)
		/// </summary>
		public static Quat operator *(Quat lhs, Quat rhs)
		{
			double x1 = lhs.x, y1 = lhs.y, z1 = lhs.z, w1 = lhs.w;
			double x2 = rhs.x, y2 = rhs.y, z2 = rhs.z, w2 = rhs.w;
			Quat nq;
			nq.x = w1 * x2 + x1 * w2 + y1 * z2 - z1 * y2;
			nq.y = w1 * y2 - x1 * z2 + y1 * w2 + z1 * x2;
			nq.z = w1 * z2 + z1 * w2 - y1 * x2 + x1 * y2;
			nq.w = w1 * w2 - x1 * x2 - y1 * y2 - z1 * z2;
			return nq;
		}

		/// <summary>
		/// Rotate: Q * V = Q * Qv * ~Q
		/// </summary>
		public static Vec3 operator *(Quat lhs, Vec3 rhs)
		{
			double x1 = lhs.x, y1 = lhs.y, z1 = lhs.z, w1 = lhs.w;
			double x2 = rhs.x, y2 = rhs.y, z2 = rhs.z;
			double nx = w1 * x2 + y1 * z2 - z1 * y2;
			double ny = w1 * y2 - x1 * z2 + z1 * x2;
			double nz = w1 * z2 + x1 * y2 - y1 * x2;
			double nw = x1 * x2 + y1 * y2 + z1 * z2;
			Vec3 nv;
			nv.x = nw * x1 + nx * w1 - ny * z1 + nz * y1;
			nv.y = nw * y1 + nx * z1 + ny * w1 - nz * x1;
			nv.z = nw * z1 - nx * y1 + ny * x1 + nz * w1;
			return nv;
		}
		
		public static Quat Slerp(Quat a, Quat b, double t)
		{
			double dot = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
			double theta = Math.Acos(dot < -1 ? -1 : dot > 1 ? 1 : dot);
			double sin = Math.Sin(theta);
			double am = Math.Sin((1 - t) * theta) / sin;
			double bm = Math.Sin(t * theta) / sin;
			Quat nq;
			nq.x = am * a.x + bm * b.x;
			nq.y = am * a.y + bm * b.y;
			nq.z = am * a.z + bm * b.z;
			nq.w = am * a.w + bm * b.w;
			return nq;
		}

		public static Quat FromEuler(double x, double y, double z)
		{
			x *= 0.5;
			y *= 0.5;
			z *= 0.5;
			double c1 = Math.Cos(x);
			double c2 = Math.Cos(y);
			double c3 = Math.Cos(z);
			double s1 = Math.Sin(x);
			double s2 = Math.Sin(y);
			double s3 = Math.Sin(z);
			Quat nq;
			nq.x = s1 * c2 * c3 + c1 * s2 * s3;
			nq.y = c1 * s2 * c3 + s1 * c2 * s3;
			nq.z = c1 * c2 * s3 - s1 * s2 * c3;
			nq.w = c1 * c2 * c3 - s1 * s2 * s3;
			return nq;
		}
		public static Quat FromEuler(Vec3 v)
		{
			return FromEuler(v.x, v.y, v.z);
		}
		public static Vec3 ToEuler(double x, double y, double z, double w)
		{
			Vec3 nv;
			nv.x = Math.Atan2(2 * (x * w - y * z), 1 - 2 * (x * x + z * z));
			nv.y = Math.Atan2(2 * (y * w - x * z), 1 - 2 * (y * y + z * z));
			nv.z = 2 * (x * y + z * w);
			nv.z = Math.Asin(nv.z > 1 ? 1 : nv.z < -1 ? -1 : nv.z);
			return nv;
		}
		public static Vec3 ToEuler(Quat q)
		{
			return ToEuler(q.x, q.y, q.z, q.w);
		}

		public static Quat FromAngleAxis(double angle, Vec3 axisNorm)
		{
			angle *= 0.5;
			double sin = Math.Sin(angle);
			Quat nq;
			nq.x = sin * axisNorm.x;
			nq.y = sin * axisNorm.y;
			nq.z = sin * axisNorm.z;
			nq.w = Math.Cos(angle);
			return nq;
		}
		public static void ToAngleAxis(Quat q, out double angle, out Vec3 axisNorm)
		{
			double ha = Math.Acos(q.w);
			double sin = Math.Sin(ha);
			angle = ha + ha;
			axisNorm.x = q.x / sin;
			axisNorm.y = q.y / sin;
			axisNorm.z = q.z / sin;
		}

		public static Quat FromMat4x4(Mat4x4 m)
		{
			Quat nq;
			nq.w = Math.Sqrt(1 + m.m00 + m.m11 + m.m22) * 0.5;
			nq.x = Math.Sqrt(1 + m.m00 - m.m11 - m.m22) * (m.m12 <= m.m21 ? 0.5 : -0.5);
			nq.y = Math.Sqrt(1 - m.m00 + m.m11 - m.m22) * (m.m20 <= m.m02 ? 0.5 : -0.5);
			nq.z = Math.Sqrt(1 - m.m00 - m.m11 + m.m22) * (m.m01 <= m.m10 ? 0.5 : -0.5);
			return nq;
		}
		public static Mat4x4 ToMat4x4(Quat q)
		{
			double x = q.x, y = q.y, z = q.z, w = q.w;
			double xx = x * x, yy = y * y, zz = z * z;
			double xy = x * y, yz = y * z, xz = x * z;
			double xw = x * w, yw = y * w, zw = z * w;
			return new Mat4x4(
				1 - 2 * (yy + zz), 2 * (xy - zw), 2 * (xz + yw), 0,
				2 * (xy + zw), 1 - 2 * (xx + zz), 2 * (yz - xw), 0,
				2 * (xz - yw), 2 * (yz + xw), 1 - 2 * (xx + yy), 0,
				0, 0, 0, 1);
		}

		public static Quat GetRandom()
		{
			double a = MathX.GetRandom(-MathX.DoublePI, MathX.DoublePI);
			return FromAngleAxis(a, Vec3.GetRandom());
		}

		public static readonly Quat zero = new Quat();
		public static readonly Quat identity = new Quat(0, 0, 0, 1);
		public static readonly Quat NaQ = new Quat(double.NaN, double.NaN, double.NaN, double.NaN);
	}
}
