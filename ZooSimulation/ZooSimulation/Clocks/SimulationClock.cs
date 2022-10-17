using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Interfaces;

namespace ZooSimulation.Clocks
{
    public class SimulationClock : IClock
    {
        private readonly DateTime _startTime;

        public SimulationClock()
        {
            _startTime = DateTime.Now;
        }

        public DateTime Time
        {
            get 
            {
				var elapsed = DateTime.Now - _startTime;
				return DateTime.Today + elapsed;
			}
        }
    }
}
