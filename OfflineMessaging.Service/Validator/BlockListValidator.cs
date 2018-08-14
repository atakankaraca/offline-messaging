using FluentValidation;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Data.Validator
{
    public class BlockListValidator : AbstractValidator<BlockListDto>
    {
        public BlockListValidator()
        {
            RuleFor(bu => bu.BlockerId).NotEmpty().NotNull().WithMessage("Blocker User should not be null or empty.");
            RuleFor(bu => bu.BlockedId).NotEmpty().NotNull().WithMessage("Blocked User should not be null or empty.");
        }
    }
}
