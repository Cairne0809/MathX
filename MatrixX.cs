using System;
using System.Text;

namespace MathematicsX
{
	public class MatrixX : IMatrix
	{
		int _c;
		int _r;
		double[] _v;

		public double this[int c, int r]
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
		public int column { get { return _c; } }
		public int row { get { return _r; } }
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

		public MatrixX(int column, int row, params double[] v)
		{
			_c = column;
			_r = row;
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

		public MatrixX Clone()
		{
			return new MatrixX(_c, _r, _v.Clone() as double[]);
		}

		public VectorX GetColumn(int c)
		{
			if (c < 0 || c >= _c) throw new Exception("The index is out of range!");
			VectorX v = new VectorX(_r);
			int vi = 0;
			for (int i = c; i < _v.Length; i += _c)
			{
				v[vi++] = _v[i];
			}
			return v;
		}
		public void SetColumn(int c, VectorX v)
		{
			if (c < 0 || c >= _c) throw new Exception("The index is out of range!");
			int vi = 0;
			for (int i = c; i < _v.Length; i += _c)
			{
				_v[i] = v[vi++];
			}
		}

		public VectorX GetRow(int r)
		{
			if (r < 0 || r >= _r) throw new Exception("The index is out of range!");
			VectorX v = new VectorX(_c);
			int vi = 0;
			for (int i = r * _c; i < r * _c + _c; i++)
			{
				v[vi++] = _v[i];
			}
			return v;
		}
		public void SetRow(int r, VectorX v)
		{
			if (r < 0 || r >= _r) throw new Exception("The index is out of range!");
			int vi = 0;
			for (int i = r * _c; i < r * _c + _c; i++)
			{
				_v[i] = v[vi++];
			}
		}


		public MatrixX Scale(double value)
		{
			for (int i = 0; i < _v.Length; i++)
			{
				_v[i] *= value;
			}
			return this;
		}

		public MatrixX Add(MatrixX mtx)
		{
			int c2 = mtx._c;
			int r2 = mtx._r;
			double[] v2 = mtx._v;
			if (_c != c2 || _r != r2) throw new Exception("Add failed: c1 != c2 || r1 != r2");
			for (int i = 0; i < _v.Length; i++)
			{
				_v[i] += v2[i];
			}
			return this;
		}

		public MatrixX Sub(MatrixX mtx)
		{
			int c2 = mtx._c;
			int r2 = mtx._r;
			double[] v2 = mtx._v;
			if (_r != r2 || _c != c2) throw new Exception("Subtract failed: c1 != c2 || r1 != r2");
			for (int i = 0; i < _v.Length; i++)
			{
				_v[i] -= v2[i];
			}
			return this;
		}
		
		public static MatrixX Mul(MatrixX lhs, MatrixX rhs)
		{
			int c1 = lhs._c;
			int r1 = lhs._r;
			int c2 = rhs._c;
			int r2 = rhs._r;
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
			return new MatrixX(c2, r1, nv);
		}

		public static MatrixX Permutate(MatrixX mtx)
		{
			int c = mtx._c;
			int r = mtx._r;
			double[] v = mtx._v;
			double[] nv = new double[c * r];
			int k = 0;
			for (int i = 0; i < c; i++)
			{
				for (int j = 0; j < r; j++)
				{
					nv[k++] = v[c * j + i];
				}
			}
			return new MatrixX(r, c, nv);
		}

	}
}
