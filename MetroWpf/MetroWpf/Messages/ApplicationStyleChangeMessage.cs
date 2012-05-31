using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Styles;

namespace MetroWpf.Messages
{
  public class ApplicationStyleChangeMessage : MessageBase
  {
    public ApplicationStyle ApplicationStyle { get; set; }
  }
}
