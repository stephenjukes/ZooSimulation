using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ZooSimulation.Models;

namespace ZooSimulation.Interfaces
{
    public interface IUserInterface
    {
        void Update(ZooConsoleUpdate status); // TODO: Can this be made generic?
    }
}
