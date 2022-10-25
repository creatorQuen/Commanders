using Commander.Models;
using System.Collections.Generic;

namespace Commanders.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
    }
}
