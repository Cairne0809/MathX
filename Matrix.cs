using System;
using System.Text;

namespace MathematicsX
{
	public class Matrix : IMatrix
	{
		int _r;
		int _c;
		double[] _v;

		public double this[int r, int c]
		{
			get
			{
				return _v[_c * r  + c];
			}
			set
			{
				_v[_c * r + c] = value;
			}
		}
		public int row { get { return _r; } }
		public int column { get { return _c; } }
		public bool isNaM
		{
			get
			{
				for (int i = 0; i < _v.Length; i++)
				{
					if (double.IsNaN(_v[i])) return true;
				}
				return false;
			}
		}

		public Matrix(int row, int column, params double[] v)
		{
			_r = row;
			_c = column;
			_v = v;
		}

		public string ToString(string format)
		{
			StringBuilder sb = new StringBuilder();
			for (int j = 0; j < _r; j++)
			{
				sb.Append("\n|\t");
				for (int i = 0; i < _c - 1; i++)
				{
					sb.Append(_v[_c * j + i].ToString(format)).Append("\t");
				}
				sb.Append(_v[_c * j + _c - 1].ToString(format)).Append("\t|");
			}
			return sb.ToString();
		}
		public override string ToString()
		{
			return ToString("");
		}

		public Matrix Clone()
		{
			return new Matrix(_r, _c, _v.Clone() as double[]);
		}

		public Matrix Scale(double value)
		{
			for (int i = 0; i < _v.Length; i++)
			{
				_v[i] *= value;
			}
			return this;
		}

		public Matrix Add(Matrix mtx)
		{
			int r2 = mtx._r;
			int c2 = mtx._c;
			double[] v2 = mtx._v;
			if (_r != r2 || _c != c2) throw new Exception("Add failed: r1 != r2 || c1 != c2");
			for (int i = 0; i < _v.Length; i++)
			{
				_v[i] += v2[i];
			}
			return this;
		}

		public Matrix Sub(Matrix mtx)
		{
			int r2 = mtx._r;
			int c2 = mtx._c;
			double[] v2 = mtx._v;
			if (_r != r2 || _c != c2) throw new Exception("Subtract failed: r1 != r2 || c1 != c2");
			for (int i = 0; i < _v.Length; i++)
			{
				_v[i] -= v2[i];
			}
			return this;
		}
		
		public static Matrix Mul(Matrix lhs, Matrix rhs)
		{
			int r1 = lhs._r;
			int c1 = lhs._c;
			int r2 = rhs._r;
			int c2 = rhs._c;
			if (c1 != r2) throw new Exception("Multiply failed: c1 != r2");
			double[] v1 = lhs._v;
			double[] v2 = rhs._v;
			double[] nv = new double[r1 * c2];
			for (int j = 0; j < r1; j++)
			{
				for (int i = 0; i < c2; i++)
				{
					double value = 0;
					for (int k = 0; k < c1; k++)
					{
						value += v1[c1 * j + k] * v2[c2 * k + i];
					}
					nv[c2 * j + i] = value;
				}
			}
			return new Matrix(r1, c2, nv);
		}

		public static Matrix Permutate(Matrix mtx)
		{
			int r = mtx._r;
			int c = mtx._c;
			double[] v = mtx._v;
			double[] nv = new double[r * c];
			int k = 0;
			for (int i = 0; i < c; i++)
			{
				for (int j = 0; j < r; j++)
				{
					nv[k] = v[c * j + i];
					k++;
				}
			}
			return new Matrix(c, r, nv);
		}

	}
}
