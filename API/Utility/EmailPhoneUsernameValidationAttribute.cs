using API.Contracts;
using System.ComponentModel.DataAnnotations;

namespace API.Utility
{
    public class EmailPhoneUsernameValidationAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public EmailPhoneUsernameValidationAttribute(string propertyName)
        {
            this._propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult($"{_propertyName} is required.");
            var userRepository = validationContext.GetService(typeof(IUserRepository))
                                        as IUserRepository;

            var checkEmailAndPhone = userRepository.CheckEmailAndPhoneAndUsername(value.ToString());
            if (checkEmailAndPhone) return new ValidationResult($"{_propertyName} '{value}' already exists.");
            return ValidationResult.Success;
        }
    }
}
