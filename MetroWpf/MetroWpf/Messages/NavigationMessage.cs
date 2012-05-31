using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Presentation;

namespace MetroWpf.Messages
{
  public class NavigationMessage : MessageBase
  {
    public Screen DisplayScreen { get; set; }
  }
}