using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ShpalljeOnline.Models;

namespace ShpalljeOnline.CustomValidations
{
    public class CategoryExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ShpalljeOnlineDbContext db = new ShpalljeOnlineDbContext();
            string categoryName = value.ToString();
            var existingCategory = db.Categories.Where(x => x.CategoryName == categoryName).FirstOrDefault();

            if (existingCategory == null)
                return ValidationResult.Success;
            else
                return new ValidationResult(this.ErrorMessage);
        }
    }
}