namespace Neighbours.Common.ValidationAttributes
{
    using GlobalConstants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BirthDateAttribute : ValidationAttribute
    {
        public BirthDateAttribute(string minDate, int minAge)
        {
            this.MinDate = minDate;
            this.MinAge = minAge;
        }

        public string MinDate { get; private set; }

        public int MinAge { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value.Equals(null))
            {
                return null;
            }

            var min = DateTime.Parse(this.MinDate);
            var max = DateTime.UtcNow.AddYears(-1 * this.MinAge);

            if ((DateTime)value < min || (DateTime)value > max)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"You should be at least 18 years old and born after {GlobalValidationConstants.MinBirthDate}";
        }
    }
}
