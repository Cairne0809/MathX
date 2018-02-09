using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Vec3 : IVector
	{
		public int dimension { get { return 3; } }

		public double x;
		public double y;
		public double z;

		public Vec2 xy { get { return new Vec2(x, y); } set { x = value.x; y = value.y; } }
		public Vec2 xz { get { return new Vec2(x, z); } set { x = value.x; z = value.y; } }
		public Vec2 yx { get { return new Vec2(y, x); } set { y = value.x; x = value.y; } }
		public Vec2 yz { get { return new Vec2(y, z); } set { y = value.x; z = value.y; } }
		public Vec2 zx { get { return new Vec2(z, x); } set { z = value.x; x = value.y; } }
		public Vec2 zy { get { return new Vec2(z, y); } set { z = value.x; y = value.y; } }

		public Vec3 xyz { get { return new Vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
		public Vec3 xzy { get { return new Vec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
		public Vec3 yxz { get { return new Vec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
		public Vec3 yzx { get { return new Vec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
		public Vec3 zxy { get { return new Vec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
		public Vec3 zyx { get { return new Vec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }

		public unsafe double this[int index]
		{
			get
			{
				if (index >= 0 && index < 3)
					fixed (double* ptr = &x) return *(ptr + index);
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 3)
					fixed (double* ptr = &x) *(ptr + index) = value;
				else throw new IndexOutOfRangeException();
			}
		}
		
		public Vec3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public Vec3(Vec2 xy, double z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}
		public Vec3(double x, Vec2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}
		public Vec3(Vec3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		public Vec2 S2(string swizzle)
		{
			Vec2 nv;
			nv.x = this[swizzle[0] - 120];
			nv.y = this[swizzle[1] - 120];
			return nv;
		}
		public Vec3 S3(string swizzle)
		{
			Vec3 nv;
			nv.x = this[swizzle[0] - 120];
			nv.y = this[swizzle[1] - 120];
			nv.z = this[swizzle[2] - 120];
			return nv;
		}
		
		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("V(")
				.Append(x.ToString(format)).Append(", ")
				.Append(y.ToString(format)).Append(", ")
				.Append(z.ToString(format)).Append(")");
			return sb.ToString();
		}
		public override string ToString() { return ToString(""); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }
		public bool ValueEquals(Vec3 v)
		{
			double pt = MathX.Tolerance;
			double nt = -pt;
			double dx = x - v.x;
			double dy = y - v.y;
			double dz = z - v.z;
			return dx <= pt && dx >= nt
				&& dy <= pt && dy >= nt
				&& dz <= pt && dz >= nt;
		}


		public static implicit operator Vec3(Vec2 v) { return new Vec3(v.x, v.y, 0); }
		public static explicit operator Vec3(Vec4 v) { return new Vec3(v.x, v.y, v.z); }
		public static explicit operator Vec3(Quat q) { return new Vec3(q.x, q.y, q.z); }

		public static bool operator ==(Vec3 lhs, Vec3 rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Vec3 lhs, Vec3 rhs) { return !lhs.ValueEquals(rhs); }

		public static Vec3 operator -(Vec3 v)
		{
			v.x = -v.x;
			v.y = -v.y;
			v.z = -v.z;
			return v;
		}

		public static Vec3 operator +(double lhs, Vec3 rhs)
		{
			rhs.x += lhs;
			rhs.y += lhs;
			rhs.z += lhs;
			return rhs;
		}
		public static Vec3 operator +(Vec3 lhs, double rhs)
		{
			lhs.x += rhs;
			lhs.y += rhs;
			lhs.z += rhs;
			return lhs;
		}
		public static Vec3 operator +(Vec3 lhs, Vec3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		public static Vec3 operator -(double lhs, Vec3 rhs)
		{
			rhs.x = lhs - rhs.x;
			rhs.y = lhs - rhs.y;
			rhs.z = lhs - rhs.z;
			return rhs;
		}
		public static Vec3 operator -(Vec3 lhs, double rhs)
		{
			lhs.x -= rhs;
			lhs.y -= rhs;
			lhs.z -= rhs;
			return lhs;
		}
		public static Vec3 operator -(Vec3 lhs, Vec3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		public static Vec3 operator *(double lhs, Vec3 rhs)
		{
			rhs.x *= lhs;
			rhs.y *= lhs;
			rhs.z *= lhs;
			return rhs;
		}
		public static Vec3 operator *(Vec3 lhs, double rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}
		public static Vec3 operator *(Vec3 lhs, Vec3 rhs)
		{
			lhs.x *= rhs.x;
			lhs.y *= rhs.y;
			lhs.z *= rhs.z;
			return lhs;
		}

		public static Vec3 operator /(double lhs, Vec3 rhs)
		{
			rhs.x = lhs / rhs.x;
			rhs.y = lhs / rhs.y;
			rhs.z = lhs / rhs.z;
			return rhs;
		}
		public static Vec3 operator /(Vec3 lhs, double rhs)
		{
			lhs.x /= rhs;
			lhs.y /= rhs;
			lhs.z /= rhs;
			return lhs;
		}
		public static Vec3 operator /(Vec3 lhs, Vec3 rhs)
		{
			lhs.x /= rhs.x;
			lhs.y /= rhs.y;
			lhs.z /= rhs.z;
			return lhs;
		}

		public static Vec3 GetRandom()
		{
			double theta = MathX.DoublePI * MathX.GetRandom();
			double phi = Math.Acos(MathX.GetRandom(-1, 1));
			Vec3 nv;
			nv.x = Math.Sin(theta) * Math.Sin(phi);
			nv.y = Math.Cos(theta) * Math.Sin(phi);
			nv.z = Math.Cos(phi);
			return nv;
		}
		
		public static Vec3 Cross(Vec3 lhs, Vec3 rhs)
		{
			Vec3 nv;
			nv.x = lhs.y * rhs.z - lhs.z * rhs.y;
			nv.y = lhs.z * rhs.x - lhs.x * rhs.z;
			nv.z = lhs.x * rhs.y - lhs.y * rhs.x;
			return nv;
		}

		public static double Mixed(Vec3 v0, Vec3 v1, Vec3 v2)
		{
			return v0.x * v1.y * v2.z - v0.x * v2.y * v1.z
				 + v1.x * v2.y * v0.z - v1.x * v0.y * v2.z
				 + v2.x * v0.y * v1.z - v2.x * v1.y * v0.z;
		}

		public static Vec3 Rotate(Vec3 src, double angle, Vec3 axisNorm)
		{
			double sx = src.x, sy = src.y, sz = src.z;
			double x = axisNorm.x, y = axisNorm.y, z = axisNorm.z;
			double xx = x * x, yy = y * y, zz = z * z;
			double xy = x * y, yz = y * z, xz = x * z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			Vec3 nv;
			nv.x = (xx + (1 - xx) * cos) * sx + (xy * (1 - cos) - z * sin) * sy + (xz * (1 - cos) + y * sin) * sz;
			nv.y = (xy * (1 - cos) + z * sin) * sx + (yy + (1 - yy) * cos) * sy + (yz * (1 - cos) - x * sin) * sz;
			nv.z = (xz * (1 - cos) - y * sin) * sx + (yz * (1 - cos) + x * sin) * sy + (zz + (1 - zz) * cos) * sz;
			return nv;
		}

		public static readonly Vec3 zero = new Vec3();
		public static readonly Vec3 one = new Vec3(1, 1, 1);
		public static readonly Vec3 right = new Vec3(1, 0, 0);
		public static readonly Vec3 up = new Vec3(0, 1, 0);
		public static readonly Vec3 forward = new Vec3(0, 0, 1);
		public static readonly Vec3 NaV = new Vec3(double.NaN, double.NaN, double.NaN);
	}
}
