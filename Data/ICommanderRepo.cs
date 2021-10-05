namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();
        
        System.Collections.Generic.IEnumerable<Commander.Models.Command> GetAllCommands();

        Commander.Models.Command GetCommandById(int id);

        void CreateCommand(Models.Command cmd);

        void UpdateCommand(Models.Command cmd);

        void DeleteCommand(Models.Command cmd);

    }//public interface ICommander
}//namespace Commander.Data