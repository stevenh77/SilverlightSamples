using GalaSoft.MvvmLight.Messaging;
using SilverlightValidation.ViewModels;

namespace SilverlightValidation.Messages
{
    public class UserViewResponseMessage : MessageBase
    {
        public UserViewModel UserViewModel { get; set; }
    }
}
