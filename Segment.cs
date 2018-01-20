using System;

namespace MathematicsX
{
	public struct Segment<T> where T : IVector, new()
	{
		public T p0;
		public T p1;

		public T this[int index]
		{
			get
			{
				if (index == 0) return p0;
				else if (index == 1) return p1;
				else throw new Exception("The index is out of range!");
			}
			set
			{
				if (index == 0) p0 = value;
				else if (index == 1) p1 = value;
				else throw new Exception("The index is out of range!");
			}
		}

		public Segment(T p0, T p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		public void MinMax(out T min, out T max)
		{
			min = new T();
			for (int i = 0; i < p0.dimension; i++)
			{
				min[i] = p1[i] < min[i] ? p1[i] : p0[i];
			}
			max = new T();
			for (int i = 0; i < p0.dimension; i++)
			{
				max[i] = p1[i] >= max[i] ? p1[i] : p0[i];
			}
		}

		public T Center()
		{
			T v = new T();
			for (int i = 0; i < p0.dimension; i++)
			{
				v[i] = (p0[i] + p1[i]) / 2;
			}
			return v;
		}

	}
}
