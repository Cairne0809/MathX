using System;
using System.Text;

namespace MathematicsX
{
	public class VectorX : IVector
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
		public VectorX abs
		{
			get
			{
				VectorX nv = new VectorX(_x.Length);
				for (int i = 0; i < _x.Length; i++)
				{
					nv[i] = Math.Abs(_x[i]);
				}
				return nv;
			}
		}

		public VectorX normalized
		{
			get
			{
				double div = sqrMagnitude;
				if (div == 1) return new VectorX(_x, true);
				VectorX nv = new VectorX(_x.Length);
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

		public VectorX(int dimension)
		{
			_x = new double[dimension];
		}
		public VectorX(double[] X, bool doCopy)
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

		public VectorX Clone()
		{
			return new VectorX(_x, true);
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

		public bool ValueEquals(VectorX v)
		{
			if (_x.Length != v._x.Length) return false;
			for (int i = 0; i < _x.Length; i++)
			{
				if (Math.Abs(_x[i] - v._x[i]) > MathX.accuracy)
					return false;
			}
			return true;
		}


		public VectorX Neg()
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] = -_x[i];
			}
			return this;
		}
		public VectorX Add(VectorX v)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] += v._x[i];
			}
			return this;
		}
		public VectorX Sub(VectorX v)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] -= v._x[i];
			}
			return this;
		}
		public VectorX Mul(double n)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] *= n;
			}
			return this;
		}
		public VectorX Div(double n)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] /= n;
			}
			return this;
		}
		public VectorX InvDiv(double n)
		{
			for (int i = 0; i < _x.Length; i++)
			{
				_x[i] = n / _x[i];
			}
			return this;
		}

		public double Dot(VectorX v)
		{
			double sum = 0;
			for (int i = 0; i < _x.Length; i++)
			{
				sum += _x[i] * v._x[i];
			}
			return sum;
		}

		public double SqrDistance(VectorX v)
		{
			double sum = 0;
			for (int i = 0; i < _x.Length; i++)
			{
				double sub = _x[i] - v._x[i];
				sum += sub * sub;
			}
			return sum;
		}
		public double Distance(VectorX v)
		{
			double sum = 0;
			for (int i = 0; i < _x.Length; i++)
			{
				double sub = _x[i] - v._x[i];
				sum += sub * sub;
			}
			return Math.Sqrt(sum);
		}

		public double Angle(VectorX v)
		{
			double m1 = magnitude;
			double m2 = v.magnitude;
			if (m1 == 0 || m2 == 0) return 0;
			double cos = Dot(v) / m1 / m2;
			return Math.Acos(cos < -1 ? -1 : cos > 1 ? 1 : cos);
		}


		public static VectorX operator -(VectorX v)
		{
			return v.Clone().Neg();
		}
		public static VectorX operator +(VectorX lhs, VectorX rhs)
		{
			return lhs.Clone().Add(rhs);
		}
		public static VectorX operator -(VectorX lhs, VectorX rhs)
		{
			return lhs.Clone().Sub(rhs);
		}
		public static VectorX operator *(double lhs, VectorX rhs)
		{
			return rhs.Clone().Mul(lhs);
		}
		public static VectorX operator *(VectorX lhs, double rhs)
		{
			return lhs.Clone().Mul(rhs);
		}
		public static VectorX operator /(VectorX lhs, double rhs)
		{
			return lhs.Clone().Div(rhs);
		}
		public static VectorX operator /(double lhs, VectorX rhs)
		{
			return rhs.Clone().InvDiv(lhs);
		}

		public static double operator *(VectorX lhs, VectorX rhs)
		{
			return lhs.Dot(rhs);
		}

		public static double SqrDistance(VectorX lhs, VectorX rhs)
		{
			return lhs.SqrDistance(rhs);
		}
		public static double Distance(VectorX lhs, VectorX rhs)
		{
			return lhs.Distance(rhs);
		}

		public static double Angle(VectorX lhs, VectorX rhs)
		{
			return lhs.Angle(rhs);
		}

		public static VectorX Project(VectorX src, VectorX dst)
		{
			double sqrMag = dst.sqrMagnitude;
			if (sqrMag > 0)
			{
				double DdSM = src.Dot(dst) / sqrMag;
				return dst.Clone().Mul(DdSM);
			}
			return new VectorX(src.dimension);
		}

		public static VectorX Mirror(VectorX src, VectorX axis)
		{
			VectorX pjt = Project(src, axis);
			return pjt.Add(pjt).Sub(src);
		}

	}
}
