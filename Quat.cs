using System;

namespace MathX
{
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
		public bool isNaQ { get { return double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z) || double.IsNaN(w); } }
		public double sqrMagnitude { get { return x * x + y * y + z * z + w * w; } }
		public double magnitude { get { return Math.Sqrt(x * x + y * y + z * z + w * w); } }

		public Quat normalized
		{
			get
			{
				double div = x * x + y * y + z * z + w * w;
				if (div > 0 && div != 1)
				{
					div = Math.Sqrt(div);
					return new Quat(x / div, y / div, z / div, w / div);
				}
				return this;
			}
		}

		public Quat(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public string ToString(string format)
		{
			return "(" + x.ToString(format) + ", " + y.ToString(format) + ", " + z.ToString(format) + ", " + w.ToString(format) + ")";
		}
		public override string ToString()
		{
			return ToString("");
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
		public bool ValueEquals(Quat v)
		{
			return this == v;
		}


		public static explicit operator Quat(Vec3 v)
		{
			return new Quat(v.x, v.y, v.z, 0);
		}
		public static explicit operator Quat(Vec4 v)
		{
			return new Quat(v.x, v.y, v.z, v.w);
		}

		public static bool operator ==(Quat lhs, Quat rhs)
		{
			bool bx = Math.Abs(lhs.x - rhs.x) <= MathX.accuracy;
			bool by = Math.Abs(lhs.y - rhs.y) <= MathX.accuracy;
			bool bz = Math.Abs(lhs.z - rhs.z) <= MathX.accuracy;
			bool bw = Math.Abs(lhs.w - rhs.w) <= MathX.accuracy;
			return bx && by && bz && bw;
		}
		public static bool operator !=(Quat lhs, Quat rhs)
		{
			bool bx = Math.Abs(lhs.x - rhs.x) > MathX.accuracy;
			bool by = Math.Abs(lhs.y - rhs.y) > MathX.accuracy;
			bool bz = Math.Abs(lhs.z - rhs.z) > MathX.accuracy;
			bool bw = Math.Abs(lhs.w - rhs.w) > MathX.accuracy;
			return bx || by || bz || bw;
		}

		/// <summary>
		/// Conjugate: ~Q = (-x, -y, -z, w)
		/// </summary>
		public static Quat operator ~(Quat q)
		{
			return new Quat(-q.x, -q.y, -q.z, q.w);
		}

		/// <summary>
		/// Q1 * Q2 = (w1 * V2 + w2 * V1 + V1 x V2, w1 * w2 - V1 * V2)
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
		/// Q * V = Q * Qv * ~Q
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
		
		public static Vec3 operator *(Vec3 lhs, Quat rhs)
		{
			return rhs * lhs;
		}

		public static double Angle(Quat lhs, Quat rhs)
		{
			double x1 = lhs.x, y1 = lhs.y, z1 = lhs.z, w1 = lhs.w;
			double x2 = rhs.x, y2 = rhs.y, z2 = rhs.z, w2 = rhs.w;
			double dot = x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2;
			double sm1 = x1 * x1 + y1 * y1 + z1 * z1 + w1 * w1;
			double sm2 = x2 * x2 + y2 * y2 + z2 * z2 + w2 * w2;
			if (sm1 == 0 || sm2 == 0) return 0;
			double cos = dot / Math.Sqrt(sm1) / Math.Sqrt(sm2);
			double angle = 2.0 * Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
			if (angle > MathX.PI) angle = MathX.DoublePI - angle;
			return angle;
		}

		public static Vec3 ToEuler(Quat quat)
		{
			double qx = quat.x, qy = quat.y, qz = quat.z, qw = quat.w;
			double sm = qx * qx + qy * qy + qz * qz + qw * qw;
			if (sm == 0) return new Vec3();
			double mag = Math.Sqrt(sm);
			qx /= mag;
			qy /= mag;
			qz /= mag;
			qw /= mag;
			double vx = Math.Atan2(2.0 * (qx * qw - qy * qz), 1.0 - 2.0 * (qx * qx + qz * qz));
			double vy = Math.Atan2(2.0 * (qy * qw - qx * qz), 1.0 - 2.0 * (qy * qy + qz * qz));
			double sin = 2.0 * (qx * qy + qz * qw);
			double vz = Math.Asin(sin > 1 ? 1 : sin < -1 ? -1 : sin);
			return new Vec3(vx, vy, vz);
		}

		public static Quat FromEuler(Vec3 euler)
		{
			double hx = euler.x / 2.0;
			double hy = euler.y / 2.0;
			double hz = euler.z / 2.0;
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

		public static Quat AxisAngle(Vec3 axis, double angle)
		{
			double ax = axis.x, ay = axis.y, az = axis.z;
			double sm = ax * ax + ay * ay + az * az;
			if (sm == 0) return new Quat(0, 0, 0, 1);
			angle /= 2.0;
			double SdM = Math.Sin(angle) / Math.Sqrt(sm);
			double qx = ax * SdM;
			double qy = ay * SdM;
			double qz = az * SdM;
			double qw = Math.Cos(angle);
			return new Quat(qx, qy, qz, qw);
		}

		public static Quat zero { get { return new Quat(); } }
		public static Quat identity { get { return new Quat(0, 0, 0, 1); } }
		public static Quat NaQ { get { return new Quat(double.NaN, double.NaN, double.NaN, double.NaN); } }
	}
}
