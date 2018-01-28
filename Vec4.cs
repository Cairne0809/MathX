using System;
using System.Text;

namespace MathematicsX
{
	[Serializable]
	public struct Vec4 : IVector
	{
		public int dimension { get { return 4; } }

		public double x;
		public double y;
		public double z;
		public double w;

		public Vec2 xy { get { return new Vec2(x, y); } set { x = value.x; y = value.y; } }
		public Vec2 xz { get { return new Vec2(x, z); } set { x = value.x; z = value.y; } }
		public Vec2 xw { get { return new Vec2(x, w); } set { x = value.x; w = value.y; } }
		public Vec2 yx { get { return new Vec2(y, x); } set { y = value.x; x = value.y; } }
		public Vec2 yz { get { return new Vec2(y, z); } set { y = value.x; z = value.y; } }
		public Vec2 yw { get { return new Vec2(y, w); } set { y = value.x; w = value.y; } }
		public Vec2 zx { get { return new Vec2(z, x); } set { z = value.x; x = value.y; } }
		public Vec2 zy { get { return new Vec2(z, y); } set { z = value.x; y = value.y; } }
		public Vec2 zw { get { return new Vec2(z, w); } set { z = value.x; w = value.y; } }
		public Vec2 wx { get { return new Vec2(w, x); } set { w = value.x; x = value.y; } }
		public Vec2 wy { get { return new Vec2(w, y); } set { w = value.x; y = value.y; } }
		public Vec2 wz { get { return new Vec2(w, z); } set { w = value.x; z = value.y; } }

		public Vec3 xyz { get { return new Vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }
		public Vec3 xyw { get { return new Vec3(x, y, w); } set { x = value.x; y = value.y; w = value.z; } }
		public Vec3 xzy { get { return new Vec3(x, z, y); } set { x = value.x; z = value.y; y = value.z; } }
		public Vec3 xzw { get { return new Vec3(x, z, w); } set { x = value.x; z = value.y; w = value.z; } }
		public Vec3 xwy { get { return new Vec3(x, w, y); } set { x = value.x; w = value.y; y = value.z; } }
		public Vec3 xwz { get { return new Vec3(x, w, z); } set { x = value.x; w = value.y; z = value.z; } }
		public Vec3 yxz { get { return new Vec3(y, x, z); } set { y = value.x; x = value.y; z = value.z; } }
		public Vec3 yxw { get { return new Vec3(y, x, w); } set { y = value.x; x = value.y; w = value.z; } }
		public Vec3 yzx { get { return new Vec3(y, z, x); } set { y = value.x; z = value.y; x = value.z; } }
		public Vec3 yzw { get { return new Vec3(y, z, w); } set { y = value.x; z = value.y; w = value.z; } }
		public Vec3 ywx { get { return new Vec3(y, w, x); } set { y = value.x; w = value.y; x = value.z; } }
		public Vec3 ywz { get { return new Vec3(y, w, z); } set { y = value.x; w = value.y; z = value.z; } }
		public Vec3 zxy { get { return new Vec3(z, x, y); } set { z = value.x; x = value.y; y = value.z; } }
		public Vec3 zxw { get { return new Vec3(z, x, w); } set { z = value.x; x = value.y; w = value.z; } }
		public Vec3 zyx { get { return new Vec3(z, y, x); } set { z = value.x; y = value.y; x = value.z; } }
		public Vec3 zyw { get { return new Vec3(z, y, w); } set { z = value.x; y = value.y; w = value.z; } }
		public Vec3 zwx { get { return new Vec3(z, w, x); } set { z = value.x; w = value.y; x = value.z; } }
		public Vec3 zwy { get { return new Vec3(z, w, y); } set { z = value.x; w = value.y; y = value.z; } }
		public Vec3 wxy { get { return new Vec3(w, x, y); } set { w = value.x; x = value.y; y = value.z; } }
		public Vec3 wxz { get { return new Vec3(w, x, z); } set { w = value.x; x = value.y; z = value.z; } }
		public Vec3 wyx { get { return new Vec3(w, y, x); } set { w = value.x; y = value.y; x = value.z; } }
		public Vec3 wyz { get { return new Vec3(w, y, z); } set { w = value.x; y = value.y; z = value.z; } }
		public Vec3 wzx { get { return new Vec3(w, z, x); } set { w = value.x; z = value.y; x = value.z; } }
		public Vec3 wzy { get { return new Vec3(w, z, y); } set { w = value.x; z = value.y; y = value.z; } }

		public Vec4 xyzw { get { return new Vec4(x, y, z, w); } set { x = value.x; y = value.y; z = value.z; w = value.w; } }
		public Vec4 xywz { get { return new Vec4(x, y, w, z); } set { x = value.x; y = value.y; w = value.z; z = value.w; } }
		public Vec4 xzyw { get { return new Vec4(x, z, y, w); } set { x = value.x; z = value.y; y = value.z; w = value.w; } }
		public Vec4 xzwy { get { return new Vec4(x, z, w, y); } set { x = value.x; z = value.y; w = value.z; y = value.w; } }
		public Vec4 xwyz { get { return new Vec4(x, w, y, z); } set { x = value.x; w = value.y; y = value.z; z = value.w; } }
		public Vec4 xwzy { get { return new Vec4(x, w, z, y); } set { x = value.x; w = value.y; z = value.z; y = value.w; } }
		public Vec4 yxzw { get { return new Vec4(y, x, z, w); } set { y = value.x; x = value.y; z = value.z; w = value.w; } }
		public Vec4 yxwz { get { return new Vec4(y, x, w, z); } set { y = value.x; x = value.y; w = value.z; z = value.w; } }
		public Vec4 yzxw { get { return new Vec4(y, z, x, w); } set { y = value.x; z = value.y; x = value.z; w = value.w; } }
		public Vec4 yzwx { get { return new Vec4(y, z, w, x); } set { y = value.x; z = value.y; w = value.z; x = value.w; } }
		public Vec4 ywxz { get { return new Vec4(y, w, x, z); } set { y = value.x; w = value.y; x = value.z; z = value.w; } }
		public Vec4 ywzx { get { return new Vec4(y, w, z, x); } set { y = value.x; w = value.y; z = value.z; x = value.w; } }
		public Vec4 zxyw { get { return new Vec4(z, x, y, w); } set { z = value.x; x = value.y; y = value.z; w = value.w; } }
		public Vec4 zxwy { get { return new Vec4(z, x, w, y); } set { z = value.x; x = value.y; w = value.z; y = value.w; } }
		public Vec4 zyxw { get { return new Vec4(z, y, x, w); } set { z = value.x; y = value.y; x = value.z; w = value.w; } }
		public Vec4 zywx { get { return new Vec4(z, y, w, x); } set { z = value.x; y = value.y; w = value.z; x = value.w; } }
		public Vec4 zwxy { get { return new Vec4(z, w, x, y); } set { z = value.x; w = value.y; x = value.z; y = value.w; } }
		public Vec4 zwyx { get { return new Vec4(z, w, y, x); } set { z = value.x; w = value.y; y = value.z; x = value.w; } }
		public Vec4 wxyz { get { return new Vec4(w, x, y, z); } set { w = value.x; x = value.y; y = value.z; z = value.w; } }
		public Vec4 wxzy { get { return new Vec4(w, x, z, y); } set { w = value.x; x = value.y; z = value.z; y = value.w; } }
		public Vec4 wyxz { get { return new Vec4(w, y, x, z); } set { w = value.x; y = value.y; x = value.z; z = value.w; } }
		public Vec4 wyzx { get { return new Vec4(w, y, z, x); } set { w = value.x; y = value.y; z = value.z; x = value.w; } }
		public Vec4 wzxy { get { return new Vec4(w, z, x, y); } set { w = value.x; z = value.y; x = value.z; y = value.w; } }
		public Vec4 wzyx { get { return new Vec4(w, z, y, x); } set { w = value.x; z = value.y; y = value.z; x = value.w; } }
		
		public unsafe double this[int index]
		{
			get
			{
				if (index >= 0 && index < 4)
					fixed (double* ptr = &x) return *(ptr + index);
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < 4)
					fixed (double* ptr = &x) *(ptr + index) = value;
				else throw new IndexOutOfRangeException();
			}
		}

		public Vec4(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		public Vec4(Vec2 xy, double z, double w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}
		public Vec4(double x, Vec2 yz, double w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}
		public Vec4(double x, double y, Vec2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}
		public Vec4(Vec2 xy, Vec2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}
		public Vec4(Vec3 xyz, double w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}
		public Vec4(double x, Vec3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}
		public Vec4(Vec4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.x;
			this.z = xyzw.y;
			this.w = xyzw.z;
		}

		public Vec2 S2(string swizzle)
		{
			Vec2 nv;
			nv.x = this[(swizzle[0] - 116) % 4];
			nv.y = this[(swizzle[1] - 116) % 4];
			return nv;
		}
		public Vec3 S3(string swizzle)
		{
			Vec3 nv;
			nv.x = this[(swizzle[0] - 116) % 4];
			nv.y = this[(swizzle[1] - 116) % 4];
			nv.z = this[(swizzle[2] - 116) % 4];
			return nv;
		}
		public Vec4 S4(string swizzle)
		{
			Vec4 nv;
			nv.x = this[(swizzle[0] - 116) % 4];
			nv.y = this[(swizzle[1] - 116) % 4];
			nv.z = this[(swizzle[2] - 116) % 4];
			nv.w = this[(swizzle[3] - 116) % 4];
			return nv;
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
		public override string ToString() { return ToString(""); }
		public override int GetHashCode() { return base.GetHashCode(); }
		public override bool Equals(object obj) { return base.Equals(obj); }
		public bool ValueEquals(Vec4 v)
		{
			return Math.Abs(x - v.x) <= MathX.Tolerance
				&& Math.Abs(y - v.y) <= MathX.Tolerance
				&& Math.Abs(z - v.z) <= MathX.Tolerance
				&& Math.Abs(w - v.w) <= MathX.Tolerance;
		}


		public static implicit operator Vec4(Vec2 v) { return new Vec4(v.x, v.y, 0, 0); }
		public static implicit operator Vec4(Vec3 v) { return new Vec4(v.x, v.y, v.z, 0); }
		public static explicit operator Vec4(Quat q) { return new Vec4(q.x, q.y, q.z, q.w); }

		public static bool operator ==(Vec4 lhs, Vec4 rhs) { return lhs.ValueEquals(rhs); }
		public static bool operator !=(Vec4 lhs, Vec4 rhs) { return !lhs.ValueEquals(rhs); }

		public static Vec4 operator -(Vec4 v) { return new Vec4(-v.x, -v.y, -v.z, -v.w); }

		public static Vec4 operator +(double lhs, Vec4 rhs) { return new Vec4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w); }
		public static Vec4 operator +(Vec4 lhs, double rhs) { return new Vec4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs); }
		public static Vec4 operator +(Vec4 lhs, Vec4 rhs) { return new Vec4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w); }

		public static Vec4 operator -(double lhs, Vec4 rhs) { return new Vec4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w); }
		public static Vec4 operator -(Vec4 lhs, double rhs) { return new Vec4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs); }
		public static Vec4 operator -(Vec4 lhs, Vec4 rhs) { return new Vec4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w); }

		public static Vec4 operator *(double lhs, Vec4 rhs) { return new Vec4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w); }
		public static Vec4 operator *(Vec4 lhs, double rhs) { return new Vec4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs); }
		public static Vec4 operator *(Vec4 lhs, Vec4 rhs) { return new Vec4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w); }

		public static Vec4 operator /(double lhs, Vec4 rhs) { return new Vec4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w); }
		public static Vec4 operator /(Vec4 lhs, double rhs) { return new Vec4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs); }
		public static Vec4 operator /(Vec4 lhs, Vec4 rhs) { return new Vec4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w); }

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

		public static readonly Vec4 zero = new Vec4();
		public static readonly Vec4 one = new Vec4(1, 1, 1, 1);
		public static readonly Vec4 NaV = new Vec4(double.NaN, double.NaN, double.NaN, double.NaN);
	}
}
