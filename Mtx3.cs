using System;
using System.Text;

namespace MathematicsX
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
			StringBuilder sb = new StringBuilder();
			sb.Append("|\t")
				.Append(v00.ToString(format)).Append("\t")
				.Append(v01.ToString(format)).Append("\t")
				.Append(v02.ToString(format)).Append("\t|\n|\t")
				.Append(v10.ToString(format)).Append("\t")
				.Append(v11.ToString(format)).Append("\t")
				.Append(v12.ToString(format)).Append("\t|\n|\t")
				.Append(v20.ToString(format)).Append("\t")
				.Append(v21.ToString(format)).Append("\t")
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


		public static bool operator ==(Mtx3 lhs, Mtx3 rhs)
		{
			return lhs.Equals(rhs);
		}
		public static bool operator !=(Mtx3 lhs, Mtx3 rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static Mtx3 operator ~(Mtx3 m)
		{
			double n01 = m.v10;
			double n02 = m.v20;

			double n10 = m.v01;
			double n12 = m.v21;

			double n20 = m.v02;
			double n21 = m.v12;

			m.v01 = n01;
			m.v02 = n02;

			m.v10 = n10;
			m.v12 = n12;

			m.v20 = n20;
			m.v21 = n21;

			return m;
		}

		public static Mtx3 operator +(Mtx3 lhs, Mtx3 rhs)
		{
			Mtx3 m = new Mtx3();

			m.v00 = lhs.v00 + rhs.v00;
			m.v01 = lhs.v01 + rhs.v01;
			m.v02 = lhs.v02 + rhs.v02;

			m.v10 = lhs.v10 + rhs.v10;
			m.v11 = lhs.v11 + rhs.v11;
			m.v12 = lhs.v12 + rhs.v12;

			m.v20 = lhs.v20 + rhs.v20;
			m.v21 = lhs.v21 + rhs.v21;
			m.v22 = lhs.v22 + rhs.v22;

			return m;
		}

		public static Mtx3 operator -(Mtx3 lhs, Mtx3 rhs)
		{
			Mtx3 m = new Mtx3();

			m.v00 = lhs.v00 - rhs.v00;
			m.v01 = lhs.v01 - rhs.v01;
			m.v02 = lhs.v02 - rhs.v02;

			m.v10 = lhs.v10 - rhs.v10;
			m.v11 = lhs.v11 - rhs.v11;
			m.v12 = lhs.v12 - rhs.v12;

			m.v20 = lhs.v20 - rhs.v20;
			m.v21 = lhs.v21 - rhs.v21;
			m.v22 = lhs.v22 - rhs.v22;

			return m;
		}

		public static Mtx3 operator *(double lhs, Mtx3 rhs)
		{
			Mtx3 m = new Mtx3();

			m.v00 = lhs * rhs.v00;
			m.v01 = lhs * rhs.v01;
			m.v02 = lhs * rhs.v02;

			m.v10 = lhs * rhs.v10;
			m.v11 = lhs * rhs.v11;
			m.v12 = lhs * rhs.v12;

			m.v20 = lhs * rhs.v20;
			m.v21 = lhs * rhs.v21;
			m.v22 = lhs * rhs.v22;

			return m;
		}

		public static Mtx3 operator *(Mtx3 lhs, double rhs)
		{
			Mtx3 m = new Mtx3();

			m.v00 = lhs.v00 * rhs;
			m.v01 = lhs.v01 * rhs;
			m.v02 = lhs.v02 * rhs;

			m.v10 = lhs.v10 * rhs;
			m.v11 = lhs.v11 * rhs;
			m.v12 = lhs.v12 * rhs;

			m.v20 = lhs.v20 * rhs;
			m.v21 = lhs.v21 * rhs;
			m.v22 = lhs.v22 * rhs;

			return m;
		}

		public static Mtx3 operator /(Mtx3 lhs, double rhs)
		{
			Mtx3 m = new Mtx3();

			m.v00 = lhs.v00 / rhs;
			m.v01 = lhs.v01 / rhs;
			m.v02 = lhs.v02 / rhs;

			m.v10 = lhs.v10 / rhs;
			m.v11 = lhs.v11 / rhs;
			m.v12 = lhs.v12 / rhs;

			m.v20 = lhs.v20 / rhs;
			m.v21 = lhs.v21 / rhs;
			m.v22 = lhs.v22 / rhs;

			return m;
		}

		public static Mtx3 operator *(Mtx3 lhs, Mtx3 rhs)
		{
			Mtx3 m = new Mtx3();

			m.v00 = lhs.v00 * rhs.v00 + lhs.v01 * rhs.v10 + lhs.v02 * rhs.v20;
			m.v01 = lhs.v00 * rhs.v01 + lhs.v01 * rhs.v11 + lhs.v02 * rhs.v21;
			m.v02 = lhs.v00 * rhs.v02 + lhs.v01 * rhs.v12 + lhs.v02 * rhs.v22;

			m.v10 = lhs.v10 * rhs.v00 + lhs.v11 * rhs.v10 + lhs.v12 * rhs.v20;
			m.v11 = lhs.v10 * rhs.v01 + lhs.v11 * rhs.v11 + lhs.v12 * rhs.v21;
			m.v12 = lhs.v10 * rhs.v02 + lhs.v11 * rhs.v12 + lhs.v12 * rhs.v22;

			m.v20 = lhs.v20 * rhs.v00 + lhs.v21 * rhs.v10 + lhs.v22 * rhs.v20;
			m.v21 = lhs.v20 * rhs.v01 + lhs.v21 * rhs.v11 + lhs.v22 * rhs.v21;
			m.v22 = lhs.v20 * rhs.v02 + lhs.v21 * rhs.v12 + lhs.v22 * rhs.v22;

			return m;
		}

	}
}
