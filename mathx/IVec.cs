using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mathx
{
	public interface IVec
	{
		double this[int index] { get; set; }
		int dimension { get; }
		bool isNaN { get; }
		double sqrMagnitude { get; }
		double magnitude { get; }
	}
}
