using System;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UriValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var url = value.ToString();

            try
            {
                var uri = new Uri(url);
                if (uri.Scheme != "http" && uri.Scheme != "https") return new ValidationResult($"{url} not the correct http url format");

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult($"{url} not the correct url format");
            }

        }
    }
}
