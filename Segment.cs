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
				if (index == 0) return p0;
				else if (index == 1) return p1;
				else throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0) p0 = value;
				else if (index == 1) p1 = value;
				else throw new IndexOutOfRangeException();
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
			min = p0;
			for (int i = 0; i < dim; i++)
			{
				if (p1[i] < min[i]) min[i] = p1[i];
			}
			max = p0;
			for (int i = 0; i < dim; i++)
			{
				if (p1[i] >= max[i]) max[i] = p1[i];
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
