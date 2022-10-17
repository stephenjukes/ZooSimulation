using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Animals;

namespace ZooSimulation.Interfaces
{
	public interface IAnimalFactory
	{
		Animal CreateAnimal<TAnimal>(string name);
	}
}
