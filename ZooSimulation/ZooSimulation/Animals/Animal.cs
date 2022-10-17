using ZooSimulation.Interfaces;
using ZooSimulation.Utilities;

namespace ZooSimulation.Animals
{
    public abstract class Animal
    {
        public readonly IRandomNumberGenerator _randomNumberGenerator;

        public Animal(string name, IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
            Name = name;
        }

        protected abstract float LifeThreshold { get; }

        public string Name { get; }

        public float Health { get; private set; } = 1;

		public virtual bool IsAlive()
        {
            return Health >= LifeThreshold;
        }

        public virtual void Consume(float nutrition)
        {
            if (IsAlive())
            {
				var newHealth = Health + (Health * nutrition.AsPercent());
				Health = Math.Min(newHealth, 1);
			}
        }

        public virtual void Exert()
        {
            if (IsAlive())
            {
				float exertion = _randomNumberGenerator.Next(20);
				Health -= (Health * exertion.AsPercent());
			}
        }
    }
}
