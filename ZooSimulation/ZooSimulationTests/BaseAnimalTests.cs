using FluentAssertions;
using Moq;
using ZooSimulation.Animals;
using ZooSimulation.Interfaces;

namespace ZooSimulationTests
{
	public class BaseAnimalTests
	{
		private readonly Mock<IRandomNumberGenerator> _mockRandomNumberGenerator;
		private readonly Animal _animal;
		private readonly float _acceptedPrecision;


		public BaseAnimalTests()
		{
			_mockRandomNumberGenerator = new Mock<IRandomNumberGenerator>();
			_animal = new TestAnimal(_mockRandomNumberGenerator.Object);
			_acceptedPrecision = 0.0000001F;
		}

		[Fact]
		public void Animals_AreInitialized_AtFullHealth()
		{
			// Assert
			_animal.Health.Should().Be(1);
		}

		[Fact]
		public void Animals_AreInitialized_AsAlive()
		{
			// Assert
			_animal.IsAlive().Should().BeTrue();
		}

		[Theory]
		[InlineData(0, 1)]
		[InlineData(30, .7)]
		[InlineData(95, .05)]
		[InlineData(100, 0)]
		public void OnExert_AnimalHealthIsReduced_ByExertionAsAPercentageOfHealth(int exertion, float expectedHealth)
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(exertion);

			// Act
			_animal.Exert();

			// Assert
			_animal.Health.Should().BeApproximately(expectedHealth, _acceptedPrecision);
		}

		[Theory]
		[InlineData(0, 1)]
		[InlineData(10, .81)]
		[InlineData(20, .64)]
		public void OnMultipleExert_AnimalHealthIsReduced_EachTimeAsPercentageOfHealth(int exertion, float expectedHealth)
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(exertion);

			// Act
			_animal.Exert();
			_animal.Exert();

			// Assert
			_animal.Health.Should().BeApproximately(expectedHealth, _acceptedPrecision);
		}

		[Theory]
		[InlineData(0, 1)]
		[InlineData(30, .7)]
		[InlineData(50, .5)]
		public void WhenHealthIsAtLeastLifeThreshold_AnimalIsAlive(int exertion, float expectedHealth)
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(exertion);

			_animal.Exert();

			// Assert
			_animal.Health.Should().BeApproximately(expectedHealth, _acceptedPrecision);
			_animal.IsAlive().Should().BeTrue();
		}

		[Theory]
		[InlineData(51, .49)]
		[InlineData(75, .25)]
		[InlineData(100, 0)]
		public void WhenHealthIsBelowLifeThreshold_AnimalIsNotAlive(int exertion, float expectedHealth)
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(exertion);

			_animal.Exert();

			// Assert
			_animal.Health.Should().BeApproximately(expectedHealth, _acceptedPrecision);
			_animal.IsAlive().Should().BeFalse();
		}


		[Theory]
		[InlineData(50, .5, 100, 1)]
		[InlineData(50, .5, 50, .75)]
		[InlineData(20, .8, 25, 1)]
		public void Consume_AddsNutrition_AsPercentageOfHealth(int exertion, float expectedPreConsumptionHealth, int nutrition, float expectedHealth)
		{
			// Arramge
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(exertion);

			_animal.Exert();
			var preConsumptionHealth = _animal.Health;

			// Act
			_animal.Consume(nutrition);

			// Assert
			preConsumptionHealth.Should().Be(expectedPreConsumptionHealth);
			_animal.Health.Should().Be(expectedHealth);
		}

		[Theory]
		[InlineData(0, 1, 50)]
		[InlineData(50, .5, 200)]
		public void Health_IsCappedAtOneHundredPercent(int exertion, float expectedPreConsumptionHealth, int nutrition)
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(exertion);

			_animal.Exert();
			var preConsumptionHealth = _animal.Health;

			// Act
			_animal.Consume(nutrition);

			// Assert
			preConsumptionHealth.Should().Be(expectedPreConsumptionHealth);
			_animal.Health.Should().Be(1);
		}

		[Fact]
		public void WhenAnAnimalIsDead_ItCannotRevive()
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(51);

			_animal.Exert();

			// Act
			_animal.Consume(100);

			// Assert
			_animal.Health.Should().Be(.49F);
			_animal.IsAlive().Should().BeFalse();
		}

		[Fact]
		public void WhenAnAnimalIsDead_ItCannotExert()
		{
			// Arrange
			_mockRandomNumberGenerator
				.Setup(g => g.Next(It.IsAny<int>()))
				.Returns(51);

			_animal.Exert();

			// Act
			_animal.Exert();

			// Assert
			_animal.Health.Should().Be(.49F);
			_animal.IsAlive().Should().BeFalse();
		}
	}

	public class TestAnimal : Animal
	{
		public TestAnimal(IRandomNumberGenerator randomNumberGenerator) : base(string.Empty, randomNumberGenerator)
		{
		}

		protected override float LifeThreshold { get; } = .5F;
	}
}
