using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ShpalljeOnline.Models;

namespace ShpalljeOnline.CustomValidations
{
    public class LocationExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ShpalljeOnlineDbContext db = new ShpalljeOnlineDbContext();
            string locationName = value.ToString();
            var existingLocation= db.Locations.Where(x => x.LocationName == locationName).FirstOrDefault();

            if (existingLocation == null)
                return ValidationResult.Success;
            else
                return new ValidationResult(this.ErrorMessage);
        }
    }
}