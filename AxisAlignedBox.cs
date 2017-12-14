using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public struct AxisAlignedBox<T> where T : IVector
	{
		public T center;
		public T extends;

		public T max
		{
			get
			{
				T nv = Activator.CreateInstance<T>();
				nv.dimension = extends.dimension;
				for (int i = 0; i < nv.dimension; i++)
				{
					nv[i] = center[i] + Math.Abs(extends[i]);
				}
				return nv;
			}
		}
		public T min
		{
			get
			{
				T nv = Activator.CreateInstance<T>();
				nv.dimension = extends.dimension;
				for (int i = 0; i < nv.dimension; i++)
				{
					nv[i] = center[i] - Math.Abs(extends[i]);
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
