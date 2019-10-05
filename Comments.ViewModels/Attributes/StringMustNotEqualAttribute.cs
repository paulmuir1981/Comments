using System;
using System.ComponentModel.DataAnnotations;

namespace Comments.ViewModels.Attributes
{
    /// <summary>
    /// Compares 2 strings - Valid if they are different
    /// Throws an exception if it cannot find the string to compare to
    /// </summary>
    public class StringMustNotEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public StringMustNotEqualAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as string ?? string.Empty;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property with name {0} not found", _comparisonProperty));
            }

            var comparisonValue = (string)property.GetValue(validationContext.ObjectInstance);

            if (currentValue != comparisonValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format("Text has not changed", comparisonValue));
        }
    }
}
