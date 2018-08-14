using FluentValidation;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Service.Validator
{
    public class UserActivityValidator : AbstractValidator<UserActivityDto>
    {
        public UserActivityValidator()
        {
            RuleFor(userActivity => userActivity.ActivityDetail).NotEmpty().NotNull().WithMessage("Activity Detail is required.");
        }
    }
}
