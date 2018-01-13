using System;
using System.Text;

namespace MathematicsX
{
	public struct Vec4 : IVector
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
		public int dimension { get { return 4; } set { } }
		public bool isNaV { get { return double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z) || double.IsNaN(w); } }
		public double sqrMagnitude { get { return x * x + y * y + z * z + w * w; } }
		public double magnitude { get { return Math.Sqrt(x * x + y * y + z * z + w * w); } }
		public Vec4 abs { get { return new Vec4(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w)); } }

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
			StringBuilder sb = new StringBuilder();
			sb.Append("V(")
				.Append(x.ToString(format)).Append(", ")
				.Append(y.ToString(format)).Append(", ")
				.Append(z.ToString(format)).Append(", ")
				.Append(w.ToString(format)).Append(")");
			return sb.ToString();
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
			bool bx = Math.Abs(x - v.x) <= MathX.accuracy;
			bool by = Math.Abs(y - v.y) <= MathX.accuracy;
			bool bz = Math.Abs(z - v.z) <= MathX.accuracy;
			bool bw = Math.Abs(w - v.w) <= MathX.accuracy;
			return bx && by && bz && bw;
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
			return lhs.ValueEquals(rhs);
		}
		public static bool operator !=(Vec4 lhs, Vec4 rhs)
		{
			return !lhs.ValueEquals(rhs);
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
		public static Vec4 operator *(Vec4 lhs, double rhs)
		{
			return new Vec4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}
		public static Vec4 operator *(double lhs, Vec4 rhs)
		{
			return rhs * lhs;
		}
		public static Vec4 operator /(Vec4 lhs, double rhs)
		{
			return new Vec4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}
		public static Vec4 operator /(double lhs, Vec4 rhs)
		{
			return new Vec4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		public static Vec4 MaxAxis(Vec4 v)
		{
			double aX = Math.Abs(v.x);
			double aY = Math.Abs(v.y);
			double aZ = Math.Abs(v.z);
			double aW = Math.Abs(v.w);
			int i = 0;
			double max = aX;
			if (aY > max)
			{
				i = 1;
				max = aY;
			}
			if (aZ > max)
			{
				i = 2;
				max = aZ;
			}
			if (aW > max) return new Vec4(0, 0, 0, v.w);
			if (i == 2) return new Vec4(0, 0, v.z, 0);
			if (i == 1) return new Vec4(0, v.y, 0, 0);
			return new Vec4(v.x, 0, 0, 0);
		}

		public static Vec4 MinAxis(Vec4 v)
		{
			double aX = Math.Abs(v.x);
			double aY = Math.Abs(v.y);
			double aZ = Math.Abs(v.z);
			double aW = Math.Abs(v.w);
			int i = 3;
			double min = aW;
			if (aZ < min)
			{
				i = 2;
				min = aZ;
			}
			if (aY < min)
			{
				i = 1;
				min = aY;
			}
			if (aX < min) return new Vec4(v.x, 0, 0, 0);
			if (i == 1) return new Vec4(0, v.y, 0, 0);
			if (i == 2) return new Vec4(0, 0, v.z, 0);
			return new Vec4(0, 0, 0, v.w);
		}

		public static double Dot(Vec4 lhs, Vec4 rhs)
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
			double sm1 = x1 * x1 + y1 * y1 + z1 * z1 + w1 * w1;
			double sm2 = x2 * x2 + y2 * y2 + z2 * z2 + w2 * w2;
			if (sm1 == 0 || sm2 == 0) return 0;
			double dot = x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2;
			double cos = dot / Math.Sqrt(sm1) / Math.Sqrt(sm2);
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

		public static Vec4 GetRandom()
		{
			//Not even
			double theta = MathX.DoublePI * MathX.GetRandom();
			double phi = Math.Acos(MathX.GetRandom(-1, 1));
			double beta = Math.Acos(MathX.GetRandom(-1, 1));
			double x = Math.Sin(theta) * Math.Sin(phi) * Math.Sin(beta);
			double y = Math.Cos(theta) * Math.Sin(phi) * Math.Sin(beta);
			double z = Math.Cos(phi) * Math.Sin(beta);
			double w = Math.Cos(beta);
			return new Vec4(x, y, z, w);
		}

		public static Vec4 zero { get { return new Vec4(); } }
		public static Vec4 one { get { return new Vec4(1, 1, 1, 1); } }
		public static Vec4 NaV { get { return new Vec4(double.NaN, double.NaN, double.NaN, double.NaN); } }
	}
}
