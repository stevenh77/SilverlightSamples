using System.Diagnostics;
using System.Reflection;
using System.Windows;
using MetroWpf.Framework.Interfaces;
using MetroWpf.Xaml.Binding;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace MetroWpf
{
    /// <summary>
    /// Application startup for initialisation and config
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeIoC();
            base.OnStartup(e);
            InitializeWpfApplication();
            InitializeBindingErrorTrace();
            LogApplicationStartup();
        }

        private static void InitializeBindingErrorTrace()
        {
            BindingErrorTraceListener.SetTrace();
        }

        private static void InitializeWpfApplication()
        {
            ServiceLocator.Current.GetInstance<IWpfApplication>().Initialize();
            //new Locator().WpfApplication.Initialize();
        }

        private static void LogApplicationStartup()
        {
            var fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            //Log.Info("{0} {1} Startup",
            //          Assembly.GetExecutingAssembly().GetName(),
            //          fvi.ProductVersion);
        }

        private static void InitializeIoC()
        {
            var kernel = new StandardKernel(new IoCModule());
            var adapter = new NinjectServiceLocatorExt(kernel);
            ServiceLocator.SetLocatorProvider(() => adapter);
            kernel.Bind<IServiceLocator>().ToConstant(ServiceLocator.Current);

            //Resources["ServiceLocator"] = ServiceLocator.Current;
        }
    }
}
