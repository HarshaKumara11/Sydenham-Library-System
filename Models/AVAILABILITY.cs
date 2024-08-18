using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sydenham_Library_System.Models
{
    public class AVAILABILITY
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Status")]
        [StringLength(50)]
        public required string STATUS { get; set; }
    }
}
