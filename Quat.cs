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

		public double this[int index]
		{
			get
			{
				if (index == 0) return x;
				else if (index == 1) return y;
				else if (index == 2) return z;
				else if (index == 3) return w;
				else throw new Exception("The index is out of range!");
			}
			set
			{
				if (index == 0) x = value;
				else if (index == 1) y = value;
				else if (index == 2) z = value;
				else if (index == 3) w = value;
				else throw new Exception("The index is out of range!");
			}
		}

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
			bool bx = Math.Abs(x - q.x) <= MathX.accuracy;
			bool by = Math.Abs(y - q.y) <= MathX.accuracy;
			bool bz = Math.Abs(z - q.z) <= MathX.accuracy;
			bool bw = Math.Abs(w - q.w) <= MathX.accuracy;
			return bx && by && bz && bw;
		}


		public static explicit operator Quat(Vec3 v) { return new Quat(v.x, v.y, v.z, 0); }
		public static explicit operator Quat(Vec4 v) { return new Quat(v.x, v.y, v.z, v.w); }

		public static bool operator ==(Quat lhs, Quat rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Quat lhs, Quat rhs) { return !lhs.ValueEquals(rhs); }

		public static bool IsNaQ(Quat q) { return double.IsNaN(q.x) || double.IsNaN(q.y) || double.IsNaN(q.z) || double.IsNaN(q.w); }

		public static Quat operator -(Quat q) { return new Quat(-q.x, -q.y, -q.z, -q.w); }
		public static Quat operator +(Quat lhs, Quat rhs) { return new Quat(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w); }
		public static Quat operator -(Quat lhs, Quat rhs) { return new Quat(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w); }
		public static Quat operator *(double lhs, Quat rhs) { return new Quat(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w); }
		public static Quat operator *(Quat lhs, double rhs) { return new Quat(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs); }
		public static Quat operator /(double lhs, Quat rhs) { return new Quat(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w); }
		public static Quat operator /(Quat lhs, double rhs) { return new Quat(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs); }

		/// <summary>
		/// Conjugate: ~Q = (-x, -y, -z, w)
		/// </summary>
		public static Quat operator ~(Quat q) { return new Quat(-q.x, -q.y, -q.z, q.w); }

		/// <summary>
		/// Q1 * Q2 = (w1 * V2 + w2 * V1 + V1 x V2, w1 * w2 - V1 • V2)
		/// </summary>
		public static Quat operator *(Quat lhs, Quat rhs)
		{
			double x1 = lhs.x, y1 = lhs.y, z1 = lhs.z, w1 = lhs.w;
			double x2 = rhs.x, y2 = rhs.y, z2 = rhs.z, w2 = rhs.w;
			double x = w1 * x2 + x1 * w2 + y1 * z2 - z1 * y2;
			double y = w1 * y2 - x1 * z2 + y1 * w2 + z1 * x2;
			double z = w1 * z2 + z1 * w2 - y1 * x2 + x1 * y2;
			double w = w1 * w2 - x1 * x2 - y1 * y2 - z1 * z2;
			return new Quat(x, y, z, w);
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
			double vx = nw * x1 + nx * w1 - ny * z1 + nz * y1;
			double vy = nw * y1 + nx * z1 + ny * w1 - nz * x1;
			double vz = nw * z1 - nx * y1 + ny * x1 + nz * w1;
			return new Vec3(vx, vy, vz);
		}

		public static double SqrLength(Quat q) { return q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w; }
		public static double Length(Quat q) { return Math.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w); }

		public static Quat Normalize(Quat q)
		{
			double len = SqrLength(q);
			if (len > 0 && len != 1) return q / Math.Sqrt(len);
			return new Quat(q);
		}

		public static double Dot(Quat lhs, Quat rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
		}

		public static double Angle(Quat lhs, Quat rhs)
		{
			double cos = Dot(lhs, rhs);
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}

		public static Quat Slerp(Quat a, Quat b, double t)
		{
			double theta = Angle(a, b);
			double sin = Math.Sin(theta);
			return Math.Sin((1 - t) * theta) / sin * a + Math.Sin(t * theta) / sin * b;
		}

		public static Quat FromEuler(double x, double y, double z)
		{
			double hx = x * 0.5;
			double hy = y * 0.5;
			double hz = z * 0.5;
			double c1 = Math.Cos(hx);
			double c2 = Math.Cos(hy);
			double c3 = Math.Cos(hz);
			double s1 = Math.Sin(hx);
			double s2 = Math.Sin(hy);
			double s3 = Math.Sin(hz);
			double qx = s1 * c2 * c3 + c1 * s2 * s3;
			double qy = c1 * s2 * c3 + s1 * c2 * s3;
			double qz = c1 * c2 * s3 - s1 * s2 * c3;
			double qw = c1 * c2 * c3 - s1 * s2 * s3;
			return new Quat(qx, qy, qz, qw);
		}
		public static Quat FromEuler(Vec3 v)
		{
			return FromEuler(v.x, v.y, v.z);
		}
		public static Vec3 ToEuler(double x, double y, double z, double w)
		{
			double vx = Math.Atan2(2 * (x * w - y * z), 1 - 2 * (x * x + z * z));
			double vy = Math.Atan2(2 * (y * w - x * z), 1 - 2 * (y * y + z * z));
			double sin = 2.0 * (x * y + z * w);
			double vz = Math.Asin(sin > 1 ? 1 : sin < -1 ? -1 : sin);
			return new Vec3(vx, vy, vz);
		}
		public static Vec3 ToEuler(Quat q)
		{
			return ToEuler(q.x, q.y, q.z, q.w);
		}

		public static Quat FromAngleAxis(double angle, Vec3 axisNorm)
		{
			angle *= 0.5;
			Vec3 qv = Math.Sin(angle) * axisNorm;
			double qw = Math.Cos(angle);
			return new Quat(qv, qw);
		}
		public static void ToAngleAxis(Quat q, out double angle, out Vec3 axisNorm)
		{
			double ha = Math.Acos(q.w);
			double sin = Math.Sin(ha);
			angle = ha + ha;
			axisNorm = new Vec3(q.x, q.y, q.z) / sin;
		}

		public static Quat FromMat4x4(Mat4x4 m)
		{
			double w = Math.Sqrt(1 + m.m00 + m.m11 + m.m22);
			double x = Math.Sqrt(1 + m.m00 - m.m11 - m.m22) * (w > 0 ^ m.m12 > m.m21 ? 1 : -1);
			double y = Math.Sqrt(1 - m.m00 + m.m11 - m.m22) * (w > 0 ^ m.m20 > m.m02 ? 1 : -1);
			double z = Math.Sqrt(1 - m.m00 - m.m11 + m.m22) * (w > 0 ^ m.m01 > m.m10 ? 1 : -1);
			return new Quat(x, y, z, w) * 0.5;
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

		public static Quat zero { get { return new Quat(); } }
		public static Quat identity { get { return new Quat(0, 0, 0, 1); } }
		public static Quat NaQ { get { return new Quat(double.NaN, double.NaN, double.NaN, double.NaN); } }
	}
}
