using AutoMapper;
using Commander.Models;
using Commanders.Data;
using Commanders.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commanders.Controllers
{
    //api/commands
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        //GET api/commands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands()
        {
            var commandItems =  await _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {
            var commandItem = await _repository.GetCommandById(id);
            if(commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            await _repository.CreateCommand(commandModel);
            await _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id , CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = await _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            await _repository.UpdateCommand(commandModelFromRepo);
            await _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = await _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            await _repository.UpdateCommand(commandModelFromRepo);

            await _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var commandModelFromRepo = await _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            await _repository.SaveChanges();

            return NoContent();
        }
    }
}
