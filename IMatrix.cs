using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsX
{
	public interface IMatrix
	{
		double this[int c, int r] { get; set; }
		int column { get; }
		int row { get; }
		bool isNaM { get; }
	}
}
