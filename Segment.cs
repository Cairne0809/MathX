using System;

namespace MathematicsX
{
	public struct Segment<T> where T : struct, IVector
	{
		public T p0;
		public T p1;

		public T this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return p0;
					case 1: return p1;
					default: throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0: p0 = value; break;
					case 1: p1 = value; break;
					default: throw new IndexOutOfRangeException();
				}
			}
		}

		public Segment(T p0, T p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		public void MinMax(out T min, out T max)
		{
			int dim = p0.dimension;
			min = new T();
			for (int i = 0; i < dim; i++)
			{
				min[i] = p1[i] < min[i] ? p1[i] : p0[i];
			}
			max = new T();
			for (int i = 0; i < dim; i++)
			{
				max[i] = p1[i] >= max[i] ? p1[i] : p0[i];
			}
		}

		public void CenterRadius(out T center, out double radius)
		{
			center = new T();
			int dim = p0.dimension;
			for (int i = 0; i < dim; i++)
			{
				center[i] = (p0[i] + p1[i]) / 2;
			}
			radius = VecX.Distance(center, p0);
		}

	}
}
