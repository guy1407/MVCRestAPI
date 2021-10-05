using System.Collections.Generic;
using System.Linq;
using Commander.Models;
namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        //////////////////////////////

        public bool SaveChanges()
        {
            var x = _context.SaveChanges();

            return (x >= 0) ;
        }

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }
        public System.Collections.Generic.IEnumerable<Commander.Models.Command> GetAllCommands()
        {
            
            return _context.Commands.ToList();

        }//GetAllCommands

        public Commander.Models.Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }//GetCommandById

        public void CreateCommand(Models.Command cmd)
        {
            if (cmd == null) 
            {
                throw new System.ArgumentNullException(nameof(cmd));
            }
            else
            {
                _context.Commands.Add(cmd);
            }
        }

        public void UpdateCommand(Command cmd)
        {
            //Do Nothing :)
        }

        public void DeleteCommand(Command cmd)
        {
                        if (cmd == null) 
            {
                throw new System.ArgumentNullException(nameof(cmd));
            }
            else
            {
                _context.Commands.Remove(cmd);
            }
        }
    }//public class SqlCommanderRepo
}