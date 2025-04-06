using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputersAPI.Dtos.Computers
{
    public class ComputerDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
    }
}   
