using FluentValidation;

namespace OfflineMessaging.Data.Validator
{
    public class BlockListValidator : AbstractValidator<BlockList>
    {
        public BlockListValidator()
        {
            RuleFor(bu => bu.Blocker).NotEmpty().NotNull().WithMessage("Blocker User should not be null or empty.");
            RuleFor(bu => bu.Blocked).NotEmpty().NotNull().WithMessage("Blocked User should not be null or empty.");
        }
    }
}
