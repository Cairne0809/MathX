using System;

namespace MathematicsX
{
	public struct Tetrahedron<T> where T : IVector
	{
		public T p0;
		public T p1;
		public T p2;
		public T p3;

		public T this[int index]
		{
			get
			{
				if (index == 0) return p0;
				else if (index == 1) return p1;
				else if (index == 2) return p2;
				else if (index == 3) return p3;
				else throw new Exception("The index is out of range!");
			}
			set
			{
				if (index == 0) p0 = value;
				else if (index == 1) p1 = value;
				else if (index == 2) p2 = value;
				else if (index == 3) p3 = value;
				else throw new Exception("The index is out of range!");
			}
		}

		public Tetrahedron(T p0, T p1, T p2, T p3)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
		}
	}
}
