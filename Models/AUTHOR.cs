using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sydenham_Library_System.Models
{
    public class AUTHOR
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Author Name")]
        [StringLength(255)]
        public required string AUTHORNAME { get; set; }
    }
}
