using System.Collections.ObjectModel;
using ZooSimulation.Animals;
using ZooSimulation.Interfaces;

namespace ZooSimulation
{
	public class Zoo
	{
		private readonly IAnimalFactory _animalFactory;
		private readonly IClock _clock;
		private readonly INutritionProvider _nutritionProvider;
		private readonly List<Animal> _animals;

		public Zoo(
			IAnimalFactory animalFactory,
			IClock clock,
			INutritionProvider nutritionProvider
			)
		{
			_animalFactory = animalFactory;
			_clock = clock;
			_nutritionProvider = nutritionProvider;
			_animals = new List<Animal>();
		}

		public DateTime Time { get => _clock.Time; }

		public IEnumerable<Animal> CreateAnimalsByName<TAnimal>(params string[] names) where TAnimal : Animal
		{
			var animals = names
				.Select(name => _animalFactory.CreateAnimal<TAnimal>(name));

			_animals.AddRange(animals);

			return animals;
		}

		public ReadOnlyCollection<Animal> GetAnimals()
		{
			return _animals.AsReadOnly();
		}

		public Dictionary<string, int> FeedAnimals()
		{
			var liveAnimalByType = _animals.GroupBy(a => a.GetType());
			var nutritionByAnimalType = new Dictionary<string, int>();

			foreach (var animalType in liveAnimalByType)
			{
				var nutrition = _nutritionProvider.GetNutrition();
				nutritionByAnimalType[animalType.Key.Name] = nutrition;

				Feed(animalType.First(), nutrition);
			}

			return nutritionByAnimalType;
		}

		public void Feed<TAnimal>(TAnimal animalSample, int nutrition)
		{
			var animalType = animalSample?.GetType();

			var animals = _animals.Where(animal => animal.GetType() == animalType);

			foreach (var animal in animals)
			{
				animal.Consume(nutrition);
			}
		}

		public void ReduceHealth()
		{
			foreach (var animal in _animals)
			{
				animal.Exert();
			}
		}
	}
}
