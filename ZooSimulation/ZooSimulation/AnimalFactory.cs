using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Animals;
using ZooSimulation.Interfaces;

namespace ZooSimulation
{
	public class AnimalFactory : IAnimalFactory
	{
		public Animal CreateAnimal<TAnimal>(string name)
		{
			var randomNumberGenerator = new RandomNumberGenerator();
			var animalType = typeof(TAnimal);

			if (animalType == typeof(Elephant))
			{
				return new Elephant(name, randomNumberGenerator);
			}

			if (animalType == typeof(Monkey))
			{
				return new Monkey(name, randomNumberGenerator);
			}

			if (animalType == typeof(Giraffe))
			{
				return new Giraffe(name, randomNumberGenerator);
			}

			throw new ArgumentException(
				$"Animal '{animalType.Name}' was not recognized and could not be created");
		}

	}
}
