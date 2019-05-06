using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace E_TIPI_LEARNING.CustomValidators
{
    public class AfterStartDateAttribute : ValidationAttribute
    {
        public string StartDatePropertyName { get; set; }

        protected override ValidationResult IsValid(object currentEndDate, ValidationContext validationContext)
        {
            PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(StartDatePropertyName);

            var startDate = endDateProperty.GetValue(validationContext.ObjectInstance, null);

            if ((startDate == null) || ((DateTime)startDate <= (DateTime)currentEndDate))
                return ValidationResult.Success;

            return new ValidationResult("End date should be after start date or the same day.");
        }
    }
}