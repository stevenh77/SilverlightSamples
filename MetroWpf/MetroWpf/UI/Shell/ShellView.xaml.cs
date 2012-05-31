using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Messages;
using MetroWpf.Styles;
using MahApps.Metro;
using System.Linq;

namespace MetroWpf.UI.Shell
{
  public partial class ShellView
  {
      public ShellView()
    {
      //DataContext = new MainWindowViewModel(Dispatcher);
      InitializeComponent();

      // setup notifications
      Messenger.Default.Register<ApplicationStyleChangeMessage>(
        this, message => ChangeTheme(message.ApplicationStyle));
    }
        
    private void ChangeTheme(ApplicationStyle applicationStyle)
    {
      switch (applicationStyle)
      {
        case ApplicationStyle.BlueLight:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Light);
          break;
        case ApplicationStyle.BlueDark:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark);
          break;
        case ApplicationStyle.GreenLight:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Light);
          break;
        case ApplicationStyle.GreenDark:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Dark);
          break;
        case ApplicationStyle.RedLight:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Light);
          break;
        case ApplicationStyle.RedDark:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Dark);
          break;
        case ApplicationStyle.PurpleLight:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Light);
          break;
        case ApplicationStyle.PurpleDark:
          ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Dark);
          break;
      }
    }
  }
}
