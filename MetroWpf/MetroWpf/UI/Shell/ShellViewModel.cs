using FxRates.UI.ViewModels;
using FxRates.UI.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MetroWpf.Messages;
using MetroWpf.Presentation;
using MetroWpf.UI.Login;
using MetroWpf.UI.Settings;
using Microsoft.Practices.ServiceLocation;
using Stocks.UI.ViewModels;
using Stocks.UI.Views;

namespace MetroWpf.UI.Shell
{
    public class ShellViewModel : ViewModelBase
    {
        public RelayCommand<Screen> MenuCommand { get; set; }

        public ShellViewModel()
        {
            ShowMenuButtons = false;
            MenuCommand = new RelayCommand<Screen>(MenuCommandExecute);

            Messenger.Default.Register<NavigationMessage>(this, NavigationMessageReceived);
            Messenger.Default.Register<UserAuthenticatedMessage>(this, UserAuthenticated);

            Messenger.Default.Send(new NavigationMessage() { DisplayScreen = Screen.Login });
        }

        private void UserAuthenticated(UserAuthenticatedMessage message)
        {
            ShowMenuButtons = true;
            Messenger.Default.Send(new NavigationMessage() { DisplayScreen = Screen.Stocks });
        }

        private object displayScreen;
        public object DisplayScreen
        {
            get { return displayScreen; }
            set
            {
                if (displayScreen == value) return;
                displayScreen = value;
                RaisePropertyChanged("DisplayScreen");
            }
        }

        private bool _showMenuButtons;
        public bool ShowMenuButtons
        {
            get { return _showMenuButtons; }
            set
            {
                if (_showMenuButtons == value) return;
                _showMenuButtons = value;
                RaisePropertyChanged("ShowMenuButtons");
            }
        }

        private void NavigationMessageReceived(NavigationMessage message)
        {
            switch (message.DisplayScreen)
            {
                case Screen.Login:
                    DisplayScreen = ServiceLocator.Current.GetInstance<LoginView>();
                    break;

                case Screen.Stocks:
                    var stocksView = ServiceLocator.Current.GetInstance<StocksView>();
                    stocksView.DataContext = ServiceLocator.Current.GetInstance<StocksViewModel>();
                    DisplayScreen = stocksView;
                    break;

                case Screen.FxRates:
                    var fxRatesView = ServiceLocator.Current.GetInstance<FxRatesView>();
                    fxRatesView.DataContext = ServiceLocator.Current.GetInstance<FxRatesViewModel>();
                    DisplayScreen = fxRatesView;
                    break;

                case Screen.UserProfile:
                    break;

                case Screen.Settings:
                    DisplayScreen = ServiceLocator.Current.GetInstance<SettingsView>();
                    break;

                case Screen.About:
                    break;

                case Screen.Help:
                    break;
            }

        }

        private void MenuCommandExecute(Screen screen)
        {
            Messenger.Default.Send(
              new NavigationMessage()
              {
                  DisplayScreen = screen
              });
        }
    }
}