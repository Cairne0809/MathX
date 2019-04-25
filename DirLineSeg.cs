using System;

namespace MathematicsX
{
	public struct DirLineSeg<T> where T : IVector, new()
	{
		public T p;
		public T d;

		public DirLineSeg(T p, T d)
		{
			this.p = p;
			this.d = d;
		}

		public void MinMax(out T min, out T max)
		{
			int dim = p.Dimension;
			min = p;
			for (int i = 0; i < dim; i++)
			{
				if (d[i] < 0) min[i] = p[i] + d[i];
			}
			max = p;
			for (int i = 0; i < dim; i++)
			{
				if (d[i] > 0) max[i] = p[i] + d[i];
			}
		}

		public void CenterRadius(out T center, out double radius)
		{
			center = new T();
			int dim = p.Dimension;
			for (int i = 0; i < dim; i++)
			{
				center[i] = p[i] + d[i] * 0.5;
			}
			radius = VecX.Distance(center, p);
		}

		public static explicit operator DirLineSeg<T>(LineSeg<T> v)
		{
			return new DirLineSeg<T>(v.p0, VecX.Sub(v.p1, v.p0));
		}

	}
}
