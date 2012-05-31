using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Messages;
using MetroWpf.Styles;

namespace MetroWpf.UI.Settings
{
  public class SettingsViewModel : ViewModelBase
  {
    ApplicationStyle _selectedApplicationStyle;

    public SettingsViewModel()
    {
      _selectedApplicationStyle = ApplicationStyle.BlueLight;
    }

    public ApplicationStyle SelectedApplicationStyle
    {
      get { return _selectedApplicationStyle; }
      set
      {
        _selectedApplicationStyle = value;
        RaisePropertyChanged("SelectedApplicationStyle");

        Messenger.Default.Send(
          new ApplicationStyleChangeMessage() 
            { ApplicationStyle = _selectedApplicationStyle });
      }
    }
  }
}
