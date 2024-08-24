using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sydenham_Library_System.Models
{
    public class RESERVATIONS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("PRODUCTS")]
        [Display(Name = "Reserved Item")]
        public required int Prodid { get; set; }

        [MaxLength(255)]
        [Display(Name = "Reserved By")]
        public required string Reservedby { get; set; }

        [MaxLength(15)]
        [Phone]
        [Display(Name = "Phone Number")]
        public required string Reservedbyphone { get; set; }

        [Display(Name = "Reserved Date")]
        public DateTime Createddate { get; set; } = DateTime.Now;

        [Display(Name = "Due Date")]
        public DateTime Duedate { get; set; }

        public PRODUCTS? PRODUCTS { get; set; }
    }
}
