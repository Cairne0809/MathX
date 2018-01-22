using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public interface IMatrix
	{
		double this[int index] { get; set; }
		double this[int row, int column] { get; set; }
	}
}
