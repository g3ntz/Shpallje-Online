using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ShpalljeOnline.Models;

namespace ShpalljeOnline.CustomValidations
{
    public class EmailExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // GET USERID PROPERTY FROM MODEL VALIDATION AND SAVE IT TO "idProperty"
            var idProperty = validationContext.ObjectType.GetProperty("UserID");
            long? userID = null;

            // IF "idProperty" IS NOT NULL USER IS LOGGED IN AND THIS MEANS USER IS EDITING HIS PROFILE
            // IF "idProperty" IS NULL USER IS TRYING TO REGISTER
            if (idProperty != null)
                userID = (long?)idProperty.GetValue(validationContext.ObjectInstance, null);

            // GET AN USER FROM DATABASE WHERE EMAIL MATCHES
            ShpalljeOnlineDbContext db = new ShpalljeOnlineDbContext();
            string email = value.ToString();
            var existingUser = db.Users.Where(x => x.Info.Email == email).FirstOrDefault();

            // IF USER IS EDITING HIS PROFILE DON'T SHOW VALIDATION ERROR WHEN USER:
            // 1. IS TRYING TO CHANGE EMAIL ADDRESS TO THE SAME EMAIL ADDRESS USER CURRENTLY HAVE 
            // 2. IS TRYING TO CHANGE EMAIL ADDRESS TO AN EMAIL THAT DOESN'T EXIST IN DATABASE
            if (userID != null)
            {
                // "existingUser.UserID == userID" IF THIS IS TRUE THEN EMAIL ADDRESS EXISTS IN DATABASE BUT IT BELONGS TO THIS USER
                if (existingUser == null || existingUser.UserID == userID)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(this.ErrorMessage);
            }
            else
            {
                if (existingUser == null)
                    return ValidationResult.Success;
                else
                    return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}