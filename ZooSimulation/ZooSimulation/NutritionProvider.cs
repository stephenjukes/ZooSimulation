using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Interfaces;
using ZooSimulation.Utilities;

namespace ZooSimulation
{
	public class NutritionProvider : INutritionProvider
	{
		private readonly IRandomNumberGenerator _randomNumberGenerator;

		public NutritionProvider(IRandomNumberGenerator randomNumberGenerator)
		{
			_randomNumberGenerator = randomNumberGenerator;
		}

		public int GetNutrition()
		{
			var nutrition = _randomNumberGenerator.Next(10, 25);

			return nutrition;
		}
	}
}
