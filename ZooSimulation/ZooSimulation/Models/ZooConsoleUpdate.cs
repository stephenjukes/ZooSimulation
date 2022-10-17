using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooSimulation.Animals;
using ZooSimulation.Interfaces;

namespace ZooSimulation.Models
{
    public class ZooConsoleUpdate : IUpdate
    {
        public DateTime Time { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> Records { get; set; } = Enumerable.Empty<string>();
    }
}
