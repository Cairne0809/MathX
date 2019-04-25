using System;
using System.Collections.Generic;

namespace MathematicsX
{
	public class SubSpace<T> where T : IVector, new()
	{
		private T m_origin;
		private T[] m_basis;
		
		public T Origin
		{
			get { return m_origin; }
			set { m_origin = value; }
		}

		public int Dimension
		{
			get { return m_basis.Length; }
		}

		public SubSpace()
		{
			m_origin = new T();
			m_basis = new T[0];
		}
		public SubSpace(T origin, IList<T> basis, bool processBasis = true)
		{
			m_origin = origin;
			SetBasis(basis, processBasis);
		}

		private void ProcessBasis()
		{
			for (int i = 0; i < m_basis.Length; i++)
			{
				T v = m_basis[i];
				for (int j = 0; j < i; j++)
				{
					v = VecX.Orthogonalize(v, m_basis[j]);
				}
				m_basis[i] = VecX.Normalize(v);
			}
		}

		public void SetBasis(IList<T> basis, bool processBasis = true)
		{
			Array.Resize(ref m_basis, Math.Min(basis.Count, m_origin.Dimension - 1));
			for (int i = 0; i < m_basis.Length; i++)
			{
				m_basis[i] = basis[i];
			}
			if (processBasis)
			{
				ProcessBasis();
			}
		}

		public T GetBasisVector(int index)
		{
			return m_basis[index];
		}

		public T Project(T v)
		{
			int dim = v.Dimension;
			v = VecX.Sub(v, m_origin);
			double[] nvArr = new double[dim];
			for (int i = 0; i < m_basis.Length; i++)
			{
				T bv = m_basis[i];
				double d = VecX.Dot(v, bv);
				for (int j = 0; j < dim; j++)
					nvArr[j] += d * bv[j];
			}
			T nv = new T();
			for (int i = 0; i < dim; i++)
			{
				nv[i] = nvArr[i] + m_origin[i];
			}
			return nv;
		}
	}
}
