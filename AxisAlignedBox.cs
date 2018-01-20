using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public struct AxisAlignedBox<T> where T : IVector, new()
	{
		public T center;
		public T extends;

		public T min
		{
			get
			{
				T nv = new T();
				for (int i = 0; i < nv.dimension; i++)
				{
					nv[i] = center[i] - Math.Abs(extends[i]);
				}
				return nv;
			}
		}
		public T max
		{
			get
			{
				T nv = new T();
				for (int i = 0; i < nv.dimension; i++)
				{
					nv[i] = center[i] + Math.Abs(extends[i]);
				}
				return nv;
			}
		}

		public AxisAlignedBox(T center, T extends)
		{
			this.center = center;
			this.extends = extends;
		}

	}
}
