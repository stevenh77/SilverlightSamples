using System;
using System.Windows.Controls;
using MetroWpf.Services.Interfaces;
using MetroWpf.Presentation;
using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Messages;

namespace MetroWpf.Services
{
  public class NavigationService : INavigationService
  {
    public void NavigateTo(Screen displayScreen)
    {
      Messenger.Default.Send(
        new NavigationMessage() 
          {
            DisplayScreen = displayScreen 
          });
    }
  }
}