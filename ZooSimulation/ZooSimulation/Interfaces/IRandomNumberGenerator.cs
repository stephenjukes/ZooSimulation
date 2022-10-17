using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooSimulation.Interfaces
{
	public interface IRandomNumberGenerator
	{
		int Next(int max);

		int Next(int min, int max);
	}
}
