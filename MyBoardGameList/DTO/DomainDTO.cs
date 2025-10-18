using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBoardGameList.DTO
{
    public class DomainDTO
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
