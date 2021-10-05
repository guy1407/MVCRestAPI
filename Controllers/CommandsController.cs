using Microsoft.AspNetCore.Mvc;

namespace Commander.Contollers
{

    //api/commands
    //[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.Route("api/commands")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class CommandsController : Microsoft.AspNetCore.Mvc.ControllerBase
    {

        //private readonly Commander.Data.MockCommanderRepo _repository = new Commander.Data.MockCommanderRepo();

        private readonly Commander.Data.ICommanderRepo _repository;

        private readonly AutoMapper.IMapper _mapper;

        public CommandsController(Commander.Data.ICommanderRepo repository, AutoMapper.IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }
        

        //get request api/commands
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IEnumerable<Commander.Dtos.CommandReadDto>> GetAllCommands()
        {

            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<System.Collections.Generic.IEnumerable<Commander.Dtos.CommandReadDto>>(commandItems));

        }//GetAllCommands

        //GET api/commands/{id}
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}", Name="GetCommandById")]
        public Microsoft.AspNetCore.Mvc.ActionResult <Commander.Dtos.CommandReadDto> GetCommandById(int id) 
        {

            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null) 
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine("Command item found!");
                }
                return Ok(_mapper.Map<Commander.Dtos.CommandReadDto>(commandItem));
            }
            else 
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine("Command item *NOT* found!");
                }

                return NotFound();
            }
            
        }//GetCommandById

        //POST api/commands
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Microsoft.AspNetCore.Mvc.ActionResult <Dtos.CommandReadDto> CreateCommand(Dtos.CommandCreateDto commandCreateDto)
        {
            //should actually verify that the object is ok            
            var commandModel = _mapper.Map<Models.Command>(commandCreateDto);

            _repository.CreateCommand(commandModel);

            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<Commander.Dtos.CommandReadDto>(commandModel);

            var routeValues = new {Id = commandReadDto.Id};

            var content = commandReadDto;
            
            return CreatedAtRoute(nameof(GetCommandById), routeValues, content);
            //return Ok(commandReadDto);

        }//CreateCommand

        //PUT api/commands{id}        
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public Microsoft.AspNetCore.Mvc.ActionResult UpdateCommand(int id, Dtos.CommandUpdateDto commandUpdateDto)
        {

            //First step, verify it exists in the database:
            var commandModelFromRepo = _repository.GetCommandById(id);

            if (commandModelFromRepo == null) 
            {
                return NotFound();
            }
            else
            {
                _mapper.Map(commandUpdateDto, commandModelFromRepo);

                _repository.UpdateCommand(commandModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
            }

            //return Ok(commandReadDto);

        }//UpdateCommand


        //PATCH api/commands/{id}
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
        public Microsoft.AspNetCore.Mvc.ActionResult PartialCommandUpdate(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<Commander.Dtos.CommandUpdateDto> patchDoc)
        {

            //First step, verify it exists in the database:
            var commandModelFromRepo = _repository.GetCommandById(id);

            if (commandModelFromRepo == null) 
            {
                return NotFound();
            }
            else
            {
                var commandToPatch = _mapper.Map<Commander.Dtos.CommandUpdateDto>(commandModelFromRepo);

                patchDoc.ApplyTo(commandToPatch, ModelState);

                if (!TryValidateModel(commandToPatch)) {
                    return ValidationProblem(ModelState);
                }
                else
                {
                    _mapper.Map(commandToPatch, commandModelFromRepo);

                    _repository.UpdateCommand(commandModelFromRepo);

                    _repository.SaveChanges();

                    return NoContent();
                }
            }

        }//PartialCommandUpdate

        //DELETE api/commands/{id}

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {

            //First step, verify it exists in the database:
            var commandModelFromRepo = _repository.GetCommandById(id);

            if (commandModelFromRepo == null) 
            {
                return NotFound();
            }
            else
            {
                _repository.DeleteCommand(commandModelFromRepo);

                _repository.SaveChanges();

                return NoContent();
            }

        }//DeleteCommand
    }//public class CommandsController

}//namespace Commander.Contollers