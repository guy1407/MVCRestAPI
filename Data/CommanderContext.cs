namespace Commander.Data
{

    public class CommanderContext : Microsoft.EntityFrameworkCore.DbContext
    {

        public CommanderContext(Microsoft.EntityFrameworkCore.DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }//public CommanderContext

        public Microsoft.EntityFrameworkCore.DbSet<Commander.Models.Command> Commands { get; set; }

    }//public class CommanderContext

}//namespace Commander.Data