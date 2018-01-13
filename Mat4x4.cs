using System;
using System.Text;

namespace MathematicsX
{
	public struct Mat4x4 : IMatrix
	{
		public double v00;
		public double v10;
		public double v20;
		public double v30;
		public double v01;
		public double v11;
		public double v21;
		public double v31;
		public double v02;
		public double v12;
		public double v22;
		public double v32;
		public double v03;
		public double v13;
		public double v23;
		public double v33;

		public double this[int c, int r]
		{
			get
			{
				int index = 4 * r + c;
				switch (index)
				{
					case 0: return v00;
					case 1: return v10;
					case 2: return v20;
					case 3: return v30;
					case 4: return v01;
					case 5: return v11;
					case 6: return v21;
					case 7: return v31;
					case 8: return v02;
					case 9: return v12;
					case 10: return v22;
					case 11: return v32;
					case 12: return v03;
					case 13: return v13;
					case 14: return v23;
					case 15: return v33;
					default: throw new Exception("The index is out of range!");
				}
			}
			set
			{
				int index = 4 * r + c;
				switch (index)
				{
					case 0: v00 = value; break;
					case 1: v10 = value; break;
					case 2: v20 = value; break;
					case 3: v30 = value; break;
					case 4: v01 = value; break;
					case 5: v11 = value; break;
					case 6: v21 = value; break;
					case 7: v31 = value; break;
					case 8: v02 = value; break;
					case 9: v12 = value; break;
					case 10: v22 = value; break;
					case 11: v32 = value; break;
					case 12: v03 = value; break;
					case 13: v13 = value; break;
					case 14: v23 = value; break;
					case 15: v33 = value; break;
					default: throw new Exception("The index is out of range!");
				}
			}
		}
		public Vec4 this[int c]
		{
			get
			{
				if (c == 0) return new Vec4(v00, v01, v02, v03);
				if (c == 1) return new Vec4(v10, v11, v12, v13);
				if (c == 2) return new Vec4(v20, v21, v22, v23);
				if (c == 3) return new Vec4(v30, v31, v32, v33);
				throw new Exception("The index is out of range!");
			}
			set
			{
				if (c == 0) { v00 = value.x; v01 = value.y; v02 = value.z; v03 = value.w; }
				else if (c == 1) { v10 = value.x; v11 = value.y; v12 = value.z; v13 = value.w; }
				else if (c == 2) { v20 = value.x; v21 = value.y; v22 = value.z; v23 = value.w; }
				else if (c == 3) { v30 = value.x; v31 = value.y; v32 = value.z; v33 = value.w; }
				else throw new Exception("The index is out of range!");
			}
		}
		public int column { get { return 4; } }
		public int row { get { return 4; } }

		public bool isNaM
		{
			get
			{
				return
					double.IsNaN(v00) ||
					double.IsNaN(v10) ||
					double.IsNaN(v20) ||
					double.IsNaN(v30) ||
					double.IsNaN(v01) ||
					double.IsNaN(v11) ||
					double.IsNaN(v21) ||
					double.IsNaN(v31) ||
					double.IsNaN(v02) ||
					double.IsNaN(v12) ||
					double.IsNaN(v22) ||
					double.IsNaN(v32) ||
					double.IsNaN(v03) ||
					double.IsNaN(v13) ||
					double.IsNaN(v23) ||
					double.IsNaN(v33);
			}
		}
		
		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(v00.ToString(format)).Append("\t")
				.Append(v10.ToString(format)).Append("\t")
				.Append(v20.ToString(format)).Append("\t")
				.Append(v30.ToString(format)).Append("\t|\n|\t")
				.Append(v01.ToString(format)).Append("\t")
				.Append(v11.ToString(format)).Append("\t")
				.Append(v21.ToString(format)).Append("\t")
				.Append(v31.ToString(format)).Append("\t|\n|\t")
				.Append(v02.ToString(format)).Append("\t")
				.Append(v12.ToString(format)).Append("\t")
				.Append(v22.ToString(format)).Append("\t")
				.Append(v32.ToString(format)).Append("\t|\n|\t")
				.Append(v03.ToString(format)).Append("\t")
				.Append(v13.ToString(format)).Append("\t")
				.Append(v23.ToString(format)).Append("\t")
				.Append(v33.ToString(format)).Append("\t|");
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

		public Vec4 GetRow(int r)
		{
			if (r == 0) return new Vec4(v00, v10, v20, v30);
			if (r == 1) return new Vec4(v01, v11, v21, v31);
			if (r == 2) return new Vec4(v02, v12, v22, v32);
			if (r == 3) return new Vec4(v03, v13, v23, v33);
			throw new Exception("The index is out of range!");
		}
		public void SetRow(int r, Vec4 v)
		{
			if (r == 0) { v00 = v.x; v10 = v.y; v20 = v.z; v30 = v.w; }
			else if (r == 1) { v01 = v.x; v11 = v.y; v21 = v.z; v31 = v.w; }
			else if (r == 2) { v02 = v.x; v12 = v.y; v22 = v.z; v32 = v.w; }
			else if (r == 3) { v03 = v.x; v13 = v.y; v23 = v.z; v33 = v.w; }
			else throw new Exception("The index is out of range!");
		}


		public static bool operator ==(Mat4x4 lhs, Mat4x4 rhs)
		{
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Mat4x4 lhs, Mat4x4 rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Mat4x4 operator ~(Mat4x4 m)
		{
			Mat4x4 nm = new Mat4x4();
			nm.v10 = m.v01;
			nm.v20 = m.v02;
			nm.v30 = m.v03;
			nm.v01 = m.v10;
			nm.v21 = m.v12;
			nm.v31 = m.v13;
			nm.v02 = m.v20;
			nm.v12 = m.v21;
			nm.v32 = m.v23;
			nm.v03 = m.v30;
			nm.v13 = m.v31;
			nm.v23 = m.v32;
			return nm;
		}

		public static Mat4x4 operator +(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = lhs.v00 + rhs.v00;
			m.v10 = lhs.v10 + rhs.v10;
			m.v20 = lhs.v20 + rhs.v20;
			m.v30 = lhs.v30 + rhs.v30;
			m.v01 = lhs.v01 + rhs.v01;
			m.v11 = lhs.v11 + rhs.v11;
			m.v21 = lhs.v21 + rhs.v21;
			m.v31 = lhs.v31 + rhs.v31;
			m.v02 = lhs.v02 + rhs.v02;
			m.v12 = lhs.v12 + rhs.v12;
			m.v22 = lhs.v22 + rhs.v22;
			m.v32 = lhs.v32 + rhs.v32;
			m.v03 = lhs.v03 + rhs.v03;
			m.v13 = lhs.v13 + rhs.v13;
			m.v23 = lhs.v23 + rhs.v23;
			m.v33 = lhs.v33 + rhs.v33;
			return m;
		}

		public static Mat4x4 operator -(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = lhs.v00 - rhs.v00;
			m.v10 = lhs.v10 - rhs.v10;
			m.v20 = lhs.v20 - rhs.v20;
			m.v30 = lhs.v30 - rhs.v30;
			m.v01 = lhs.v01 - rhs.v01;
			m.v11 = lhs.v11 - rhs.v11;
			m.v21 = lhs.v21 - rhs.v21;
			m.v31 = lhs.v31 - rhs.v31;
			m.v02 = lhs.v02 - rhs.v02;
			m.v12 = lhs.v12 - rhs.v12;
			m.v22 = lhs.v22 - rhs.v22;
			m.v32 = lhs.v32 - rhs.v32;
			m.v03 = lhs.v03 - rhs.v03;
			m.v13 = lhs.v13 - rhs.v13;
			m.v23 = lhs.v23 - rhs.v23;
			m.v33 = lhs.v33 - rhs.v33;
			return m;
		}

		public static Mat4x4 operator *(Mat4x4 lhs, double rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = lhs.v00 * rhs;
			m.v10 = lhs.v10 * rhs;
			m.v20 = lhs.v20 * rhs;
			m.v30 = lhs.v30 * rhs;
			m.v01 = lhs.v01 * rhs;
			m.v11 = lhs.v11 * rhs;
			m.v21 = lhs.v21 * rhs;
			m.v31 = lhs.v31 * rhs;
			m.v02 = lhs.v02 * rhs;
			m.v12 = lhs.v12 * rhs;
			m.v22 = lhs.v22 * rhs;
			m.v32 = lhs.v32 * rhs;
			m.v03 = lhs.v03 * rhs;
			m.v13 = lhs.v13 * rhs;
			m.v23 = lhs.v23 * rhs;
			m.v33 = lhs.v33 * rhs;
			return m;
		}
		public static Mat4x4 operator *(double lhs, Mat4x4 rhs)
		{
			return rhs * lhs;
		}
		
		public static Mat4x4 operator /(Mat4x4 lhs, double rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = lhs.v00 / rhs;
			m.v10 = lhs.v10 / rhs;
			m.v20 = lhs.v20 / rhs;
			m.v30 = lhs.v30 / rhs;
			m.v01 = lhs.v01 / rhs;
			m.v11 = lhs.v11 / rhs;
			m.v21 = lhs.v21 / rhs;
			m.v31 = lhs.v31 / rhs;
			m.v02 = lhs.v02 / rhs;
			m.v12 = lhs.v12 / rhs;
			m.v22 = lhs.v22 / rhs;
			m.v32 = lhs.v32 / rhs;
			m.v03 = lhs.v03 / rhs;
			m.v13 = lhs.v13 / rhs;
			m.v23 = lhs.v23 / rhs;
			m.v33 = lhs.v33 / rhs;
			return m;
		}
		public static Mat4x4 operator /(double lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = lhs / rhs.v00;
			m.v10 = lhs / rhs.v10;
			m.v20 = lhs / rhs.v20;
			m.v30 = lhs / rhs.v30;
			m.v01 = lhs / rhs.v01;
			m.v11 = lhs / rhs.v11;
			m.v21 = lhs / rhs.v21;
			m.v31 = lhs / rhs.v31;
			m.v02 = lhs / rhs.v02;
			m.v12 = lhs / rhs.v12;
			m.v22 = lhs / rhs.v22;
			m.v32 = lhs / rhs.v32;
			m.v03 = lhs / rhs.v03;
			m.v13 = lhs / rhs.v13;
			m.v23 = lhs / rhs.v23;
			m.v33 = lhs / rhs.v33;
			return m;
		}

		public static Mat4x4 operator *(Mat4x4 lhs, Mat4x4 rhs)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = lhs.v00 * rhs.v00 + lhs.v10 * rhs.v01 + lhs.v20 * rhs.v02 + lhs.v30 * rhs.v03;
			m.v10 = lhs.v00 * rhs.v10 + lhs.v10 * rhs.v11 + lhs.v20 * rhs.v12 + lhs.v30 * rhs.v13;
			m.v20 = lhs.v00 * rhs.v20 + lhs.v10 * rhs.v21 + lhs.v20 * rhs.v22 + lhs.v30 * rhs.v23;
			m.v30 = lhs.v00 * rhs.v30 + lhs.v10 * rhs.v31 + lhs.v20 * rhs.v32 + lhs.v30 * rhs.v33;
			m.v01 = lhs.v01 * rhs.v00 + lhs.v11 * rhs.v01 + lhs.v21 * rhs.v02 + lhs.v31 * rhs.v03;
			m.v11 = lhs.v01 * rhs.v10 + lhs.v11 * rhs.v11 + lhs.v21 * rhs.v12 + lhs.v31 * rhs.v13;
			m.v21 = lhs.v01 * rhs.v20 + lhs.v11 * rhs.v21 + lhs.v21 * rhs.v22 + lhs.v31 * rhs.v23;
			m.v31 = lhs.v01 * rhs.v30 + lhs.v11 * rhs.v31 + lhs.v21 * rhs.v32 + lhs.v31 * rhs.v33;
			m.v02 = lhs.v02 * rhs.v00 + lhs.v12 * rhs.v01 + lhs.v22 * rhs.v02 + lhs.v32 * rhs.v03;
			m.v12 = lhs.v02 * rhs.v10 + lhs.v12 * rhs.v11 + lhs.v22 * rhs.v12 + lhs.v32 * rhs.v13;
			m.v22 = lhs.v02 * rhs.v20 + lhs.v12 * rhs.v21 + lhs.v22 * rhs.v22 + lhs.v32 * rhs.v23;
			m.v32 = lhs.v02 * rhs.v30 + lhs.v12 * rhs.v31 + lhs.v22 * rhs.v32 + lhs.v32 * rhs.v33;
			m.v03 = lhs.v03 * rhs.v00 + lhs.v13 * rhs.v01 + lhs.v23 * rhs.v02 + lhs.v33 * rhs.v03;
			m.v13 = lhs.v03 * rhs.v10 + lhs.v13 * rhs.v11 + lhs.v23 * rhs.v12 + lhs.v33 * rhs.v13;
			m.v23 = lhs.v03 * rhs.v20 + lhs.v13 * rhs.v21 + lhs.v23 * rhs.v22 + lhs.v33 * rhs.v23;
			m.v33 = lhs.v03 * rhs.v30 + lhs.v13 * rhs.v31 + lhs.v23 * rhs.v32 + lhs.v33 * rhs.v33;
			return m;
		}

		public static Vec4 operator *(Mat4x4 lhs, Vec4 rhs)
		{
			Vec4 v = new Vec4();
			v.x = lhs.v00 * rhs.x + lhs.v10 * rhs.y + lhs.v20 * rhs.z + lhs.v30 * rhs.w;
			v.y = lhs.v01 * rhs.x + lhs.v11 * rhs.y + lhs.v21 * rhs.z + lhs.v31 * rhs.w;
			v.z = lhs.v02 * rhs.x + lhs.v12 * rhs.y + lhs.v22 * rhs.z + lhs.v32 * rhs.w;
			v.w = lhs.v03 * rhs.x + lhs.v13 * rhs.y + lhs.v23 * rhs.z + lhs.v33 * rhs.w;
			return v;
		}
		public static Vec3 operator *(Mat4x4 lhs, Vec3 rhs)
		{
			Vec3 v = new Vec3();
			v.x = lhs.v00 * rhs.x + lhs.v10 * rhs.y + lhs.v20 * rhs.z + lhs.v30;
			v.y = lhs.v01 * rhs.x + lhs.v11 * rhs.y + lhs.v21 * rhs.z + lhs.v31;
			v.z = lhs.v02 * rhs.x + lhs.v12 * rhs.y + lhs.v22 * rhs.z + lhs.v32;
			return v;
		}


		public static Mat4x4 Translate(Vec3 delta)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = m.v11 = m.v22 = m.v33 = 1;
			m.v30 = delta.x;
			m.v31 = delta.y;
			m.v32 = delta.z;
			return m;
		}

		public static Mat4x4 RotateX(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.v00 = m.v33 = 1;
			double cx = Math.Cos(angle);
			double sx = Math.Sin(angle);
			m.v11 = cx;
			m.v21 = -sx;
			m.v12 = sx;
			m.v22 = cx;
			return m;
		}
		public static Mat4x4 RotateY(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.v11 = m.v33 = 1;
			double cy = Math.Cos(angle);
			double sy = Math.Sin(angle);
			m.v00 = cy;
			m.v20 = sy;
			m.v02 = -sy;
			m.v22 = cy;
			return m;
		}
		public static Mat4x4 RotateZ(double angle)
		{
			Mat4x4 m = new Mat4x4();
			m.v22 = m.v33 = 1;
			double cz = Math.Cos(angle);
			double sz = Math.Sin(angle);
			m.v00 = cz;
			m.v10 = -sz;
			m.v01 = sz;
			m.v11 = cz;
			return m;
		}

		public static Mat4x4 RotateZYX(Vec3 euler)
		{
			Mat4x4 m = new Mat4x4();
			m.v33 = 1;
			double cx = Math.Cos(euler.x);
			double sx = Math.Sin(euler.x);
			double cy = Math.Cos(euler.y);
			double sy = Math.Sin(euler.y);
			double cz = Math.Cos(euler.z);
			double sz = Math.Sin(euler.z);
			m.v00 = cy * cz;
			m.v10 = -cx * sz + sx * sy * cz;
			m.v20 = sx * sz + cx * sy * cz;
			m.v01 = cy * sz;
			m.v11 = cx * cz + sx * sy * sz;
			m.v21 = -sx * cz + cx * sy * sz;
			m.v02 = -sy;
			m.v12 = sx * cy;
			m.v22 = cx * cy;
			return m;
		}
		public static Mat4x4 RotateYXZ(Vec3 euler)
		{
			Mat4x4 m = new Mat4x4();
			m.v33 = 1;
			double cz = Math.Cos(euler.z);
			double sz = Math.Sin(euler.z);
			double cx = Math.Cos(euler.x);
			double sx = Math.Sin(euler.x);
			double cy = Math.Cos(euler.y);
			double sy = Math.Sin(euler.y);
			m.v00 = cy * cz + sx * sy * sz;
			m.v10 = -cy * sz + sx * sy * cz;
			m.v20 = cx * sy;
			m.v01 = cx * sz;
			m.v11 = cx * cz;
			m.v21 = -sx;
			m.v02 = -sy * cz + sx * cy * sz;
			m.v12 = sy * sz + sx * cy * cz;
			m.v22 = cx * cy;
			return m;
		}

		public static Mat4x4 Rotate(Vec3 euler)
		{
			return RotateYXZ(euler);
		}
		public static Mat4x4 Rotate(double angle, Vec3 normalAxis)
		{
			Mat4x4 m = new Mat4x4();
			m.v33 = 1;
			double ax = normalAxis.x, ay = normalAxis.y, az = normalAxis.z;
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			m.v00 = ax * ax + (1.0 - ax * ax) * cos;
			m.v10 = ax * ay * (1.0 - cos) - az * sin;
			m.v20 = ax * az * (1.0 - cos) + ay * sin;
			m.v01 = ay * ax * (1.0 - cos) + az * sin;
			m.v11 = ay * ay + (1.0 - ay * ay) * cos;
			m.v21 = ay * az * (1.0 - cos) - ax * sin;
			m.v02 = az * ax * (1.0 - cos) - ay * sin;
			m.v12 = az * ay * (1.0 - cos) + ax * sin;
			m.v22 = az * az + (1.0 - az * az) * cos;
			return m;
		}
		public static Mat4x4 Rotate(Quat rotation)
		{
			double angle;
			Vec3 normalAxis;
			Quat.ToAngleAxis(rotation, out angle, out normalAxis);
			return Rotate(angle, normalAxis);
		}

		public static Mat4x4 Scale(Vec3 scale)
		{
			Mat4x4 m = new Mat4x4();
			m.v33 = 1;
			m.v00 = scale.x;
			m.v11 = scale.y;
			m.v22 = scale.z;
			return m;
		}

		public static Mat4x4 TRzyxS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * RotateZYX(euler) * Scale(scale);
		}
		public static Mat4x4 TRyxzS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * RotateYXZ(euler) * Scale(scale);
		}

		public static Mat4x4 TRS(Vec3 delta, Vec3 euler, Vec3 scale)
		{
			return Translate(delta) * Rotate(euler) * Scale(scale);
		}
		public static Mat4x4 TRS(Vec3 delta, double angle, Vec3 normalAxis, Vec3 scale)
		{
			return Translate(delta) * Rotate(angle, normalAxis) * Scale(scale);
		}
		public static Mat4x4 TRS(Vec3 delta, Quat rotation, Vec3 scale)
		{
			return Translate(delta) * Rotate(rotation) * Scale(scale);
		}

	}
}
