using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShpalljeOnline.CustomValidations;

namespace ShpalljeOnline.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? CategoryID { get; set; }

        [Required(ErrorMessage = "Kategoria duhet plotsuar")]
        [MinLength(2,ErrorMessage = "Kategoria duhet të permbajë të paktën 2 shkronja")]
        [MaxLength(25, ErrorMessage = "Kategoria nuk mund të permbajë më shumë se 25 shkronja")]
        [CategoryExist(ErrorMessage = "Kategoria egziston, ju lutem shkruani nje kategori tjeter")]
        [Display(Name = "Emri i Kategorise")]
        public string CategoryName { get; set; }
    }
}