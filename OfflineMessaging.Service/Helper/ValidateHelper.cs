using FluentValidation.Results;
using OfflineMessaging.Data.Validator;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Service.Helper
{
    public class ValidateHelper
    {
        public ValidationResult ValidateUserModel(UserDto userModel)
        {
            var validator = new UserValidator();
            var validatorResult = validator.Validate(userModel);
            return validatorResult;
        }

        public ValidationResult ValidateBlockListModel(BlockListDto blockListModel)
        {
            var validator = new BlockListValidator();
            var validatorResult = validator.Validate(blockListModel);            
            return validatorResult;
        }

        public ValidationResult ValidateMessageModel(MessageDto messageModel)
        {
            var validator = new MessageValidator();
            var validatorResult = validator.Validate(messageModel);
            return validatorResult;
        }
    }
}
