using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ShpalljeOnline.CustomValidations;

namespace ShpalljeOnline.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Emaili duhet plotsuar")]
        [MinLength(3, ErrorMessage = "Emaili duhet të permbajë të paktën 3 shkronja")]
        [MaxLength(100, ErrorMessage = "Emaili nuk mund të permbajë më shumë se 100 shkronja")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Emaili nuk eshte valid")]
        [Display(Name = "Email Adresa")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fjalekalimi duhet plotsuar")]
        [MinLength(6, ErrorMessage = "Fjalekalimi duhet të permbajë të paktën 6 shkronja")]
        [MaxLength(100, ErrorMessage = "Fjalekalimi nuk mund të permbajë më shumë se 100 shkronja")]
        [Display(Name = "Fjalekalimi")]
        public string Password { get; set; }
    }
}