using System.ComponentModel.DataAnnotations;

namespace DirectorInformation.API.Models
{
    public class FilmForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value. ")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]

        public string? Description { get; set; }
    }
}
