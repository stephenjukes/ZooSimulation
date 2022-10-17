using System.Timers;
using ZooSimulation.Animals;
using ZooSimulation.Interfaces;
using ZooSimulation.Models;

namespace ZooSimulation
{
	public class ZooApp
	{
		private readonly Zoo _zoo;
		private readonly System.Timers.Timer _timer;
		private readonly IUserInterface _ui;

		public ZooApp(Zoo zoo, System.Timers.Timer timer, IUserInterface ui)
		{
			_zoo = zoo;
			_timer = timer;
			_ui = ui;
		}

		public void Run()
		{
			// Configure timer
			_timer.Elapsed += RoutineUpdate;
			_timer.Interval = 3000;
			_timer.AutoReset = true;
			_timer.Enabled = true;

			// Add animals
			_zoo.CreateAnimalsByName<Elephant>("Babar", "Dumbo", "Nellie", "Elmer", "Manny");
			_zoo.CreateAnimalsByName<Monkey>("Cheeta", "Nikko", "Betsy", "Louie", "Caesar");
			_zoo.CreateAnimalsByName<Giraffe>("Zarafa", "Nessa", "Melman", "Gerald", "Geoffrey");

			var keyFuncs = new Dictionary<ConsoleKey, Action>
			{
				{ ConsoleKey.F, () => FeedUpdate(_zoo.Time) },
				{ ConsoleKey.Q, () => Terminate() }
			};

			while (true)
			{
				var keyInfo = Console.ReadKey(intercept: true);

				var hasAction = keyFuncs.TryGetValue(keyInfo.Key, out var keyAction);
				if (hasAction && keyAction != null)
				{
					keyAction();
				}
			}
		}

		private void Terminate()
		{
			Console.WriteLine("Terminating the application...");

			_timer.Stop();
			_timer.Dispose();

			Environment.Exit(0);
		}

		private void RoutineUpdate(object source, ElapsedEventArgs ev)
		{
			var animals = _zoo.GetAnimals();
			var hasLiveAnimals = animals.Any(a => a.IsAlive());

			if (!hasLiveAnimals)
			{
				_ui.Update(new ZooConsoleUpdate
				{
					Time = _zoo.Time,
					Title = "Unfortunately, all your animals have died."
				});

				Terminate();
			}

			_zoo.ReduceHealth();

			var animalUpdates = animals.Select(a =>
			{
				var lifeStatus = a.IsAlive() ? "alive" : "dead";
				return $"{a.Name} the {a.GetType().Name} is {lifeStatus} with a health of {a.Health * 100:0.0}%";
			});

			_ui.Update(new ZooConsoleUpdate
			{
				Time = ev.SignalTime,
				Title = "Hourly Update\n(Press 'F' to feed you animals, or 'Q' to quit)",
				Records = animalUpdates
			});
		}

		private void FeedUpdate(DateTime time)
		{
			var nutritionByAnimalType = _zoo.FeedAnimals();

			var nutritionBreakDown = nutritionByAnimalType.Select(kvp => $"{kvp.Key}s were fed {kvp.Value} calories");

			_ui.Update(new ZooConsoleUpdate
			{
				Time = time,
				Title = "You fed the animals",
				Records = nutritionBreakDown
			});
		}
	}
}
