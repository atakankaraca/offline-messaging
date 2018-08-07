using FluentValidation;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Data.Validator
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.SenderId).NotEmpty().NotNull().WithMessage("From field should not be null or empty.");
            RuleFor(message => message.ReceiverId).NotEmpty().NotNull().WithMessage("To field should not be null or empty.");
            RuleFor(message => message.MessageString).NotEmpty().WithMessage("Message field field should not be null or empty.");
        }
    }
}
