using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public class LnEqn<T> where T : IVector, new()
	{
		public T coefs;
		public double constant;

		public LnEqn(T coefs, double constant)
		{
			this.coefs = coefs;
			this.constant = constant;
		}
		public LnEqn(LnEqn<T> eqn)
		{
			this.coefs = eqn.coefs;
			this.constant = eqn.constant;
		}

		public LnEqn<T> NormalizeAt(int index)
		{
			if (coefs[index] == 0)
				throw new Exception();

			double m = 1 / coefs[index];
			for (int i = 0; i < coefs.Dimension; i++)
			{
				coefs[i] *= m;
			}
			constant *= m;
			return this;
		}

		public LnEqn<T> Eliminate(int index, double value)
		{
			constant -= coefs[index] * value;
			coefs[index] = 0;
			return this;
		}

		public LnEqn<T> Eliminate(LnEqn<T> from, int index)
		{
			if (from.coefs[index] == 0)
				throw new Exception();

			double m = coefs[index] / from.coefs[index];
			for (int i = 0; i < coefs.Dimension; i++)
			{
				coefs[i] -= from.coefs[i] * m;
			}
			constant -= from.constant * m;
			return this;
		}
	}

	public static class LinearEquation
	{
		public static LnEqn<T> NormalizeAt<T>(LnEqn<T> eqn, int index) where T : IVector, new()
		{
			return new LnEqn<T>(eqn).NormalizeAt(index);
		}

		public static LnEqn<T> Eliminate<T>(LnEqn<T> eqn, int index, double value) where T : IVector, new()
		{
			return new LnEqn<T>(eqn).Eliminate(index, value);
		}

		public static LnEqn<T> Eliminate<T>(LnEqn<T> eqn, LnEqn<T> from, int index) where T : IVector, new()
		{
			return new LnEqn<T>(eqn).Eliminate(from, index);
		}

		public static T SolveEquations<T>(params LnEqn<T>[] equations) where T : IVector, new()
		{
			return SolveEquations((IList<LnEqn<T>>)equations);
		}
		public static T SolveEquations<T>(IList<LnEqn<T>> equations) where T : IVector, new()
		{
			if (equations.Count != equations[0].coefs.Dimension)
				throw new Exception("");

			LnEqn<T>[] eqns = new LnEqn<T>[equations.Count];
			for (int i = 0; i < eqns.Length; i++)
			{
				eqns[i] = new LnEqn<T>(equations[i]);
			}

			for (int i = 0; i < eqns.Length - 1; i++)
			{
				for (int j = i + 1; j < eqns.Length; j++)
				{
					eqns[j].Eliminate(eqns[i], i);
				}
			}
			T results = new T();
			for (int i = eqns.Length - 1; i >= 1; i--)
			{
				eqns[i].NormalizeAt(i);
				results[i] = eqns[i].constant;
				eqns[i - 1].Eliminate(i, results[i]);
			}
			eqns[0].NormalizeAt(0);
			results[0] = eqns[0].constant;
			return results;
		}
	}
}
