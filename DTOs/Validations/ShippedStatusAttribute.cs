using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ECommerce.DTOs.Validations
{
    public class ShippedStatusAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
			string val = value.ToString();
			var OrderStatus = @"^shipped$";
			if (Regex.IsMatch(val, OrderStatus))
				return ValidationResult.Success;
			else
				return new ValidationResult("Order status can only be 'shipped'.");
		}
	}
    
}
