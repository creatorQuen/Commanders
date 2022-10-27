using Commander.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Commanders.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public async Task CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            await _context.Commands.AddAsync(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Remove(cmd);
        }

        public async Task<IEnumerable<Command>> GetAllCommands()
        {
            return await _context.Commands!.ToListAsync();
        }

        public async Task<Command> GetCommandById(int id)
        {
            return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommand(Command cmd)
        {
            //Nothing
        }
    }
}
