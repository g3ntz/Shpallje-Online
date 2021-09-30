using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShpalljeOnline.CustomValidations;

namespace ShpalljeOnline.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? LocationID { get; set; }

        [Required(ErrorMessage = "Lokacioni duhet plotsuar")]
        [MinLength(2, ErrorMessage = "Lokacioni duhet të permbajë të paktën 2 shkronja")]
        [MaxLength(17, ErrorMessage = "Lokacioni nuk mund të permbajë më shumë se 17 shkronja")]
        [LocationExist(ErrorMessage = "Lokacioni egziston, ju lutem shkruani nje lokacion tjeter")]
        [Display(Name = "Emri i Lokacionit")]
        public string LocationName { get; set; }
    }
}