using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Interfaces;

namespace ZooSimulation.Clocks
{
	public class RealClock : IClock
	{
		public DateTime Time => DateTime.Now;
	}
}
