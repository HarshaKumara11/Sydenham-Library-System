using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sydenham_Library_System.Models
{
    public class GENRES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Genre")]
        [StringLength(50)]
        public required string GENRENAME { get; set; }
    }
}
