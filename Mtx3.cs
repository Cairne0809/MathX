using System;

namespace mathx
{
	public struct Mtx3
	{
		public double v00;
		public double v01;
		public double v02;

		public double v10;
		public double v11;
		public double v12;

		public double v20;
		public double v21;
		public double v22;

		public double this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return v00;
					case 1: return v01;
					case 2: return v02;
					
					case 4: return v10;
					case 5: return v11;
					case 6: return v12;
					
					case 8: return v20;
					case 9: return v21;
					case 10: return v22;
					
					default: throw new Exception("The index is out of range!");
				}
			}
			set
			{
				switch (index)
				{
					case 0: v00 = value; break;
					case 1: v01 = value; break;
					case 2: v02 = value; break;
					
					case 4: v10 = value; break;
					case 5: v11 = value; break;
					case 6: v12 = value; break;
					
					case 8: v20 = value; break;
					case 9: v21 = value; break;
					case 10: v22 = value; break;
					
					default: throw new Exception("The index is out of range!");
				}
			}
		}
		public bool isNaN
		{
			get
			{
				return
					double.IsNaN(v00) ||
					double.IsNaN(v01) ||
					double.IsNaN(v02) ||

					double.IsNaN(v10) ||
					double.IsNaN(v11) ||
					double.IsNaN(v12) ||

					double.IsNaN(v20) ||
					double.IsNaN(v21) ||
					double.IsNaN(v22);
			}
		}
		
		public string ToString(string format)
		{
			string str = "";
			str += "|\t";
			str += v00.ToString(format) + "\t";
			str += v01.ToString(format) + "\t";
			str += v02.ToString(format) + "\t|\n|\t";
			str += v10.ToString(format) + "\t";
			str += v11.ToString(format) + "\t";
			str += v12.ToString(format) + "\t|\n|\t";
			str += v20.ToString(format) + "\t";
			str += v21.ToString(format) + "\t";
			str += v22.ToString(format) + "\t|";
			return str;
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


		public static bool operator ==(Mtx44 lhs, Mtx44 rhs)
		{
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Mtx44 lhs, Mtx44 rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Mtx44 operator ~(Mtx44 m)
		{
			double n12 = m.v10;
			double n13 = m.v20;
			double n14 = m.v30;

			double n21 = m.v01;
			double n23 = m.v21;
			double n24 = m.v31;

			double n31 = m.v02;
			double n32 = m.v12;
			double n34 = m.v32;

			double n41 = m.v03;
			double n42 = m.v13;
			double n43 = m.v23;

			m.v01 = n12;
			m.v02 = n13;
			m.v03 = n14;

			m.v10 = n21;
			m.v12 = n23;
			m.v13 = n24;

			m.v20 = n31;
			m.v21 = n32;
			m.v23 = n34;

			m.v30 = n41;
			m.v31 = n42;
			m.v32 = n43;

			return m;
		}

		public static Mtx44 operator +(Mtx44 lhs, Mtx44 rhs)
		{
			Mtx44 m = new Mtx44();

			m.v00 = lhs.v00 + rhs.v00;
			m.v01 = lhs.v01 + rhs.v01;
			m.v02 = lhs.v02 + rhs.v02;
			m.v03 = lhs.v03 + rhs.v03;

			m.v10 = lhs.v10 + rhs.v10;
			m.v11 = lhs.v11 + rhs.v11;
			m.v12 = lhs.v12 + rhs.v12;
			m.v13 = lhs.v13 + rhs.v13;

			m.v20 = lhs.v20 + rhs.v20;
			m.v21 = lhs.v21 + rhs.v21;
			m.v22 = lhs.v22 + rhs.v22;
			m.v23 = lhs.v23 + rhs.v23;

			m.v30 = lhs.v30 + rhs.v30;
			m.v31 = lhs.v31 + rhs.v31;
			m.v32 = lhs.v32 + rhs.v32;
			m.v33 = lhs.v33 + rhs.v33;

			return m;
		}

		public static Mtx44 operator -(Mtx44 lhs, Mtx44 rhs)
		{
			Mtx44 m = new Mtx44();

			m.v00 = lhs.v00 - rhs.v00;
			m.v01 = lhs.v01 - rhs.v01;
			m.v02 = lhs.v02 - rhs.v02;
			m.v03 = lhs.v03 - rhs.v03;

			m.v10 = lhs.v10 - rhs.v10;
			m.v11 = lhs.v11 - rhs.v11;
			m.v12 = lhs.v12 - rhs.v12;
			m.v13 = lhs.v13 - rhs.v13;

			m.v20 = lhs.v20 - rhs.v20;
			m.v21 = lhs.v21 - rhs.v21;
			m.v22 = lhs.v22 - rhs.v22;
			m.v23 = lhs.v23 - rhs.v23;

			m.v30 = lhs.v30 - rhs.v30;
			m.v31 = lhs.v31 - rhs.v31;
			m.v32 = lhs.v32 - rhs.v32;
			m.v33 = lhs.v33 - rhs.v33;

			return m;
		}

		public static Mtx44 operator *(double lhs, Mtx44 rhs)
		{
			Mtx44 m = new Mtx44();

			m.v00 = lhs * rhs.v00;
			m.v01 = lhs * rhs.v01;
			m.v02 = lhs * rhs.v02;
			m.v03 = lhs * rhs.v03;

			m.v10 = lhs * rhs.v10;
			m.v11 = lhs * rhs.v11;
			m.v12 = lhs * rhs.v12;
			m.v13 = lhs * rhs.v13;

			m.v20 = lhs * rhs.v20;
			m.v21 = lhs * rhs.v21;
			m.v22 = lhs * rhs.v22;
			m.v23 = lhs * rhs.v23;

			m.v30 = lhs * rhs.v30;
			m.v31 = lhs * rhs.v31;
			m.v32 = lhs * rhs.v32;
			m.v33 = lhs * rhs.v33;

			return m;
		}

		public static Mtx44 operator *(Mtx44 lhs, double rhs)
		{
			Mtx44 m = new Mtx44();

			m.v00 = lhs.v00 * rhs;
			m.v01 = lhs.v01 * rhs;
			m.v02 = lhs.v02 * rhs;
			m.v03 = lhs.v03 * rhs;

			m.v10 = lhs.v10 * rhs;
			m.v11 = lhs.v11 * rhs;
			m.v12 = lhs.v12 * rhs;
			m.v13 = lhs.v13 * rhs;

			m.v20 = lhs.v20 * rhs;
			m.v21 = lhs.v21 * rhs;
			m.v22 = lhs.v22 * rhs;
			m.v23 = lhs.v23 * rhs;

			m.v30 = lhs.v30 * rhs;
			m.v31 = lhs.v31 * rhs;
			m.v32 = lhs.v32 * rhs;
			m.v33 = lhs.v33 * rhs;

			return m;
		}

		public static Mtx44 operator /(Mtx44 lhs, double rhs)
		{
			Mtx44 m = new Mtx44();

			m.v00 = lhs.v00 / rhs;
			m.v01 = lhs.v01 / rhs;
			m.v02 = lhs.v02 / rhs;
			m.v03 = lhs.v03 / rhs;

			m.v10 = lhs.v10 / rhs;
			m.v11 = lhs.v11 / rhs;
			m.v12 = lhs.v12 / rhs;
			m.v13 = lhs.v13 / rhs;

			m.v20 = lhs.v20 / rhs;
			m.v21 = lhs.v21 / rhs;
			m.v22 = lhs.v22 / rhs;
			m.v23 = lhs.v23 / rhs;

			m.v30 = lhs.v30 / rhs;
			m.v31 = lhs.v31 / rhs;
			m.v32 = lhs.v32 / rhs;
			m.v33 = lhs.v33 / rhs;

			return m;
		}

		public static Mtx44 operator *(Mtx44 lhs, Mtx44 rhs)
		{
			Mtx44 m = new Mtx44();

			m.v00 = lhs.v00 * rhs.v00 + lhs.v01 * rhs.v10 + lhs.v02 * rhs.v20 + lhs.v03 * rhs.v30;
			m.v01 = lhs.v00 * rhs.v01 + lhs.v01 * rhs.v11 + lhs.v02 * rhs.v21 + lhs.v03 * rhs.v31;
			m.v02 = lhs.v00 * rhs.v02 + lhs.v01 * rhs.v12 + lhs.v02 * rhs.v22 + lhs.v03 * rhs.v32;
			m.v03 = lhs.v00 * rhs.v03 + lhs.v01 * rhs.v13 + lhs.v02 * rhs.v23 + lhs.v03 * rhs.v33;

			m.v10 = lhs.v10 * rhs.v00 + lhs.v11 * rhs.v10 + lhs.v12 * rhs.v20 + lhs.v13 * rhs.v30;
			m.v11 = lhs.v10 * rhs.v01 + lhs.v11 * rhs.v11 + lhs.v12 * rhs.v21 + lhs.v13 * rhs.v31;
			m.v12 = lhs.v10 * rhs.v02 + lhs.v11 * rhs.v12 + lhs.v12 * rhs.v22 + lhs.v13 * rhs.v32;
			m.v13 = lhs.v10 * rhs.v03 + lhs.v11 * rhs.v13 + lhs.v12 * rhs.v23 + lhs.v13 * rhs.v33;

			m.v20 = lhs.v20 * rhs.v00 + lhs.v21 * rhs.v10 + lhs.v22 * rhs.v20 + lhs.v23 * rhs.v30;
			m.v21 = lhs.v20 * rhs.v01 + lhs.v21 * rhs.v11 + lhs.v22 * rhs.v21 + lhs.v23 * rhs.v31;
			m.v22 = lhs.v20 * rhs.v02 + lhs.v21 * rhs.v12 + lhs.v22 * rhs.v22 + lhs.v23 * rhs.v32;
			m.v23 = lhs.v20 * rhs.v03 + lhs.v21 * rhs.v13 + lhs.v22 * rhs.v23 + lhs.v23 * rhs.v33;

			m.v30 = lhs.v30 * rhs.v00 + lhs.v31 * rhs.v10 + lhs.v32 * rhs.v20 + lhs.v33 * rhs.v30;
			m.v31 = lhs.v30 * rhs.v01 + lhs.v31 * rhs.v11 + lhs.v32 * rhs.v21 + lhs.v33 * rhs.v31;
			m.v32 = lhs.v30 * rhs.v02 + lhs.v31 * rhs.v12 + lhs.v32 * rhs.v22 + lhs.v33 * rhs.v32;
			m.v33 = lhs.v30 * rhs.v03 + lhs.v31 * rhs.v13 + lhs.v32 * rhs.v23 + lhs.v33 * rhs.v33;

			return m;
		}

	}
}
