using System;
using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Areas.Admin.Models
{
    public class FutureDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if(value.GetType() != typeof(DateTime))
                return new ValidationResult("Must be formated as a date.");

            DateTime dateValue = (DateTime)value;

            if (dateValue <= DateTime.Now)
                return new ValidationResult("Unban date must be sometime in the future.");

            return ValidationResult.Success;
        }
    }
}
