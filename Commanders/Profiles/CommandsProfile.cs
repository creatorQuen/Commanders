using AutoMapper;
using Commander.Models;
using Commanders.Dtos;

namespace Commanders.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
        }
    }
}
