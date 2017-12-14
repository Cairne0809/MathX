using System;

namespace MathematicsX
{
	public struct Segment<T> where T : IVector
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
	}
}
