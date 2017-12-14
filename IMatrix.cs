using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public interface IMatrix
	{
		double this[int r, int c] { get; set; }
		int row { get; }
		int column { get; }
		bool isNaM { get; }
	}
}
