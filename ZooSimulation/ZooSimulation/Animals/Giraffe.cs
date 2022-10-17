using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Interfaces;

namespace ZooSimulation.Animals
{
	public class Giraffe : Animal
	{
		public Giraffe(string name, IRandomNumberGenerator randomNumberGenerator) : base(name, randomNumberGenerator)
		{
		}

		protected override float LifeThreshold { get; } = .5F;
	}
}
