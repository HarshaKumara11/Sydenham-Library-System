using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sydenham_Library_System.Models
{
    public class MESSAGES
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Display(Name ="From")] 
        public required string From { get; set; }

        [MaxLength(15)]
        [Phone]
        [Display(Name ="Phone Number")]
        public required string Phone { get; set; }

        [MaxLength(255)]
        [Display(Name = "Subject")]
        public required string Subject { get; set; }

        [Display(Name = "Message")]
        public string? Msgbody { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "UNREAD";  // Default status is 'unread'

        [Display(Name = "Date Created")]
        public DateTime Createddate { get; set; } = DateTime.Now;

        [Display(Name = "Date Read")]
        public DateTime? Readdate { get; set; }
    }
}
