using FxRates.UI.ViewModels;
using GalaSoft.MvvmLight;
using MetroWpf.Presentation.UserProfile;
using MetroWpf.UI.About;
using MetroWpf.UI.Login;
using MetroWpf.UI.Settings;
using MetroWpf.UI.Shell;
using Microsoft.Practices.ServiceLocation;
using Stocks.UI.ViewModels;

namespace MetroWpf
{
    public class Locator
    {
        public Locator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time services and viewmodels
                
            }
            else
            {
                // Create run time services and view models
            }
        }

        public ShellViewModel ShellViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ShellViewModel>(); }
        }

        public LoginViewModel LoginViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public UserProfileViewModel UserProfileViewModel
        {
            get { return ServiceLocator.Current.GetInstance<UserProfileViewModel>(); }
        }

        public SettingsViewModel SettingsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }

        public AboutViewModel AboutViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); }
        }

        public StocksViewModel StocksViewModel
        {
            get { return ServiceLocator.Current.GetInstance<StocksViewModel>(); }
        }

        public FxRatesViewModel FxRatesViewModel
        {
            get { return ServiceLocator.Current.GetInstance<FxRatesViewModel>(); }
        }

        //public IWpfApplication WpfApplication
        //{
        //    get { return ServiceLocator.Current.GetInstance<WpfApplication>(); }
        //}
    }
}