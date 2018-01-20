using System;

namespace MathematicsX
{
	public struct Triangle<T> where T : IVector, new()
	{
		public T p0;
		public T p1;
		public T p2;

		public T this[int index]
		{
			get
			{
				if (index == 0) return p0;
				else if (index == 1) return p1;
				else if (index == 2) return p2;
				else throw new Exception("The index is out of range!");
			}
			set
			{
				if (index == 0) p0 = value;
				else if (index == 1) p1 = value;
				else if (index == 2) p2 = value;
				else throw new Exception("The index is out of range!");
			}
		}

		public Triangle(T p0, T p1, T p2)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
		}

		public void MinMax(out T min, out T max)
		{
			min = new T();
			for (int i = 0; i < p0.dimension; i++)
			{
				min[i] = p0[i];
				if (p1[i] < min[i]) min[i] = p1[i];
				if (p2[i] < min[i]) min[i] = p2[i];
			}
			max = new T();
			for (int i = 0; i < p0.dimension; i++)
			{
				max[i] = p0[i];
				if (p1[i] >= max[i]) max[i] = p1[i];
				if (p2[i] >= max[i]) max[i] = p2[i];
			}
		}

		public T Center()
		{
			T v = new T();
			for (int i = 0; i < p0.dimension; i++)
			{
				v[i] = (p0[i] + p1[i] + p2[i]) / 3;
			}
			return v;
		}

	}
}
