using OfflineMessaging.Data;
using OfflineMessaging.Data.Validator;

namespace OfflineMessaging.Service.Helper
{
    public class ValidateHelper
    {
        public static bool ValidateUserModel(User userModel)
        {
            var validator = new UserValidator();
            var validatorResult = validator.Validate(userModel).IsValid;

            return validatorResult;
        }

        public static bool ValidaBlockListModel(BlockList blockListModel)
        {
            var validator = new BlockListValidator();
            var validatorResult = validator.Validate(blockListModel).IsValid;

            return validatorResult;
        }

        public static bool ValidateMessageModel(Message messageModel)
        {
            var validator = new MessageValidator();
            var validatorResult = validator.Validate(messageModel).IsValid;

            return validatorResult;
        }
    }
}
