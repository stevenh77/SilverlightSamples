using System.Threading.Tasks;
using FxRates.Common;
using FxRates.Service;
using FxRates.UI.ViewModels;
using FxRates.UI.Views;
using MetroWpf.Framework;
using MetroWpf.Framework.Interfaces;
using MetroWpf.Presentation.UserProfile;
using MetroWpf.UI.About;
using MetroWpf.UI.Login;
using MetroWpf.UI.Settings;
using MetroWpf.UI.Shell;
using Microsoft.Practices.ServiceLocation;
using Ninject.Modules;
using Stocks.Common;
using Stocks.Service;
using Stocks.UI.ViewModels;
using Stocks.UI.Views;

namespace MetroWpf
{
    public class IoCModule : NinjectModule
    {
        public override void Load()
        {
            // start up
            Bind<IWpfApplication>().To<WpfApplication>();
            Bind<ShellViewModel>().ToSelf();
            Bind<LoginViewModel>().ToSelf();

            Task.Factory.StartNew(AsyncOtherDependencies);
        }

        private void AsyncOtherDependencies()
        {
            // stocks
            Bind<IConfigurationService>()
                .To<ConfigurationService>()
                .WithConstructorArgument("filename", "companyData.json");

            Bind<IWebClientShim>().To<WebClientShim>();
            Bind<IStocksService>().To<StocksService>();
            
            // force loading of ctor whilst in async code
            ServiceLocator.Current.GetInstance<StocksService>();

            Bind<StocksViewModel>().ToSelf();
            Bind<StocksView>().ToSelf();

            // fx rates
            Bind<IPricingService>().To<PricingService>();
            Bind<FxRatesViewModel>().ToSelf();
            Bind<FxRatesView>().ToSelf();

            // other views
            Bind<UserProfileViewModel>().ToSelf();
            Bind<SettingsViewModel>().ToSelf();
            Bind<AboutViewModel>().ToSelf();
        }
    }
}
