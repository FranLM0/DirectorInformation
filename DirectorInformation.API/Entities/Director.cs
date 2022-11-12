using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DirectorInformation.API.Entities;

namespace DirectorInformation.API.Entities
{
    public class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<Film> Films { get; set; }
            = new List<Film>();


        public Director(string name)
        {
            Name = name;
        }



    }
}
