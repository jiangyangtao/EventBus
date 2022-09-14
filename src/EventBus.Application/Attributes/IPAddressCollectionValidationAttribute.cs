using EventBus.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EventBus.Application.Attributes
{
    /// <summary>
    /// ip 地址格式校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IPAddressCollectionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var ipaddressArray = value as string[];
            if (ipaddressArray.IsNullOrEmpty()) return ValidationResult.Success;
            foreach (var item in ipaddressArray)
            {
                if (item.IsIPAddress() == false) return new ValidationResult($"{item} not the correct ipv4 format");
            }

            return ValidationResult.Success;
        }
    }
}
