using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZooSimulation.Interfaces;
using ZooSimulation.Models;

namespace ZooSimulation
{
	// TODO: Consider Bridge pattern for Zoo and UI type
    public class ZooConsoleUserInterface : IUserInterface
	{
		public void Update(ZooConsoleUpdate status)
		{
			var records = status.Records.Select(record => "\t" + record);

			var message = status.Time + "\n" + 
				status.Title + "\n\n" + 
				string.Join("\n", records) + "\n";

			Console.WriteLine(message);
		}
	}
}
