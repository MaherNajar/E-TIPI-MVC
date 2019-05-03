using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace E_TIPI_LEARNING.CustomValidators
{
    public class BeforeEndDateAttribute : ValidationAttribute
    {
        public string EndDatePropertyName { get; set; }

        protected override ValidationResult IsValid(object currentStartDate, ValidationContext validationContext)
        {
            PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(EndDatePropertyName);

            var endDate = endDateProperty.GetValue(validationContext.ObjectInstance, null);

            if (endDate == null || ((DateTime)endDate >= (DateTime)currentStartDate))
                return ValidationResult.Success;

            return new ValidationResult("Start date should be before End date or the same day.");
        }
    }
}
