using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShpalljeOnline.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? UserID { get; set; }

        [Display(Name = "Roli")]
        [Required(ErrorMessage = "Roli duhet zgjedhur")]
        public long? RoleID { get; set; }

        public long? InfoID { get; set; }

        [Required(ErrorMessage = "Emri duhet plotsuar")]
        [MinLength(2,ErrorMessage = "Emri duhet të permbajë të paktën 2 shkronja")]
        [MaxLength(20, ErrorMessage = "Emri nuk mund të permbajë më shumë se 20 shkronja")]
        [Display(Name = "Emri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mbiemri duhet plotsuar")]
        [MinLength(2, ErrorMessage = "Mbiemri duhet të permbajë të paktën 2 shkronja")]
        [MaxLength(20, ErrorMessage = "Mbiemri nuk mund të permbajë më shumë se 20 shkronja")]
        [Display(Name = "Mbiemri")]
        public string Surname { get; set; }

        [Display(Name = "Fjalëkalimi")]
        public string Password { get; set; }

        public virtual Role Role { get; set; }

        public virtual Info Info { get; set; }

    }
}