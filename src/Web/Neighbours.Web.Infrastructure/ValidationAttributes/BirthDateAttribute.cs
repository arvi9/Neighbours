namespace Neighbours.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class BirthDateAttribute : ValidationAttribute
    {
        public BirthDateAttribute(DateTime minDate, DateTime maxDate)
        {
            this.MinDate = minDate;
            this.MaxDate = maxDate;
        }

        public DateTime MinDate { get; private set; }

        public DateTime MaxDate { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value < this.MinDate || (DateTime)value > this.MaxDate)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
