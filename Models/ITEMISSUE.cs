using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sydenham_Library_System.Models
{
    public class ITEMISSUE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("PRODUCTS")]
        [Display(Name = "Issued Item")]
        public required int Prodid { get; set; }

        [MaxLength(255)]
        [Display(Name = "Issued To")]
        public required string Issuedto { get; set; }

        [MaxLength(15)]
        [Phone]
        [Display(Name = "Phone Number")]
        public required string Phone { get; set; }

        [Display(Name = "Issued Date")]
        public DateTime Createddate { get; set; } = DateTime.Now;

        [Display(Name = "Due Date")]
        public DateTime Duedate { get; set; }

        [Display(Name = "Returned Date")]
        public DateTime? Returndate { get; set; }

        [ForeignKey("Availability")]
        [Display(Name = "Status")]
        public int Status { get; set; }

        public PRODUCTS? PRODUCTS { get; set; }
        public AVAILABILITY? Availability { get; set; }
    }
}
