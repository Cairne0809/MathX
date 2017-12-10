using System;

namespace MathematicsX
{
	public struct Vec4
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
		public bool isNaV { get { return double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z) || double.IsNaN(w); } }
		public double sqrMagnitude { get { return x * x + y * y + z * z + w * w; } }
		public double magnitude { get { return Math.Sqrt(x * x + y * y + z * z + w * w); } }

		public Vec4 normalized
		{
			get
			{
				double div = x * x + y * y + z * z + w * w;
				if (div > 0 && div != 1)
				{
					div = Math.Sqrt(div);
					return new Vec4(x / div, y / div, z / div, w / div);
				}
				return this;
			}
		}

		public Vec4(double x, double y, double z, double w)
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
		public bool ValueEquals(Vec4 v)
		{
			return this == v;
		}


		public static implicit operator Vec4(Vec2 v)
		{
			return new Vec4(v.x, v.y, 0, 0);
		}
		public static implicit operator Vec4(Vec3 v)
		{
			return new Vec4(v.x, v.y, v.z, 0);
		}
		public static explicit operator Vec4(Quat q)
		{
			return new Vec4(q.x, q.y, q.z, q.w);
		}

		public static bool operator ==(Vec4 lhs, Vec4 rhs)
		{
			bool bx = Math.Abs(lhs.x - rhs.x) <= MathX.accuracy;
			bool by = Math.Abs(lhs.y - rhs.y) <= MathX.accuracy;
			bool bz = Math.Abs(lhs.z - rhs.z) <= MathX.accuracy;
			bool bw = Math.Abs(lhs.w - rhs.w) <= MathX.accuracy;
			return bx && by && bz && bw;
		}
		public static bool operator !=(Vec4 lhs, Vec4 rhs)
		{
			return !(lhs == rhs);
		}

		public static Vec4 operator -(Vec4 v)
		{
			return new Vec4(-v.x, -v.y, -v.z, -v.w);
		}
		public static Vec4 operator +(Vec4 lhs, Vec4 rhs)
		{
			return new Vec4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}
		public static Vec4 operator -(Vec4 lhs, Vec4 rhs)
		{
			return new Vec4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}
		public static Vec4 operator *(double lhs, Vec4 rhs)
		{
			return new Vec4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}
		public static Vec4 operator *(Vec4 lhs, double rhs)
		{
			return new Vec4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}
		public static Vec4 operator /(Vec4 lhs, double rhs)
		{
			return new Vec4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		public static double operator *(Vec4 lhs, Vec4 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z + lhs.w * rhs.w;
		}

		public static double SqrDistance(Vec4 lhs, Vec4 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			double dw = lhs.w - rhs.w;
			return dx * dx + dy * dy + dz * dz + dw * dw;
		}
		public static double Distance(Vec4 lhs, Vec4 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			double dw = lhs.w - rhs.w;
			return Math.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
		}

		public static double Angle(Vec4 lhs, Vec4 rhs)
		{
			double x1 = lhs.x, y1 = lhs.y, z1 = lhs.z, w1 = lhs.w;
			double x2 = rhs.x, y2 = rhs.y, z2 = rhs.z, w2 = rhs.w;
			double m1 = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1 + w1 * w1);
			double m2 = Math.Sqrt(x2 * x2 + y2 * y2 + z2 * z2 + w2 * w2);
			if (m1 == 0 || m2 == 0) return double.NaN;
			double dot = x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2;
			double cos = dot / m1 / m2;
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}

		public static Vec4 Project(Vec4 src, Vec4 dst)
		{
			double x = dst.x, y = dst.y, z = dst.z, w = dst.w;
			double sqrMag = x * x + y * y + z * z + w * w;
			if (sqrMag > 0)
			{
				double DdSM = (src.x * x + src.y * y + src.z * z + src.w * w) / sqrMag;
				return new Vec4(x * DdSM, y * DdSM, z * DdSM, w * DdSM);
			}
			return new Vec4();
		}

		public static Vec4 Mirror(Vec4 src, Vec4 axis)
		{
			Vec4 pjt = Project(src, axis);
			return pjt + pjt - src;
		}

		public static Vec4 zero { get { return new Vec4(); } }
		public static Vec4 one { get { return new Vec4(1, 1, 1, 1); } }
		public static Vec4 NaV { get { return new Vec4(double.NaN, double.NaN, double.NaN, double.NaN); } }
	}
}
