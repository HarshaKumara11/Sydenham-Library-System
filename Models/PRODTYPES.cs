using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sydenham_Library_System.Models
{
    public class PRODTYPES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name ="Product Type")]
        [StringLength(10)]
        public required string PRODTYPE { get; set; }

        [Display(Name = "Description")]
        [StringLength(50)]
        public string? DESCRIPTION {  get; set; }

    }
}
