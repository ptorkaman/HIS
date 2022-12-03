using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HIS.CustomAttributes
{
    public class EnumRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var enumValue = value.ToString();

            if (enumValue == "SelectAnOption")
            {
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
