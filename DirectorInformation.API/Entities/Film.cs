using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DirectorInformation.API.Entities
{
    public class Film
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [ForeignKey("DirectorId")]
        public Director? Director { get; set; }
        public int DirectorId { get; set; }

        public Film(string name)
        {
            Name = name;
        }

    }
}
