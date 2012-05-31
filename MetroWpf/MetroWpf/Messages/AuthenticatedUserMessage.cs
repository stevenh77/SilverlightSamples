using GalaSoft.MvvmLight.Messaging;

namespace MetroWpf.Messages
{
    public class UserAuthenticatedMessage : MessageBase
    {
        public string UserId { get; set; }
    }
}
