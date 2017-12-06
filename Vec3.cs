using System;

namespace mathx
{
	public struct Vec3
	{
		public double x;
		public double y;
		public double z;

		public double this[int index]
		{
			get
			{
				if (index == 0) return x;
				else if (index == 1) return y;
				else if (index == 2) return z;
				else throw new Exception("The index is out of range!");
			}
			set
			{
				if (index == 0) x = value;
				else if (index == 1) y = value;
				else if (index == 2) z = value;
				else throw new Exception("The index is out of range!");
			}
		}
		public int dimension { get { return 3; } }
		public bool isNaV { get { return double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z); } }
		public double sqrMagnitude { get { return x * x + y * y + z * z; } }
		public double magnitude { get { return Math.Sqrt(x * x + y * y + z * z); } }

		public Vec3 normalized
		{
			get
			{
				double mag = Math.Sqrt(x * x + y * y + z * z);
				if (mag > 0) return new Vec3(x / mag, y / mag, z / mag);
				return new Vec3();
			}
		}

		public Vec3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public string ToString(string format)
		{
			return "(" + x.ToString(format) + ", " + y.ToString(format) + ", " + z.ToString(format) + ")";
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
		public bool ValueEquals(Vec3 v)
		{
			return this == v;
		}


		public static implicit operator Vec3(Vec2 v)
		{
			return new Vec3(v.x, v.y, 0);
		}
		public static explicit operator Vec3(Vec4 v)
		{
			return new Vec3(v.x, v.y, v.z);
		}
		public static explicit operator Vec3(Quat q)
		{
			return new Vec3(q.x, q.y, q.z);
		}

		public static bool operator ==(Vec3 lhs, Vec3 rhs)
		{
			bool bx = Math.Abs(lhs.x - rhs.x) <= MathX.accuracy;
			bool by = Math.Abs(lhs.y - rhs.y) <= MathX.accuracy;
			bool bz = Math.Abs(lhs.z - rhs.z) <= MathX.accuracy;
			return bx && by && bz;
		}
		public static bool operator !=(Vec3 lhs, Vec3 rhs)
		{
			return !(lhs == rhs);
		}

		public static Vec3 operator -(Vec3 v)
		{
			return new Vec3(-v.x, -v.y, -v.z);
		}
		public static Vec3 operator +(Vec3 lhs, Vec3 rhs)
		{
			return new Vec3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}
		public static Vec3 operator -(Vec3 lhs, Vec3 rhs)
		{
			return new Vec3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}
		public static Vec3 operator *(double lhs, Vec3 rhs)
		{
			return new Vec3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}
		public static Vec3 operator *(Vec3 lhs, double rhs)
		{
			return new Vec3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}
		public static Vec3 operator /(Vec3 lhs, double rhs)
		{
			return new Vec3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		public static double operator *(Vec3 lhs, Vec3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		public static double Det(Vec3 lhs, Vec3 rhs)
		{
			double x = lhs.z * rhs.y - lhs.y * rhs.z;
			double y = lhs.x * rhs.z - lhs.z * rhs.x;
			double z = lhs.y * rhs.x - lhs.x * rhs.y;
			return Math.Sqrt(x * x + y * y + z * z);
		}

		public static Vec3 Cross(Vec3 lhs, Vec3 rhs)
		{
			double x = lhs.z * rhs.y - lhs.y * rhs.z;
			double y = lhs.x * rhs.z - lhs.z * rhs.x;
			double z = lhs.y * rhs.x - lhs.x * rhs.y;
			return new Vec3(x, y, z);
		}

		public static double Mixed(Vec3 v1, Vec3 v2, Vec3 v3)
		{
			double x1 = v1.x, y1 = v1.y, z1 = v1.z;
			double x2 = v2.x, y2 = v2.y, z2 = v2.z;
			double x3 = v3.x, y3 = v3.y, z3 = v3.z;
			double mixed = x1 * y2 * z3 + y1 * z2 * x3 + z1 * x2 * y3
				 - z1 * y2 * x3 - y1 * x2 * z3 - x1 * z2 * y3;
			return mixed;
		}

		public static double SqrDistance(Vec3 lhs, Vec3 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			return dx * dx + dy * dy + dz * dz;
		}
		public static double Distance(Vec3 lhs, Vec3 rhs)
		{
			double dx = lhs.x - rhs.x;
			double dy = lhs.y - rhs.y;
			double dz = lhs.z - rhs.z;
			return Math.Sqrt(dx * dx + dy * dy + dz * dz);
		}

		public static double Angle(Vec3 lhs, Vec3 rhs)
		{
			double x1 = lhs.x, y1 = lhs.y, z1 = lhs.z;
			double x2 = rhs.x, y2 = rhs.y, z2 = rhs.z;
			double m1 = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
			double m2 = Math.Sqrt(x2 * x2 + y2 * y2 + z2 * z2);
			if (m1 == 0 || m2 == 0) return double.NaN;
			double dot = x1 * x2 + y1 * y2 + z1 * z2;
			double cos = dot / m1 / m2;
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}

		public static Vec3 Rotate(Vec3 src, Vec3 axis, double angle)
		{
			double sx = src.x, sy = src.y, sz = src.z;
			double ax = axis.x, ay = axis.y, az = axis.z;
			double mag = Math.Sqrt(ax * ax + ay * ay + az * az);
			if (mag == 0) return src;
			ax /= mag;
			ay /= mag;
			az /= mag;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double vx = (ax * ax + (1.0 - ax * ax) * cos) * sx + (ax * ay * (1.0 - cos) - az * sin) * sy + (ax * az * (1.0 - cos) + ay * sin) * sz;
			double vy = (ay * ax * (1.0 - cos) + az * sin) * sx + (ay * ay + (1.0 - ay * ay) * cos) * sy + (ay * az * (1.0 - cos) - ax * sin) * sz;
			double vz = (az * ax * (1.0 - cos) - ay * sin) * sx + (az * ay * (1.0 - cos) + ax * sin) * sy + (az * az + (1.0 - az * az) * cos) * sz;
			return new Vec3(vx, vy, vz);
		}

		public static Vec3 Project(Vec3 src, Vec3 dst)
		{
			double x = dst.x, y = dst.y, z = dst.z;
			double sqrMag = x * x + y * y + z * z;
			if (sqrMag > 0)
			{
				double DdSM = (src.x * x + src.y * y + src.z * z) / sqrMag;
				return new Vec3(x * DdSM, y * DdSM, z * DdSM);
			}
			return new Vec3();
		}

		public static Vec3 ProjectOnPlane(Vec3 src, Vec3 norm)
		{
			return Project(src, Cross(Cross(norm, src), norm));
		}

		public static Vec3 Mirror(Vec3 src, Vec3 axis)
		{
			Vec3 pjt = Project(src, axis);
			return pjt + pjt - src;
		}

		public static Vec3 zero { get { return new Vec3(); } }
		public static Vec3 one { get { return new Vec3(1, 1, 1); } }
		public static Vec3 right { get { return new Vec3(1, 0, 0); } }
		public static Vec3 up { get { return new Vec3(0, 1, 0); } }
		public static Vec3 forward { get { return new Vec3(0, 0, 1); } }
		public static Vec3 NaV { get { return new Vec3(double.NaN, double.NaN, double.NaN); } }
	}
}
