using ZooSimulation.Interfaces;

namespace ZooSimulation.Animals
{
	public class Elephant : Animal
	{
		public Elephant(string name, IRandomNumberGenerator randomNumberGenerator) : base(name, randomNumberGenerator)
		{
		}

		protected override float LifeThreshold { get; } = 0.7F;

		public int HoursRequiringFeeding { get; private set; } = 0;

		public override bool IsAlive()
		{
			return HoursRequiringFeeding <= 1;
		}

		public override void Consume(float nutrition)
		{
			base.Consume(nutrition);

			if (Health >= LifeThreshold)
			{
				HoursRequiringFeeding = 0;
			}
		}

		public override void Exert()
		{
			base.Exert();

			if (Health < LifeThreshold)
			{
				HoursRequiringFeeding++;
			}
		}
	}
}
