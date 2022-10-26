using System.ComponentModel.DataAnnotations;

namespace Commanders.Dtos
{
    public class CommandCreateDto
    { 
        public string HowTo { get; set; }

        public string Line { get; set; }

        public string Platform { get; set; }
    }
}
