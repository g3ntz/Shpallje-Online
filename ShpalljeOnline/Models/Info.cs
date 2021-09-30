using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShpalljeOnline.CustomValidations;

namespace ShpalljeOnline.Models
{
    [Table("Infos")]
    public class Info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? InfoID { get; set; }

        [Required(ErrorMessage = "Emaili duhet plotsuar")]
        [MinLength(3, ErrorMessage = "Emaili duhet të permbajë të paktën 3 shkronja")]
        [MaxLength(100, ErrorMessage = "Emaili nuk mund të permbajë më shumë se 100 shkronja")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",ErrorMessage = "Emaili nuk eshte valid")]
        [Display(Name = "Email Adresa")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefoni duhet plotsuar")]
        [MinLength(8, ErrorMessage = "Numri i telefonit duhet të permbajë të paktën 8 numra")]
        [MaxLength(11, ErrorMessage = "Numri i telefonit nuk mund të permbajë më shumë se 11 numra")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numri i telefonit nuk eshte valid, lejohen vetem numra")]
        [Display(Name = "Telefoni")]
        public string PhoneNr { get; set; }
    }
}