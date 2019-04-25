using System;

namespace MathematicsX
{
	public struct Tetrahedron<T> where T : IVector, new()
	{
		public T p0;
		public T p1;
		public T p2;
		public T p3;

		public T this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return p0;
					case 1: return p1;
					case 2: return p2;
					case 3: return p3;
					default: throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0: p0 = value; break;
					case 1: p1 = value; break;
					case 2: p2 = value; break;
					case 3: p3 = value; break;
					default: throw new IndexOutOfRangeException();
				}
			}
		}

		public Tetrahedron(T p0, T p1, T p2, T p3)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
		}

		public void MinMax(out T min, out T max)
		{
			int dim = p0.Dimension;
			min = p0;
			for (int i = 0; i < dim; i++)
			{
				if (p1[i] < min[i]) min[i] = p1[i];
				if (p2[i] < min[i]) min[i] = p2[i];
				if (p3[i] < min[i]) min[i] = p3[i];
			}
			max = p0;
			for (int i = 0; i < dim; i++)
			{
				if (p1[i] >= max[i]) max[i] = p1[i];
				if (p2[i] >= max[i]) max[i] = p2[i];
				if (p3[i] >= max[i]) max[i] = p3[i];
			}
		}
		
		public void CenterRadius(out T center, out double radius)
		{
			center = new T();
			int dim = p0.Dimension;
			for (int i = 0; i < dim; i++)
			{
				center[i] = (p0[i] + p1[i] + p2[i] + p3[i]) / 4;
			}
			radius = VecX.Distance(center, p0);
			radius = Math.Max(radius, VecX.Distance(center, p1));
			radius = Math.Max(radius, VecX.Distance(center, p2));
			radius = Math.Max(radius, VecX.Distance(center, p3));
		}

	}
}
