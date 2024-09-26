using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BCarAuctionManagementSystem.Validations
{
    public class TwoDecimalPlacesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal decimalValue)
            {
                var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(decimalValue)[3])[2];

                if (decimalPlaces > 2)
                {
                    return new ValidationResult("The value must have no more than 2 decimal places.");
                }
            }
            return ValidationResult.Success!;
        }
    }
}