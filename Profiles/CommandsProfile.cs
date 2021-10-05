namespace Commander.Profiles 
{

    public class CommandsProfile : AutoMapper.Profile
    {
        public CommandsProfile()
        {
            //Source -> Target
            CreateMap<Commander.Models.Command, Commander.Dtos.CommandReadDto>();

            CreateMap<Commander.Dtos.CommandCreateDto, Commander.Models.Command>();

            CreateMap<Commander.Dtos.CommandUpdateDto, Commander.Models.Command>();

            CreateMap<Commander.Models.Command, Commander.Dtos.CommandUpdateDto>();
        }

    }//public class CommandsProfile

}//namespace Commander.Profiles 