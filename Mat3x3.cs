using System;
using System.Text;

namespace MathematicsX
{
	public struct Mat3x3 : IMatrix
	{
		public double v00;
		public double v10;
		public double v20;
		public double v01;
		public double v11;
		public double v21;
		public double v02;
		public double v12;
		public double v22;

		public double this[int c, int r]
		{
			get
			{
				int index = 3 * r + c;
				switch (index)
				{
					case 0: return v00;
					case 1: return v10;
					case 2: return v20;
					case 3: return v01;
					case 4: return v11;
					case 5: return v21;
					case 6: return v02;
					case 7: return v12;
					case 8: return v22;
					default: throw new Exception("The index is out of range!");
				}
			}
			set
			{
				int index = 3 * r + c;
				switch (index)
				{
					case 0: v00 = value; break;
					case 1: v10 = value; break;
					case 2: v20 = value; break;
					case 3: v01 = value; break;
					case 4: v11 = value; break;
					case 5: v21 = value; break;
					case 6: v02 = value; break;
					case 7: v12 = value; break;
					case 8: v22 = value; break;
					default: throw new Exception("The index is out of range!");
				}
			}
		}
		public Vec3 this[int c]
		{
			get
			{
				if (c == 0) return new Vec3(v00, v01, v02);
				if (c == 1) return new Vec3(v10, v11, v12);
				if (c == 2) return new Vec3(v20, v21, v22);
				throw new Exception("The index is out of range!");
			}
			set
			{
				if (c == 0) { v00 = value.x; v01 = value.y; v02 = value.z; }
				else if (c == 1) { v10 = value.x; v11 = value.y; v12 = value.z; }
				else if (c == 2) { v20 = value.x; v21 = value.y; v22 = value.z; }
				else throw new Exception("The index is out of range!");
			}
		}
		public int column { get { return 3; } }
		public int row { get { return 3; } }

		public bool isNaM
		{
			get
			{
				return
					double.IsNaN(v00) ||
					double.IsNaN(v10) ||
					double.IsNaN(v20) ||
					double.IsNaN(v01) ||
					double.IsNaN(v11) ||
					double.IsNaN(v21) ||
					double.IsNaN(v02) ||
					double.IsNaN(v12) ||
					double.IsNaN(v22);
			}
		}
		
		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(v00.ToString(format)).Append("\t")
				.Append(v10.ToString(format)).Append("\t")
				.Append(v20.ToString(format)).Append("\t|\n|\t")
				.Append(v01.ToString(format)).Append("\t")
				.Append(v11.ToString(format)).Append("\t")
				.Append(v21.ToString(format)).Append("\t|\n|\t")
				.Append(v02.ToString(format)).Append("\t")
				.Append(v12.ToString(format)).Append("\t")
				.Append(v22.ToString(format)).Append("\t|");
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

		public Vec3 GetRow(int r)
		{
			if (r == 0) return new Vec3(v00, v10, v20);
			if (r == 1) return new Vec3(v01, v11, v21);
			if (r == 2) return new Vec3(v02, v12, v22);
			throw new Exception("The index is out of range!");
		}
		public void SetRow(int r, Vec3 v)
		{
			if (r == 0) { v00 = v.x; v10 = v.y; v20 = v.z; }
			else if (r == 1) { v01 = v.x; v11 = v.y; v21 = v.z; }
			else if (r == 2) { v02 = v.x; v12 = v.y; v22 = v.z; }
			else throw new Exception("The index is out of range!");
		}


		public static bool operator ==(Mat3x3 lhs, Mat3x3 rhs)
		{
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Mat3x3 lhs, Mat3x3 rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Mat3x3 operator ~(Mat3x3 m)
		{
			Mat3x3 nm = new Mat3x3();
			nm.v10 = m.v01;
			nm.v20 = m.v02;
			nm.v01 = m.v10;
			nm.v21 = m.v12;
			nm.v02 = m.v20;
			nm.v12 = m.v21;
			return nm;
		}

		public static Mat3x3 operator +(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = lhs.v00 + rhs.v00;
			m.v10 = lhs.v10 + rhs.v10;
			m.v20 = lhs.v20 + rhs.v20;
			m.v01 = lhs.v01 + rhs.v01;
			m.v11 = lhs.v11 + rhs.v11;
			m.v21 = lhs.v21 + rhs.v21;
			m.v02 = lhs.v02 + rhs.v02;
			m.v12 = lhs.v12 + rhs.v12;
			m.v22 = lhs.v22 + rhs.v22;
			return m;
		}

		public static Mat3x3 operator -(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = lhs.v00 - rhs.v00;
			m.v10 = lhs.v10 - rhs.v10;
			m.v20 = lhs.v20 - rhs.v20;
			m.v01 = lhs.v01 - rhs.v01;
			m.v11 = lhs.v11 - rhs.v11;
			m.v21 = lhs.v21 - rhs.v21;
			m.v02 = lhs.v02 - rhs.v02;
			m.v12 = lhs.v12 - rhs.v12;
			m.v22 = lhs.v22 - rhs.v22;
			return m;
		}

		public static Mat3x3 operator *(Mat3x3 lhs, double rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = lhs.v00 * rhs;
			m.v10 = lhs.v10 * rhs;
			m.v20 = lhs.v20 * rhs;
			m.v01 = lhs.v01 * rhs;
			m.v11 = lhs.v11 * rhs;
			m.v21 = lhs.v21 * rhs;
			m.v02 = lhs.v02 * rhs;
			m.v12 = lhs.v12 * rhs;
			m.v22 = lhs.v22 * rhs;
			return m;
		}
		public static Mat3x3 operator *(double lhs, Mat3x3 rhs)
		{
			return rhs * lhs;
		}

		public static Mat3x3 operator /(Mat3x3 lhs, double rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = lhs.v00 / rhs;
			m.v10 = lhs.v10 / rhs;
			m.v20 = lhs.v20 / rhs;
			m.v01 = lhs.v01 / rhs;
			m.v11 = lhs.v11 / rhs;
			m.v21 = lhs.v21 / rhs;
			m.v02 = lhs.v02 / rhs;
			m.v12 = lhs.v12 / rhs;
			m.v22 = lhs.v22 / rhs;
			return m;
		}
		public static Mat3x3 operator /(double lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = lhs / rhs.v00;
			m.v10 = lhs / rhs.v10;
			m.v20 = lhs / rhs.v20;
			m.v01 = lhs / rhs.v01;
			m.v11 = lhs / rhs.v11;
			m.v21 = lhs / rhs.v21;
			m.v02 = lhs / rhs.v02;
			m.v12 = lhs / rhs.v12;
			m.v22 = lhs / rhs.v22;
			return m;
		}

		public static Mat3x3 operator *(Mat3x3 lhs, Mat3x3 rhs)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = lhs.v00 * rhs.v00 + lhs.v10 * rhs.v01 + lhs.v20 * rhs.v02;
			m.v10 = lhs.v00 * rhs.v10 + lhs.v10 * rhs.v11 + lhs.v20 * rhs.v12;
			m.v20 = lhs.v00 * rhs.v20 + lhs.v10 * rhs.v21 + lhs.v20 * rhs.v22;
			m.v01 = lhs.v01 * rhs.v00 + lhs.v11 * rhs.v01 + lhs.v21 * rhs.v02;
			m.v11 = lhs.v01 * rhs.v10 + lhs.v11 * rhs.v11 + lhs.v21 * rhs.v12;
			m.v21 = lhs.v01 * rhs.v20 + lhs.v11 * rhs.v21 + lhs.v21 * rhs.v22;
			m.v02 = lhs.v02 * rhs.v00 + lhs.v12 * rhs.v01 + lhs.v22 * rhs.v02;
			m.v12 = lhs.v02 * rhs.v10 + lhs.v12 * rhs.v11 + lhs.v22 * rhs.v12;
			m.v22 = lhs.v02 * rhs.v20 + lhs.v12 * rhs.v21 + lhs.v22 * rhs.v22;
			return m;
		}

		public static Vec3 operator *(Mat3x3 lhs, Vec3 rhs)
		{
			Vec3 v = new Vec3();
			v.x = lhs.v00 * rhs.x + lhs.v10 * rhs.y + lhs.v20 * rhs.z;
			v.y = lhs.v01 * rhs.x + lhs.v11 * rhs.y + lhs.v21 * rhs.z;
			v.z = lhs.v02 * rhs.x + lhs.v12 * rhs.y + lhs.v22 * rhs.z;
			return v;
		}
		public static Vec2 operator *(Mat3x3 lhs, Vec2 rhs)
		{
			Vec2 v = new Vec2();
			v.x = lhs.v00 * rhs.x + lhs.v10 * rhs.y + lhs.v20;
			v.y = lhs.v01 * rhs.x + lhs.v11 * rhs.y + lhs.v21;
			return v;
		}


		public static Mat3x3 Translate(Vec2 delta)
		{
			Mat3x3 m = new Mat3x3();
			m.v00 = m.v11 = m.v22 = 1;
			m.v20 = delta.x;
			m.v21 = delta.y;
			return m;
		}

		public static Mat3x3 Rotate(double angle)
		{
			Mat3x3 m = new Mat3x3();
			m.v22 = 1;
			double sin = Math.Sin(angle);
			double cos = Math.Cos(angle);
			m.v00 = cos;
			m.v10 = -sin;
			m.v01 = sin;
			m.v11 = cos;
			return m;
		}

		public static Mat3x3 Scale(Vec2 scale)
		{
			Mat3x3 m = new Mat3x3();
			m.v22 = 1;
			m.v00 = scale.x;
			m.v11 = scale.y;
			return m;
		}

		public static Mat3x3 TRS(Vec2 delta, double angle, Vec2 scale)
		{
			return Translate(delta) * Rotate(angle) * Scale(scale);
		}

	}
}
