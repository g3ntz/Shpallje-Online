using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ShpalljeOnline.CustomValidations;

namespace ShpalljeOnline.ViewModels
{
    public class RegisterViewModel
    {
        public long? UserID { get; set; }

        [Required(ErrorMessage = "Emri duhet plotsuar")]
        [MinLength(2, ErrorMessage = "Emri duhet të permbajë të paktën 2 shkronja")]
        [MaxLength(20, ErrorMessage = "Emri nuk mund të permbajë më shumë se 20 shkronja")]
        [Display(Name = "Emri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mbiemri duhet plotsuar")]
        [MinLength(2, ErrorMessage = "Mbiemri duhet të permbajë të paktën 2 shkronja")]
        [MaxLength(20, ErrorMessage = "Mbiemri nuk mund të permbajë më shumë se 20 shkronja")]
        [Display(Name = "Mbiemri")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Emaili duhet plotsuar")]
        [MinLength(3, ErrorMessage = "Emaili duhet të permbajë të paktën 3 shkronja")]
        [MaxLength(100, ErrorMessage = "Emaili nuk mund të permbajë më shumë se 100 shkronja")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Emaili nuk eshte valid")]
        [EmailExist(ErrorMessage = "Emaili egziston, ju lutem provoni tjeter email")]
        [Display(Name = "Email Adresa")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fjalekalimi duhet plotsuar")]
        [MinLength(6, ErrorMessage = "Fjalekalimi duhet të permbajë të paktën 6 shkronja")]
        [MaxLength(100, ErrorMessage = "Fjalekalimi nuk mund të permbajë më shumë se 100 shkronja")]
        [Display(Name = "Fjalëkalimi")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Fjalekalimi duhet konfirmuar")]
        [Compare("Password", ErrorMessage = "Fjalekalimi i konfirmuar nuk perputhet")]
        [Display(Name = "Konfirmo Fjalëkalimin")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Numri i telefonit duhet plotsuar")]
        [MinLength(8, ErrorMessage = "Numri i telefonit duhet të permbajë të paktën 8 numra")]
        [MaxLength(11, ErrorMessage = "Numri i telefonit nuk mund të permbajë më shumë se 11 numra")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Numri i telefonit nuk eshte valid, lejohen vetem numra")]
        [Display(Name = "Telefoni")]
        public string PhoneNr { get; set; }
    }
}