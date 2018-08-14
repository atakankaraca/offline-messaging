using FluentValidation;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Data.Validator
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).NotEmpty().MinimumLength(6).MaximumLength(25);

            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(user => user.Password).MinimumLength(6).MaximumLength(40).WithMessage("Password lengt should be between 6 to 40");

            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.FullName).NotEmpty().WithMessage("Full name is required.");
        }
    }
}
