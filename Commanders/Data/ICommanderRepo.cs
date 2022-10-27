using Commander.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commanders.Data
{
    public interface ICommanderRepo
    {
        Task SaveChanges();

        Task<IEnumerable<Command>> GetAllCommands();
        Task<Command> GetCommandById(int id);
        Task CreateCommand(Command cmd);
        Task UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}
