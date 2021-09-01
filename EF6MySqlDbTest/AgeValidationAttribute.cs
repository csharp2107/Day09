using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6MySqlDbTest
{
    class AgeValidationAttribute : ValidationAttribute 
    {
        private int minAge;
        public AgeValidationAttribute(int minAge)
        {
            this.minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, 
                                ValidationContext validationContext)
        {
            if ((int)value >= minAge)
                return ValidationResult.Success;

            //return new ValidationResult("Not valid value for Age property");
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

    }
}
