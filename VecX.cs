using System;
using System.Text;

namespace MathematicsX
{
	public class VecX : IVector
	{
		double[] _x;

		public double this[int index] { get { return _x[index]; } set { _x[index] = value; } }
		public int dimension
		{
			get
			{
				return _x == null ? -1 : _x.Length;
			}
			set
			{
				if (_x == null)
				{
					_x = new double[value];
				}
				else
				{
					Array.Resize(ref _x, value);
				}
			}
		}
		public bool isNaV
		{
			get
			{
				for (int i = 0; i < _x.Length; i++)
				{
					if (double.IsNaN(_x[i]))
						return true;
				}
				return false;
			}
		}
		public double sqrMagnitude
		{
			get
			{
				double sum = 0;
				for (int i = 0; i < _x.Length; i++)
				{
					sum += _x[i] * _x[i];
				}
				return sum;
			}
		}
		public double magnitude
		{
			get
			{
				double sum = 0;
				for (int i = 0; i < _x.Length; i++)
				{
					sum += _x[i] * _x[i];
				}
				return Math.Sqrt(sum);
			}
		}
		public VecX abs
		{
			get
			{
				VecX nv = new VecX(_x.Length);
				for (int i = 0; i < _x.Length; i++)
				{
					nv[i] = Math.Abs(_x[i]);
				}
				return nv;
			}
		}

		public VecX normalized
		{
			get
			{
				double div = sqrMagnitude;
				if (div == 1) return new VecX(_x, true);
				VecX nv = new VecX(_x.Length);
				if (div > 0)
				{
					div = Math.Sqrt(div);
					for (int i = 0; i < _x.Length; i++)
					{
						nv[i] = _x[i] / div;
					}
				}
				return nv;
			}
		}

		internal VecX()
		{
			
		}

		public VecX(int dimension)
		{
			_x = new double[dimension];
		}
		public VecX(double[] X, bool doCopy)
		{
			if (doCopy)
			{
				_x = new double[X.Length];
				X.CopyTo(_x, 0);
			}
			else
			{
				_x = X;
			}
		}

		public VecX Clone()
		{
			return new VecX(_x, true);
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("V(");
			int len = _x.Length - 1;
			for (int i = 0; i < len; i++)
			{
				sb.Append(_x[i].ToString(format)).Append(", ");
			}
			sb.Append(_x[len].ToString(format)).Append(")");
			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString("");
		}

		public bool ValueEquals(VecX v)
		{
			if (_x.Length != v._x.Length) return false;
			for (int i = 0; i < _x.Length; i++)
			{
				if (Math.Abs(_x[i] - v._x[i]) > MathX.accuracy)
					return false;
			}
			return true;
		}


		public VecX Neg()
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] = -_x[i];
			}
			return this;
		}
		public VecX Add(VecX v)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] += v._x[i];
			}
			return this;
		}
		public VecX Sub(VecX v)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] -= v._x[i];
			}
			return this;
		}
		public VecX Mul(double n)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] *= n;
			}
			return this;
		}
		public VecX Div(double n)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] /= n;
			}
			return this;
		}

		public double Dot(VecX v)
		{
			double sum = 0;
			for (int i = 0; i < _x.Length; i++)
			{
				sum += _x[i] * v._x[i];
			}
			return sum;
		}

		public double SqrDistance(VecX v)
		{
			double sum = 0;
			for (int i = 0; i < _x.Length; i++)
			{
				double sub = _x[i] - v._x[i];
				sum += sub * sub;
			}
			return sum;
		}
		public double Distance(VecX v)
		{
			double sum = 0;
			for (int i = 0; i < _x.Length; i++)
			{
				double sub = _x[i] - v._x[i];
				sum += sub * sub;
			}
			return Math.Sqrt(sum);
		}

		public double Angle(VecX v)
		{
			double m1 = magnitude;
			double m2 = v.magnitude;
			if (m1 == 0 || m2 == 0) return 0;
			double cos = Dot(v) / m1 / m2;
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}


		public static VecX operator -(VecX v)
		{
			return v.Clone().Neg();
		}
		public static VecX operator +(VecX lhs, VecX rhs)
		{
			return lhs.Clone().Add(rhs);
		}
		public static VecX operator -(VecX lhs, VecX rhs)
		{
			return lhs.Clone().Sub(rhs);
		}
		public static VecX operator *(double lhs, VecX rhs)
		{
			return rhs.Clone().Mul(lhs);
		}
		public static VecX operator *(VecX lhs, double rhs)
		{
			return lhs.Clone().Mul(rhs);
		}
		public static VecX operator /(VecX lhs, double rhs)
		{
			return lhs.Clone().Div(rhs);
		}

		public static double operator *(VecX lhs, VecX rhs)
		{
			return lhs.Dot(rhs);
		}

		public static double SqrDistance(VecX lhs, VecX rhs)
		{
			return lhs.SqrDistance(rhs);
		}
		public static double Distance(VecX lhs, VecX rhs)
		{
			return lhs.Distance(rhs);
		}

		public static double Angle(VecX lhs, VecX rhs)
		{
			return lhs.Angle(rhs);
		}

		public static VecX Project(VecX src, VecX dst)
		{
			double sqrMag = dst.sqrMagnitude;
			if (sqrMag > 0)
			{
				double DdSM = src.Dot(dst) / sqrMag;
				return dst.Clone().Mul(DdSM);
			}
			return new VecX(src.dimension);
		}

		public static VecX Mirror(VecX src, VecX axis)
		{
			VecX pjt = Project(src, axis);
			return pjt.Add(pjt).Sub(src);
		}

	}
}
