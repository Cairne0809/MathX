using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public interface IVector
	{
		double this[int index] { get; set; }
		int dimension { get; }
	}
}
