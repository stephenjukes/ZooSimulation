using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Interfaces;

namespace ZooSimulation
{
	public class RandomNumberGenerator : IRandomNumberGenerator
	{
		private readonly Random _random = new();

		public int Next(int max)
		{
			return _random.Next(max);
		}

		public int Next(int min, int max)
		{
			return _random.Next(min, max);
		}
	}
}
