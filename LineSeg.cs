using System;

namespace MathematicsX
{
	public struct LineSeg<T> where T : IVector, new()
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

		public LineSeg(T p0, T p1)
		{
			this.p0 = p0;
			this.p1 = p1;
		}

		public void MinMax(out T min, out T max)
		{
			int dim = p0.Dimension;
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
			int dim = p0.Dimension;
			for (int i = 0; i < dim; i++)
			{
				center[i] = (p0[i] + p1[i]) / 2;
			}
			radius = VecX.Distance(center, p0);
		}

		public static explicit operator LineSeg<T>(DirLineSeg<T> v)
		{
			return new LineSeg<T>(v.p, VecX.Add(v.p, v.d));
		}

	}
}
