using System;
using FluentValidation;
using OfflineMessaging.Data.Model;

namespace OfflineMessaging.Service.Validator
{
    public class UserActivityValidator : AbstractValidator<UserActivity>
    {
        public UserActivityValidator()
        {
            RuleFor(userActivity => userActivity.ActivityDetail).NotEmpty().NotNull().WithMessage("Activity Detail is required.");
        }
    }
}
