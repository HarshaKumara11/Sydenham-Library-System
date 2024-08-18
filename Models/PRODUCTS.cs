using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sydenham_Library_System.Models
{
    public class PRODUCTS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name ="Product ID")]
        [StringLength(10)]
        public required string PRODID { get; set; }

        [Display(Name = "Title")]
        [StringLength(255)]
        public required string TITLE { get; set; }

        [ForeignKey("AUTHORS")]
        [Display(Name = "Author")]
        public int AUTHOR {  get; set; }

        [ForeignKey("PRODTYPE")]
        [Display(Name = "Product Type")]
        public int PRODTYPES { get; set; }

        [ForeignKey("GENRE")]
        [Display(Name = "Genre")]
        public int GENRES { get; set; }

        [ForeignKey("Availability")]
        [Display(Name = "Availability")]
        public int AVAILABILITY { get; set; }


        public AUTHOR? AUTHORS { get; set; }
        public PRODTYPES? PRODTYPE { get; set; }
        public GENRES? GENRE {  get; set; }
        public AVAILABILITY? Availability { get; set; }


    }
}
