using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShpalljeOnline.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? PostID { get; set; }
        public long? UserID { get; set; }

        [Required(ErrorMessage = "Lokacioni duhet zgjedhur")]
        [Display(Name = "Lokacioni")]
        public long? LocationID { get; set; }

        [Required(ErrorMessage = "Kategoria duhet zgjedhur")]
        [Display(Name = "Kategoria")]
        public long? CategoryID { get; set; }

        public long? InfoID { get; set; }

        [Required(ErrorMessage = "Titulli duhet plotsuar")]
        [MinLength(5, ErrorMessage = "Titulli duhet të permbajë të paktën 5 shkronja")]
        [MaxLength(70, ErrorMessage = "Titulli nuk mund të permbajë më shumë se 70 shkronja")]
        [Display(Name = "Titulli")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Permbajtja duhet plotsuar")]
        [MinLength(20, ErrorMessage = "Permbajtja duhet të permbajë të paktën 20 shkronja")]
        [MaxLength(2500, ErrorMessage = "Permbajtja nuk mund të permbajë më shumë se 2500 shkronja")]
        [Display(Name = "Permbajtja")]
        public string Description { get; set; }
        public DateTime? Date { get; set; }

        [Display(Name = "Fotoja")]
        public string Photo { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual Location Location { get; set; }
        public virtual Category Category { get; set; }
        public virtual Info Info { get; set; }
    }
}